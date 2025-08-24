# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Removed
- **BREAKING**: Removed embedded SandboxTests project from solution
- Interactive Blazor test application moved to separate [CoinbaseAdvancedTradeClient-Sandbox](https://github.com/PearlAegis/CoinbaseAdvancedTradeClient-Sandbox) repository

### Changed
- Streamlined solution to focus on core library and unit tests only
- Updated documentation to reference separate sandbox project
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