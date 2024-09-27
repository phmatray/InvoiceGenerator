using System.Collections;
using System.Text;
using Invoicex.CLI.Ports;

namespace Invoicex.CLI.Adapters;

public class LaTeXGenerator(string templateDirectory) : ILaTeXGenerator
{
    public string GenerateLaTeX<T>(T dataObject, string templateName)
    {
        string templatePath = Path.Combine(templateDirectory, $"{templateName}.tex");
        if (!File.Exists(templatePath))
        {
            throw new FileNotFoundException($"Template file not found at {templatePath}");
        }

        string latexTemplate = File.ReadAllText(templatePath);

        // Process the template
        latexTemplate = ProcessTemplate(latexTemplate, dataObject);

        // Generate output file name
        string outputFileName = $"{templateName.Replace("Template", string.Empty)}_{Guid.NewGuid()}.tex";
        string outputFilePath = Path.Combine(templateDirectory, outputFileName);
        File.WriteAllText(outputFilePath, latexTemplate);

        Console.WriteLine($"LaTeX file generated: {outputFilePath}");

        return outputFilePath;  // Returning the path to the generated LaTeX file
    }

    private string ProcessTemplate<T>(string template, T dataObject)
    {
        StringBuilder result = new StringBuilder();

        using (StringReader reader = new StringReader(template))
        {
            while (reader.ReadLine() is { } line)
            {
                // Check for loop start
                if (line.Contains("<<for each"))
                {
                    // Extract collection name
                    string loopStart = line.Trim();
                    int startIdx = loopStart.IndexOf("<<for each", StringComparison.Ordinal) + "<<for each".Length;
                    int endIdx = loopStart.IndexOf(">>", StringComparison.Ordinal);
                    string loopExpression = loopStart.Substring(startIdx, endIdx - startIdx).Trim();
                    // Expected format: item in CollectionName
                    string[] parts = loopExpression.Split(new string[] { "in" }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        string itemName = parts[0].Trim();
                        string collectionName = parts[1].Trim();

                        // Read loop body
                        StringBuilder loopBody = new StringBuilder();
                        while ((line = reader.ReadLine()) != null && !line.Contains("<<end for>>"))
                        {
                            loopBody.AppendLine(line);
                        }

                        // Get collection from dataObject
                        if (GetPropertyValue(dataObject, collectionName) is IEnumerable collection)
                        {
                            foreach (var item in collection)
                            {
                                string loopContent = loopBody.ToString();
                                // Replace placeholders in loop body
                                loopContent = ReplacePlaceholders(loopContent, item, itemName);
                                result.Append(loopContent);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid loop syntax in template.");
                    }
                }
                else
                {
                    // Replace placeholders in line
                    string processedLine = ReplacePlaceholders(line, dataObject);
                    result.AppendLine(processedLine);
                }
            }
        }

        return result.ToString();
    }

    private string ReplacePlaceholders(string template, object? dataObject, string prefix = "")
    {
        // Find all placeholders in template
        int idx = 0;
        while ((idx = template.IndexOf("<<", idx, StringComparison.Ordinal)) != -1)
        {
            int endIdx = template.IndexOf(">>", idx, StringComparison.Ordinal);
            if (endIdx == -1)
                break; // No closing >>

            string placeholder = template.Substring(idx, endIdx - idx + 2);
            string propertyPath = placeholder.Substring(2, placeholder.Length - 4).Trim(); // Remove << and >>

            object? value;
            if (!string.IsNullOrEmpty(prefix))
            {
                if (propertyPath.StartsWith(prefix + "."))
                {
                    propertyPath = propertyPath.Substring(prefix.Length + 1);
                    value = GetPropertyValue(dataObject, propertyPath);
                }
                else
                {
                    // Skip placeholders that don't match the prefix in loop context
                    idx = endIdx + 2;
                    continue;
                }
            }
            else
            {
                value = GetPropertyValue(dataObject, propertyPath);
            }

            string replacement = value?.ToString() ?? string.Empty;
            template = template.Replace(placeholder, replacement);

            idx += replacement.Length;
        }
        return template;
    }

    private object? GetPropertyValue(object? obj, string propertyPath)
    {
        if (obj == null || string.IsNullOrEmpty(propertyPath))
            return null;

        string[] properties = propertyPath.Split('.');

        object? value = obj;
        foreach (string prop in properties)
        {
            if (value == null)
                return null;

            var type = value.GetType();
            var propertyInfo = type.GetProperty(prop);
            if (propertyInfo == null)
                return null;

            value = propertyInfo.GetValue(value);
        }
        return value;
    }
}