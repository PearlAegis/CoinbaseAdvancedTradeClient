# Coinbase Advanced Trade Client

[![Pull Request Build](https://github.com/PearlAegis/CoinbaseAdvancedTradeClient/actions/workflows/pull-request.yml/badge.svg)](https://github.com/PearlAegis/CoinbaseAdvancedTradeClient/actions/workflows/pull-request.yml)
[![Release Build](https://github.com/PearlAegis/CoinbaseAdvancedTradeClient/actions/workflows/release.yml/badge.svg)](https://github.com/PearlAegis/CoinbaseAdvancedTradeClient/actions/workflows/release.yml)

A comprehensive .NET client library for the Coinbase Advanced Trade API, providing both REST API and WebSocket functionality for cryptocurrency trading applications.

## ⚠️ Beta Software Notice

**This software is currently in beta (version 0.x.x) and is not yet considered a stable release.**

- API may change without notice in future versions
- Some features may be incomplete or under development  
- Use in production environments at your own risk
- Please report issues and provide feedback to help improve the library

We are actively working toward a 1.0.0 stable release. Check the [CHANGELOG.md](CHANGELOG.md) for the latest updates and breaking changes.

## Features

- **Complete API Coverage**: Support for all Coinbase Advanced Trade endpoints including accounts, orders, products, and transaction summaries
- **WebSocket Support**: Real-time market data streaming with support for multiple channels
- **Authentication**: Secure API key-based authentication with HMAC signature generation
- **Type Safety**: Strongly-typed models for all API requests and responses
- **Testing**: Comprehensive unit test suite with 142+ passing tests
- **Modern .NET**: Built for .NET 9.0 with nullable reference types and implicit usings

## Installation

Install via GitHub Packages:

```bash
dotnet add package CoinbaseAdvancedTradeClient --source https://nuget.pkg.github.com/PearlAegis/index.json
```

Or add to your `.csproj` file:

```xml
<PackageReference Include="CoinbaseAdvancedTradeClient" Version="0.2.0" />
```

## Quick Start

### API Client Setup

```csharp
using CoinbaseAdvancedTradeClient;
using CoinbaseAdvancedTradeClient.Models.Config;

var config = new ApiClientConfig
{
    ApiKey = "your-api-key",
    ApiSecret = "your-api-secret"
};

using var client = new CoinbaseAdvancedTradeApiClient(config);

// Get account information
var accounts = await client.GetAccountsAsync();

// Place a market order
var orderResponse = await client.CreateOrderAsync(new CreateOrderParameters
{
    // Order configuration here
});
```

### WebSocket Client Setup

```csharp
using CoinbaseAdvancedTradeClient;
using CoinbaseAdvancedTradeClient.Models.Config;

var config = new WebsocketClientConfig
{
    ApiKey = "your-api-key",
    ApiSecret = "your-api-secret"
};

using var wsClient = new CoinbaseAdvancedTradeWebsocketClient(config);

// Subscribe to market data
await wsClient.SubscribeAsync("BTC-USD", SubscriptionType.Ticker);
```

## API Reference

### Endpoints

- **Accounts**: Get account information and balances
- **Orders**: Create, cancel, and retrieve order information
- **Products**: Get product details, market data, and trading pairs
- **Transaction Summary**: Retrieve fee tiers and transaction summaries

### WebSocket Channels

- **Ticker**: Real-time price updates
- **Level2**: Order book data
- **Market Trades**: Recent trade information
- **User**: Account-specific updates

## Build and Test

### Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or VS Code

### Building

```bash
git clone https://github.com/PearlAegis/CoinbaseAdvancedTradeClient.git
cd CoinbaseAdvancedTradeClient
dotnet build
```

### Running Tests

```bash
dotnet test CoinbaseAdvancedTradeClient/CoinbaseAdvancedTradeClient.UnitTests/
```

### Interactive Testing

For interactive API testing, use the separate [CoinbaseAdvancedTradeClient-Sandbox](https://github.com/PearlAegis/CoinbaseAdvancedTradeClient-Sandbox) project, which provides a comprehensive Blazor Server application with full WebSocket support.

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## Versioning

This project uses semantic versioning with synchronized assembly versions:

### Package Versioning
- **RC packages**: Generated automatically on pull requests with `-rc` suffix (e.g., `1.2.3-rc`)
- **Release packages**: Created when GitHub releases are published (e.g., `1.2.3`)
- All packages are published to GitHub Packages

### Assembly Versioning
Assembly versions are automatically synchronized with package versions using a numeric scheme:
- **Release packages** (`1.2.3`): Assembly version = `1.2.3.0`
- **RC packages** (`1.2.3-rc`): Assembly version = `1.2.3.1` 

This ensures that `Assembly.GetName().Version` returns the correct version in consuming applications, with the revision number indicating release (0) vs RC (1) builds.

## Documentation

- **[CHANGELOG.md](CHANGELOG.md)**: Version history and migration notes
- **[CLAUDE.md](CLAUDE.md)**: Development configuration and commands for Claude Code
- **GitHub Actions**: Automated CI/CD with pull request and release workflows

## Recent Updates

### v0.2.0 (Latest)
- ✅ **Migrated from Azure DevOps to GitHub Actions** - Complete CI/CD pipeline migration
- ✅ **Upgraded to .NET 9.0** - Full compatibility with latest .NET runtime
- ✅ **Updated dependencies** - All packages compatible with .NET 9
- ✅ **Enhanced testing** - All 142+ unit tests passing
- ✅ **Automated versioning** - Semantic versioning with RC packages on pull requests

## License

Copyright © 2025 Pearl Aegis. All rights reserved.