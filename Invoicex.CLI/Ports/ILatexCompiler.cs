namespace Invoicex.CLI.Ports;

/// <summary>
/// Represents the LaTeX compiler.
/// </summary>
public interface ILatexCompiler
{
    /// <summary>
    /// Compiles the specified TeX file.
    /// </summary>
    /// <param name="texFilePath">The path to the TeX file.</param>
    /// <param name="outputDirectory">The output directory.</param>
    /// <returns>The task representing the compilation operation.</returns>
    Task Compile(string texFilePath, string outputDirectory);
}