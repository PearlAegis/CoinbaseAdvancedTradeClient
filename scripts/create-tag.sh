#!/bin/bash
set -e

# Script to create a Git tag using GitHub API
# Usage: ./create-tag.sh <version> <sha>
# Environment variables required: GITHUB_TOKEN, GITHUB_REPOSITORY

VERSION=${1:-}
SHA=${2:-$GITHUB_SHA}

if [ -z "$VERSION" ]; then
    echo "Error: Version is required as first argument"
    exit 1
fi

if [ -z "$SHA" ]; then
    echo "Error: SHA is required (either as second argument or GITHUB_SHA env var)"
    exit 1
fi

if [ -z "$GITHUB_TOKEN" ]; then
    echo "Error: GITHUB_TOKEN environment variable is required"
    exit 1
fi

if [ -z "$GITHUB_REPOSITORY" ]; then
    echo "Error: GITHUB_REPOSITORY environment variable is required"
    exit 1
fi

echo "Creating tag v${VERSION} for commit ${SHA}..."

gh api --method POST /repos/${GITHUB_REPOSITORY}/git/refs \
  --field ref="refs/tags/v${VERSION}" \
  --field sha="${SHA}"

echo "Tag v${VERSION} created successfully"