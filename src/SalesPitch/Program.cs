using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Extensions;
using Spectre.Console.Cli;
using SalesPitch.Commands;
using SalesPitch.Services.Language;

// Load configuration
IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .Build();

// Create a type registrar and register any dependencies.
// A type registrar is an adapter for a DI framework.
var registrations = new ServiceCollection();
registrations.AddSingleton<IConfiguration>(configuration);
registrations.AddSingleton<EnglishLanguageService>();
registrations.AddSingleton<FrenchLanguageService>();
registrations.AddSingleton<SpanishLanguageService>();
registrations.AddSingleton<GermanLanguageService>();
registrations.AddSingleton<SpectreLanguageServiceFactory>();
registrations.AddOpenAIService();
var registrar = new SalesPitch.Infrastructure.TypeRegistrar(registrations);

// Create a new command app with the registrar
// and run it with the provided arguments.
var app = new CommandApp<SalesPitchCommand>(registrar);
app.Configure(config =>
{
#if DEBUG
    config.PropagateExceptions();
    config.ValidateExamples();
#endif

    config.AddCommand<SalesPitchCommand>("sales-pitch")
        .WithDescription("Sales pitch");
});

// Run the application
await app.RunAsync(args);