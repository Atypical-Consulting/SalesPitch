# SalesPitch

SalesPitch est une application de génération de pitch de vente utilisant OpenAI GPT-3. Créez des pitchs de vente percutants pour vos produits en quelques étapes simples.

## Fonctionnalités

* Interagit avec OpenAI GPT-3.5 pour générer des pitchs de vente
* Prend en charge plusieurs langues
* Comprend plusieurs cadres de pitch de vente
* Interface utilisateur conviviale avec Spectre.Console
* Possibilité d'utiliser des données de démonstration

## Installation

Assurez-vous d'avoir installé .NET 6.0 ou une version ultérieure sur votre machine.

Clonez ce dépôt et naviguez jusqu'au dossier source :

```sh
git clone https://github.com/user/SalesPitch.git
cd SalesPitch
```

Installez les dépendances et construisez le projet :

```sh
dotnet restore
dotnet build --configuration Release
```

## Configuration

1. Obtenez une clé API auprès d'OpenAI (https://beta.openai.com/signup/)
2. Créez un fichier appsettings.json à la racine du projet avec la clé API :

```json
{
  "OpenAI": {
    "ApiKey": "votre_clé_API"
  }
}
```

## Utilisation

Exécutez l'application en utilisant la commande suivante à la racine du projet :

```sh
dotnet run --configuration Release --project SalesPitch
```

L'application vous guidera à travers plusieurs étapes pour générer un pitch de vente pour votre produit. Vous pouvez également utiliser les données de démonstration pour voir comment l'application fonctionne.

## Contribution

Si vous souhaitez contribuer à ce projet, veuillez soumettre une pull request ou ouvrir un problème dans le dépôt GitHub.

## Licence

Ce projet est sous licence MIT. Voir le fichier LICENSE pour plus d'informations.
