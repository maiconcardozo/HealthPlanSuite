# HealthPlan Suite - .NET Health Plan Quote Management System

[![CI/CD Pipeline](https://github.com/maiconcardozo/HealthPlanSuite/actions/workflows/ci.yml/badge.svg)](https://github.com/maiconcardozo/HealthPlanSuite/actions/workflows/ci.yml)
[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0.11-blue.svg)](https://docs.microsoft.com/en-us/ef/core/)

## 📋 Overview

**HealthPlan Suite** is a comprehensive .NET application for managing health plan quotes and related insurance operations. This system follows Clean Architecture principles and includes complete health plan management functionality for insurance companies and brokers.

## ⚠️ Framework Requirements

This application is built with **.NET 8.0** and uses the following technologies:

- **Entity Framework Core 8.0.11** - Database ORM and data access
- **ASP.NET Core 8.0.11** - Web framework for REST API development  
- **Swashbuckle 6.8.0** - OpenAPI/Swagger documentation
- **Testing frameworks** - xUnit 2.9.x, FluentAssertions 6.x, Moq 4.20.x
- **C# language features** - Modern C# syntax and features

**Current framework versions:**
- `global.json` SDK version: 8.0.100
- All `.csproj` TargetFramework: net8.0
- Package dependencies aligned with .NET 8.0

### 🔐 Key Features

- **Health Plan Management**: Complete health plan quote and coverage management
- **Clean Architecture**: Well-organized layers with proper separation of concerns
- **Repository Pattern**: Generic repository with Entity Framework implementation
- **Unit of Work**: Transaction management and consistency
- **Service Layer**: Business logic separation with proper error handling
- **RESTful API**: Complete CRUD endpoints for health plan operations
- **AutoMapper Integration**: DTO mapping configuration
- **Entity Framework**: Database configuration and migrations
- **Dependency Injection**: Proper IoC container setup
- **Unit Testing**: Comprehensive test coverage with FluentAssertions
- **Quote Management**: Generate and manage health plan quotes
- **Coverage Management**: Define and manage different types of coverage
- **Company Management**: Manage insurance companies and their details
- **Age-based Pricing**: Support for age range-based pricing models

## 🏗️ Architecture

The application is organized in well-defined layers following Clean Architecture principles:

```
HealthPlanSuite/
├── Src/
│   ├── HealthPlan.API/                # API Layer
│   │   ├── Controllers/               # API Controllers
│   │   │   └── QuoteController.cs     # Health plan quote management
│   │   │   └── CoverageController.cs  # Coverage management
│   │   │   └── CompanyController.cs   # Insurance company management
│   │   ├── Middleware/                # Custom middleware
│   │   ├── Swagger/                   # API documentation
│   │   └── Resource/                  # Localization resources
│   │
│   └── HealthPlan.Quote/              # Domain & Business Logic
│       ├── Domain/                    # Domain entities
│       │   ├── Implementation/        # Concrete implementations
│       │   │   ├── Quote.cs           # Quote entity
│       │   │   ├── Coverage.cs        # Coverage entity
│       │   │   ├── Company.cs         # Company entity
│       │   │   ├── HealthPlan.cs      # Health plan entity
│       │   │   ├── AgeRange.cs        # Age range pricing
│       │   │   └── Beneficiary.cs     # Beneficiary entity
│       │   └── Interface/             # Domain interfaces
│       ├── Services/                  # Business services
│       │   ├── Implementation/        # Service implementations
│       │   │   ├── QuoteService.cs    # Quote business logic
│       │   │   ├── CoverageService.cs # Coverage business logic
│       │   │   └── CompanyService.cs  # Company business logic
│       │   └── Interface/             # Service contracts
│       ├── Repository/                # Data access layer
│       │   ├── Implementation/        # Repository implementations
│       │   │   ├── QuoteRepository.cs # Quote data access
│       │   │   ├── CoverageRepository.cs # Coverage data access
│       │   │   └── CompanyRepository.cs # Company data access
│       │   └── Interface/             # Repository contracts
│       ├── DTO/                       # Data transfer objects
│       ├── Infrastructure/            # Entity configurations
│       │   └── Implementation/        # EF Core mappings
│       ├── UnitOfWork/                # Unit of Work pattern
│       └── Foundation/                # Base classes
│           └── Base/                  # Foundation base implementations
│
└── HealthPlan.Test/                   # Test project
    ├── Unit/                          # Unit tests
    └── Helpers/                       # Test utilities
│
├── docs/                              # Documentation
├── scripts/                           # Build & test scripts
└── Solution/                          # Solution configuration
```

### 📁 Repository Organization

The repository follows a clean, organized structure:

- **`/Src/`** - Source code (API, business logic, tests)
- **`/docs/`** - All documentation including status reports
- **`/scripts/`** - Build, test, and utility scripts
- **Root level** - Essential configuration files only

## 🔧 Technologies Used

- **.NET 8.0** - Main framework (net8.0)
- **ASP.NET Core 8.0.11** - RESTful API framework
- **Entity Framework Core 8.0.11** - ORM for data access
- **FluentValidation 12.0.0** - Input validation
- **Konscious.Security.Cryptography.Argon2 1.3.1** - Secure password hashing
- **MySQL/MariaDB** - Database support (MySqlConnector 2.3.7, Pomelo.EntityFrameworkCore.MySql 8.0.2)
- **Swagger/OpenAPI 6.8.0** - API documentation
- **AutoMapper 13.0.1** - Object mapping

## 🚀 Development (Quick Start)

### Development Environment Setup

```bash
# 1. Clone the repository
git clone https://github.com/maiconcardozo/HealthPlanSuite.git
cd HealthPlanSuite

# 2. Install .NET 8.0 SDK (8.0.100 or later)
# Download from: https://dotnet.microsoft.com/download/dotnet/8.0

# 3. Verify .NET 8.0 installation
dotnet --version
# Should output: 8.0.x (e.g., 8.0.414)

# 4. Restore dependencies
dotnet restore Solution/HealthPlan.sln

# 5. Build in Debug mode (development)
dotnet build Solution/HealthPlan.sln --configuration Debug

# 6. Run the API
cd Src/HealthPlan.API
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
dotnet build Solution/HealthPlan.sln --configuration Release
# Should complete without errors

# Run all tests  
scripts/run-tests.sh all        # Linux/Mac
scripts/run-tests.bat all       # Windows
# Should show test results

# Start the application
cd Src/HealthPlan.API
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

## ⚠️ .NET 8.0 Framework Information

This project uses **.NET 8.0** as specified in the project files.

### Current Framework Configuration:
- **SDK Version**: 8.0.100 (specified in global.json)
- **Target Framework**: net8.0 (all .csproj files)
- **Entity Framework Core**: 8.0.11
- **ASP.NET Core**: 8.0.11

### Installation:
1. **Download .NET 8.0 SDK**: https://dotnet.microsoft.com/download/dotnet/8.0
2. **Verify Installation**: `dotnet --version` should show 8.0.x
3. **Check Project**: `dotnet build` should complete without framework errors

### 🔧 Troubleshooting .NET 8.0 Setup

#### Common Issues and Solutions:

**Issue**: `NETSDK1045: The current .NET SDK does not support targeting .NET 8.0`
```bash
# Solution: Install .NET 8.0 SDK
# 1. Download from: https://dotnet.microsoft.com/download/dotnet/8.0
# 2. Verify installation: dotnet --version
# 3. Should show: 8.0.x
```

**Issue**: `A compatible .NET SDK was not found`
```bash
# Solution: Check global.json configuration
cat global.json
# Should specify version: "8.0.100"

# Verify SDK installation
dotnet --list-sdks
# Should include: 8.0.x
```

**Issue**: Project won't build or restore
```bash
# Solution: Clean and rebuild
dotnet clean Solution/HealthPlan.sln
dotnet restore Solution/HealthPlan.sln
dotnet build Solution/HealthPlan.sln
```

**Issue**: Tests won't run
```bash
# Solution: Use convenience scripts (they handle dependencies)
scripts/run-tests.sh clean     # Clean and test
scripts/run-tests.sh verbose   # Detailed output for debugging
```

## 📦 Production Installation

### Prerequisites
- .NET 8.0 SDK or higher
- MySQL 8.0+ or higher
- Entity Framework Core 8.0.11

### Cloning and Building Locally
```bash
git clone https://github.com/maiconcardozo/HealthPlanSuite.git
cd HealthPlanSuite
dotnet build Solution/HealthPlan.sln --configuration Release
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
dotnet clean Solution/HealthPlan.sln
dotnet restore Solution/HealthPlan.sln  
dotnet build Solution/HealthPlan.sln --configuration Release

# 2. Test execution verification
./run-tests.sh all                  # Linux/Mac
./run-tests.bat all                 # Windows

# 3. API startup verification
cd Src/HealthPlan.API
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
- Builds with .NET 8.0 in Ubuntu environment
- Executes all tests and generates reports
- Provides code coverage and security scanning

**For other CI systems:**
- Use `./test.sh` (Linux) or `./test.bat` (Windows) as the main test command
- Ensure .NET 8.0 SDK is installed in the CI environment
- Configure artifact collection for test results in `TestResults/` directory

## 🚀 Quick Usage (Development)

> **💡 Development Focus**: All examples prioritize development configurations and practices to facilitate the developer experience.

### 1. Database Configuration (Development)

Update the connection string in `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HealthPlanDB;Uid=your_user;Pwd=your_password;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 2. Initializing the Database

```bash
cd Src/HealthPlan.API
dotnet ef database update --context ApplicationContext
```

### 3. Basic Health Plan Management

```csharp
// Example: Creating a health plan quote
public class QuoteController : ControllerBase
{
    private readonly IQuoteService _quoteService;
    
    public QuoteController(IQuoteService quoteService)
    {
        _quoteService = quoteService;
    }
    
    [HttpPost("CreateQuote")]
    public async Task<IActionResult> CreateQuote([FromBody] QuotePayloadDTO request)
    {
        var response = await _quoteService.CreateQuoteAsync(request);
        return Ok(response);
    }
    
    [HttpGet("GetQuotes")]
    public async Task<IActionResult> GetQuotes()
    {
        var quotes = await _quoteService.GetAllQuotesAsync();
        return Ok(quotes);
    }
}
```

### 4. Using the Authentication Service (With Debug)

```csharp
using Authentication.Login.Services;
using Authentication.Login.DTO;
using Microsoft.Extensions.Logging;

public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;
    private readonly ILogger<AuthController> _logger;
    
    public AuthController(
        IAuthenticationService authService,
        ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }
    
    [HttpPost("GenerateToken")]
    public async Task<IActionResult> GenerateToken([FromBody] LoginRequestDTO request)
    {
        #if DEBUG
        _logger.LogDebug("Login attempt for user: {UserName} at {Time}", 
            request.UserName, DateTime.Now);
        #endif
        
        try
        {
            var response = await _authService.AuthenticateAsync(request);
            
            #if DEBUG
            _logger.LogDebug("Token generated successfully for: {UserName}", request.UserName);
            #endif
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            #if DEBUG
            _logger.LogError(ex, "Authentication error for: {UserName}", request.UserName);
            #endif
            
            return Unauthorized("Invalid credentials");
        }
    }
    
    [HttpPost("AddAccount")]
    public async Task<IActionResult> AddAccount([FromBody] CreateUserRequestDTO request)
    {
        #if DEBUG
        _logger.LogDebug("Creating account for user: {UserName}", request.UserName);
        #endif
        
        var result = await _authService.CreateUserAsync(request);
        
        #if DEBUG
        _logger.LogDebug("Account created successfully: {UserName}", request.UserName);
        #endif
        
        return Ok(result);
    }
}
```

### 5. Installation Verification

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

- **`AuthenticationController`**: Main controller for authentication
- **`Middleware`**: Custom middleware for JWT and logging

### 🔐 Services Layer

- **`IAuthenticationService`**: Interface for authentication services
- **`AuthenticationService`**: Authentication services implementation

### 🗃️ Repository Layer

- **`IUserRepository`**: Interface for user data access
- **`UserRepository`**: Implementation with user CRUD operations

### 🛠️ Utilities

- **`JwtTokenGenerator`**: JWT token generation and validation
- **`PasswordHasher`**: Password hashing and verification with Argon2

## 🔐 Security

The service includes robust security features:

```csharp
using Foundation.Base.Util;

// Password hashing with Argon2
string passwordHash = StringHelper.ComputeArgon2Hash("myPassword123");

// Password verification
bool isValidPassword = StringHelper.VerifyArgon2Hash("myPassword123", passwordHash);

// JWT Token generation
var tokenDescriptor = new SecurityTokenDescriptor
{
    Subject = new ClaimsIdentity(new[]
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    }),
    Expires = DateTime.UtcNow.AddMinutes(60),
    SigningCredentials = new SigningCredentials(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), 
        SecurityAlgorithms.HmacSha256Signature)
};
```

## ✅ Validation

Native integration with FluentValidation:

```csharp
using FluentValidation;

public class LoginRequestValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password must have at least 6 characters");
    }
}

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequestDTO>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]")
            .WithMessage("Password must contain at least: 1 lowercase letter, 1 uppercase letter, 1 number and 1 special character");
    }
}

// In the controller
var validationResult = await ValidationHelper.ValidateEntityAsync(request, serviceProvider, this);
if (validationResult != null) return validationResult;
```

## 🔐 RBAC System - Practical Example

### Step-by-Step Permission Configuration

```bash
# 1. Authenticate and get JWT token
curl -X POST "https://localhost:7001/Authentication/GenerateToken" \
  -H "Content-Type: application/json" \
  -d '{"userName": "admin", "password": "password123"}'

# 2. Create a claim (permission)
curl -X POST "https://localhost:7001/Claim/AddClaim" \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d '{"type": "Permission", "value": "user:manage", "description": "Manage users"}'

# 3. Create an action
curl -X POST "https://localhost:7001/Action/AddAction" \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d '{"name": "CreateUser"}'

# 4. Map claim to action
curl -X POST "https://localhost:7001/ClaimAction/AddClaimAction" \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d '{"claimId": 1, "actionId": 1}'

# 5. Assign permission to a user
curl -X POST "https://localhost:7001/AccountClaimAction/AddAccountClaimAction" \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d '{"accountId": 123, "claimActionId": 1}'
```

### Permission Verification Flow

1. **User logs in** → Receives JWT token
2. **System checks permissions** → Queries `AccountClaimAction`
3. **Action validation** → Verifies if the claim allows the desired action
4. **Authorized execution** → User can execute the operation

## 🌐 API Endpoints

### Main Health Plan Management Endpoints

| Method | Endpoint | Description | Authentication |
|--------|----------|-------------|----------------|
| **GET** | `/Quote/GetQuotes` | 📋 List all quotes | ✅ |
| **GET** | `/Quote/GetQuoteById/{id}` | 🔍 Get quote by ID | ✅ |
| **POST** | `/Quote/CreateQuote` | ➕ Create new quote | ✅ |
| **PUT** | `/Quote/UpdateQuote/{id}` | ✏️ Update quote | ✅ |
| **DELETE** | `/Quote/DeleteQuote/{id}` | ❌ Delete quote | ✅ |
| **GET** | `/Coverage/GetCoverages` | 📋 List all coverages | ✅ |
| **GET** | `/Coverage/GetCoverageById/{id}` | 🔍 Get coverage by ID | ✅ |
| **POST** | `/Coverage/CreateCoverage` | ➕ Create new coverage | ✅ |
| **PUT** | `/Coverage/UpdateCoverage/{id}` | ✏️ Update coverage | ✅ |
| **DELETE** | `/Coverage/DeleteCoverage/{id}` | ❌ Delete coverage | ✅ |
| **GET** | `/Company/GetCompanies` | 📋 List all companies | ✅ |
| **GET** | `/Company/GetCompanyById/{id}` | 🔍 Get company by ID | ✅ |
| **POST** | `/Company/CreateCompany` | ➕ Create new company | ✅ |
| **PUT** | `/Company/UpdateCompany/{id}` | ✏️ Update company | ✅ |
| **DELETE** | `/Company/DeleteCompany/{id}` | ❌ Delete company | ✅ |
| **GET** | `/health` | ❤️ Health check | ❌ |

### 📋 Create Health Plan Quote

Creates a new health plan quote with coverage details:

```bash
curl -X POST "https://localhost:7001/Quote/CreateQuote" \
  -H "Content-Type: application/json" \
  -d '{
    "companyId": 1,
    "beneficiaryCount": 2,
    "coverages": [
      {
        "coverageId": 1,
        "ageRangeId": 1
      }
    ]
  }'
```

**Success Response (200):**
```json
{
  "data": {
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "expiresIn": 3600,
    "userName": "admin",
    "claims": ["user:read", "user:write"],
    "tokenType": "Bearer"
  }
}
```

**Error Responses:**

**401 Unauthorized** - Invalid credentials:
```json
{
  "title": "Unauthorized",
  "status": 401,
  "detail": "Invalid username or password",
  "type": "https://tools.ietf.org/html/rfc7231#section-6.3.1",
  "instance": "/Authentication/GenerateToken"
}
```

**400 Bad Request** - Validation error:
```json
{
  "title": "Bad Request",
  "status": 400,
  "detail": "One or more validation errors occurred",
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "instance": "/Authentication/GenerateToken"
}
```

### 👤 Create User Account

Registers a new user account with duplicate username prevention:

```bash
curl -X POST "https://localhost:7001/Account/AddAccount" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "newUser",
    "password": "SecurePassword123!"
  }'
```

**Success Response (200):**
```json
{
  "type": "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.1",
  "title": "OK.",
  "status": 200,
  "detail": "Request was successful.",
  "instance": "/example/instance",
  "data": {
    "userId": 123,
    "userName": "newUser",
    "email": "newUser@example.com"
  }
}
```

**Conflict Response (409):**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "Invalid request.",
  "status": 400,
  "detail": "One or more validation errors occurred.",
  "instance": "/example/instance"
}
```

## 📖 Detailed Documentation

### 🚀 **For Developers (START HERE)**
- **[Quick Start Guide](docs/QUICK_START.md)** - **5-minute setup from zero to running API**
- **[Development Guide](docs/DEVELOPMENT.md)** - **Complete development workflow and best practices**

### 📚 **Technical Documentation**
- [Service Architecture](docs/ARCHITECTURE.md) - Clean Architecture patterns and design decisions
- [JWT Authentication Guide](docs/JWT.md) - Token-based authentication implementation
- [Security Configuration](docs/SECURITY.md) - Argon2 hashing and security best practices
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
git clone https://github.com/maiconcardozo/HealthPlanSuite.git
cd HealthPlanSuite

# Install dependencies (requires .NET 8.0 SDK)
dotnet restore Solution/HealthPlan.sln

# Build the project
dotnet build Solution/HealthPlan.sln --configuration Debug

# Run in development mode
dotnet run --project Src/HealthPlan.API

# Run tests
dotnet test Solution/HealthPlan.sln
```

# Or run tests manually
dotnet test Src/Authentication.Tests/Authentication.Tests.csproj
```

## 🧪 Running Tests

The project includes a comprehensive test suite following TDD architecture with convenient scripts for easy execution:

### 🎯 Quick Test Execution (Recommended)

**Single Command Test Execution:**
```bash
# Universal test command (works in any environment)
./test.sh                   # Linux/Mac
test.bat                    # Windows

# Alternative: Direct dotnet command
dotnet test Solution/HealthPlan.sln
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
./run-tests.sh clean        # Clean, rebuild, then test
./run-tests.sh help         # Show all available options
```

### 🔧 Manual Test Commands

```bash
# Run all tests
dotnet test Src/Authentication.Tests/Authentication.Tests.csproj

# Run tests with verbosity
dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --verbosity normal

# Run integration tests only
dotnet test --filter "FullyQualifiedName~Integration"

# Run unit tests only
dotnet test --filter "FullyQualifiedName~Unit"

# Run with code coverage
dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --collect:"XPlat Code Coverage"
```

### 📊 Test Status & Coverage

- ✅ **Total Tests**: 342 tests (integration + unit tests)
- ✅ **Coverage Target**: > 80% code coverage  
- ✅ **Execution Time**: All tests complete in < 30 seconds
- ✅ **Test Framework**: xUnit with FluentAssertions
- ✅ **Database**: In-memory database for isolation
- ✅ **CI/CD Integration**: Automated pipeline with GitHub Actions

**Current Test Status:**
- **Integration Tests**: Complete API endpoint coverage
- **Unit Tests**: Business logic and component testing  
- **Error Scenarios**: 400, 401, 404, 409, 500 status codes
- **Security Testing**: Authentication, authorization, password hashing
- **Data Validation**: Input validation and boundary testing

### 📚 **Detailed Testing Documentation**

For comprehensive testing information, see:
- **[Testing Guide](docs/TESTING.md)** - Complete testing documentation with troubleshooting
- **[Test Execution Status](docs/TEST_EXECUTION_STATUS.md)** - Current test infrastructure status  
- **[Development Guide](docs/DEVELOPMENT.md)** - Development workflow including testing

### 🚀 **Automated Testing (CI/CD)**

The project includes automated testing through GitHub Actions:
- ✅ Runs on every push and pull request
- ✅ Uses .NET 8.0 in Ubuntu environment
- ✅ Generates test reports and coverage analysis
- ✅ Stores artifacts for 30 days
- ✅ Enforces quality gates before merge

## 🔄 Continuous Integration

The project includes a comprehensive CI/CD pipeline using GitHub Actions that automatically:

### 🚀 Automated Pipeline Features

**On every push and pull request:**
- ✅ **Build Verification**: Compiles the project in Release mode with .NET 8.0
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

#### 🏗️ SOLID Principles Enforced:
- **Cognitive Complexity**: Methods must not be overly complex
- **Single Responsibility**: Classes should have focused responsibilities  
- **Method Complexity**: Avoid deeply nested control structures
- **Clean Code**: Empty statements and dead code are blocked
- **Interface Design**: Proper abstraction and cohesion required

### 📋 CI/CD Status & Reports

The pipeline provides detailed feedback including:
- **Build Status**: ✅ Success/❌ Failure for each step
- **Test Results**: Detailed test reports with pass/fail status
- **Coverage Reports**: Code coverage percentage and detailed analysis
- **Security Alerts**: Notifications about vulnerable dependencies
- **Build Artifacts**: Downloadable test results and logs

### 🛠️ Workflow Configuration

The CI pipeline is defined in `.github/workflows/ci.yml` and includes:

```yaml
# Runs on: Ubuntu Latest with .NET 8.0
# Triggers: Push/PR to main and develop branches
# Steps: Build → Test → Quality Check → Security Scan
```

**View the CI/CD status:**
- Check the **Actions** tab in the GitHub repository
- Build status badges are available for README integration
- Detailed logs for each pipeline step

### 🔧 Local Development CI Simulation

Run the same checks locally before pushing:

```bash
# Full CI simulation
./test.sh                           # Main test execution
dotnet build --configuration Release # Release build
dotnet format --verify-no-changes   # Code formatting check
dotnet list package --vulnerable     # Security check

# Test code quality rules specifically
./test-code-quality.sh              # Validate else statement prevention
```

### 💻 Development Workflow with Code Quality

When developing, follow these practices to avoid CI failures:

```bash
# 1. Check code quality before committing
dotnet build Solution/HealthPlan.sln /warnaserror:IDE0046,IDE0045

# 2. Fix any else statement violations
# Replace: if/else return → ternary operator
# Replace: if/else assignment → ternary operator

# 3. Verify SOLID principle compliance
dotnet build Solution/HealthPlan.sln /warnaserror:S3776,S1541,S1200

# 4. Run full test suite
./test.sh
```

#### 🛠️ IDE Support

Configure your IDE for optimal development experience:

**Visual Studio / VS Code:**
- Install C# extension with latest language support
- Enable EditorConfig support (automatically enforces rules)
- Configure Roslyn analyzers for real-time feedback

**Code Analysis Settings:**
```xml
<!-- Already configured in Directory.Build.props -->
<PropertyGroup>
  <EnableNETAnalyzers>true</EnableNETAnalyzers>
  <AnalysisLevel>latest</AnalysisLevel>
  <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
</PropertyGroup>
```

### 📈 Pipeline Optimization

The CI/CD pipeline is optimized for:
- **Fast Execution**: Parallel jobs and dependency caching
- **Comprehensive Coverage**: Multiple quality gates
- **Developer Feedback**: Clear error messages and actionable reports
- **Artifact Preservation**: Test results and coverage reports stored for analysis

## 📄 License

This project is licensed under the [MIT License](LICENSE).

## 👨‍💻 Author

**Maicon Cardozo**
- GitHub: [@maiconcardozo](https://github.com/maiconcardozo)

## 📞 Support

For questions, suggestions, or to report issues:
- Open an [issue](https://github.com/maiconcardozo/HealthPlanSuite/issues)
- Contact through GitHub

---

⭐ If this project was useful to you, please consider giving it a star!
