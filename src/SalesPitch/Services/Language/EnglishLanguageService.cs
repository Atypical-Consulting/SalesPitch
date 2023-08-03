using SalesPitch.Commands;

namespace SalesPitch.Services.Language;

public class EnglishLanguageService : ILanguageService
{
    public string GetChatGPTUserPrompt(SalesPitchSettings settings)
    {
        return $"""
            The structure should be based on the {settings.Framework} framework. 
            Your focus should predominantly be on highlighting the product's challenges and advantages, 
            using compelling headlines to draw in the audience.
            The output must be print-ready without any instructions.

            Product Details:
            Name : {settings.Product}
            Price : {settings.Price}
            Features : {settings.Features}
            Benefits : {settings.Benefits}

            Your expected output should be a detailed sales pitch, ideally between 500 and 750 words,
            which meets the following criteria:

            Uses the {settings.Framework} framework effectively.
            Draws attention to both problems the product solves and its benefits.
            Incorporates catchy headlines at relevant intervals throughout the pitch.
            Is formatted and ready for print, devoid of any further instructions.
            Please emulate the style of a seasoned advertising executive in your writing,
            and keep the tone professional yet engaging.

            Remember, the objective is to convince potential customers to invest in the product, 
            not just provide information about it. Your pitch should make them feel that the product is a solution 
            to their problems and a significant enhancement to their lives.
            """;
    }

    public string GetChatGPTSetupSystemMessage()
        => "As an expert copywriter, your task is to develop an enticing sales pitch for the product mentioned below.";

    public string TableSettingKey()
        => "Setting";

    public string TableSettingValue()
        => "Value";

    public string SalesPitchFrameworkMarkup(SalesPitchFramework? framework)
        => framework switch
        {
            SalesPitchFramework.AIDA
                => $"{framework} (Attention, Interest, Desire, Action)",
            SalesPitchFramework.PAS
                => $"{framework} (Problem-Agitate-Solve)",
            SalesPitchFramework.USP
                => $"{framework} (Unique Selling Proposition)",
            SalesPitchFramework.FeaturesBenefits
                => $"{framework} (Features-Benefits)",
            SalesPitchFramework.Storytelling
                => $"{framework} (Storytelling)",
            SalesPitchFramework.WIIFM
                => $"{framework} (What’s In It For Me)",
            SalesPitchFramework.Youtility
                => $"{framework} (Youtility)",
            SalesPitchFramework.FAB
                => $"{framework} (Features, Advantages, Benefits)",
            SalesPitchFramework.HHE
                => $"{framework} (Headline, Hook, Empathy)",
            SalesPitchFramework.SUSPENSE
                => $"{framework} (Surprise, Uniqueness, Specifics, Promise, Excitement, Newness, Story)",
            _ => throw new ArgumentOutOfRangeException(nameof(framework), framework, null)
        };

    public string SalesPitchFrameworkPrompt()
        => "Select a sales pitch framework:";

    public string SalesPitchFrameworkSetting()
        => "Sales pitch framework";

    public string LanguageSetting()
        => "Language";

    public string LanguageMarkup(SupportedLanguage? language)
        => language switch
        {
            SupportedLanguage.English => "English",
            SupportedLanguage.French  => "French",
            SupportedLanguage.Spanish => "Spanish",
            SupportedLanguage.German  => "German",
            _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
        };

    public string UseDemoDataSetting()
        => "Use demo data";

    public string UseDemoDataPrompt()
        => "Use demo data?";

    public string UseDemoDataMarkup(bool isDemo)
        => isDemo
            ? "Yes"
            : "No";

    public string ProductSetting()
        => "Product name";

    public string ProductPrompt()
        => "What is the product to advertise?";

    public string ProductNotValid()
        => "The product name is not valid.";

    public string ProductDemo()
        => "Microsoft Power BI Premium";

    public string PriceSetting()
        => "Price";

    public string PricePrompt()
        => "What is the price of the product?";

    public string PriceNotValid()
        => "The price is not valid.";

    public string PriceDemo()
        => "16,90 € by user/month";

    public string FeaturesSetting()
        => "Features";

    public string FeaturesPrompt()
        => "What are the features of the product?";

    public string FeaturesNotValid()
        => "The features are not valid.";

    public string FeaturesDemo()
        => "Multiple data sources, Interactive reporting, Custom dashboards, Sharing and collaboration, Integration with other Microsoft services, Real-time data updates, Security and data governance";

    public string BenefitsSetting()
        => "Benefits";

    public string BenefitsPrompt()
        => "What are the benefits of the product?";

    public string BenefitsNotValid()
        => "The benefits are not valid.";

    public string BenefitsDemo()
        => "Informed decision making, Increased productivity, Accessibility, Flexibility, Scalability, Continuous improvement";
}