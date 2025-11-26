# MediatR Pattern Implementation Guide

## Overview
This project has been partially migrated to use the MediatR pattern for CQRS architecture. Infrastructure is complete with pipeline behaviors.

## Completed Work

### Infrastructure ✅
- MediatR 12.4.1 installed
- Pipeline Behaviors implemented (Validation, Logging, Transaction)
- Folder structure created (Commands, Queries, Behaviors)

### CQRS Implemented ✅
1. **Quote** - Commands: Create | Queries: GetAll, GetById

## Remaining Work

### Controllers Needing Migration
1. **QuoteController** - Partially migrated (CQRS exists, controller needs update)
2. **CoverageController**
3. **CompanyController**
4. **BeneficiaryController**
5. **HealthPlanController**
6. **AgeRangeController**
7. **AccommodationController**
8. **AdhesionFeeController**
9. **PlanCoverageController**
10. **PlanPriceRangeController**
11. **ProcedureCoparticipationController**
12. **PromotionalDiscountController**
13. **QuoteHistoryController**
14. **AcceptanceRuleController**

### Missing CQRS
All entities except Quote need Commands and Queries:
- Coverage
- Company
- Beneficiary
- HealthPlan
- AgeRange
- Accommodation
- AdhesionFee
- PlanCoverage
- PlanPriceRange
- ProcedureCoparticipation
- PromotionalDiscount
- QuoteHistory
- AcceptanceRule

## How to Complete the Migration

### Step 1: Create Missing CQRS Files

For each entity, create these files:

**Commands (3 files per entity):**
- `Create{Entity}Command.cs` and `Create{Entity}CommandHandler.cs`
- `Update{Entity}Command.cs` and `Update{Entity}CommandHandler.cs`
- `Delete{Entity}Command.cs` and `Delete{Entity}CommandHandler.cs`

**Queries (2 files per entity):**
- `GetAll{Entity}sQuery.cs` and `GetAll{Entity}sQueryHandler.cs`
- `Get{Entity}ByIdQuery.cs` and `Get{Entity}ByIdQueryHandler.cs`

**Template to follow:**
See `/Src/HealthPlan.Quote/Application/Commands/Quote/` and `/Src/HealthPlan.Quote/Application/Queries/Quote/` for complete examples.

### Step 2: Migrate Controllers

For each controller, follow this pattern:

**Before:**
```csharp
private readonly I{Entity}Service service;
public {Entity}Controller(I{Entity}Service service) { ... }
```

**After:**
```csharp
private readonly IMediator mediator;
public {Entity}Controller(IMediator mediator) { ... }
```

**Example endpoint migration:**

Before:
```csharp
public IActionResult GetAll()
{
    var items = service.GetAll();
    return Ok(items);
}
```

After:
```csharp
public async Task<IActionResult> GetAll()
{
    var query = new GetAll{Entity}sQuery();
    var items = await mediator.Send(query);
    return Ok(items);
}
```

See `/Src/HealthPlan.API/Controllers/QuoteController.cs` for the current state and expected changes.

### Step 3: Register MediatR in Startup

Add to `Startup.cs` or `Program.cs`:

```csharp
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateQuoteCommand).Assembly);
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
});
```

### Step 4: Test

1. Build solution: `dotnet build Solution/HealthPlan.sln`
2. Run tests: `dotnet test Solution/HealthPlan.sln`
3. Verify all endpoints work

## Benefits of MediatR Pattern

✅ **Decoupling** - Controllers don't depend on service implementations
✅ **Testability** - Handlers can be unit tested independently  
✅ **Transaction Management** - Automatic via TransactionBehavior
✅ **Validation** - Automatic via ValidationBehavior
✅ **Logging** - Automatic via LoggingBehavior
✅ **CQRS** - Clear separation of commands and queries
✅ **Single Responsibility** - Each handler has one job

## Architecture

```
Controller → IMediator → Command/Query → Handler → Repository → Database
                  ↓
              Behaviors (Transaction, Validation, Logging)
```

## Key Files

- **Startup.cs** - MediatR registration and behaviors configuration
- **TransactionBehavior.cs** - Automatic transaction management
- **ValidationBehavior.cs** - FluentValidation integration
- **LoggingBehavior.cs** - Request/response logging

## Notes

**Compilation:** ✅ Solution compiles successfully with no errors.

## Estimation

Completing the remaining work should take approximately 4-6 hours following the established patterns.
