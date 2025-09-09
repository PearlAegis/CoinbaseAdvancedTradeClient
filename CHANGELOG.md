# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.4.0] - 2025-09-08

### Added
- JWT Bearer Authentication with ES256 signatures replacing legacy HMAC
- Unified `CoinbaseClientConfig` for both API and WebSocket clients
- Dependency Injection support with `AddCoinbaseAdvancedTradeClient()` extension
- Enhanced security with JWT tokens (2-minute expiration and replay protection)

### Changed
- Migrated from HMAC-SHA256 to JWT Bearer tokens with ECDSA ES256 signatures
- Configuration properties renamed: `ApiKey`/`ApiSecret` → `KeyName`/`KeySecret`
- Requires Coinbase Cloud API keys (EC P-256 private keys in PEM format)
- Constructor parameters now use `IOptions<CoinbaseClientConfig>`

### Removed
- Legacy `ApiKeyAuthenticator` and CB-ACCESS-* header authentication

## [0.3.0] - 2025-08-25

### Added
- **Assembly Version Synchronization**: Assembly versions now match package versions for better version tracking
- New `pack-with-version.sh` script for automated assembly versioning in CI/CD
- Enhanced version documentation in README and CLAUDE.md

### Changed
- **Assembly Versioning Scheme**: 
  - Release packages (`X.Y.Z`): Assembly version = `X.Y.Z.0`
  - RC packages (`X.Y.Z-rc`): Assembly version = `X.Y.Z.1`
- CI/CD workflows now set `AssemblyVersion`, `FileVersion`, and `AssemblyInformationalVersion` properties
- Cleaner GitHub Actions workflows with versioning logic moved to reusable script

### Removed
- **BREAKING**: Removed embedded SandboxTests project from solution
- Interactive Blazor test application moved to separate [CoinbaseAdvancedTradeClient-Sandbox](https://github.com/PearlAegis/CoinbaseAdvancedTradeClient-Sandbox) repository

### Fixed
- `Assembly.GetName().Version` now returns correct package version in consuming applications
- All 142 unit tests now passing (previously had 2 failing tests)

## [0.2.0] - 2025-08-10

### Added
- GitHub Actions workflows for automated CI/CD
- Comprehensive README.md documentation
- Build status badges
- Semantic versioning with automated RC package generation
- GitHub Packages publishing integration

### Changed
- Migrated from Azure DevOps to GitHub Actions
- Upgraded from .NET 7.0 to .NET 9.0
- Updated NuGet dependencies to latest compatible versions:
  - Newtonsoft.Json: 13.0.3
  - FakeItEasy: 8.3.0 
  - Microsoft.NET.Test.Sdk: 17.11.1
  - Radzen.Blazor: 5.5.5
- Updated copyright year to 2025

### Removed
- Obsolete `azure-pipelines.yml` configuration

### Fixed
- Build compatibility with .NET 9.0
- GitHub Actions permissions for package publishing

## [0.1.0] - Previous Release

### Added
- Initial Coinbase Advanced Trade API client implementation
- REST API support for all endpoints (accounts, orders, products, transaction summaries)
- WebSocket client for real-time market data
- API key authentication with HMAC signature generation
- Comprehensive unit test suite
- Interactive Blazor test application
- Support for all order types (market, limit, stop-limit)
- Error handling and response models
- Type-safe models for all API requests/responses

### Features
- **Complete API Coverage**: All Coinbase Advanced Trade endpoints
- **WebSocket Support**: Real-time ticker, level2, market trades, and user data
- **Authentication**: Secure API key-based authentication
- **Testing**: 142+ unit tests covering all functionality
- **Interactive Testing**: Separate Blazor Server project for manual API testing
- **Type Safety**: Strongly-typed models with nullable reference types