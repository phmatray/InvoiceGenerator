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
        return new InvoiceData
        {
            UserName = "John Doe",
            InvoiceNumber = "INV-12345",
            Date = now.ToString(DateFormat),
            DueDate = now.AddDays(30).ToString(DateFormat),
            CompanyName = "Example Corp.",
            CompanyAddress = "123 Business St, Business City, BC 12345",
            CustomerAddress = "789 Customer Rd, Customer Town, CT 67890",
            Items =
            [
                new InvoiceItem()
                {
                    Description = "Service A",
                    UnitPrice = 100.00m,
                    Quantity = 2
                },

                new InvoiceItem()
                {
                    Description = "Product B",
                    UnitPrice = 50.00m,
                    Quantity = 3
                }
            ]
        };
    }
}