namespace Invoicex.CLI.Templates;

/// <summary>
/// Represents an invoice item.
/// </summary>
/// <param name="Description">The description of the item.</param>
/// <param name="UnitPrice">The unit price of the item.</param>
/// <param name="Quantity">The quantity of the item.</param>
public record InvoiceItem(
    string Description,
    decimal UnitPrice,
    int Quantity)
{
    /// <summary>
    /// Gets the total amount for the item.
    /// </summary>
    public decimal Total
        => UnitPrice * Quantity;
}

/// <summary>
/// Represents the invoice data.
/// </summary>
/// <param name="UserName">The username.</param>
/// <param name="InvoiceNumber">The invoice number.</param>
/// <param name="Date">The date of the invoice.</param>
/// <param name="DueDate">The due date of the invoice.</param>
/// <param name="CompanyName">The name of the company.</param>
/// <param name="CompanyAddress">The address of the company.</param>
/// <param name="CustomerAddress">The address of the customer.</param>
/// <param name="Items">The items on the invoice.</param>
public record InvoiceData(
    string UserName,
    string InvoiceNumber,
    string Date,
    string DueDate,
    string CompanyName,
    string CompanyAddress,
    string CustomerAddress,
    List<InvoiceItem> Items)
{
    /// <summary>
    /// Gets the total amount of the invoice.
    /// </summary>
    public decimal TotalAmount
        => Items.Sum(item => item.Total);
}