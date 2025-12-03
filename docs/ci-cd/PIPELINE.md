# CI/CD Pipeline Documentation

This document describes the CI/CD pipeline for the HealthPlan Suite project.

## Pipeline Overview

The CI/CD pipeline is configured in `.github/workflows/ci.yml` and includes the following stages:

### 1. Build and Test

- **Build**: Compiles the solution in Release mode
- **Test**: Runs all unit and integration tests
- **Coverage**: Generates code coverage reports

### 2. Code Quality

- **Formatting**: Validates code formatting standards
- **SOLID Principles**: Checks adherence to SOLID principles
- **Static Analysis**: Runs Roslyn analyzers

### 3. Security Scan

- **Vulnerable Packages**: Checks for known vulnerabilities in dependencies
- **Secret Scanning**: Validates no secrets are committed

## Running Locally

To simulate the CI/CD pipeline locally:

```bash
# Build
dotnet build Solution/HealthPlanSuite.sln --configuration Release

# Test
dotnet test Solution/HealthPlanSuite.sln --configuration Release

# Format check
dotnet format --verify-no-changes

# Security check
dotnet list package --vulnerable
```

## Pipeline Configuration

See the workflow file at `.github/workflows/ci.yml` for complete configuration details.

## Related Documentation

- [Development Guide](../guides/DEVELOPMENT.md)
- [Testing Guide](../guides/TESTING.md)
- [CI Status](.github/ci-status.md)
