#!/bin/bash
set -e

# Script to publish NuGet packages to GitHub Packages
# Usage: ./publish-package.sh [artifacts-path]
# Environment variables required: GITHUB_TOKEN, GITHUB_REPOSITORY_OWNER

ARTIFACTS_PATH=${1:-"./artifacts"}

if [ -z "$GITHUB_TOKEN" ]; then
    echo "Error: GITHUB_TOKEN environment variable is required"
    exit 1
fi

if [ -z "$GITHUB_REPOSITORY_OWNER" ]; then
    echo "Error: GITHUB_REPOSITORY_OWNER environment variable is required"
    exit 1
fi

if [ ! -d "$ARTIFACTS_PATH" ]; then
    echo "Error: Artifacts directory '$ARTIFACTS_PATH' does not exist"
    exit 1
fi

PACKAGE_COUNT=$(find "$ARTIFACTS_PATH" -name "*.nupkg" | wc -l)
if [ "$PACKAGE_COUNT" -eq 0 ]; then
    echo "Error: No .nupkg files found in '$ARTIFACTS_PATH'"
    exit 1
fi

echo "Publishing $PACKAGE_COUNT package(s) to GitHub Packages..."

dotnet nuget push ${ARTIFACTS_PATH}/*.nupkg \
  --source https://nuget.pkg.github.com/${GITHUB_REPOSITORY_OWNER}/index.json \
  --api-key ${GITHUB_TOKEN} \
  --skip-duplicate

echo "Packages published successfully to GitHub Packages"