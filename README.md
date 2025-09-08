# Clean Template Repository - .NET Clean Architecture Template

[![CI/CD Pipeline](https://github.com/maiconcardozo/CleanTemplateRepository/actions/workflows/ci.yml/badge.svg)](https://github.com/maiconcardozo/CleanTemplateRepository/actions/workflows/ci.yml)
[![.NET 9.0](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-9.0.7-blue.svg)](https://docs.microsoft.com/en-us/ef/core/)

## 📋 Overview

**Clean Template Repository** is a pre-configured .NET template that provides a complete Clean Architecture foundation for building modern web applications. This template follows Domain-Driven Design (DDD) principles and includes a fully functional example entity (`CleanEntity`) demonstrating all layers and patterns.

### 🔐 Key Features

- **Clean Architecture**: Well-organized layers with proper separation of concerns
- **Example Entity**: Complete `CleanEntity` implementation showing all patterns
- **Repository Pattern**: Generic repository with Entity Framework implementation
- **Unit of Work**: Transaction management and consistency
- **Service Layer**: Business logic separation with proper error handling
- **RESTful API**: Complete CRUD endpoints with proper HTTP status codes
- **AutoMapper Integration**: DTO mapping configuration
- **Entity Framework**: Database configuration and migrations
- **Dependency Injection**: Proper IoC container setup
- **Unit Testing**: Comprehensive test coverage with FluentAssertions
- **JWT Infrastructure**: Token generation and validation ready for implementation

## 🏗️ Architecture

The template is organized in well-defined layers following Clean Architecture principles:

```
CleanTemplateRepository/
├── Src/
│   ├── Authentication.API/           # API Layer
│   │   ├── Controllers/             # API Controllers
│   │   │   └── CleanEntityController.cs  # Example CRUD controller
│   │   ├── Middleware/              # Custom middleware
│   │   ├── Swagger/                 # API documentation
│   │   └── Data/                    # Database contexts
│   │
│   └── Authentication.Login/        # Domain & Business Logic
│       ├── Domain/                  # Domain entities
│       │   ├── Implementation/      # Concrete implementations
│       │   │   ├── CleanEntity.cs   # Example entity
│       │   │   ├── Token.cs         # JWT token (optional)
│       │   │   └── JwtSettings.cs   # JWT configuration (optional)
│       │   └── Interface/          # Domain interfaces
│       ├── Services/               # Business services
│       │   ├── Implementation/     # Service implementations
│       │   │   └── CleanEntityService.cs
│       │   └── Interface/         # Service contracts
│       │       └── ICleanEntityService.cs
│       ├── Repository/             # Data access layer
│       │   ├── Implementation/     # Repository implementations
│       │   │   └── CleanEntityRepository.cs
│       │   └── Interface/         # Repository contracts
│       │       └── ICleanEntityRepository.cs
│       ├── DTO/                   # Data transfer objects
│       │   ├── CleanEntityPayLoadDTO.cs
│       │   └── CleanEntityResponseDTO.cs
│       ├── Infrastructure/        # Entity configurations
│       │   ├── Implementation/    # EF Core mappings
│       │   └── Interface/        # Context contracts
│       └── UnitOfWork/           # Unit of Work pattern
│           ├── Implementation/    # UoW implementation
│           └── Interface/        # UoW contract
│
└── Foundation.Base/                 # Shared base library
│   ├── Domain/                      # Base domain entities
│   ├── Repository/                  # Generic repository patterns
│   ├── UnitOfWork/                  # Transaction management
│   └── Util/                        # Common utilities
│
├── docs/                            # Documentation
│   ├── status/                      # Project status reports
│   └── *.md                         # Technical documentation
│
├── scripts/                         # Build & test scripts
│   ├── build.sh / build.bat         # Cross-platform build scripts
│   ├── run-tests.sh / run-tests.bat # Test execution scripts
│   └── README.md                    # Scripts documentation
│
└── Solution/                        # Solution configuration
```

### 📁 Repository Organization

The repository follows a clean, organized structure:

- **`/Src/`** - Source code (API, business logic, tests)
- **`/docs/`** - All documentation including status reports
- **`/scripts/`** - Build, test, and utility scripts
- **Root level** - Essential configuration files only

## 🔧 Technologies Used

- **.NET 9.0** - Main framework (REQUIRED - never downgrade to 8.0)
- **ASP.NET Core 9.0.7** - RESTful API framework
- **Entity Framework Core 9.0.7** - ORM for data access
- **JWT Bearer 8.14.0** - Token-based authentication
- **FluentValidation 12.0.0** - Input validation
- **Argon2 1.3.1** - Secure password hashing
- **MySQL/MariaDB** - Database support (MySqlConnector 2.4.0)
- **Swagger/OpenAPI 6.8.1** - API documentation
- **AutoMapper 15.0.1** - Object mapping

## 🚀 Development (Quick Start)

### Development Environment Setup

```bash
# 1. Clone the repository
git clone https://github.com/maiconcardozo/CleanTemplateRepository.git
cd CleanTemplateRepository

# 2. Install .NET 9.0 SDK (REQUIRED - see requirements section below)
# Download from: https://dotnet.microsoft.com/download/dotnet/9.0

# 3. Verify .NET 9.0 installation
dotnet --version
# Should output: 9.0.x

# 4. Restore dependencies
dotnet restore Solution/Authentication.sln

# 5. Build in Debug mode (development)
dotnet build Solution/Authentication.sln --configuration Debug

# 6. Run the API
cd Src/Authentication.API
dotnet run --configuration Debug
```

### ✅ Setup Verification

After following the setup steps, verify everything works:

```bash
# 1. Use convenience build script (recommended)
scripts/build.sh verify         # Linux/Mac - complete verification
scripts/build.bat verify        # Windows - complete verification

# 2. Single command test execution (quickest method)
scripts/test.sh                 # Linux/Mac - build and test everything
scripts/test.bat                # Windows - build and test everything

# 3. Manual verification steps:
# Verify compilation
dotnet build Solution/Authentication.sln --configuration Release
# Should complete without errors

# Run all tests  
scripts/run-tests.sh all        # Linux/Mac
scripts/run-tests.bat all       # Windows
# Should show test results

# Start the application
cd Src/Authentication.API
dotnet run
# Should start on https://localhost:7001
```

### 🛠️ Available Helper Scripts

The project includes convenience scripts to simplify development:

**Build Scripts:**
```bash
scripts/build.sh debug         # Compile in Debug mode (default)
scripts/build.sh release       # Compile in Release mode  
scripts/build.sh clean         # Clean and rebuild
scripts/build.sh restore       # Restore dependencies only
scripts/build.sh verify        # Complete verification (build + tests)
scripts/build.sh help          # Show all options
```

**Test Scripts:**
```bash
scripts/run-tests.sh all       # Run all tests
scripts/run-tests.sh unit      # Run unit tests only
scripts/run-tests.sh integration  # Run integration tests only
scripts/run-tests.sh coverage  # Run with code coverage
scripts/run-tests.sh verbose   # Run with detailed output
scripts/run-tests.sh watch     # Run in watch mode
scripts/run-tests.sh clean     # Clean, rebuild, then test
```

*Note: Windows users should use `.bat` extensions instead of `.sh`*

### 🎯 Recommended Development Configuration

The project is optimized for local development with **Debug** as the default configuration:

```bash
# Development configuration active by default
export ASPNETCORE_ENVIRONMENT=Development
export DOTNET_ENVIRONMENT=Development

# Continuous build during development
dotnet watch run --configuration Debug
```

### 💻 Recommended IDEs
- **Visual Studio 2022** (17.14+) with .NET workload
- **Visual Studio Code** with C# Dev Kit extension
- **JetBrains Rider** 2024.1+

## ⚠️ .NET 9.0 Framework Requirements

**CRITICAL**: This project requires .NET 9.0 and must never be downgraded to .NET 8.0.

### Why .NET 9.0 is Required:
- **Performance Improvements**: Enhanced runtime performance and memory usage
- **Package Compatibility**: Latest versions of Entity Framework Core 9.0.7 and related packages
- **Security Updates**: Latest security patches and improvements
- **Modern Features**: Access to newest C# language features and framework improvements

### Installation:
1. **Download .NET 9.0 SDK**: https://dotnet.microsoft.com/download/dotnet/9.0
2. **Verify Installation**: `dotnet --version` should show 9.0.x
3. **Check Project**: `dotnet build` should complete without framework errors

### Framework Validation:
The project includes comprehensive protection against .NET version regression:

#### 🔒 Multi-Layer Protection System:
1. **global.json Enforcement**: Forces .NET 9.0 SDK usage and prevents accidental downgrade to 8.0
2. **Project File Validation**: All `.csproj` files strictly target `net9.0` framework
3. **CI/CD Protection**: Automated workflows fail if any .NET 8.0 references are detected
4. **Documentation Guards**: Clear warnings throughout the codebase about version requirements

#### 🚨 Automatic Regression Detection:
- **GitHub Actions Workflow**: `.github/workflows/dotnet-version-check.yml` scans all project files
- **Build-Time Validation**: The existing build workflow includes framework targeting validation
- **Pre-commit Protection**: The SDK enforcement in `global.json` prevents local builds with wrong versions

#### 🛡️ Why This Protection Exists:
- **Package Compatibility**: EF Core 9.0.7 and other dependencies require .NET 9.0
- **Performance**: .NET 9.0 runtime optimizations are essential for production
- **Security**: Latest security patches only available in .NET 9.0
- **Future-Proofing**: Prevents accidental downgrades during development or deployment

### 🔧 Troubleshooting .NET 9.0 Setup

#### Common Issues and Solutions:

**Issue**: `NETSDK1045: The current .NET SDK does not support targeting .NET 9.0`
```bash
# Solution: Install .NET 9.0 SDK
# 1. Download from: https://dotnet.microsoft.com/download/dotnet/9.0
# 2. Verify installation: dotnet --version
# 3. Should show: 9.0.x
```

**Issue**: `A compatible .NET SDK was not found`
```bash
# Solution: Check global.json configuration
cat global.json
# Should specify version: "9.0.0"

# Verify SDK installation
dotnet --list-sdks
# Should include: 9.0.x
```

**Issue**: Project won't build or restore
```bash
# Solution: Clean and rebuild
dotnet clean Solution/Authentication.sln
dotnet restore Solution/Authentication.sln
dotnet build Solution/Authentication.sln
```

**Issue**: Tests won't run
```bash
# Solution: Use convenience scripts (they handle dependencies)
scripts/run-tests.sh clean     # Clean and test
scripts/run-tests.sh verbose   # Detailed output for debugging
```

## 📦 Production Installation

### Prerequisites
- .NET 9.0 SDK or higher (REQUIRED - never use 8.0)
- MySQL 8.0+ or higher
- Entity Framework Core 9.0.7

### Cloning and Building Locally
```bash
git clone https://github.com/maiconcardozo/Authentication.git
cd Authentication
dotnet build Solution/Authentication.sln --configuration Release
```

### 🔍 Project Compilation Verification

To ensure the project compiles correctly and all tests pass:

```bash
# Method 1: Single command (recommended for CI/CD)
scripts/test.sh                           # Linux/Mac - complete build and test
scripts/test.bat                          # Windows - complete build and test

# Method 2: Use convenience script (recommended for development)
scripts/build.sh verify                   # Linux/Mac
scripts/build.bat verify                  # Windows

# Method 3: Manual verification steps
# 1. Full compilation check (Release mode)
dotnet clean Solution/Authentication.sln
dotnet restore Solution/Authentication.sln  
dotnet build Solution/Authentication.sln --configuration Release

# 2. Test execution verification
./run-tests.sh all                  # Linux/Mac
./run-tests.bat all                 # Windows

# 3. API startup verification
cd Src/Authentication.API
dotnet run --configuration Release  # Should start on https://localhost:7001

# 4. Access API documentation
# Open browser: https://localhost:7001
# Should display Swagger UI with API documentation
```

#### Expected Results:
- ✅ **Build**: Should complete without errors
- ✅ **Tests**: Should run and show test results summary
- ✅ **API**: Should start and be accessible at https://localhost:7001
- ✅ **Documentation**: Swagger UI should be available with complete API documentation

### 🚀 CI/CD Integration

The project includes automated CI/CD pipeline support:

**For GitHub repositories:**
- Pipeline automatically runs on push/PR
- Builds with .NET 9.0 in Ubuntu environment
- Executes all tests and generates reports
- Provides code coverage and security scanning

**For other CI systems:**
- Use `./test.sh` (Linux) or `./test.bat` (Windows) as the main test command
- Ensure .NET 9.0 SDK is installed in the CI environment
- Configure artifact collection for test results in `TestResults/` directory