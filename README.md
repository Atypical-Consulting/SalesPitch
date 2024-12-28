# SalesPitch [![Sparkline](https://stars.medv.io/Atypical-Consulting/SalesPitch.svg)](https://stars.medv.io/Atypical-Consulting/SalesPitch)

SalesPitch is a sales pitch generation application using OpenAI GPT-4. Create impactful sales pitches for your products in just a few simple steps.

![SalesPitch](./assets/salespitch.png)

## Features

* Interacts with OpenAI GPT-4o to generate sales pitches
* Supports multiple languages (French, English, German, and Spanish)
* Includes various sales pitch frameworks
    * AIDA (Attention, Interest, Desire, Action)
    * PAS (Problem-Agitate-Solve)
    * USP (Unique Selling Proposition)
    * Features-Benefits
    * Storytelling
    * WIIFM (What’s In It For Me)
    * Youtility
    * FAB (Features, Advantages, Benefits)
    * HHE (Headline, Hook, Empathy)
    * SUSPENSE (Surprise, Uniqueness, Specifics, Promise, Excitement, Newness, Story)
* User-friendly interface with Spectre.Console
* Ability to use demo data

## Stats

![Alt](https://repobeats.axiom.co/api/embed/9e8cea0532101e0c02a5034825d6be9f1b40f732.svg "Repobeats analytics image")

## Installation

Ensure that you have .NET 9.0 or a later version installed on your machine.

Clone this repository and navigate to the source folder:

```sh
git clone https://github.com/user/SalesPitch.git
cd SalesPitch
```

Install the dependencies and build the project:

```sh
dotnet restore
dotnet build --configuration Release
```

## Configuration

1. Obtain an API key from OpenAI ([https://beta.openai.com/signup/](https://beta.openai.com/signup/))
2. Create an `appsettings.json` file at the root of the project with the API key:

```json
{
  "OpenAIServiceOptions": {
    "ApiKey": "<your_openai_api_key>",
    "Organization": "<your_organization_id>"
  }
}
```

## Usage

Run the application using the following command at the root of the project:

```sh
dotnet run --configuration Release --project src/SalesPitch
```

The application will guide you through several steps to generate a sales pitch for your product. You can also use the demo data to see how the application works.

## Contribution

If you wish to contribute to this project, please submit a pull request or open an issue in the GitHub repository.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Acknowledgments

This application was inspired by the following article: [Write A Great Chat GPT Sales Pitch in 5 Steps](https://txtly.ai/write-a-chat-gpt-sales-pitch/)

* [OpenAI](https://openai.com/)
* [Spectre.Console](https://spectreconsole.net/)
* [Betalgo.OpenAI](https://github.com/betalgo/openai)