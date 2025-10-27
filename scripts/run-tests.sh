#!/bin/bash

# Authentication Tests Runner
# This script facilitates the execution of Authentication project tests

set -e

echo "🧪 Authentication Tests Runner"
echo "================================"

# Function to show help
show_help() {
    echo "Usage: $0 [option]"
    echo ""
    echo "Options:"
    echo "  all           Run all tests (default)"
    echo "  integration   Run only integration tests"
    echo "  unit          Run only unit tests"
    echo "  coverage      Run tests with code coverage"
    echo "  watch         Run tests in watch mode"
    echo "  verbose       Run tests with detailed output"
    echo "  clean         Clean and rebuild before running"
    echo "  help          Show this help"
    echo ""
    echo "Examples:"
    echo "  $0                # Run all tests"
    echo "  $0 integration    # Run only integration tests"
    echo "  $0 coverage       # Run with code coverage"
}

# Navigate to the project root directory
cd "$(dirname "$0")/.."

# Check if test project exists
if [ ! -f "Src/Authentication.Tests/Authentication.Tests.csproj" ]; then
    echo "❌ Test project not found!"
    echo "Check if you are in the Authentication project root."
    exit 1
fi

# Restore dependencies if necessary
if [ ! -d "Src/Authentication.Tests/bin" ]; then
    echo "📦 Restoring dependencies..."
    dotnet restore Solution/Authentication.sln
fi

# Function to run tests
run_tests() {
    local test_command="$1"
    echo "🏃 Running: $test_command"
    echo ""
    
    if eval "$test_command"; then
        echo ""
        echo "✅ Tests executed successfully!"
    else
        echo ""
        echo "❌ Some tests failed!"
        exit 1
    fi
}

# Process arguments
case "${1:-all}" in
    "all")
        echo "🎯 Running all tests..."
        run_tests "dotnet test Src/Authentication.Tests/Authentication.Tests.csproj"
        ;;
    
    "integration")
        echo "🔗 Running integration tests..."
        run_tests "dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --filter \"FullyQualifiedName~Integration\""
        ;;
    
    "unit")
        echo "🧩 Running unit tests..."
        run_tests "dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --filter \"FullyQualifiedName~Unit\""
        ;;
    
    "coverage")
        echo "📊 Running tests with code coverage..."
        run_tests "dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --collect:\"XPlat Code Coverage\""
        echo ""
        echo "📈 Coverage report generated in: TestResults/"
        ;;
    
    "watch")
        echo "👀 Running tests in watch mode..."
        echo "Press Ctrl+C to stop"
        dotnet watch test Src/Authentication.Tests/Authentication.Tests.csproj
        ;;
    
    "verbose")
        echo "📝 Running tests with detailed output..."
        run_tests "dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --verbosity normal"
        ;;
    
    "clean")
        echo "🧹 Cleaning and rebuilding..."
        dotnet clean Solution/Authentication.sln
        dotnet build Solution/Authentication.sln
        echo "🎯 Running all tests..."
        run_tests "dotnet test Src/Authentication.Tests/Authentication.Tests.csproj"
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