# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

SalesPitch is a .NET 9.0 CLI application that generates professional sales pitches using OpenAI GPT-4o. The application supports multiple languages (English, French, German, Spanish) and implements 10 different sales frameworks (AIDA, PAS, USP, etc.).

## Common Commands

### Build and Run
```bash
# Restore dependencies and build
dotnet restore
dotnet build --configuration Release

# Run the application
dotnet run --configuration Release --project src/SalesPitch

# Using NUKE build automation
./build.sh        # Unix/macOS
build.cmd         # Windows
build.ps1         # PowerShell
```

### Development
```bash
# Build for development
dotnet build --configuration Debug --project src/SalesPitch

# Run tests (if any are added)
dotnet test

# Publish for distribution
dotnet publish src/SalesPitch --configuration Release --self-contained --runtime osx-x64
```

## Architecture

### Core Components
- **Commands Layer** (`src/SalesPitch/Commands/`): Contains the main CLI command implementation using Spectre.Console.Cli
- **Services Layer** (`src/SalesPitch/Services/Language/`): Language-specific services implementing `ILanguageService` for internationalization
- **Infrastructure** (`src/SalesPitch/Infrastructure/`): Dependency injection adapters for Spectre.Console
- **Extensions** (`src/SalesPitch/Extensions/`): Helper methods for OpenAI API responses

### Key Technologies
- **Spectre.Console**: Rich console UI framework for CLI interactions
- **Betalgo.Ranul.OpenAI**: OpenAI API integration
- **Microsoft.Extensions.DependencyInjection**: Dependency injection container
- **NUKE**: Build automation system

### Configuration
- OpenAI API configuration in `appsettings.json`
- User Secrets recommended for API keys in development
- Language services factory pattern for multi-language support

### Sales Frameworks
The application implements 10 sales frameworks as an enum (`SalesPitchFramework`):
AIDA, PAS, USP, Features-Benefits, Storytelling, WIIFM, Youtility, FAB, HHE, SUSPENSE

### Language Support
Multi-language architecture with dedicated service classes for each supported language, managed through `SpectreLanguageServiceFactory`.