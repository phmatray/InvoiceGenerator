namespace Invoicex.CLI.Templates;

public record InvoiceData
{
    public required string UserName { get; init; }
    public required string InvoiceNumber { get; init; }
    public required string Date { get; init; }
    public required double Amount { get; init; }
}