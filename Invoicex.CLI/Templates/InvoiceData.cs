namespace Invoicex.CLI.Templates;

public record InvoiceItem
{
    public required string Description { get; init; }
    public required decimal UnitPrice { get; init; }
    public required int Quantity { get; init; }

    public decimal Total => UnitPrice * Quantity;
}

public record InvoiceData
{
    public required string UserName { get; init; }
    public required string InvoiceNumber { get; init; }
    public required string Date { get; init; }
    public required string DueDate { get; init; }
    public required string CompanyName { get; init; }
    public required string CompanyAddress { get; init; }
    public required string CustomerAddress { get; init; }
    public required List<InvoiceItem> Items { get; init; }

    public decimal TotalAmount => Items.Sum(item => item.Total);
}