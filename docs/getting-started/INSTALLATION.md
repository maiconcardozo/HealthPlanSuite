# Installation Guide

This guide covers the installation process for the HealthPlan Suite.

## Prerequisites

Before installing, ensure you have:

- **.NET 8.0 SDK** or higher
- **MySQL 8.0+** or compatible database
- **Git** for version control

## Installation Steps

### 1. Clone the Repository

```bash
git clone https://github.com/maiconcardozo/HealthPlanSuite.git
cd HealthPlanSuite
```

### 2. Install .NET SDK

Download and install .NET 8.0 SDK from:
https://dotnet.microsoft.com/download/dotnet/8.0

Verify installation:
```bash
dotnet --version
# Should output: 8.0.x
```

### 3. Restore Dependencies

```bash
dotnet restore Solution/HealthPlanSuite.sln
```

### 4. Build the Project

```bash
dotnet build Solution/HealthPlanSuite.sln --configuration Release
```

### 5. Configure Database

Update the connection string in `src/HealthPlan.API/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HealthPlanDB;Uid=your_user;Pwd=your_password;"
  }
}
```

### 6. Run Database Migrations

```bash
cd src/HealthPlan.API
dotnet ef database update
```

### 7. Run the Application

```bash
dotnet run
```

The API will be available at: https://localhost:7001

## Verification

- Open https://localhost:7001 in your browser
- Swagger UI should be displayed with API documentation
- Health check endpoint: https://localhost:7001/health

## Next Steps

- Follow the [Quick Start Guide](QUICK_START.md) for first-time setup
- See [Configuration Guide](CONFIGURATION.md) for advanced settings
- Check [Development Guide](../guides/DEVELOPMENT.md) for development workflow
