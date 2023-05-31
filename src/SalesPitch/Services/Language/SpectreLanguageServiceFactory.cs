namespace SalesPitch.Services.Language;

public class SpectreLanguageServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public SpectreLanguageServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ILanguageService GetLanguageService(SupportedLanguage? language)
        => language switch
           {
               SupportedLanguage.English => _serviceProvider
                   .GetService(typeof(EnglishLanguageService)) as ILanguageService,
               SupportedLanguage.French => _serviceProvider
                   .GetService(typeof(FrenchLanguageService)) as ILanguageService,
               SupportedLanguage.Spanish => _serviceProvider
                   .GetService(typeof(SpanishLanguageService)) as ILanguageService,
               _ => throw new InvalidOperationException($"Unsupported language: {language}")
           } 
           ?? throw new InvalidOperationException($"Language service not found for language: {language}");
}