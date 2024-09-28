namespace Invoicex.CLI.Ports;

public interface IInvoiceDataProvider
{
    InvoiceData GetInvoiceData();
}