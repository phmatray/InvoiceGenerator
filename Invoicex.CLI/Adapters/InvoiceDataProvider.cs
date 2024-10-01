namespace Invoicex.CLI.Adapters;

/// <summary>
/// Represents the invoice data provider.
/// </summary>
public class InvoiceDataProvider : IInvoiceDataProvider
{
    private const string DateFormat = "yyyy-MM-dd";

    /// <inheritdoc/>
    public InvoiceData GetInvoiceData()
    {
        var now = DateTime.Now;

        // Simulate fetching invoice data from a database or external service
        return new InvoiceData(
            "John Doe",
            "INV-12345",
            now.ToString(DateFormat),
            now.AddDays(30).ToString(DateFormat),
            "Example Corp.",
            "123 Business St, Business City, BC 12345",
            "789 Customer Rd, Customer Town, CT 67890",
            [
                new InvoiceItem("Service A", 100.00m, 2),
                new InvoiceItem("Product B", 50.00m, 3)
            ]);
    }
}