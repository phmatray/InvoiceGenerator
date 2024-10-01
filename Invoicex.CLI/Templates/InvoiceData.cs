namespace Invoicex.CLI.Templates;

/// <summary>
/// Represents an invoice item.
/// </summary>
public record InvoiceItem
{
    /// <summary>
    /// The description of the item.
    /// </summary>
    public required string Description { get; init; }

    /// <summary>
    /// The unit price of the item.
    /// </summary>
    public required decimal UnitPrice { get; init; }

    /// <summary>
    /// The quantity of the item.
    /// </summary>
    public int Quantity { get; init; } = 1;
    
    /// <summary>
    /// Gets the total amount for the item.
    /// </summary>
    public decimal Total
        => UnitPrice * Quantity;
}

/// <summary>
/// Represents the invoice data.
/// </summary>
public record InvoiceData
{
    /// <summary>
    /// The username.
    /// </summary>
    public required string UserName { get; init; }

    /// <summary>
    /// The invoice number.
    /// </summary>
    public required string InvoiceNumber { get; init; }

    /// <summary>
    /// The date of the invoice.
    /// </summary>
    public required string Date { get; init; }

    /// <summary>
    /// The due date of the invoice.
    /// </summary>
    public required string DueDate { get; init; }

    /// <summary>
    /// The name of the company.
    /// </summary>
    public required string CompanyName { get; init; }

    /// <summary>
    /// The address of the company.
    /// </summary>
    public required string CompanyAddress { get; init; }

    /// <summary>
    /// The address of the customer.
    /// </summary>
    public required string CustomerAddress { get; init; }

    /// <summary>
    /// The items on the invoice.
    /// </summary>
    public List<InvoiceItem> Items { get; init; } = [];
    
    /// <summary>
    /// Gets the total amount of the invoice.
    /// </summary>
    public decimal TotalAmount
        => Items.Sum(item => item.Total);
}