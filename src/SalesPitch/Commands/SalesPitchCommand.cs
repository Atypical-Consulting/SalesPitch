using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels.ResponseModels;
using SalesPitch.Extensions;
using SalesPitch.Services.Language;
using Spectre.Console;
using Spectre.Console.Cli;

namespace SalesPitch.Commands;

public sealed class SalesPitchCommand
    : AsyncCommand<SalesPitchSettings>
{
    private readonly IOpenAIService _openAIService;
    private readonly SpectreLanguageServiceFactory _spectreLanguageServiceFactory;
    
    public SalesPitchCommand(
        IOpenAIService openAIService,
        SpectreLanguageServiceFactory spectreLanguageServiceFactory)
    {
        _openAIService = openAIService;
        _spectreLanguageServiceFactory = spectreLanguageServiceFactory;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, SalesPitchSettings settings)
    {
        AnsiConsole.Write(
            new FigletText("SalesPitch")
                .LeftJustified()
                .Color(Color.Green));

        AnsiConsole.Write(new Rule());

        settings.Language = AskLanguage();
        var languageService = _spectreLanguageServiceFactory
            .GetLanguageService(settings.Language);

        settings.Framework = AskSalesPitchFramework(languageService);
        settings.IsDemo = AskIsDemo(languageService);

        // set demo data if needed
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
        
        // settings table
        AnsiConsole.WriteLine();
        var table = GetSettingsTable(settings, languageService);
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
        
        AnsiConsole.Write(
            new Rule("[red]Prompt[/]").LeftJustified());
        AnsiConsole.WriteLine();

        ChatCompletionCreateRequest request = new()
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(languageService.GetChatGPTSetupSystemMessage()),
                ChatMessage.FromUser(languageService.GetChatGPTUserPrompt(settings)),
            }
        };
        
        IAsyncEnumerable<ChatCompletionCreateResponse> completionResult = 
            _openAIService.ChatCompletion
                .CreateCompletionAsStream(request, Models.ChatGpt3_5Turbo);
        
        await foreach (ChatCompletionCreateResponse completion in completionResult)
        {
            if (completion.Successful)
            {
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
                    _ => throw new ArgumentOutOfRangeException(nameof(x), x, null)
                }));

        return language;
    }

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
    
    private static string AskProductIfMissing(string? product, ILanguageService languageService)
        => !string.IsNullOrWhiteSpace(product)
            ? product
            : AnsiConsole.Prompt(
                new TextPrompt<string>(languageService.ProductPrompt())
                    .InvalidChoiceMessage(
                        SurroundWithRed(languageService.ProductNotValid()))
                    .Validate(productToValidate => !string.IsNullOrWhiteSpace(productToValidate)));

    private static string AskPriceIfMissing(string? price, ILanguageService languageService)
        => !string.IsNullOrWhiteSpace(price)
            ? price
            : AnsiConsole.Prompt(
                new TextPrompt<string>(languageService.PricePrompt())
                    .InvalidChoiceMessage(
                        SurroundWithRed(languageService.PriceNotValid()))
                    .Validate(priceToValidate => !string.IsNullOrWhiteSpace(priceToValidate)));

    private static string AskFeaturesIfMissing(string? features, ILanguageService languageService)
        => !string.IsNullOrWhiteSpace(features)
            ? features
            : AnsiConsole.Prompt(
                new TextPrompt<string>(languageService.FeaturesPrompt())
                    .InvalidChoiceMessage(
                        SurroundWithRed(languageService.FeaturesNotValid()))
                    .Validate(featuresToValidate => !string.IsNullOrWhiteSpace(featuresToValidate)));

    private static string AskBenefitsIfMissing(string? settingsBenefits, ILanguageService languageService)
        => !string.IsNullOrWhiteSpace(settingsBenefits)
            ? settingsBenefits
            : AnsiConsole.Prompt(
                new TextPrompt<string>(languageService.BenefitsPrompt())
                    .InvalidChoiceMessage(
                        SurroundWithRed(languageService.BenefitsNotValid()))
                    .Validate(benefitsToValidate => !string.IsNullOrWhiteSpace(benefitsToValidate)));
    
    private static string SurroundWithColor(string text, ConsoleColor color)
        => $"[{color}]{text}[/]";
    
    private static string SurroundWithRed(string text)
        => SurroundWithColor(text, ConsoleColor.Red);
}