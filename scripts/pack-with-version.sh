#!/bin/bash
set -e

# Script to pack NuGet package with proper assembly versioning
# Usage: ./pack-with-version.sh <project_path> <package_version> [output_dir]
# 
# Assembly Version Logic:
# - Release versions (1.2.3): Assembly version = 1.2.3.0
# - RC versions (1.2.3-rc): Assembly version = 1.2.3.1

PROJECT_PATH=${1:-}
PACKAGE_VERSION=${2:-}
OUTPUT_DIR=${3:-artifacts}

if [ -z "$PROJECT_PATH" ]; then
    echo "Error: Project path is required as first argument"
    exit 1
fi

if [ -z "$PACKAGE_VERSION" ]; then
    echo "Error: Package version is required as second argument"
    exit 1
fi

echo "Packing NuGet package with version: ${PACKAGE_VERSION}"

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

# Create output directory if it doesn't exist
mkdir -p "$OUTPUT_DIR"

# Pack with package version (assembly version already set during build)
echo "Executing dotnet pack..."
dotnet pack "$PROJECT_PATH" \
    --configuration Release \
    --no-build \
    --output "$OUTPUT_DIR" \
    -p:PackageVersion="$PACKAGE_VERSION"

echo "Package created successfully with:"
echo "  Package Version: $PACKAGE_VERSION"
echo "  Assembly Version: $ASSEMBLY_VERSION"
echo "  Output Directory: $OUTPUT_DIR"