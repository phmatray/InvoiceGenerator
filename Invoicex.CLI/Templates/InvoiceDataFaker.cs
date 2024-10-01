using Bogus;

namespace Invoicex.CLI.Templates;

/// <summary>
/// Represents a fake invoice item.
/// </summary>
public sealed class InvoiceItemFaker : Faker<InvoiceItem>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvoiceItemFaker"/> class.
    /// </summary>
    public InvoiceItemFaker()
    {
        RuleFor(x => x.Description, f => f.Commerce.ProductName());
        RuleFor(x => x.UnitPrice, f => Math.Round(f.Random.Decimal(1, 100), 2));
        RuleFor(x => x.Quantity, f => f.Random.Int(1, 5));
    }
}

/// <summary>
/// Represents a fake invoice data.
/// </summary>
public sealed class InvoiceDataFaker : Faker<InvoiceData>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvoiceDataFaker"/> class.
    /// </summary>
    public InvoiceDataFaker()
    {
        RuleFor(x => x.UserName, f => f.Person.FullName);
        RuleFor(x => x.InvoiceNumber, f => f.Random.AlphaNumeric(8));
        RuleFor(x => x.Date, f => f.Date.Past().ToString("yyyy-MM-dd"));
        RuleFor(x => x.DueDate, f => f.Date.Future().ToString("yyyy-MM-dd"));
        RuleFor(x => x.CompanyName, f => f.Company.CompanyName());
        RuleFor(x => x.CompanyAddress, f => f.Address.FullAddress());
        RuleFor(x => x.CustomerAddress, f => f.Address.FullAddress());
        RuleFor(x => x.Items, f => GenerateItems(f.Random.Int(1, 5)));
    }

    private static List<InvoiceItem> GenerateItems(int count)
    {
        var faker = new InvoiceItemFaker();
        return faker.Generate(count);
    }
}