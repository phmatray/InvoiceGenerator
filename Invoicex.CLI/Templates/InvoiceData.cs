namespace Invoicex.CLI.Templates;

public record InvoiceItem
{
    public required string Description { get; init; }
    public required double UnitPrice { get; init; }
    public required int Quantity { get; init; }

    public double Total => UnitPrice * Quantity;
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

    public double TotalAmount => Items.Sum(item => item.Total);
}