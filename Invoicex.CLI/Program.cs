using Invoicex.CLI.Adapters;

// Define paths
var currentDirectory = Directory.GetCurrentDirectory();
var templateDirectory = Path.Combine(currentDirectory, "Templates");
var outputDirectory = Path.Combine(currentDirectory, "..", "..", "..", "Output");

// Ensure directories exist
Directory.CreateDirectory(templateDirectory);
Directory.CreateDirectory(outputDirectory);

// Example data object to be used for generating the LaTeX file
IInvoiceDataProvider invoiceDataProvider = new InvoiceDataProvider();
var invoiceData = invoiceDataProvider.GetInvoiceData();

// Path to pdflatex executable (update this with your actual path)
const string pdflatexPath = "/Library/TeX/texbin/pdflatex"; // Example path for Linux, update for Windows

// Create instances of the adapters (in a real-world application, these would be injected)
ILaTeXGenerator generator = new LaTeXGenerator(templateDirectory);
ILaTeXCompiler compiler = new LaTeXCompiler(pdflatexPath);

try
{
    // Step 1: Generate LaTeX file
    string texFilePath = generator.GenerateLaTeX(invoiceData, "InvoiceTemplate");

    // Step 2: Compile the LaTeX file to PDF
    await compiler.Compile(texFilePath, outputDirectory);

    Console.WriteLine("PDF successfully generated.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}