using SalesPitch.Commands;

namespace SalesPitch.Services.Language;

public class SpanishLanguageService : ILanguageService
{
    public string GetChatGPTUserPrompt(SalesPitchSettings settings)
    {
        return $"""
            Redacta un argumento de venta para el siguiente producto utilizando el marco de ventas {settings.Framework}. 
            Insiste en los problemas y las ventajas. 
            Formula el argumento de manera que pueda ser presentado al público, con títulos que atraigan la atención a lo largo del argumento, 
            no añadas instrucciones.
            Quiero que esté listo para ser impreso.

            Nombre del producto   : {settings.Product}
            Precio                : {settings.Price}
            Características       : {settings.Features}
            Ventajas              : {settings.Benefits}
            """;
    }

    public string GetChatGPTSetupSystemMessage()
        => "Actúas como redactor con 20 años de experiencia en ventas. "
           + "Tu misión es redactar un argumento de venta para un producto.";

    public string TableSettingKey()
        => "Configuración";

    public string TableSettingValue()
        => "Valor";

    public string SalesPitchFrameworkMarkup(SalesPitchFramework? framework)
        => framework switch
        {
            SalesPitchFramework.AIDA
                => $"{framework} (Atención, Interés, Deseo, Acción)",
            SalesPitchFramework.PAS
                => $"{framework} (Problema-Agitador-Solución)",
            SalesPitchFramework.USP
                => $"{framework} (Propuesta de venta única)",
            SalesPitchFramework.FeaturesBenefits
                => $"{framework} (Características-Ventajas)",
            SalesPitchFramework.Storytelling
                => $"{framework} (Narración)",
            SalesPitchFramework.WIIFM
                => $"{framework} (¿Qué hay en ello para mí?)",
            SalesPitchFramework.Youtility
                => $"{framework} (Youtility)",
            SalesPitchFramework.FAB
                => $"{framework} (Características, Ventajas, Beneficios)",
            SalesPitchFramework.HHE
                => $"{framework} (Título, Gancho, Empatía)",
            SalesPitchFramework.SUSPENSE
                => $"{framework} (Sorpresa, Originalidad, Detalles específicos, Promesa, Excitación, Novedad, Historia)",
            _ => throw new ArgumentOutOfRangeException(nameof(framework), framework, null)
        };

    public string SalesPitchFrameworkPrompt()
        => "Elija un marco de ventas:";

    public string SalesPitchFrameworkSetting()
        => "Marco de ventas";

    public string LanguageSetting()
        => "Idioma";

    public string LanguageMarkup(SupportedLanguage? language)
        => language switch
        {
            SupportedLanguage.English => "Inglés",
            SupportedLanguage.French  => "Francés",
            SupportedLanguage.Spanish => "Español",
            SupportedLanguage.German  => "Alemán",
            _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
        };

    public string UseDemoDataSetting()
        => "Datos de demostración";

    public string UseDemoDataPrompt()
        => "¿Utilizar datos de demostración?";

    public string UseDemoDataMarkup(bool isDemo)
        => isDemo
            ? "Sí"
            : "No";

    public string ProductSetting()
        => "Nombre del producto";

    public string ProductPrompt()
        => "¿Cuál es el nombre del producto?";

    public string ProductNotValid()
        => "El nombre del producto debe ser proporcionado.";

    public string ProductDemo()
        => "Microsoft Power BI Premium";

    public string PriceSetting()
        => "Precio";

    public string PricePrompt()
        => "¿Cuál es el precio del producto?";

    public string PriceNotValid()
        => "El precio del producto debe ser proporcionado.";

    public string PriceDemo()
        => "16,90 € por usuario/mes";

    public string FeaturesSetting()
        => "Características";

    public string FeaturesPrompt()
        => "¿Cuáles son las características del producto?";

    public string FeaturesNotValid()
        => "Las características del producto deben ser proporcionadas.";

    public string FeaturesDemo()
        => "Fuentes de datos variadas, Creación de informes interactivos, Tableros personalizados, Compartir y colaborar, Integración con otros servicios de Microsoft, Actualización de datos en tiempo real, Seguridad y gobernabilidad de datos";

    public string BenefitsSetting()
        => "Beneficios";

    public string BenefitsPrompt()
        => "¿Cuáles son los beneficios del producto?";

    public string BenefitsNotValid()
        => "Los beneficios del producto deben ser proporcionados.";

    public string BenefitsDemo()
        => "Toma de decisiones informada, Mayor productividad, Accesibilidad, Flexibilidad, Escalabilidad, Mejora continua";
}