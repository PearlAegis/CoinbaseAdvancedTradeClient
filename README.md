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
- **Authentication**: Secure JWT Bearer token authentication with ECDSA ES256 signatures
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

### Configuration

⚠️ **Security Warning**: Never commit your API key names or secrets to version control. Use secure configuration methods:
- User secrets for development: `dotnet user-secrets set "CoinbaseClientConfig:KeyName" "your-key-name"`
- Azure Key Vault or AWS Secrets Manager for production
- Environment variables with proper access controls
- Kubernetes secrets or similar container orchestration secrets

Add your Coinbase Cloud API credentials to `appsettings.json`:

```json
{
  "CoinbaseClientConfig": {
    "KeyName": "your-key-name",
    "KeySecret": "-----BEGIN EC PRIVATE KEY-----\n...\n-----END EC PRIVATE KEY-----",
    "ApiBaseUrl": "https://api.coinbase.com", // Optional, defaults to production
    "WebSocketUrl": "wss://advanced-trade-ws.coinbase.com" // Optional, defaults to production
  }
}
```

### API Client Setup

```csharp
using CoinbaseAdvancedTradeClient.Extensions;

// Register during startup
builder.Services.AddCoinbaseAdvancedTradeClient();

// Use in your services
public class TradingService
{
    private readonly ICoinbaseAdvancedTradeApiClient _client;
    
    public TradingService(ICoinbaseAdvancedTradeApiClient client)
    {
        _client = client;
    }
    
    public async Task<ApiResponse<AccountsPage>> GetAccountsAsync()
    {
        return await _client.GetAccountsAsync();
    }
}
```

### WebSocket Client Setup

```csharp
// WebSocket client is automatically registered with the API client
public class MarketDataService
{
    private readonly ICoinbaseAdvancedTradeWebSocketClient _wsClient;
    
    public MarketDataService(ICoinbaseAdvancedTradeWebSocketClient wsClient)
    {
        _wsClient = wsClient;
    }
    
    public async Task SubscribeToTicker(string productId)
    {
        // Connect to WebSocket first
        await _wsClient.ConnectAsync();
        
        // Then subscribe to market data
        await _wsClient.SubscribeAsync(productId, SubscriptionType.Ticker);
    }
}
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