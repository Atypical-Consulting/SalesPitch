# SalesPitch

> **Generate tailored, professional sales pitches in seconds using AI — stop choosing between generic copy and hours of personalization.**

<!-- Badges: Row 1 — Identity -->
[![Atypical-Consulting - SalesPitch](https://img.shields.io/static/v1?label=Atypical-Consulting&message=SalesPitch&color=blue&logo=github)](https://github.com/Atypical-Consulting/SalesPitch)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET 9](https://img.shields.io/badge/.NET-9.0-purple?logo=dotnet)](https://dotnet.microsoft.com/)
[![stars - SalesPitch](https://img.shields.io/github/stars/Atypical-Consulting/SalesPitch?style=social)](https://github.com/Atypical-Consulting/SalesPitch)
[![forks - SalesPitch](https://img.shields.io/github/forks/Atypical-Consulting/SalesPitch?style=social)](https://github.com/Atypical-Consulting/SalesPitch)

<!-- Badges: Row 2 — Activity -->
[![GitHub tag](https://img.shields.io/github/tag/Atypical-Consulting/SalesPitch?include_prereleases=&sort=semver&color=blue)](https://github.com/Atypical-Consulting/SalesPitch/releases/)
[![issues - SalesPitch](https://img.shields.io/github/issues/Atypical-Consulting/SalesPitch)](https://github.com/Atypical-Consulting/SalesPitch/issues)
[![GitHub pull requests](https://img.shields.io/github/issues-pr/Atypical-Consulting/SalesPitch)](https://github.com/Atypical-Consulting/SalesPitch/pulls)
[![GitHub last commit](https://img.shields.io/github/last-commit/Atypical-Consulting/SalesPitch)](https://github.com/Atypical-Consulting/SalesPitch/commits/main)

<!-- Badges: Row 3 — Quality -->
[![Build](https://github.com/Atypical-Consulting/SalesPitch/actions/workflows/continuous.yml/badge.svg)](https://github.com/Atypical-Consulting/SalesPitch/actions/workflows/continuous.yml)

---

![SalesPitch](./assets/salespitch.png)

## Table of Contents

- [The Problem](#the-problem)
- [The Solution](#the-solution)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Roadmap](#roadmap)
- [Stats](#stats)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgments](#acknowledgments)

## The Problem

Writing compelling, tailored sales pitches for every prospect is time-consuming. Sales teams either write generic pitches that fail to resonate, or spend hours personalizing each one. Existing tools lack support for proven sales frameworks and multi-language output, forcing teams to manually adapt their messaging for different audiences and markets.

## The Solution

**SalesPitch** uses OpenAI GPT-4 to generate tailored, professional sales pitches from minimal input via an interactive CLI. Choose from 10 proven sales frameworks, pick your target language, describe your product, and get a polished pitch in seconds.

```sh
dotnet run --configuration Release --project src/SalesPitch

# The interactive CLI will guide you through:
# 1. Select a language (French, English, German, Spanish)
# 2. Choose a sales framework (AIDA, PAS, USP, etc.)
# 3. Describe your product
# 4. Receive a tailored sales pitch powered by GPT-4
```

## Features

- [x] AI-powered pitch generation using OpenAI GPT-4o
- [x] 10 proven sales pitch frameworks (AIDA, PAS, USP, Features-Benefits, Storytelling, WIIFM, Youtility, FAB, HHE, SUSPENSE)
- [x] Multi-language support (French, English, German, Spanish)
- [x] Interactive CLI powered by Spectre.Console
- [x] Demo data mode for quick evaluation
- [x] User secrets support for secure API key storage

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Runtime | .NET 9.0 |
| AI | OpenAI GPT-4 via [Betalgo.Ranul.OpenAI](https://github.com/betalgo/openai) 9.x |
| CLI | [Spectre.Console](https://spectreconsole.net/) 0.49 |
| DI | [Scrutor](https://github.com/khellang/Scrutor) 5.x |
| Build | [Nuke](https://nuke.build/) |

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- An [OpenAI API key](https://platform.openai.com/signup/)

### Installation

**From Source**

```bash
git clone https://github.com/Atypical-Consulting/SalesPitch.git
cd SalesPitch
dotnet restore
dotnet build --configuration Release
```

### Configuration

Set your OpenAI API key using .NET user secrets (recommended):

```bash
cd src/SalesPitch
dotnet user-secrets set "OpenAIServiceOptions:ApiKey" "<your_openai_api_key>"
dotnet user-secrets set "OpenAIServiceOptions:Organization" "<your_organization_id>"
```

Or create an `appsettings.json` file in the project root:

```json
{
  "OpenAIServiceOptions": {
    "ApiKey": "<your_openai_api_key>",
    "Organization": "<your_organization_id>"
  }
}
```

## Usage

Run the application from the repository root:

```sh
dotnet run --configuration Release --project src/SalesPitch
```

The interactive CLI will guide you through several steps to generate a sales pitch for your product. You can also use the demo data to see how the application works.

## Architecture

```
┌─────────────────────────────────────────────────┐
│                   CLI Input                      │
│          (Language, Framework, Product)           │
└──────────────────────┬──────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────┐
│              Spectre.Console CLI                 │
│     (Interactive prompts, rich formatting)       │
└──────────────────────┬──────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────┐
│            Language Service Layer                │
│   (FR / EN / DE / ES prompt localization)        │
└──────────────────────┬──────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────┐
│              OpenAI GPT-4 API                    │
│         (Betalgo.Ranul.OpenAI SDK)               │
└──────────────────────┬──────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────┐
│             Formatted Output                     │
│       (Sales pitch in chosen language)           │
└─────────────────────────────────────────────────┘
```

### Project Structure

```
SalesPitch/
├── src/
│   └── SalesPitch/
│       ├── Commands/           # CLI command definitions and settings
│       ├── Extensions/         # Response extension methods
│       ├── Infrastructure/     # DI type resolver and registrar
│       ├── Services/
│       │   └── Language/       # Language-specific prompt services
│       ├── TypeConverters/     # Custom type converters
│       └── Program.cs          # Application entry point
├── build/                      # Nuke build automation
├── assets/                     # Screenshots and images
├── SalesPitch.sln              # Solution file
└── README.md
```

## Roadmap

- [ ] Add more languages (Italian, Portuguese, Japanese, Chinese)
- [ ] Template customization — let users define custom pitch structures
- [ ] Batch generation — generate pitches for multiple products at once
- [ ] Export to PDF/Markdown/HTML
- [ ] Tone control — formal, casual, persuasive, technical
- [ ] Cost estimation — show token usage and API cost per generation

> Want to contribute? Pick any roadmap item and open a PR!

## Stats

![Alt](https://repobeats.axiom.co/api/embed/9e8cea0532101e0c02a5034825d6be9f1b40f732.svg "Repobeats analytics image")

## Contributing

Contributions are welcome! Here's how to get started:

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit using [conventional commits](https://www.conventionalcommits.org/) (`git commit -m 'feat: add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Conventional Commits

This project follows the [Conventional Commits](https://www.conventionalcommits.org/) specification:

| Prefix | Purpose |
|--------|---------|
| `feat:` | A new feature |
| `fix:` | A bug fix |
| `docs:` | Documentation changes |
| `refactor:` | Code refactoring (no feature or fix) |
| `test:` | Adding or updating tests |
| `chore:` | Maintenance tasks |

## License

[MIT](LICENSE) © 2023 [Atypical Consulting](https://atypical.garry-ai.cloud)

## Acknowledgments

- Inspired by [Write A Great Chat GPT Sales Pitch in 5 Steps](https://txtly.ai/write-a-chat-gpt-sales-pitch/)
- [OpenAI](https://openai.com/) — GPT-4 language model
- [Spectre.Console](https://spectreconsole.net/) — Beautiful CLI framework for .NET
- [Betalgo.OpenAI](https://github.com/betalgo/openai) — .NET SDK for OpenAI

---

Built with care by [Atypical Consulting](https://atypical.garry-ai.cloud) — opinionated, production-grade open source.

[![Contributors](https://contrib.rocks/image?repo=Atypical-Consulting/SalesPitch)](https://github.com/Atypical-Consulting/SalesPitch/graphs/contributors)
