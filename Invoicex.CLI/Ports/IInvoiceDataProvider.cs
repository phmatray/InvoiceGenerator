namespace Invoicex.CLI.Ports;

/// <summary>
/// Represents the invoice data provider.
/// </summary>
public interface IInvoiceDataProvider
{
    /// <summary>
    /// Gets the invoice data.
    /// </summary>
    /// <returns>The invoice data.</returns>
    InvoiceData GetInvoiceData();
}