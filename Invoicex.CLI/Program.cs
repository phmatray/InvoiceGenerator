using Invoicex.CLI.Adapters;
using Invoicex.CLI.Ports;
using Invoicex.CLI.Templates;

// Define paths
var currentDirectory = Directory.GetCurrentDirectory();
var templateDirectory = Path.Combine(currentDirectory, "Templates");
var outputDirectory = Path.Combine(currentDirectory, "..", "..", "..", "Output");

// Ensure directories exist
Directory.CreateDirectory(templateDirectory);
Directory.CreateDirectory(outputDirectory);

// Example data object to be used for generating the LaTeX file
var invoiceData = new InvoiceData
{
    UserName = "John Doe",
    InvoiceNumber = "INV-12345",
    Date = DateTime.Now.ToString("yyyy-MM-dd"),
    DueDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd"),
    CompanyName = "Example Corp.",
    CompanyAddress = "123 Business St, Business City, BC 12345",
    CustomerAddress = "789 Customer Rd, Customer Town, CT 67890",
    Items =
    [
        new InvoiceItem { Description = "Service A", UnitPrice = 100.00, Quantity = 2 },
        new InvoiceItem { Description = "Product B", UnitPrice = 50.00, Quantity = 3 }
    ]
};

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
