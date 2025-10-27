# 🗺️ HealthPlan Suite Project Mapping

This document provides a complete mapping of controllers, endpoints, main classes, and architecture of the HealthPlan Suite project.

## 📋 Table of Contents
- [Controllers and Endpoints](#controllers-and-endpoints)
- [Main Classes](#main-classes)
- [Project Organization](#project-organization)
- [Notes](#notes)

---

## 🎮 Controllers and Endpoints

### 1. PlanCoverageController
**Description**: Manages CRUD operations for plan coverages.

**Base Route**: `/PlanCoverage`

**Endpoints**:
- `GET /PlanCoverage/plan-coverages` - Retrieves all active plan coverages
- `GET /PlanCoverage/{id}` - Retrieves a specific plan coverage by ID
- `GET /PlanCoverage/health-plan/{healthPlanId}` - Retrieves coverages by health plan ID
- `POST /PlanCoverage` - Creates a new plan coverage
- `PUT /PlanCoverage/{id}` - Updates an existing plan coverage
- `DELETE /PlanCoverage/{id}` - Deletes a plan coverage

**File**: `Src/HealthPlan.API/Controllers/PlanCoverageController.cs`

---

### 2. CoverageController
**Description**: Manages available coverages in the system.

**Base Route**: `/Coverage`

**Endpoints**:
- `GET /Coverage/coverages` - Lists all coverages
- `GET /Coverage/{id}` - Retrieves a coverage by ID
- `GET /Coverage/type/{coverageType}` - Filters coverages by type
- `POST /Coverage` - Creates new coverage
- `PUT /Coverage/{id}` - Updates existing coverage
- `DELETE /Coverage/{id}` - Deletes coverage

**File**: `Src/HealthPlan.API/Controllers/CoverageController.cs`

---

### 3. QuoteController
**Description**: Manages health plan quotes.

**Base Route**: `/Quote`

**Endpoints**:
- `GET /Quote/quotes` - Lists all quotes
- `GET /Quote/{id}` - Retrieves quote by ID
- `POST /Quote` - Creates new quote
- `PUT /Quote/{id}` - Updates quote
- `DELETE /Quote/{id}` - Deletes quote

**File**: `Src/HealthPlan.API/Controllers/QuoteController.cs`

---

### 4. HealthPlanController
**Description**: Manages health plans.

**Base Route**: `/HealthPlan`

**Endpoints**:
- `GET /HealthPlan/healthplans` - Lists all plans
- `GET /HealthPlan/{id}` - Retrieves plan by ID
- `GET /HealthPlan/company/{companyId}` - Filters plans by insurance company
- `GET /HealthPlan/code/{code}` - Searches plan by code
- `POST /HealthPlan` - Creates new plan
- `PUT /HealthPlan/{id}` - Updates plan
- `DELETE /HealthPlan/{id}` - Deletes plan

**File**: `Src/HealthPlan.API/Controllers/HealthPlanController.cs`

---

### 5. CompanyController
**Description**: Manages health plan insurance companies.

**Base Route**: `/Company`

**Endpoints**:
- `GET /Company/companies` - Lists all companies
- `GET /Company/{id}` - Retrieves company by ID
- `GET /Company/cnpj/{cnpj}` - Searches company by CNPJ (Brazilian tax ID)
- `POST /Company` - Creates new company
- `PUT /Company/{id}` - Updates company
- `DELETE /Company/{id}` - Deletes company

**File**: `Src/HealthPlan.API/Controllers/CompanyController.cs`

---

### 6. BeneficiaryController
**Description**: Manages plan beneficiaries.

**Base Route**: `/Beneficiary`

**Endpoints**:
- `GET /Beneficiary/beneficiaries` - Lists all beneficiaries
- `GET /Beneficiary/{id}` - Retrieves beneficiary by ID
- `GET /Beneficiary/cpf/{cpf}` - Searches beneficiary by CPF (Brazilian ID)
- `POST /Beneficiary` - Creates new beneficiary
- `PUT /Beneficiary/{id}` - Updates beneficiary
- `DELETE /Beneficiary/{id}` - Deletes beneficiary

**File**: `Src/HealthPlan.API/Controllers/BeneficiaryController.cs`

---

### 7. AgeRangeController
**Description**: Manages age ranges for price calculation.

**Base Route**: `/AgeRange`

**Endpoints**:
- `GET /AgeRange/age-ranges` - Lists all age ranges
- `GET /AgeRange/{id}` - Retrieves age range by ID
- `POST /AgeRange` - Creates new age range
- `PUT /AgeRange/{id}` - Updates age range
- `DELETE /AgeRange/{id}` - Deletes age range

**File**: `Src/HealthPlan.API/Controllers/AgeRangeController.cs`

---

### 8. AccommodationController
**Description**: Manages hospital accommodation types.

**Base Route**: `/Accommodation`

**Endpoints**:
- `GET /Accommodation/accommodations` - Lists all accommodations
- `GET /Accommodation/{id}` - Retrieves accommodation by ID
- `GET /Accommodation/type/{type}` - Filters accommodations by type
- `POST /Accommodation` - Creates new accommodation
- `PUT /Accommodation/{id}` - Updates accommodation
- `DELETE /Accommodation/{id}` - Deletes accommodation

**File**: `Src/HealthPlan.API/Controllers/AccommodationController.cs`

---

### 9. AcceptanceRuleController
**Description**: Manages plan acceptance rules.

**Base Route**: `/AcceptanceRule`

**Endpoints**:
- `GET /AcceptanceRule/acceptance-rules` - Lists all acceptance rules
- `GET /AcceptanceRule/{id}` - Retrieves rule by ID
- `GET /AcceptanceRule/health-plan/{healthPlanId}` - Filters rules by plan
- `POST /AcceptanceRule` - Creates new rule
- `PUT /AcceptanceRule/{id}` - Updates rule
- `DELETE /AcceptanceRule/{id}` - Deletes rule

**File**: `Src/HealthPlan.API/Controllers/AcceptanceRuleController.cs`

---

### 10. QuoteHistoryController
**Description**: Manages quote history.

**Base Route**: `/QuoteHistory`

**Endpoints**:
- `GET /QuoteHistory/quote-histories` - Lists all history
- `GET /QuoteHistory/{id}` - Retrieves history by ID
- `GET /QuoteHistory/quote/{quoteId}` - Filters history by quote
- `POST /QuoteHistory` - Creates new history record
- `PUT /QuoteHistory/{id}` - Updates history
- `DELETE /QuoteHistory/{id}` - Deletes history

**File**: `Src/HealthPlan.API/Controllers/QuoteHistoryController.cs`

---

### 11. TaxaAdesaoController
**Description**: Manages plan adhesion fees.

**Base Route**: `/TaxaAdesao`

**Endpoints**:
- `GET /TaxaAdesao/taxas-adesao` - Lists all fees
- `GET /TaxaAdesao/{id}` - Retrieves fee by ID
- `POST /TaxaAdesao` - Creates new fee
- `PUT /TaxaAdesao/{id}` - Updates fee
- `DELETE /TaxaAdesao/{id}` - Deletes fee

**File**: `Src/HealthPlan.API/Controllers/TaxaAdesaoController.cs`

---

### 12. DescontoPromocionalController
**Description**: Manages promotional discounts.

**Base Route**: `/DescontoPromocional`

**Endpoints**:
- `GET /DescontoPromocional/descontos-promocionais` - Lists all discounts
- `GET /DescontoPromocional/{id}` - Retrieves discount by ID
- `POST /DescontoPromocional` - Creates new discount
- `PUT /DescontoPromocional/{id}` - Updates discount
- `DELETE /DescontoPromocional/{id}` - Deletes discount

**File**: `Src/HealthPlan.API/Controllers/DescontoPromocionalController.cs`

---

### 13. CoparticipacaoProcedimentoController
**Description**: Manages procedure co-participation.

**Base Route**: `/CoparticipacaoProcedimento`

**Endpoints**:
- `GET /CoparticipacaoProcedimento/coparticipacoes` - Lists all co-participations
- `GET /CoparticipacaoProcedimento/{id}` - Retrieves co-participation by ID
- `POST /CoparticipacaoProcedimento` - Creates new co-participation
- `PUT /CoparticipacaoProcedimento/{id}` - Updates co-participation
- `DELETE /CoparticipacaoProcedimento/{id}` - Deletes co-participation

**File**: `Src/HealthPlan.API/Controllers/CoparticipacaoProcedimentoController.cs`

---

### 14. PlanPriceRangeController
**Description**: Manages plan prices by age range.

**Base Route**: `/PlanPriceRange`

**Endpoints**:
- `GET /PlanPriceRange` - Lists all prices
- `GET /PlanPriceRange/{id}` - Retrieves price by ID
- `POST /PlanPriceRange` - Creates new price
- `PUT /PlanPriceRange` - Updates price
- `DELETE /PlanPriceRange/{id}` - Deletes price

**File**: `Src/HealthPlan.API/Controllers/PlanPriceRangeController.cs`

---

## 🔧 Main Classes

### 1. ApplicationConstants
**Location**: 
- `Src/HealthPlan.API/Constants/ApplicationConstants.cs`
- `Src/HealthPlan.Quote/Constants/ApplicationConstants.cs`

**Description**: Defines constants used throughout the application.

**Main Constants**:
- `DefaultCreatedByUser`: Default user for record creation
- `DefaultConnectionStringName`: Default connection string name
- `ClaimTypes.Permission`: Claim type for permissions
- `Environment.Production/Development`: Execution environments
- `Cors.AllowAllPolicy`: CORS policy
- `Api.Title`, `Api.Version`: API information
- `Api.SwaggerEndpoint`: Swagger endpoint

**Purpose**: Centralize constant values and application configurations, facilitating maintenance and standardization.

---

### 2. BaseApiContext
**Location**: `Src/HealthPlan.API/Data/BaseApiContext.cs`

**Description**: Abstract base class for API database contexts.

**Features**:
- Inherits from Entity Framework Core `DbContext`
- Automatic database connection configuration
- MySQL support in production
- InMemoryDatabase support for tests
- Automatic data model loading via `ApplicationContext`

**Responsibilities**:
- Manage database connections
- Apply Entity Framework configurations
- Facilitate testing with in-memory database

---

### 3. SucessDetails
**Location**: `Src/HealthPlan.API/Swagger/SucessDetails.cs`

**Description**: Class for standardizing API success responses.

**Properties**:
- `Status`: HTTP status code (inherits from ProblemDetails)
- `Title`: Response title
- `Detail`: Additional details
- `Type`: RFC URI defining the response type
- `Data`: Object with response data
- `Instance`: Request path

**Usage**: Return consistent and standardized responses in success endpoints.

**Factory Example**: `SuccessResponseExampleFactory.ForSuccess()` creates configured instances.

---

### 4. Utils
**Location**: `Src/HealthPlan.API/Util/Utils.cs`

**Description**: Utility class with helper methods.

**Main Methods**:
- `GetConnectionString()`: Returns the appropriate connection string
  - Automatically detects test environment
  - Returns InMemoryDatabase for tests
  - Returns configured connection string for production/development

**Purpose**: Provide reusable helper functions in different parts of the application.

---

### 5. Other Important Classes

#### ProblemDetailsExampleFactory
**Location**: `Src/HealthPlan.API/Swagger/ProblemDetailsExampleFactory.cs`

**Description**: Factory for creating standardized error responses.

**Methods**:
- `ForBadRequest()`: Validation errors (400)
- `ForUnauthorized()`: Authorization errors (401)
- `ForNotFound()`: Resources not found (404)
- `ForConflict()`: Data conflicts (409)
- `ForInternalServerError()`: Internal errors (500)

#### CleanTemplateApplicationMapperInitializer
**Location**: `Src/HealthPlan.Quote/Mapping/`

**Description**: Initializes and configures AutoMapper for mapping between DTOs and domain entities.

#### Route Classes
**Location**: `Src/HealthPlan.API/Swagger/*Routes.cs`

**Description**: Define route constants for each controller, ensuring consistency and facilitating refactoring.

---

## 🏗️ Project Organization

The HealthPlan Suite project follows **Clean Architecture** principles, promoting separation of concerns, testability, and maintainability.

### Architecture Layers

```
HealthPlanSuite/
├── Src/
│   ├── HealthPlan.API/              # Presentation Layer
│   │   ├── Controllers/             # API Endpoints
│   │   ├── Middleware/              # HTTP Middlewares
│   │   ├── Swagger/                 # API Documentation and Examples
│   │   │   ├── Routes/              # Route Constants
│   │   │   └── Examples/            # Swagger Examples
│   │   ├── Data/                    # API-specific Contexts
│   │   ├── Constants/               # API Constants
│   │   ├── Util/                    # Utilities
│   │   └── Resource/                # Localization Resources
│   │
│   └── HealthPlan.Quote/            # Domain, Application and Infrastructure Layers
│       ├── Domain/                  # Domain Layer
│       │   ├── Interface/           # Entity Interfaces
│       │   └── Implementation/      # Domain Entities
│       │
│       ├── Services/                # Application Layer
│       │   ├── Interface/           # Service Interfaces
│       │   └── Implementation/      # Business Logic
│       │
│       ├── Repository/              # Infrastructure Layer - Data
│       │   ├── Interface/           # Repository Interfaces
│       │   └── Implementation/      # Data Access
│       │
│       ├── Infrastructure/          # Infrastructure Layer
│       │   ├── Data/                # Context Configurations
│       │   └── Implementation/      # EF Core Mappings
│       │
│       ├── DTO/                     # Data Transfer Objects
│       ├── Mapping/                 # AutoMapper Configurations
│       ├── UnitOfWork/              # Unit of Work Pattern
│       ├── Constants/               # Domain Constants
│       └── Validation/              # Validation Rules
│
└── HealthPlan.Test/                 # Test Layer
    ├── Unit/                        # Unit Tests
    ├── Integration/                 # Integration Tests
    └── Helpers/                     # Test Utilities
```

### Applied Principles

#### 1. **Separation of Concerns**
Each layer has well-defined responsibilities:
- **Presentation (API)**: Receives HTTP requests, validates input, returns responses
- **Application (Services)**: Contains business logic and orchestration
- **Domain**: Defines entities and fundamental business rules
- **Infrastructure**: Implements data access and external integrations

#### 2. **Dependency Inversion**
- Inner layers don't depend on outer layers
- Interfaces define contracts between layers
- Dependency injection managed by ASP.NET Core

#### 3. **Single Responsibility**
- Each class has a single responsibility
- Controllers only manage requests/responses
- Services contain business logic
- Repositories manage persistence

#### 4. **Clean Code**
- DTOs separate API data representations from domain
- Automatic mapping with AutoMapper
- Centralized validations
- Standardized error handling

---

## 📚 Design Patterns Used

### 1. Repository Pattern
**Location**: `Src/HealthPlan.Quote/Repository/`

Abstracts data access, allowing:
- Change persistence implementation without affecting business logic
- Facilitate testing with mock repositories
- Centralize queries and data operations

### 2. Unit of Work Pattern
**Location**: `Src/HealthPlan.Quote/UnitOfWork/`

Manages transactions:
- Ensures consistency in multiple operations
- Controls transaction commit/rollback
- Coordinates multiple repositories

### 3. Dependency Injection
Configured in `Program.cs`:
- Registration of services and repositories
- Lifecycle control (Scoped, Singleton, Transient)
- Facilitates testing and decoupling

### 4. DTO Pattern
**Location**: `Src/HealthPlan.Quote/DTO/`

Separates representations:
- `*PayLoadDTO`: Input data (POST/PUT)
- `*ResponseDTO`: Output data (GET)
- Protects domain model
- Controls which data is exposed

### 5. Factory Pattern
**Location**: `Src/HealthPlan.API/Swagger/*Factory.cs`

Creates complex objects:
- `SuccessResponseExampleFactory`: Success responses
- `ProblemDetailsExampleFactory`: Error responses
- Ensures consistency and facilitates maintenance

---

## 🔍 Notes

### Navigating the Code

1. **Explore Controllers**: Start with `Src/HealthPlan.API/Controllers/` to understand available endpoints

2. **Understand Entities**: See `Src/HealthPlan.Quote/Domain/Implementation/` to know the domain model

3. **Review Services**: Analyze `Src/HealthPlan.Quote/Services/Implementation/` for business logic

4. **Check DTOs**: Review `Src/HealthPlan.Quote/DTO/` for input/output structures

5. **Consult Documentation**: Use Swagger at `/swagger` when the application is running

### GitHub Search

To find controllers, classes and specific features:
- Use GitHub code search: Press `/` and type the term
- Filter by file type: `filename:Controller.cs`
- Search for specific classes: `class:ApplicationConstants`
- Find interfaces: `interface:IService`

### Useful Links

- **Complete Architecture**: [docs/ARCHITECTURE.md](./ARCHITECTURE.md)
- **Development Guide**: [docs/DEVELOPMENT.md](./DEVELOPMENT.md)
- **API Documentation**: [docs/API.md](./API.md)
- **Testing Guide**: [docs/TESTING.md](./TESTING.md)
- **Quick Start**: [docs/QUICK_START.md](./QUICK_START.md)

### Contributing

To add new features or modify existing ones:
1. Read [CONTRIBUTING.md](../CONTRIBUTING.md) for guidelines
2. Follow established architectural patterns
3. Maintain coherence with existing code
4. Add tests for new features
5. Update documentation as needed

---

## 📞 Support

For questions or issues:
- Open an issue on GitHub
- Consult complete documentation in the `docs/` folder
- Review examples in `docs/EXAMPLES.md`

---

**Last Updated**: January 2025
**Document Version**: 1.0
