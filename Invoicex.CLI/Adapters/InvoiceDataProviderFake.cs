namespace Invoicex.CLI.Adapters;

/// <summary>
/// Represents a fake invoice data provider.
/// </summary>
public class InvoiceDataProviderFake : IInvoiceDataProvider
{
    /// <inheritdoc/>
    public InvoiceData GetInvoiceData()
    {
        var faker = new InvoiceDataFaker();
        return faker.Generate();
    }
}