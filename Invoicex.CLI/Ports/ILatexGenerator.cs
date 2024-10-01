namespace Invoicex.CLI.Ports;

/// <summary>
/// Represents the LaTeX generator.
/// </summary>
public interface ILatexGenerator
{
    /// <summary>
    /// Generates a LaTeX file from the specified data object and template.
    /// </summary>
    /// <param name="dataObject">The data object.</param>
    /// <param name="templateName">The template name.</param>
    /// <typeparam name="T">The type of the data object.</typeparam>
    /// <returns>The LaTeX content.</returns>
    string GenerateLaTeX<T>(T dataObject, string templateName);
}