#!/bin/bash

# Authentication Build Script
# This script facilitates the compilation of the Authentication project

set -e

echo "🏗️ Authentication Build Script"
echo "=============================="

# Function to show help
show_help() {
    echo "Usage: $0 [option]"
    echo ""
    echo "Options:"
    echo "  debug         Compile in Debug mode (default)"
    echo "  release       Compile in Release mode"
    echo "  clean         Clean and rebuild"
    echo "  restore       Only restore dependencies"
    echo "  verify        Verify compilation and tests"
    echo "  help          Show this help"
    echo ""
    echo "Examples:"
    echo "  $0              # Compile in Debug mode"
    echo "  $0 release      # Compile in Release mode"
    echo "  $0 verify       # Verify everything works"
}

# Navigate to the project root directory
cd "$(dirname "$0")/.."

# Check if solution file exists
if [ ! -f "Solution/Authentication.sln" ]; then
    echo "❌ Solution file not found!"
    echo "Check if you are in the Authentication project root."
    exit 1
fi

# Check .NET 9.0
echo "🔍 Checking .NET version..."
DOTNET_VERSION=$(dotnet --version 2>/dev/null || echo "not found")
if [[ ! "$DOTNET_VERSION" =~ ^9\. ]]; then
    echo "❌ .NET 9.0 SDK not found!"
    echo "Current version: $DOTNET_VERSION"
    echo "Install .NET 9.0 SDK from: https://dotnet.microsoft.com/download/dotnet/9.0"
    exit 1
fi
echo "✅ .NET version: $DOTNET_VERSION"

# Function to execute build
run_build() {
    local configuration="$1"
    echo "🏃 Compiling in $configuration mode..."
    echo ""
    
    if dotnet build Solution/Authentication.sln --configuration "$configuration"; then
        echo ""
        echo "✅ Compilation completed successfully!"
    else
        echo ""
        echo "❌ Compilation failed!"
        exit 1
    fi
}

# Process arguments
case "${1:-debug}" in
    "debug")
        echo "🛠️ Restoring dependencies..."
        dotnet restore Solution/Authentication.sln
        run_build "Debug"
        ;;
    
    "release")
        echo "🛠️ Restoring dependencies..."
        dotnet restore Solution/Authentication.sln
        run_build "Release"
        ;;
    
    "clean")
        echo "🧹 Cleaning project..."
        dotnet clean Solution/Authentication.sln
        echo "🛠️ Restoring dependencies..."
        dotnet restore Solution/Authentication.sln
        run_build "Debug"
        ;;
    
    "restore")
        echo "📦 Restoring dependencies..."
        if dotnet restore Solution/Authentication.sln; then
            echo "✅ Dependencies restored successfully!"
        else
            echo "❌ Failed to restore dependencies!"
            exit 1
        fi
        ;;
    
    "verify")
        echo "🔍 Full project verification..."
        echo ""
        
        # Restore
        echo "📦 Restoring dependencies..."
        dotnet restore Solution/Authentication.sln
        
        # Compile Release
        run_build "Release"
        
        # Run tests
        echo ""
        echo "🧪 Running tests..."
        scripts/run-tests.sh all
        
        echo ""
        echo "🎉 Full verification successful!"
        echo "✅ Project compiles correctly"
        echo "✅ All tests passed"
        ;;
    
    "help"|"-h"|"--help")
        show_help
        ;;
    
    *)
        echo "❌ Invalid option: $1"
        echo ""
        show_help
        exit 1
        ;;
esac

echo ""
echo "🎉 Script executed successfully!"