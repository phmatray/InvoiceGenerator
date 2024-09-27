namespace Invoicex.CLI.Ports;

public interface ILaTeXCompiler
{
    Task Compile(string texFilePath, string outputDirectory);
}