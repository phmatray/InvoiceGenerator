namespace Invoicex.CLI.Ports;

public interface ILaTeXGenerator
{
    string GenerateLaTeX<T>(T dataObject);
}