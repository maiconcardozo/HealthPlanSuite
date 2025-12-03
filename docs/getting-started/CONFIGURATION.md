# Configuration Guide

This guide covers all configuration options for the HealthPlan Suite.

## Configuration Files

- `appsettings.json` - Base configuration
- `appsettings.Development.json` - Development overrides
- `appsettings.Production.json` - Production settings

## Database Configuration

### Connection String

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HealthPlanDB;Uid=root;Pwd=password;"
  }
}
```

### Database Providers

The application supports:
- MySQL 8.0+
- MariaDB 10.5+

## JWT Authentication

```json
{
  "Jwt": {
    "Key": "your-secret-key-here",
    "Issuer": "HealthPlanSuite",
    "Audience": "HealthPlanSuiteUsers",
    "ExpirationMinutes": 60
  }
}
```

## Logging Configuration

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Warning"
    }
  }
}
```

## CORS Configuration

Configure CORS in `Program.cs` for allowed origins:

```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
```

## Environment Variables

Override settings with environment variables:

- `ASPNETCORE_ENVIRONMENT` - Set environment (Development, Production)
- `ConnectionStrings__DefaultConnection` - Override connection string
- `Jwt__Key` - Override JWT secret key

## Swagger Configuration

Swagger is enabled by default in Development. To configure:

```json
{
  "Swagger": {
    "Enabled": true,
    "Title": "HealthPlan Suite API",
    "Version": "v1"
  }
}
```

## Related Documentation

- [Installation Guide](INSTALLATION.md)
- [Quick Start Guide](QUICK_START.md)
- [Deployment Guide](../guides/DEPLOYMENT.md)
