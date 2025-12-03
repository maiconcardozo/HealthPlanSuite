# Dependency Injection Configuration

Add the following service registrations to your Startup.cs or Program.cs file to enable the new entities:

## Service Registrations

```csharp
// Repository registrations
services.AddScoped<ITaxaAdesaoRepository, TaxaAdesaoRepository>();
services.AddScoped<IDescontoPromocionalRepository, DescontoPromocionalRepository>();
services.AddScoped<ICoparticipacaoProcedimentoRepository, CoparticipacaoProcedimentoRepository>();
services.AddScoped<IPlanPriceRangeRepository, PlanPriceRangeRepository>();

// Service registrations
services.AddScoped<ITaxaAdesaoService, TaxaAdesaoService>();
services.AddScoped<IDescontoPromocionalService, DescontoPromocionalService>();
services.AddScoped<ICoparticipacaoProcedimentoService, CoparticipacaoProcedimentoService>();
services.AddScoped<IPlanPriceRangeService, PlanPriceRangeService>();
```

## Using Statements Required

Add these using statements to the top of your Startup.cs or Program.cs:

```csharp
using HealthPlan.Quote.Services.Interface;
using HealthPlan.Quote.Services.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Repository.Implementation;
```

These registrations ensure that the dependency injection container can resolve the dependencies for all the new controllers and enable the full functionality of the implemented entities.