#!/bin/bash
set -e

# Script to build solution with proper assembly versioning
# Usage: ./build-with-version.sh <solution_path> <package_version>
# 
# Assembly Version Logic:
# - Release versions (1.2.3): Assembly version = 1.2.3.0
# - RC versions (1.2.3-rc): Assembly version = 1.2.3.1

SOLUTION_PATH=${1:-}
PACKAGE_VERSION=${2:-}

if [ -z "$SOLUTION_PATH" ]; then
    echo "Error: Solution path is required as first argument"
    exit 1
fi

if [ -z "$PACKAGE_VERSION" ]; then
    echo "Error: Package version is required as second argument"
    exit 1
fi

echo "Building solution with version: ${PACKAGE_VERSION}"

# Extract base version (remove -rc suffix if present)
BASE_VERSION=${PACKAGE_VERSION%-rc}
echo "Base version: ${BASE_VERSION}"

# Determine assembly version based on whether this is RC or release
if [[ "$PACKAGE_VERSION" == *"-rc" ]]; then
    ASSEMBLY_VERSION="${BASE_VERSION}.1"
    echo "RC build - Assembly version: ${ASSEMBLY_VERSION}"
else
    ASSEMBLY_VERSION="${BASE_VERSION}.0"
    echo "Release build - Assembly version: ${ASSEMBLY_VERSION}"
fi

# Build with all version properties set
echo "Executing dotnet build..."
dotnet build "$SOLUTION_PATH" \
    --configuration Release \
    --no-restore \
    -p:AssemblyVersion="$ASSEMBLY_VERSION" \
    -p:FileVersion="$ASSEMBLY_VERSION" \
    -p:AssemblyInformationalVersion="$PACKAGE_VERSION"

echo "Build completed successfully with:"
echo "  Package Version: $PACKAGE_VERSION"
echo "  Assembly Version: $ASSEMBLY_VERSION"