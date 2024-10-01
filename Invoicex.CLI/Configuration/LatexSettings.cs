namespace Invoicex.CLI.Configuration;

/// <summary>
/// Represents the LaTeX settings.
/// </summary>
/// <param name="TemplateDirectory">The directory where the LaTeX templates are stored.</param>
/// <param name="OutputDirectory">The directory where the output files are stored.</param>
/// <param name="PdfLaTeXPath">The path to the pdflatex executable.</param>
public record LatexSettings(
    string TemplateDirectory,
    string OutputDirectory,
    string PdfLaTeXPath);
