using SalesPitch.Commands;

namespace SalesPitch.Services.Language;

public class FrenchLanguageService : ILanguageService
{
    public string GetChatGPTUserPrompt(SalesPitchSettings settings)
    {
        return $"""
            Rédige un argumentaire de vente pour le produit suivant en utilisant le framework de vente {settings.Framework}. 
            Insiste sur les problèmes et les avantages. 
            Formule l'argumentaire de manière à ce qu'il puisse être présenté au public, avec des titres qui attirent l'attention tout au long de l'argumentaire, 
            n'ajoute pas d'instructions.
            Je veux qu'il soit prêt à être imprimé.

            Nom du produit   : {settings.Product}
            Prix             : {settings.Price}
            Caractéristiques : {settings.Features}
            Avantages        : {settings.Benefits}
            """;
    }

    public string GetChatGPTSetupSystemMessage()
        => "Tu agis en tant que copywriter avec 20 ans d'expérience en vente. "
           + "Tu as pour mission de rédiger un argumentaire de vente pour un produit.";

    public string TableSettingKey()
        => "Réglage";

    public string TableSettingValue()
        => "Valeur";

    public string SalesPitchFrameworkMarkup(SalesPitchFramework? framework)
        => framework switch
        {
            SalesPitchFramework.AIDA
                => $"{framework} (Attention, Intérêt, Désir, Action)",
            SalesPitchFramework.PAS
                => $"{framework} (Problème-Agitateur-Solution)",
            SalesPitchFramework.USP
                => $"{framework} (Proposition de vente unique)",
            SalesPitchFramework.FeaturesBenefits
                => $"{framework} (Caractéristiques-Bénéfices)",
            SalesPitchFramework.Storytelling
                => $"{framework} (Narration)",
            SalesPitchFramework.WIIFM
                => $"{framework} (Qu’est-ce que je vais en tirer?)",
            SalesPitchFramework.Youtility
                => $"{framework} (Youtility)",
            SalesPitchFramework.FAB
                => $"{framework} (Caractéristiques, Avantages, Bénéfices)",
            SalesPitchFramework.HHE
                => $"{framework} (Titre, Accroche, Empathie)",
            SalesPitchFramework.SUSPENSE
                => $"{framework} (Surprise, Originalité, Détails spécifiques, Promesse, Excitation, Nouveauté, Histoire)",
            _ => throw new ArgumentOutOfRangeException(nameof(framework), framework, null)
        };

    public string SalesPitchFrameworkPrompt()
        => "Choisissez un framework de vente:";

    public string SalesPitchFrameworkSetting()
        => "Framework de vente";

    public string LanguageSetting()
        => "Langue";

    public string LanguageMarkup(SupportedLanguage? language)
        => language switch
        {
            SupportedLanguage.English => "Anglais",
            SupportedLanguage.French  => "Français",
            SupportedLanguage.Spanish => "Espagnol",
            SupportedLanguage.German  => "Allemand",
            _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
        };

    public string UseDemoDataSetting()
        => "Données de démonstration";

    public string UseDemoDataPrompt()
        => "Utiliser les données de démonstration?";

    public string UseDemoDataMarkup(bool isDemo)
        => isDemo
            ? "Oui"
            : "Non";

    public string ProductSetting()
        => "Nom du produit";

    public string ProductPrompt()
        => "Quel est le nom du produit?";

    public string ProductNotValid()
        => "Le nom du produit doit être renseigné.";

    public string ProductDemo()
        => "Microsoft Power BI Premium";

    public string PriceSetting()
        => "Prix";

    public string PricePrompt()
        => "Quel est le prix du produit?";

    public string PriceNotValid()
        => "Le prix du produit doit être renseigné.";

    public string PriceDemo()
        => "16,90 € par utilisateur/mois";

    public string FeaturesSetting()
        => "Caractéristiques";

    public string FeaturesPrompt()
        => "Quelles sont les caractéristiques du produit?";

    public string FeaturesNotValid()
        => "Les caractéristiques du produit doivent être renseignées.";

    public string FeaturesDemo()
        => "Sources de données variées, Création de rapports interactifs, Tableaux de bord personnalisés, Partage et collaboration, Intégration avec d'autres services Microsoft, Actualisation des données en temps réel, Sécurité et gouvernance des données";

    public string BenefitsSetting()
        => "Bénéfices";

    public string BenefitsPrompt()
        => "Quels sont les bénéfices du produit?";

    public string BenefitsNotValid()
        => "Les bénéfices du produit doivent être renseignés.";

    public string BenefitsDemo()
        => "Prise de décision éclairée, Productivité accrue, Accessibilité, Flexibilité, Scalabilité, Amélioration continue";
}