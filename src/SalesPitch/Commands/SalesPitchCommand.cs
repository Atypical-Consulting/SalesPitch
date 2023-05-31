using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels.ResponseModels;
using SalesPitch.Extensions;
using SalesPitch.Services.Language;
using Spectre.Console;
using Spectre.Console.Cli;

namespace SalesPitch.Commands;

/// <summary>
///     This C# class defines a SalesPitchCommand class that represents a command-line interface (CLI)
///     command for generating sales pitches. It uses OpenAI GPT-3 for generating the content and
///     the Spectre.Console library for creating a user-friendly CLI.
/// </summary>
public sealed class SalesPitchCommand
    : AsyncCommand<SalesPitchSettings>
{
    private readonly IOpenAIService _openAIService;
    private readonly SpectreLanguageServiceFactory _spectreLanguageServiceFactory;
    
    /// <summary>
    ///     Initializes a new instance of the <see cref="SalesPitchCommand"/> class.
    /// </summary>
    /// <param name="openAIService">The OpenAI GPT-3 service that is used for generating the sales pitch.</param>
    /// <param name="spectreLanguageServiceFactory">The Spectre.Console language service factory that is used for creating the language service.</param>
    public SalesPitchCommand(
        IOpenAIService openAIService,
        SpectreLanguageServiceFactory spectreLanguageServiceFactory)
    {
        _openAIService = openAIService;
        _spectreLanguageServiceFactory = spectreLanguageServiceFactory;
    }

    /// <summary>
    ///     Main method for executing the command asynchronously
    /// </summary>
    /// <param name="context">The command context</param>
    /// <param name="settings">The command settings</param>
    /// <returns>The exit code</returns>
    public override async Task<int> ExecuteAsync(
        CommandContext context,
        SalesPitchSettings settings)
    {
        // Display a header and a rule
        AnsiConsole.Write(
            new FigletText("SalesPitch")
                .LeftJustified()
                .Color(Color.Green));

        AnsiConsole.Write(new Rule());

        // Prompt the user for various settings and store them
        settings.Language = AskLanguage();
        var languageService = _spectreLanguageServiceFactory
            .GetLanguageService(settings.Language);

        settings.Framework = AskSalesPitchFramework(languageService);
        settings.IsDemo = AskIsDemo(languageService);

        // Set demo data if needed
        if (settings.IsDemo)
        {
            settings.Product = languageService.ProductDemo();
            settings.Price = languageService.PriceDemo();
            settings.Features = languageService.FeaturesDemo();
            settings.Benefits = languageService.BenefitsDemo();
        }
        else
        {
            settings.Product = AskProductIfMissing(settings.Product, languageService);
            settings.Price = AskPriceIfMissing(settings.Price, languageService);
            settings.Features = AskFeaturesIfMissing(settings.Features, languageService);
            settings.Benefits = AskBenefitsIfMissing(settings.Benefits, languageService);
        }
        
        // Display a table with the settings
        AnsiConsole.WriteLine();
        var table = GetSettingsTable(settings, languageService);
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
        
        // Display a rule and a prompt
        AnsiConsole.Write(
            new Rule("[red]Prompt[/]").LeftJustified());
        AnsiConsole.WriteLine();

        // Prepare a request for the OpenAI GPT-3 API
        ChatCompletionCreateRequest request = new()
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(languageService.GetChatGPTSetupSystemMessage()),
                ChatMessage.FromUser(languageService.GetChatGPTUserPrompt(settings)),
            }
        };
        
        // Send the request and process the response
        IAsyncEnumerable<ChatCompletionCreateResponse> completionResult = 
            _openAIService.ChatCompletion
                .CreateCompletionAsStream(request, Models.ChatGpt3_5Turbo);
        
        await foreach (ChatCompletionCreateResponse completion in completionResult)
        {
            if (completion.Successful)
            {
                // Display the generated sales pitch character by character
                AnsiConsole.Write(completion.GetContent());
            }
            else
            {
                if (completion.Error == null)
                {
                    throw new Exception("Unknown Error");
                }
        
                AnsiConsole.WriteLine(completion.GetError());
                return 1;
            }
        }

        return 0;
    }

    /// <summary>
    ///     Helper method to create a table with the current settings
    /// </summary>
    /// <param name="settings">The current settings</param>
    /// <param name="languageService">The language service</param>
    /// <returns>A table with the current settings</returns>
    private static Table GetSettingsTable(SalesPitchSettings settings, ILanguageService languageService)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.Width(100);

        table.AddColumn(SurroundWithColor(
            languageService.TableSettingKey(), Color.Blue));

        table.AddColumn(SurroundWithColor(
            languageService.TableSettingValue(), Color.Blue));
        
        // add demo data
        if (settings.IsDemo)
        {
            table.AddRow(
                languageService.UseDemoDataSetting(),
                SurroundWithRed(languageService.UseDemoDataMarkup(true)));
        }

        // add language
        table.AddRow(
            languageService.LanguageSetting(),
            languageService.LanguageMarkup(settings.Language));

        // add framework
        table.AddRow(
            languageService.SalesPitchFrameworkSetting(),
            languageService.SalesPitchFrameworkMarkup(settings.Framework));

        // add product
        table.AddRow(
            languageService.ProductSetting(),
            settings.Product ?? "N/A");

        // add price
        table.AddRow(
            languageService.PriceSetting(),
            settings.Price ?? "N/A");

        // add features
        table.AddRow(
            languageService.FeaturesSetting(),
            settings.Features ?? "N/A");

        // add benefits
        table.AddRow(
            languageService.BenefitsSetting(),
            settings.Benefits ?? "N/A");

        return table;
    }

    /// <summary>
    ///     Methods for prompting the user to select or input Language
    /// </summary>
    /// <returns>The selected language</returns>
    private static SupportedLanguage AskLanguage()
    {
        // use reflection to get all the enum values for the SupportedLanguage
        var supportedLanguages = Enum
            .GetValues(typeof(SupportedLanguage))
            .Cast<SupportedLanguage>()
            .ToArray();

        // prompt for language
        var language = AnsiConsole.Prompt(
            new SelectionPrompt<SupportedLanguage>()
                .Title("Select a language")
                .AddChoices(supportedLanguages)
                .UseConverter(x => x switch
                {
                    SupportedLanguage.English => "English",
                    SupportedLanguage.French => "Français",
                    SupportedLanguage.Spanish => "Español",
                    _ => throw new ArgumentOutOfRangeException(nameof(x), x, null)
                }));

        return language;
    }

    /// <summary>
    ///     Methods for prompting the user to select or input SalesPitchFramework
    /// </summary>
    /// <param name="languageService">The language service</param>
    /// <returns>The selected SalesPitchFramework</returns>
    private static SalesPitchFramework AskSalesPitchFramework(ILanguageService languageService)
    {
        // use reflection to get all the enum values for the SalesPitchFramework
        var salesPitchFrameworks = Enum
            .GetValues(typeof(SalesPitchFramework))
            .Cast<SalesPitchFramework>()
            .ToArray();

        // prompt for framework
        var salesPitchFramework = AnsiConsole.Prompt(
            new SelectionPrompt<SalesPitchFramework>()
                .Title(languageService.SalesPitchFrameworkPrompt())
                .AddChoices(salesPitchFrameworks)
                .UseConverter(x => languageService.SalesPitchFrameworkMarkup(x)));

        return salesPitchFramework;
    }

    /// <summary>
    ///     Method for prompting the user to select or input IsDemo flag
    /// </summary>
    /// <param name="languageService">The language service</param>
    /// <returns>The selected IsDemo flag</returns>
    private static bool AskIsDemo(ILanguageService languageService)
    {
        // prompt for demo data
        var isDemo = AnsiConsole.Prompt(
            new SelectionPrompt<bool>()
                .Title(languageService.UseDemoDataPrompt())
                .AddChoices(true, false)
                .UseConverter(languageService.UseDemoDataMarkup));

        return isDemo;
    }
    
    /// <summary>
    ///     Method for prompting the user to select or input Product
    /// </summary>
    /// <param name="product">The current product</param>
    /// <param name="languageService">The language service</param>
    /// <returns>A product name as a string</returns>
    private static string AskProductIfMissing(string? product, ILanguageService languageService)
        => !string.IsNullOrWhiteSpace(product)
            ? product
            : AnsiConsole.Prompt(
                new TextPrompt<string>(languageService.ProductPrompt())
                    .InvalidChoiceMessage(
                        SurroundWithRed(languageService.ProductNotValid()))
                    .Validate(productToValidate => !string.IsNullOrWhiteSpace(productToValidate)));

    /// <summary>
    ///     Method for prompting the user to select or input Price
    /// </summary>
    /// <param name="price">The current price</param>
    /// <param name="languageService">The language service</param>
    /// <returns>A price as a string</returns>
    private static string AskPriceIfMissing(string? price, ILanguageService languageService)
        => !string.IsNullOrWhiteSpace(price)
            ? price
            : AnsiConsole.Prompt(
                new TextPrompt<string>(languageService.PricePrompt())
                    .InvalidChoiceMessage(
                        SurroundWithRed(languageService.PriceNotValid()))
                    .Validate(priceToValidate => !string.IsNullOrWhiteSpace(priceToValidate)));

    /// <summary>
    ///     Method for prompting the user to select or input Features
    /// </summary>
    /// <param name="features">The features of the product</param>
    /// <param name="languageService">The language service</param>
    /// <returns>A list of features as a string</returns>
    private static string AskFeaturesIfMissing(string? features, ILanguageService languageService)
        => !string.IsNullOrWhiteSpace(features)
            ? features
            : AnsiConsole.Prompt(
                new TextPrompt<string>(languageService.FeaturesPrompt())
                    .InvalidChoiceMessage(
                        SurroundWithRed(languageService.FeaturesNotValid()))
                    .Validate(featuresToValidate => !string.IsNullOrWhiteSpace(featuresToValidate)));

    /// <summary>
    ///     Method for prompting the user to select or input Benefits of the product
    /// </summary>
    /// <param name="benefits">The benefits of the product</param>
    /// <param name="languageService">The language service</param>
    /// <returns>A list of benefits as a string</returns>
    private static string AskBenefitsIfMissing(string? benefits, ILanguageService languageService)
        => !string.IsNullOrWhiteSpace(benefits)
            ? benefits
            : AnsiConsole.Prompt(
                new TextPrompt<string>(languageService.BenefitsPrompt())
                    .InvalidChoiceMessage(
                        SurroundWithRed(languageService.BenefitsNotValid()))
                    .Validate(benefitsToValidate => !string.IsNullOrWhiteSpace(benefitsToValidate)));
    
    /// <summary>
    ///     Surrounds the text with the specified color
    /// </summary>
    /// <param name="text">The text to surround</param>
    /// <param name="color">The color to surround the text with</param>
    /// <returns>The text surrounded with the specified color</returns>
    private static string SurroundWithColor(string text, ConsoleColor color)
        => $"[{color}]{text}[/]";
    
    /// <summary>
    ///     Surrounds the text with red
    /// </summary>
    /// <param name="text">The text to surround</param>
    /// <returns>The text surrounded with red</returns>
    private static string SurroundWithRed(string text)
        => SurroundWithColor(text, ConsoleColor.Red);
}