namespace Invoicex.CLI.Models;

/// <summary>
/// Represents a LaTeX template.
/// </summary>
/// <param name="Directory">The directory where the template is stored.</param>
/// <param name="Name">The name of the template.</param>
public record Template(string Directory, string Name)
{
    /// <summary>
    /// Gets the path to the template file.
    /// </summary>
    public string Path
        => System.IO.Path.Combine(Directory, $"{Name}.tex");

    /// <summary>
    /// Gets the output file name.
    /// </summary>
    public string OutputFileName
        => $"{Name.Replace("Template", string.Empty)}_{Guid.NewGuid()}.tex";
}