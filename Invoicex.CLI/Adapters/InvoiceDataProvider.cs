namespace Invoicex.CLI.Adapters;

public class InvoiceDataProvider : IInvoiceDataProvider
{
    public InvoiceData GetInvoiceData()
    {
        // Simulate fetching invoice data from a database or external service
        return new InvoiceData
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
                new InvoiceItem { Description = "Service A", UnitPrice = 100.00m, Quantity = 2 },
                new InvoiceItem { Description = "Product B", UnitPrice = 50.00m, Quantity = 3 }
            ]
        };
    }
}