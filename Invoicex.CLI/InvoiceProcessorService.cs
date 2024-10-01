using Invoicex.CLI.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Invoicex.CLI;

/// <summary>
/// Represents the invoice processor service.
/// </summary>
/// <param name="invoiceDataProvider">The invoice data provider.</param>
/// <param name="generator">The LaTeX generator.</param>
/// <param name="compiler">The LaTeX compiler.</param>
/// <param name="settings">The settings for LaTeX processing.</param>
/// <param name="logger">The logger.</param>
public class InvoiceProcessorService(
    IInvoiceDataProvider invoiceDataProvider,
    ILatexGenerator generator,
    ILatexCompiler compiler,
    LatexSettings settings,
    ILogger<InvoiceProcessorService> logger)
    : BackgroundService
{
    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            logger.LogInformation("Starting invoice processing...");

            // Ensure directories exist
            string currentDirectory = Directory.GetCurrentDirectory();
            string templateDirectory = Path.Combine(currentDirectory, settings.TemplateDirectory);
            string outputDirectory = Path.Combine(currentDirectory, settings.OutputDirectory);

            Directory.CreateDirectory(templateDirectory);
            Directory.CreateDirectory(outputDirectory);

            // Step 1: Get invoice data from the data provider
            var invoiceData = invoiceDataProvider.GetInvoiceData();

            // Step 2: Generate LaTeX file
            string texFilePath = generator.GenerateLaTeX(invoiceData, "InvoiceTemplate");

            // Step 3: Compile the LaTeX file to PDF
            await compiler.Compile(texFilePath, outputDirectory);

            logger.LogInformation("PDF successfully generated at {OutputDirectory}", outputDirectory);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing the invoice.");
        }
    }
}