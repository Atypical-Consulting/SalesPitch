using SalesPitch.Commands;

namespace SalesPitch.Services.Language;

public interface ILanguageService
{
    string GetChatGPTUserPrompt(SalesPitchSettings settings);
    
    string GetChatGPTSetupSystemMessage();
    
    string TableSettingKey();
    
    string TableSettingValue();
    
    string SalesPitchFrameworkMarkup(SalesPitchFramework? framework);
    
    string SalesPitchFrameworkPrompt();

    string SalesPitchFrameworkSetting();

    string LanguageSetting();

    string LanguageMarkup(SupportedLanguage? language);

    string UseDemoDataSetting();
    
    string UseDemoDataPrompt();

    string UseDemoDataMarkup(bool isDemo);

    string ProductSetting();
    
    string ProductPrompt();
    
    string ProductNotValid();

    string ProductDemo();

    string PriceSetting();
    
    string PricePrompt();
    
    string PriceNotValid();

    string PriceDemo();

    string FeaturesSetting();
    
    string FeaturesPrompt();
    
    string FeaturesNotValid();

    string FeaturesDemo();

    string BenefitsSetting();
    
    string BenefitsPrompt();
    
    string BenefitsNotValid();

    string BenefitsDemo();
}