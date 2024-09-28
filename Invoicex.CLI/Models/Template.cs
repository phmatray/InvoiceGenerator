namespace Invoicex.CLI.Models;

public record Template(string Directory, string Name)
{
    public string Path
        => System.IO.Path.Combine(Directory, $"{Name}.tex");

    public string OutputFileName
        => $"{Name.Replace("Template", string.Empty)}_{Guid.NewGuid()}.tex";
}