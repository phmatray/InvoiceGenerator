using Bogus;

namespace Invoicex.CLI.Templates;

public sealed class InvoiceItemFaker : Faker<InvoiceItem>
{
    public InvoiceItemFaker()
    {
        RuleFor(x => x.Description, f => f.Commerce.ProductName());
        RuleFor(x => x.UnitPrice, f => Math.Round(f.Random.Decimal(1, 100), 2));
        RuleFor(x => x.Quantity, f => f.Random.Int(1, 5));
    }
}

public sealed class InvoiceDataFaker : Faker<InvoiceData>
{
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

    private List<InvoiceItem> GenerateItems(int count)
    {
        var faker = new InvoiceItemFaker();
        return faker.Generate(count);
    }
}