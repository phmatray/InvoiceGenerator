namespace Invoicex.CLI.Adapters;

public class InvoiceDataProviderFake : IInvoiceDataProvider
{
    public InvoiceData GetInvoiceData()
    {
        var faker = new InvoiceDataFaker();
        return faker.Generate();
    }
}