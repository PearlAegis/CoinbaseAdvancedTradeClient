#!/bin/bash
set -e

# Script to extract version from GitHub release tag
# This script processes the release tag name to get clean semantic version

echo "Extracting version from release tag..."

# Extract version from the release tag
TAG_NAME="${1:-$GITHUB_REF_NAME}"
echo "Tag name: ${TAG_NAME}"

# Remove 'v' prefix if present
VERSION=${TAG_NAME#v}
echo "version=${VERSION}" >> $GITHUB_OUTPUT
echo "Release version: ${VERSION}"