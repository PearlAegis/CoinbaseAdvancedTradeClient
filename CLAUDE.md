# Claude Project Configuration

This file contains configuration and context for working with Claude Code on this project.

## Project Overview

**Coinbase Advanced Trade Client** is a comprehensive .NET client library for the Coinbase Advanced Trade API, providing both REST API and WebSocket functionality for cryptocurrency trading applications.

## Development Commands

### Build and Test
```bash
# Build the project
dotnet build CoinbaseAdvancedTradeClient/CoinbaseAdvancedTradeClient/CoinbaseAdvancedTradeClient.csproj --configuration Release

# Run unit tests
dotnet test CoinbaseAdvancedTradeClient/CoinbaseAdvancedTradeClient.UnitTests/CoinbaseAdvancedTradeClient.UnitTests.csproj --configuration Release

# Build the complete solution
dotnet build CoinbaseAdvancedTradeClient.sln --configuration Release
```

### Packaging
```bash
# Create NuGet package
dotnet pack CoinbaseAdvancedTradeClient/CoinbaseAdvancedTradeClient/CoinbaseAdvancedTradeClient.csproj --configuration Release --output ./artifacts

# Publish to GitHub Packages (requires authentication)
dotnet nuget push ./artifacts/*.nupkg --source https://nuget.pkg.github.com/PearlAegis/index.json --api-key YOUR_TOKEN
```

## Project Structure

```
CoinbaseAdvancedTradeClient/
├── .github/workflows/           # GitHub Actions CI/CD
├── CoinbaseAdvancedTradeClient/ # Main library project
│   ├── Authentication/          # API authentication
│   ├── Endpoints/              # API endpoint implementations
│   ├── Enums/                  # Enumeration types
│   ├── Extensions/             # Extension methods
│   ├── Interfaces/             # Interface definitions
│   ├── Models/                 # Data models
│   └── Resources/              # Resource files
├── CoinbaseAdvancedTradeClient.UnitTests/     # Unit tests (142+ tests)
├── README.md                   # Main documentation
├── CHANGELOG.md               # Version history
└── CLAUDE.md                  # This file
```

## Key Technologies

- **.NET 9.0**: Target framework
- **Flurl/Flurl.Http**: HTTP client functionality
- **Newtonsoft.Json**: JSON serialization
- **WebSocket4Net**: WebSocket connections
- **xUnit + FakeItEasy**: Unit testing
- **Separate Sandbox**: Interactive testing via standalone CoinbaseAdvancedTradeClient-Sandbox project

## CI/CD Pipeline

### GitHub Actions Workflows

1. **Pull Request Workflow** (`pull-request.yml`)
   - Triggers: Pull request events
   - Actions: Build, test, create RC package
   - Version: Auto-increment patch + `-rc` suffix

2. **Release Workflow** (`release.yml`)
   - Triggers: GitHub release creation
   - Actions: Build, test, create stable package
   - Version: Clean semantic version from release tag

### Versioning Strategy
- **RC Packages**: `X.Y.Z-rc` (auto-generated on PRs)
  - Package Version: `X.Y.Z-rc`
  - Assembly Version: `X.Y.Z.1`
- **Release Packages**: `X.Y.Z` (from GitHub releases)
  - Package Version: `X.Y.Z`
  - Assembly Version: `X.Y.Z.0`
- **Current Version**: 0.2.0 (includes .NET 9 upgrade and GitHub Actions migration)

### Assembly Versioning
Assembly versions are automatically synchronized with package versions using the `pack-with-version.sh` script, ensuring `Assembly.GetName().Version` returns the correct version in consuming applications.

## Common Development Tasks

### Adding New API Endpoints
1. Add interface in `Interfaces/Endpoints/`
2. Implement in `Endpoints/`
3. Add models in `Models/Api/`
4. Create unit tests in `UnitTests/Endpoints/`
5. Test manually using the separate CoinbaseAdvancedTradeClient-Sandbox project

### Updating Dependencies
1. Check compatibility with .NET 9
2. Update package references in `.csproj` files
3. Run tests to verify compatibility
4. Update documentation if needed

### Testing
- **Unit Tests**: 142+ tests covering all functionality
- **Integration Tests**: Separate CoinbaseAdvancedTradeClient-Sandbox project for manual testing
- **CI Tests**: Automated testing on every PR

## Authentication

The client uses API key authentication with HMAC-SHA256 signatures:
- API Key: Identifies the client
- API Secret: Used for signature generation
- Signatures include timestamp, method, path, and body

## Known Issues

- Build produces warnings for nullable reference types (expected with .NET 9)
- All unit tests now passing (142 tests)

## Getting Help

- Check existing unit tests for usage examples
- Use the separate CoinbaseAdvancedTradeClient-Sandbox project for interactive testing
- Review the comprehensive README.md for API documentation
- Check CHANGELOG.md for recent changes and migration notes