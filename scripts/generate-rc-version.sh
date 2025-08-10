#!/bin/bash
set -e

# Script to generate release candidate version for GitHub Actions
# This script increments the patch version and adds '-rc' suffix

echo "Generating release candidate version..."

# Get the latest tag (including lightweight tags)
LATEST_TAG=$(git tag -l --sort=-version:refname | head -n 1)
echo "Latest tag: ${LATEST_TAG}"

# Remove 'v' prefix if present
VERSION=${LATEST_TAG#v}
echo "Raw version: ${VERSION}"

# Remove -rc suffix if present to get base version
BASE_VERSION=${VERSION%-rc}
echo "Base version: ${BASE_VERSION}"

# Split version into components
IFS='.' read -ra VERSION_PARTS <<< "$BASE_VERSION"
MAJOR=${VERSION_PARTS[0]}
MINOR=${VERSION_PARTS[1]}
PATCH=${VERSION_PARTS[2]}

# Increment patch version
PATCH=$((PATCH + 1))

# Generate RC version
RC_VERSION="${MAJOR}.${MINOR}.${PATCH}-rc"
echo "version=${RC_VERSION}" >> $GITHUB_OUTPUT
echo "Generated RC version: ${RC_VERSION}"