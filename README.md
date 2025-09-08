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
- Use `./scripts/test.sh` (Linux) or `./scripts/test.bat` (Windows) as the main test command
- Ensure .NET 9.0 SDK is installed in the CI environment
- Configure artifact collection for test results in `TestResults/` directory

## 🚀 Quick Usage (Development)

> **💡 Development Focus**: All examples prioritize development configurations and practices to facilitate the developer experience.

### 1. Database Configuration (Development)

Update the connection string in `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CleanTemplateDB;Uid=your_user;Pwd=your_password;"
  },
  "JwtSettings": {
    "Issuer": "CleanTemplate",
    "Audience": "CleanTemplateClients",
    "SecretKey": "your-secret-key-minimum-32-characters",
    "ExpirationMinutes": 60
  }
}
```

### 2. Initializing the Database

```bash
cd Src/CleanTemplate.API
dotnet ef database update --context ApiContextDevelopment
```

### 3. JWT Configuration for Development

```csharp
// Program.cs - Development-specific configuration
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Development-specific configuration
        if (builder.Environment.IsDevelopment())
        {
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.Logging.SetMinimumLevel(LogLevel.Debug);
        }
        
        // JWT configuration for development
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
                };
                
                #if DEBUG
                // Debug-specific configurations
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine($"Token validated for: {context.Principal?.Identity?.Name}");
                        return Task.CompletedTask;
                    }
                };
                #endif
            });
        
        var app = builder.Build();
        
        // Development-specific middleware
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.Run();
    }
}
```

### 4. Installation Verification

- 🌐 **API Endpoint**: https://localhost:7001
- 📖 **API Documentation**: https://localhost:7001 (automatically redirects to Swagger UI)
- ❤️ **Health Check**: https://localhost:7001/health

### 🌍 Language Support

The API supports internationalization with English and Portuguese languages:

- **English**: Access with `?culture=en` (e.g., `https://localhost:7001/?culture=en`)
- **Portuguese**: Access with `?culture=pt-BR` (e.g., `https://localhost:7001/?culture=pt-BR`)

The selected language is automatically saved as a cookie and will persist for all subsequent requests, including Swagger UI documentation. This ensures that both the API responses and Swagger documentation appear in your preferred language.

## 📚 Main Components

### 🏛️ API Layer

- **`CleanEntityController`**: Main controller for CRUD operations
- **`Middleware`**: Custom middleware for logging and request handling

### 🔐 Services Layer

- **`ICleanEntityService`**: Interface for business services
- **`CleanEntityService`**: Business services implementation

### 🗃️ Repository Layer

- **`ICleanEntityRepository`**: Interface for data access
- **`CleanEntityRepository`**: Implementation with CRUD operations

### 🛠️ Utilities

- **`Entity`**: Base entity class from Foundation.Base
- **`Repository<T>`**: Generic repository pattern implementation

## 🔐 Security

The service includes robust security features:

```csharp
using Foundation.Base.Util;

// Password hashing with Argon2 (if implemented)
string passwordHash = StringHelper.ComputeArgon2Hash("myPassword123");

// Password verification
bool isValidPassword = StringHelper.VerifyArgon2Hash("myPassword123", passwordHash);
```

## ✅ Validation

Native integration with FluentValidation:

```csharp
using FluentValidation;

public class CleanEntityRequestValidator : AbstractValidator<CleanEntityRequest>
{
    public CleanEntityRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description must not exceed 500 characters");
    }
}

// In the controller
var validationResult = await ValidationHelper.ValidateEntityAsync(request, serviceProvider, this);
if (validationResult != null) return validationResult;
```

## 🌐 API Endpoints

### Main CleanEntity Endpoints

| Method | Endpoint | Description | Authentication |
|--------|----------|-------------|----------------|
| **GET** | `/api/CleanEntity` | 📋 List all entities | ❌ |
| **GET** | `/api/CleanEntity/{id}` | 🔍 Get entity by ID | ❌ |
| **POST** | `/api/CleanEntity` | ➕ Create new entity | ❌ |
| **PUT** | `/api/CleanEntity/{id}` | ✏️ Update entity | ❌ |
| **DELETE** | `/api/CleanEntity/{id}` | ❌ Delete entity | ❌ |
| **GET** | `/health` | ❤️ Health check | ❌ |

### 📖 Detailed Documentation

### 🚀 **For Developers (START HERE)**
- **[Quick Start Guide](docs/QUICK_START.md)** - **5-minute setup from zero to running API**
- **[Development Guide](docs/DEVELOPMENT.md)** - **Complete development workflow and best practices**

### 📚 **Technical Documentation**
- [Service Architecture](docs/ARCHITECTURE.md) - Clean Architecture patterns and design decisions
- [Complete API Reference](docs/API.md) - Detailed endpoint documentation
- [Deployment Guide](docs/DEPLOYMENT.md) - Production deployment strategies
- [Practical Examples](docs/EXAMPLES.md) - Real-world integration examples

### 🛠️ **Development Resources**
- [Code Documentation Standards](docs/CODE_DOCUMENTATION.md) - XML comments and inline documentation guidelines
- [Troubleshooting Guide](docs/TROUBLESHOOTING.md) - Common issues and solutions
- [Testing Guide](docs/TESTING.md) - Unit and integration testing strategies

> **🎯 Important**: New to the project? Start with the [Quick Start Guide](docs/QUICK_START.md) for immediate results, then explore the [Development Guide](docs/DEVELOPMENT.md) for comprehensive understanding!

## 🤝 Contributing

Contributions are welcome! Please read the [contribution guide](CONTRIBUTING.md) before submitting pull requests.

### Contribution Environment Setup

```bash
# Clone the repository
git clone https://github.com/maiconcardozo/CleanTemplateRepository.git
cd CleanTemplateRepository

# Install dependencies (requires .NET 9.0 SDK)
dotnet restore Solution/CleanTemplate.sln

# Build the project
dotnet build Solution/CleanTemplate.sln --configuration Debug

# Run in development mode
dotnet run --project Src/CleanTemplate.API

# Run tests using convenience scripts (recommended)
scripts/build.sh verify          # Linux/Mac - runs build and tests
scripts/build.bat verify         # Windows - runs build and tests
```

## 🧪 Running Tests

The project includes a comprehensive test suite following TDD architecture with convenient scripts for easy execution:

### 🎯 Quick Test Execution (Recommended)

**Single Command Test Execution:**
```bash
# Universal test command (works in any environment)
scripts/build.sh verify         # Linux/Mac
scripts/build.bat verify        # Windows

# Alternative: Direct dotnet command
dotnet test Solution/CleanTemplate.sln
```

**Using convenience scripts (advanced options):**
```bash
# Using convenience scripts (easiest method)
scripts/run-tests.sh all          # Linux/Mac - runs all tests
scripts/run-tests.bat all         # Windows - runs all tests

# Available script options:
scripts/run-tests.sh unit         # Run only unit tests
scripts/run-tests.sh integration  # Run only integration tests  
scripts/run-tests.sh coverage     # Run with code coverage
scripts/run-tests.sh verbose      # Run with detailed output
scripts/run-tests.sh watch        # Run in watch mode (continuous)
scripts/run-tests.sh clean        # Clean, rebuild, then test
scripts/run-tests.sh help         # Show all available options
```

### 🔧 Manual Test Commands

```bash
# Run all tests
dotnet test Solution/CleanTemplate.sln

# Run tests with verbosity
dotnet test Solution/CleanTemplate.sln --verbosity normal

# Run integration tests only
dotnet test --filter "FullyQualifiedName~Integration"

# Run unit tests only
dotnet test --filter "FullyQualifiedName~Unit"

# Run with code coverage
dotnet test Solution/CleanTemplate.sln --collect:"XPlat Code Coverage"
```

### 📊 Test Status & Coverage

- ✅ **Total Tests**: Comprehensive test coverage
- ✅ **Coverage Target**: > 80% code coverage  
- ✅ **Test Framework**: xUnit with FluentAssertions
- ✅ **Database**: In-memory database for isolation
- ✅ **CI/CD Integration**: Automated pipeline with GitHub Actions

## 🔄 Continuous Integration

The project includes a comprehensive CI/CD pipeline using GitHub Actions that automatically:

### 🚀 Automated Pipeline Features

**On every push and pull request:**
- ✅ **Build Verification**: Compiles the project in Release mode with .NET 9.0
- ✅ **Test Execution**: Runs all tests and generates reports
- ✅ **Code Quality**: Enforces coding standards and SOLID principles
- ✅ **Else Statement Prevention**: Blocks if/else patterns in favor of conditional expressions
- ✅ **SOLID Principles Enforcement**: Validates adherence to clean code principles
- ✅ **Security Scanning**: Checks for vulnerable and deprecated packages
- ✅ **Coverage Reports**: Generates code coverage analysis
- ✅ **Artifact Storage**: Preserves test results and coverage data

### 📊 Code Quality Standards

The project enforces strict code quality rules that **will fail the build** if violated:

#### 🚫 Blocked Patterns (Build will fail):
```csharp
// ❌ NOT ALLOWED - if/else return statements
if (condition)
    return "true";
else
    return "false";

// ❌ NOT ALLOWED - if/else assignments  
if (condition)
    result = "true";
else
    result = "false";
```

#### ✅ Required Patterns:
```csharp
// ✅ REQUIRED - Use conditional expressions instead
return condition ? "true" : "false";

// ✅ REQUIRED - Use conditional expressions for assignments
result = condition ? "true" : "false";
```

## 📄 License

This project is licensed under the [MIT License](LICENSE).

## 👨‍💻 Author

**Maicon Cardozo**
- GitHub: [@maiconcardozo](https://github.com/maiconcardozo)

## 📞 Support

For questions, suggestions, or to report issues:
- Open an [issue](https://github.com/maiconcardozo/CleanTemplateRepository/issues)
- Contact through GitHub

---

⭐ If this project was useful to you, please consider giving it a star!