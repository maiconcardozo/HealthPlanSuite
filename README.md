# 🏥NET Health Plan Quote Management System

<!-- Build and CI/CD Status Badges -->
[![CI/CD Pipeline](https://github.com/maiconcardozo/HealthPlanSuite/actions/workflows/ci.yml/badge.svg)](https://github.com/maiconcardozo/HealthPlanSuite/actions/workflows/ci.yml)

<!-- Technology and Framework Badges -->
[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blue.svg?logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![C#](https://img.shields.io/badge/C%23-12.0-239120.svg?logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0.11-blue.svg?logo=nuget)](https://docs.microsoft.com/en-us/ef/core/)

<!-- Quality -->
[![Code Coverage](https://img.shields.io/badge/Coverage-80%25+-brightgreen?logo=codecov)](https://github.com/maiconcardozo/HealthPlanSuite)
[![Tests](https://img.shields.io/badge/Tests-34%20passing-brightgreen?logo=checkmarx)](https://github.com/maiconcardozo/HealthPlanSuite)
[![License](https://img.shields.io/badge/License-MIT-blue.svg?logo=open-source-initiative)](LICENSE)
[![Last Commit](https://img.shields.io/badge/Last%20Commit-recent-brightgreen?logo=git)](https://github.com/maiconcardozo/HealthPlanSuite/commits/main)
[![Contributors](https://img.shields.io/badge/Contributors-1-blue?logo=github)](https://github.com/maiconcardozo/HealthPlanSuite/graphs/contributors)

## 📋 About

A production-ready .NET 8.0 health plan quote management system implementing Clean Architecture principles and the MediatR pattern for CQRS. Built for insurance companies and brokers, it provides comprehensive health plan management with quote generation, coverage configuration, age-based pricing, and company management features.

### ✨ Key Features

- 📋 **Quote Management** - Generate and manage health plan quotes
- 🏥 **Health Plan Configuration** - Complete health plan setup and management
- 🛡️ **Coverage Management** - Define and manage different types of coverage
- 🏢 **Company Management** - Manage insurance companies and their details
- 👥 **Beneficiary Management** - Track beneficiaries and their plans
- 📊 **Age-based Pricing** - Support for age range-based pricing models
- ✅ **Validation** - FluentValidation for input validation
- 📖 **Swagger/OpenAPI** - Interactive API documentation
- 🎯 **MediatR Pattern** - CQRS implementation with pipeline behaviors

## 🛠️ Technologies

- **.NET 8.0** - Latest LTS framework
- **ASP.NET Core** - RESTful API
- **Entity Framework Core 8.0.11** - ORM
- **MediatR 12.4.1** - CQRS and Mediator pattern
- **MySQL/MariaDB** - Database
- **FluentValidation 12.0.0** - Validation
- **AutoMapper 13.0.1** - Object mapping
- **Swagger/OpenAPI** - API docs
- **xUnit** - Testing framework

## 🏗️ Project Structure (Clean Architecture)

This project follows **Clean Architecture** and **Domain-Driven Design (DDD)** principles with clear separation of concerns:

```
src/
├── HealthPlan.Domain/              # Pure business logic, zero dependencies
│   ├── Entities/                   # Quote, HealthPlan, Company, Coverage, etc.
│   ├── Interfaces/                 # Repository interfaces, IUnitOfWork
│   └── Exceptions/                 # Domain exceptions
│
├── HealthPlan.Application/         # Use cases and orchestration
│   ├── Commands/                   # MediatR commands (write operations - CQRS)
│   ├── Queries/                    # MediatR queries (read operations - CQRS)
│   ├── Behaviors/                  # MediatR pipeline behaviors
│   ├── Services/                   # QuoteService, CoverageService, etc.
│   ├── DTOs/                       # PayloadDTOs, ResponseDTOs
│   ├── Mappers/                    # AutoMapper profiles
│   ├── Validators/                 # FluentValidation validators
│   └── Constants/                  # Application constants
│
├── HealthPlan.Infrastructure/      # External dependencies implementation
│   ├── Persistence/                # ApplicationContext (EF DbContext), Mappings
│   ├── Repositories/               # Repository implementations
│   └── UnitOfWork/                 # UnitOfWork implementation
│
├── HealthPlan.API/                 # Presentation layer
│   ├── Controllers/                # API endpoints
│   ├── Middleware/                 # Request pipeline
│   ├── Swagger/                    # API documentation setup
│   └── Program.cs/Startup.cs       # Application entry point
│
├── HealthPlan.Shared/              # Shared kernel
│   └── Kernel/                     # DI extensions, shared utilities
│
└── HealthPlan.Tests/               # Test projects
    ├── Unit/                       # Unit tests
    └── Integration/                # Integration tests
```

**Key Architectural Benefits:**
- ✅ **Domain Layer** - Zero infrastructure dependencies, pure business logic
- ✅ **Dependency Inversion** - All dependencies point inward to Domain
- ✅ **Testability** - Easy to mock and test in isolation
- ✅ **Maintainability** - Clear separation of concerns
- ✅ **Scalability** - Easy to extend and modify

## 🚀 Getting Started

**New to the project? Start here:**

- **[Installation Guide](docs/getting-started/INSTALLATION.md)** - Step-by-step installation instructions
- **[Quick Start Guide](docs/getting-started/QUICK_START.md)** - 5-minute setup from zero to running API
- **[Configuration Guide](docs/getting-started/CONFIGURATION.md)** - Complete configuration reference

## 📖 User Guides

**Comprehensive guides for using and developing with the service:**

- **[Development Guide](docs/guides/DEVELOPMENT.md)** - Development workflow and best practices
- **[Testing Guide](docs/guides/TESTING.md)** - Unit and integration testing strategies
- **[Deployment Guide](docs/guides/DEPLOYMENT.md)** - Production deployment strategies
- **[Contributing Guide](docs/guides/CONTRIBUTING.md)** - How to contribute to the project

## 🌐 API Documentation

**Everything about the API:**

- **[API Reference](docs/api/API.md)** - Complete API documentation
- **[Authentication Guide](docs/api/AUTHENTICATION.md)** - JWT authentication implementation
- **[Integration Examples](docs/api/EXAMPLES.md)** - Real-world integration examples
- **[Swagger Configuration](docs/api/swagger-configuration.md)** - API documentation configuration

## 🏗️ Architecture & Design

**Understanding the system architecture:**

- **[Architecture Guide](docs/architecture/ARCHITECTURE.md)** - Clean Architecture patterns and design decisions
- **[Security Guide](docs/architecture/SECURITY.md)** - Security best practices
- **[Entity Mapping](docs/architecture/MAPEAMENTO.md)** - Entity relationship mappings
- **[New Entities Implementation](docs/architecture/NEW_ENTITIES_IMPLEMENTATION.md)** - Guide for adding new entities
- **[Dependency Injection](docs/architecture/DEPENDENCY_INJECTION_CONFIG.md)** - DI configuration guide

## 🔄 CI/CD

**Continuous Integration and Deployment:**

- **[Pipeline Documentation](docs/ci-cd/PIPELINE.md)** - CI/CD pipeline overview

## 📚 Reference

**Additional resources:**

- **[Troubleshooting Guide](docs/reference/TROUBLESHOOTING.md)** - Common issues and solutions
- **[FAQ](docs/reference/FAQ.md)** - Frequently Asked Questions
- **[Code Documentation Standards](docs/reference/CODE_DOCUMENTATION.md)** - XML comments and inline documentation guidelines
- **[Reorganization Notes](docs/reference/REORGANIZATION_NOTES.md)** - Documentation reorganization history

## 🧪 Testing

**Testing documentation:**

- **[Testing Guide](docs/guides/TESTING.md)** - Testing strategies and execution
- **[Detailed Test Documentation](docs/tests/DETAILED_TEST_DOCUMENTATION.md)** - Comprehensive test documentation
- **[Test Execution Status](docs/tests/TEST_EXECUTION_STATUS.md)** - Detailed test execution results

## 📊 Project Status

**Current project status and history:**

- **[Changelog](docs/status/CHANGELOG.md)** - Version history and changes

## 📋 Documentation by Audience

### For New Users

1. [Installation Guide](docs/getting-started/INSTALLATION.md)
2. [Quick Start Guide](docs/getting-started/QUICK_START.md)
3. [FAQ](docs/reference/FAQ.md)

### For Developers

1. [Quick Start Guide](docs/getting-started/QUICK_START.md)
2. [Development Guide](docs/guides/DEVELOPMENT.md)
3. [API Reference](docs/api/API.md)
4. [Architecture Guide](docs/architecture/ARCHITECTURE.md)
5. [Testing Guide](docs/guides/TESTING.md)

### For API Users

1. [API Reference](docs/api/API.md)
2. [Authentication Guide](docs/api/AUTHENTICATION.md)
3. [Integration Examples](docs/api/EXAMPLES.md)

### For System Administrators

1. [Installation Guide](docs/getting-started/INSTALLATION.md)
2. [Configuration Guide](docs/getting-started/CONFIGURATION.md)
3. [Deployment Guide](docs/guides/DEPLOYMENT.md)
4. [Security Guide](docs/architecture/SECURITY.md)
5. [Troubleshooting Guide](docs/reference/TROUBLESHOOTING.md)

## 🔍 Quick Links

### Most Common Tasks

- **Install the service**: [Installation Guide](docs/getting-started/INSTALLATION.md)
- **Configure database**: [Configuration Guide](docs/getting-started/CONFIGURATION.md)
- **Run tests**: [Testing Guide](docs/guides/TESTING.md)
- **Deploy to production**: [Deployment Guide](docs/guides/DEPLOYMENT.md)
- **Troubleshoot issues**: [Troubleshooting Guide](docs/reference/TROUBLESHOOTING.md)

### Key Features Documentation

- **Health Plan Management**: [API Reference](docs/api/API.md)
- **Quote Generation**: [API Reference](docs/api/API.md)
- **Coverage Configuration**: [Architecture Guide](docs/architecture/ARCHITECTURE.md)
- **API Validation**: [Configuration Guide](docs/getting-started/CONFIGURATION.md)

## 📁 Documentation Structure

```
docs/
├── getting-started/          # Installation and setup
│   ├── INSTALLATION.md       # Installation instructions
│   ├── QUICK_START.md        # Quick start guide
│   └── CONFIGURATION.md      # Configuration reference
│
├── guides/                   # User guides
│   ├── DEVELOPMENT.md        # Development guide
│   ├── TESTING.md            # Testing guide
│   ├── DEPLOYMENT.md         # Deployment guide
│   └── CONTRIBUTING.md       # Contributing guide
│
├── api/                      # API documentation
│   ├── API.md                # Complete API reference
│   ├── AUTHENTICATION.md     # Authentication guide
│   ├── EXAMPLES.md           # Integration examples
│   └── swagger-configuration.md # Swagger configuration
│
├── architecture/             # Architecture documentation
│   ├── ARCHITECTURE.md       # Architecture overview
│   ├── SECURITY.md           # Security guide
│   ├── MAPEAMENTO.md         # Entity mappings
│   ├── NEW_ENTITIES_IMPLEMENTATION.md # New entities guide
│   └── DEPENDENCY_INJECTION_CONFIG.md # DI configuration
│
├── ci-cd/                    # CI/CD documentation
│   └── PIPELINE.md           # Pipeline documentation
│
├── reference/                # Reference documentation
│   ├── TROUBLESHOOTING.md    # Troubleshooting
│   ├── FAQ.md                # FAQ
│   ├── CODE_DOCUMENTATION.md # Code documentation standards
│   └── REORGANIZATION_NOTES.md # Documentation reorganization notes
│
├── status/                   # Project status
│   └── CHANGELOG.md          # Version history
│
├── tests/                    # Testing documentation
│   ├── DETAILED_TEST_DOCUMENTATION.md # Detailed test documentation
│   └── TEST_EXECUTION_STATUS.md # Test results
│
└── modeling/                 # Database modeling
    └── HealthPlanModeling.sql # SQL modeling file
```

## 💡 Tips for Using This Documentation

1. **Use the search**: Press `Ctrl+F` / `Cmd+F` to search within pages
2. **Follow the links**: Documentation is heavily cross-referenced
3. **Check the FAQ**: Many common questions are answered in the [FAQ](docs/reference/FAQ.md)
4. **Start with Quick Start**: The [Quick Start Guide](docs/getting-started/QUICK_START.md) gets you up and running quickly
5. **Reference when needed**: Use the [API Reference](docs/api/API.md) and [Troubleshooting](docs/reference/TROUBLESHOOTING.md) as needed

## 🤝 Contributing

Contributions are welcome! Please see our guidelines:

- **[Contributing Guide](CONTRIBUTING.md)** - Quick contributing overview
- **[Detailed Contributing Guide](docs/guides/CONTRIBUTING.md)** - Complete contributing guidelines
- **[Code of Conduct](CODE_OF_CONDUCT.md)** - Community standards and expectations

Found an issue or want to improve the documentation?
- Report documentation issues on [GitHub Issues](https://github.com/maiconcardozo/HealthPlanSuite/issues)
- Submit documentation improvements via Pull Request
- Check our documentation style guidelines in the [Contributing Guide](docs/guides/CONTRIBUTING.md)

## 📄 License

This project is licensed under the [MIT License](LICENSE).

## 👨‍💻 Author

**Maicon Cardozo**
- GitHub: [@maiconcardozo](https://github.com/maiconcardozo)

## 📞 Support

Need help? We have several resources:

- **[Support Guide](SUPPORT.md)** - How to get help
- **[Security Policy](SECURITY.md)** - Report security vulnerabilities
- 📖 Check the [FAQ](docs/reference/FAQ.md)
- 🔧 Review the [Troubleshooting Guide](docs/reference/TROUBLESHOOTING.md)
- 🐛 Report issues: [GitHub Issues](https://github.com/maiconcardozo/HealthPlanSuite/issues)
- 💬 Ask questions: [GitHub Discussions](https://github.com/maiconcardozo/HealthPlanSuite/discussions)

## 📖 External Resources

- [.NET 8.0 Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [MediatR Documentation](https://github.com/jbogard/MediatR)

---

⭐ If this project was useful to you, please consider giving it a star!
