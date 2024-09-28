using Invoicex.CLI;
using Invoicex.CLI.Adapters;
using Invoicex.CLI.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// Step 1: Load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Step 2: Bind configuration to a settings object
var latexSettings = builder.Configuration
    .GetSection("LaTeXSettings")
    .Get<LaTeXSettings>();

// Step 3: Configure services
builder.Services
    .AddSingleton(latexSettings!)
    .AddSingleton<IInvoiceDataProvider, InvoiceDataProviderFake>()
    .AddSingleton<ILaTeXGenerator, LaTeXGenerator>(provider =>
    {
        var settings = provider.GetRequiredService<LaTeXSettings>();
        string currentDirectory = Directory.GetCurrentDirectory();
        string templateDirectory = Path.Combine(currentDirectory, settings.TemplateDirectory);
        var logger = provider.GetRequiredService<ILogger<LaTeXGenerator>>();
        return new LaTeXGenerator(logger, templateDirectory);
    })
    .AddSingleton<ILaTeXCompiler, LaTeXCompiler>(provider =>
    {
        var settings = provider.GetRequiredService<LaTeXSettings>();
        var logger = provider.GetRequiredService<ILogger<LaTeXCompiler>>();
        return new LaTeXCompiler(logger, settings.PdfLaTeXPath);
    })
    .AddHostedService<InvoiceProcessorService>();

var app = builder.Build();

app.Run();