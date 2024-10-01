using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Invoicex.CLI.Adapters;

public class LatexCompiler(ILogger<LatexCompiler> logger, string pdflatexPath)
    : ILatexCompiler
{
    public async Task Compile(string texFilePath, string outputDirectory)
    {
        if (!File.Exists(texFilePath))
        {
            throw new FileNotFoundException($"LaTeX file not found at {texFilePath}");
        }

        // Setup the ProcessStartInfo to call pdflatex
        ProcessStartInfo psi = new()
        {
            FileName = pdflatexPath,
            Arguments = $"-output-directory=\"{outputDirectory}\" \"{texFilePath}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process();

        process.StartInfo = psi;
        process.Start();
        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            string error = await process.StandardError.ReadToEndAsync();
            throw new Exception($"pdflatex failed with the following error: {error}");
        }

        logger.LogInformation("LaTeX compilation successful. Output PDF in {OutputDirectory}", outputDirectory);
    }
}

