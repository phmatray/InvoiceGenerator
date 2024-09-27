using System.Reflection;
using Invoicex.CLI.Ports;

namespace Invoicex.CLI.Adapters;

public class LaTeXGenerator(string templateDirectory) : ILaTeXGenerator
{
    // T is a generic object with properties we want to replace in the template
    public string GenerateLaTeX<T>(T dataObject)
    {
        string templatePath = Path.Combine(templateDirectory, "InvoiceTemplate.tex");
        if (!File.Exists(templatePath))
        {
            throw new FileNotFoundException($"Template file not found at {templatePath}");
        }

        string latexTemplate = File.ReadAllText(templatePath);

        // Replace each property placeholder in the template with the corresponding value from the dataObject
        foreach (PropertyInfo prop in typeof(T).GetProperties())
        {
            string placeholder = $"<<{prop.Name}>>";
            string value = prop.GetValue(dataObject)?.ToString() ?? string.Empty;
            latexTemplate = latexTemplate.Replace(placeholder, value);
        }

        // Save the modified template to a new file
        string outputFileName = "Invoice.tex";
        string outputFilePath = Path.Combine(templateDirectory, outputFileName);
        File.WriteAllText(outputFilePath, latexTemplate);

        Console.WriteLine($"LaTeX file generated: {outputFilePath}");

        return outputFilePath;  // Returning the path to the generated LaTeX file
    }
}