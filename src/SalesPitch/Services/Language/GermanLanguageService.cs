using SalesPitch.Commands;

namespace SalesPitch.Services.Language;

public class GermanLanguageService : ILanguageService
{
    public string GetChatGPTUserPrompt(SalesPitchSettings settings)
    {
        return $"""
            Verfasse ein Verkaufsargument für das folgende Produkt mit dem Verkaufsrahmen {settings.Framework}. 
            Betone die Probleme und Vorteile. 
            Formuliere das Verkaufsargument so, dass es dem Publikum präsentiert werden kann, mit Aufmerksamkeit erregenden Überschriften während des gesamten Arguments, 
            füge keine Anweisungen hinzu.
            Ich möchte, dass es druckfertig ist.

            Produktname     : {settings.Product}
            Preis           : {settings.Price}
            Eigenschaften   : {settings.Features}
            Vorteile        : {settings.Benefits}
            """;
    }

    public string GetChatGPTSetupSystemMessage()
        => "Du agierst als Texter mit 20 Jahren Erfahrung im Verkauf. "
           + "Deine Aufgabe ist es, ein Verkaufsargument für ein Produkt zu verfassen.";

    public string TableSettingKey()
        => "Einstellung";

    public string TableSettingValue()
        => "Wert";

    public string SalesPitchFrameworkMarkup(SalesPitchFramework? framework)
        => framework switch
        {
            SalesPitchFramework.AIDA
                => $"{framework} (Aufmerksamkeit, Interesse, Verlangen, Aktion)",
            SalesPitchFramework.PAS
                => $"{framework} (Problem-Agitation-Lösung)",
            SalesPitchFramework.USP
                => $"{framework} (Einzigartiges Verkaufsversprechen)",
            SalesPitchFramework.FeaturesBenefits
                => $"{framework} (Eigenschaften-Vorteile)",
            SalesPitchFramework.Storytelling
                => $"{framework} (Geschichtenerzählen)",
            SalesPitchFramework.WIIFM
                => $"{framework} (Was ist für mich drin?)",
            SalesPitchFramework.Youtility
                => $"{framework} (Youtility)",
            SalesPitchFramework.FAB
                => $"{framework} (Eigenschaften, Vorteile, Nutzen)",
            SalesPitchFramework.HHE
                => $"{framework} (Überschrift, Haken, Empathie)",
            SalesPitchFramework.SUSPENSE
                => $"{framework} (Überraschung, Originalität, spezifische Details, Versprechen, Aufregung, Neuheit, Geschichte)",
            _ => throw new ArgumentOutOfRangeException(nameof(framework), framework, null)
        };

    public string SalesPitchFrameworkPrompt()
        => "Wählen Sie einen Verkaufsrahmen:";

    public string SalesPitchFrameworkSetting()
        => "Verkaufsrahmen";

    public string LanguageSetting()
        => "Sprache";

    public string LanguageMarkup(SupportedLanguage? language)
        => language switch
        {
            SupportedLanguage.English => "Englisch",
            SupportedLanguage.German  => "Deutsch",
            SupportedLanguage.French  => "Französisch",
            SupportedLanguage.Spanish => "Spanisch",
            _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
        };

    public string UseDemoDataSetting()
        => "Demodaten";

    public string UseDemoDataPrompt()
        => "Demodaten verwenden?";

    public string UseDemoDataMarkup(bool isDemo)
        => isDemo
            ? "Ja"
            : "Nein";

    public string ProductSetting()
        => "Produktname";

    public string ProductPrompt()
        => "Wie lautet der Name des Produkts?";

    public string ProductNotValid()
        => "Der Produktname muss angegeben werden.";

    public string ProductDemo()
        => "Microsoft Power BI Premium";

    public string PriceSetting()
        => "Preis";

    public string PricePrompt()
        => "Wie viel kostet das Produkt?";

    public string PriceNotValid()
        => "Der Preis des Produkts muss angegeben werden.";

    public string PriceDemo()
        => "16,90 € pro Benutzer/Monat";

    public string FeaturesSetting()
        => "Eigenschaften";

    public string FeaturesPrompt()
        => "Was sind die Eigenschaften des Produkts?";

    public string FeaturesNotValid()
        => "Die Eigenschaften des Produkts müssen angegeben werden.";

    public string FeaturesDemo()
        => "Verschiedene Datenquellen, Erstellung interaktiver Berichte, Benutzerdefinierte Dashboards, Teilen und Zusammenarbeit, Integration mit anderen Microsoft-Diensten, Echtzeit-Datenaktualisierung, Datensicherheit und -verwaltung";

    public string BenefitsSetting()
        => "Vorteile";

    public string BenefitsPrompt()
        => "Was sind die Vorteile des Produkts?";

    public string BenefitsNotValid()
        => "Die Vorteile des Produkts müssen angegeben werden.";

    public string BenefitsDemo()
        => "Informierte Entscheidungsfindung, Erhöhte Produktivität, Zugänglichkeit, Flexibilität, Skalierbarkeit, Kontinuierliche Verbesserung";
}