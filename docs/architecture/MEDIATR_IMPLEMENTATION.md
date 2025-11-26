# MediatR Implementation Guide

## Overview

This document describes the implementation of the Mediator pattern using MediatR library across the HealthPlanSuite project, following Clean Architecture and Domain-Driven Design (DDD) principles.

## Technology Stack

- **.NET**: 8.0
- **MediatR**: 12.4.1
- **FluentValidation**: 12.0.0
- **Entity Framework Core**: 8.0.11
- **AutoMapper**: 13.0.1

## Architecture Goals

- **Decouple** controllers from application services
- **Centralize** cross-cutting concerns (validation, logging, transactions)
- **Implement** CQRS pattern (Command Query Responsibility Segregation)
- **Improve** testability and maintainability
- **Follow** Clean Architecture and DDD best practices

## Project Structure

```
Src/HealthPlan.Quote/Application/
├── Commands/
│   └── Quote/
│       ├── CreateQuoteCommand.cs
│       ├── CreateQuoteCommandHandler.cs
│       ├── CreateQuoteCommandValidator.cs
│       ├── UpdateQuoteCommand.cs
│       ├── UpdateQuoteCommandHandler.cs
│       ├── DeleteQuoteCommand.cs
│       └── DeleteQuoteCommandHandler.cs
├── Queries/
│   └── Quote/
│       ├── GetAllQuotesQuery.cs
│       ├── GetAllQuotesQueryHandler.cs
│       ├── GetQuoteByIdQuery.cs
│       └── GetQuoteByIdQueryHandler.cs
├── Behaviors/
│   ├── ValidationBehavior.cs
│   ├── LoggingBehavior.cs
│   └── TransactionBehavior.cs
└── ...
```

## Key Components

### 1. Commands

Commands represent **write operations** that change the system state.

**Example: CreateQuoteCommand**

```csharp
public class CreateQuoteCommand : IRequest<QuoteResponseDTO>
{
    public int IdCompany { get; set; }
    public int IdBeneficiary { get; set; }
    public int IdHealthPlan { get; set; }
    public int IdAgeRange { get; set; }
    public decimal MonthlyPremium { get; set; }
    public DateTime ValidUntil { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
```

### 2. Command Handlers

Handlers contain the business logic for executing commands.

**Example: CreateQuoteCommandHandler**

```csharp
public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, QuoteResponseDTO>
{
    private readonly IApplicationUnitOfWork unitOfWork;

    public CreateQuoteCommandHandler(IApplicationUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public Task<QuoteResponseDTO> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
    {
        // Business logic here
        // Transaction is handled automatically by TransactionBehavior
    }
}
```

### 3. Queries

Queries represent **read operations** that don't modify state.

**Example: GetAllQuotesQuery**

```csharp
public class GetAllQuotesQuery : IRequest<IEnumerable<QuoteResponseDTO>>
{
}
```

### 4. Query Handlers

Handlers for retrieving data.

**Example: GetAllQuotesQueryHandler**

```csharp
public class GetAllQuotesQueryHandler : IRequestHandler<GetAllQuotesQuery, IEnumerable<QuoteResponseDTO>>
{
    private readonly IApplicationUnitOfWork unitOfWork;

    public GetAllQuotesQueryHandler(IApplicationUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<QuoteResponseDTO>> Handle(GetAllQuotesQuery request, CancellationToken cancellationToken)
    {
        var quotes = unitOfWork.QuoteRepository.GetAll();
        // Map and return
    }
}
```

### 5. Validators

FluentValidation validators for commands and queries.

**Example: CreateQuoteCommandValidator**

```csharp
public class CreateQuoteCommandValidator : AbstractValidator<CreateQuoteCommand>
{
    public CreateQuoteCommandValidator()
    {
        RuleFor(x => x.IdCompany)
            .GreaterThan(0).WithMessage("Company ID must be greater than 0");

        RuleFor(x => x.IdBeneficiary)
            .GreaterThan(0).WithMessage("Beneficiary ID must be greater than 0");

        RuleFor(x => x.MonthlyPremium)
            .GreaterThan(0).WithMessage("Monthly Premium must be greater than 0");
    }
}
```

### 6. Pipeline Behaviors

Behaviors intercept requests and add cross-cutting concerns.

#### ValidationBehavior

Automatically validates all requests using FluentValidation:

```csharp
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    // Validates request before reaching handler
    // Throws ValidationException if validation fails
}
```

#### LoggingBehavior

Logs all requests and their execution time:

```csharp
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    // Logs request name, execution time, and errors
}
```

#### TransactionBehavior

Wraps commands in database transactions:

```csharp
public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    // Only applies to commands (not queries)
    // Automatically commits on success
    // Rolls back on exception
}
```

## Configuration

### Startup.cs

```csharp
// Register MediatR with all behaviors
services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateQuoteCommand).Assembly);
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
});

// Register validators
services.AddTransient<IValidator<CreateQuoteCommand>, CreateQuoteCommandValidator>();
```

## Controller Usage

### Before (Using Services Directly)

```csharp
[HttpPost]
public async Task<IActionResult> CreateQuote([FromBody] QuotePayLoadDTO dto)
{
    var validationResult = await ValidationHelper.ValidateEntityAsync(dto, serviceProvider, this);
    if (validationResult != null) return validationResult;

    var quote = Mapper.Map<Quote>(dto);
    
    try
    {
        quoteService.AddQuote(quote);
        unitOfWork.SaveChanges();
        var response = Mapper.Map<QuoteResponseDTO>(quote);
        return Ok(response);
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

### After (Using MediatR)

```csharp
private readonly IMediator mediator;

public QuoteController(IMediator mediator)
{
    this.mediator = mediator;
}

[HttpPost]
public async Task<IActionResult> CreateQuote([FromBody] CreateQuoteCommand command)
{
    try
    {
        // Validation, logging, and transaction are handled automatically
        var response = await mediator.Send(command);
        return Ok(response);
    }
    catch (ValidationException ex)
    {
        return BadRequest(ex.Errors);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}
```

## Benefits

### 1. **Separation of Concerns**
- Controllers only handle HTTP concerns
- Business logic is in handlers
- Cross-cutting concerns are in behaviors

### 2. **Testability**
- Handlers can be tested independently
- Easy to mock dependencies
- Behaviors can be tested in isolation

### 3. **Consistency**
- All commands/queries follow the same pattern
- Validation, logging, and transactions are consistent
- Error handling is centralized

### 4. **Maintainability**
- Easy to find where business logic lives
- Clear separation between reads and writes (CQRS)
- New features follow established patterns

### 5. **Extensibility**
- Add new behaviors without changing existing code
- Add new commands/queries without touching controllers
- Easy to add middleware-like functionality

## Adding New Commands/Queries

### Step 1: Create Command/Query

```csharp
// Commands/YourEntity/YourCommand.cs
public class YourCommand : IRequest<YourResponseDTO>
{
    public string Property { get; set; }
}
```

### Step 2: Create Handler

```csharp
// Commands/YourEntity/YourCommandHandler.cs
public class YourCommandHandler : IRequestHandler<YourCommand, YourResponseDTO>
{
    private readonly IApplicationUnitOfWork unitOfWork;

    public YourCommandHandler(IApplicationUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<YourResponseDTO> Handle(YourCommand request, CancellationToken cancellationToken)
    {
        // Business logic
        return result;
    }
}
```

### Step 3: Create Validator (Optional)

```csharp
// Commands/YourEntity/YourCommandValidator.cs
public class YourCommandValidator : AbstractValidator<YourCommand>
{
    public YourCommandValidator()
    {
        RuleFor(x => x.Property).NotEmpty();
    }
}
```

### Step 4: Register Validator in Startup

```csharp
services.AddTransient<IValidator<YourCommand>, YourCommandValidator>();
```

### Step 5: Use in Controller

```csharp
[HttpPost]
public async Task<IActionResult> YourAction([FromBody] YourCommand command)
{
    var result = await mediator.Send(command);
    return Ok(result);
}
```

## Best Practices

### Commands
- ✅ Use for write operations
- ✅ Name with verb: CreateXCommand, UpdateXCommand
- ✅ Include all required data
- ✅ Return DTOs, not entities

### Queries
- ✅ Use for read operations
- ✅ Name with Get: GetXByIdQuery, GetAllXQuery
- ✅ No side effects
- ✅ Can bypass UnitOfWork for performance

### Handlers
- ✅ Single responsibility
- ✅ No business logic in controllers
- ✅ Use async/await properly
- ✅ Handle cancellation tokens

### Validators
- ✅ Validate structure and business rules
- ✅ Use clear error messages
- ✅ Don't query database for validation (do in handler)

## Troubleshooting

### Handler Not Found
- Ensure handler is in the same assembly as command
- Check MediatR registration in Startup

### Validation Not Working
- Verify validator is registered in DI
- Check ValidationBehavior is added to pipeline

### Transaction Not Committing
- Ensure command name ends with "Command"
- Check TransactionBehavior is added to pipeline
- Verify no exceptions are thrown

## References

- [MediatR Documentation](https://github.com/jbogard/MediatR)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern](https://martinfowler.com/bliki/CQRS.html)
- [FluentValidation](https://fluentvalidation.net/)

## Last Updated

Created: 2025-11-26
Version: 1.0
