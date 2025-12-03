# Frequently Asked Questions (FAQ)

## General Questions

### What is HealthPlan Suite?

HealthPlan Suite is a comprehensive .NET application for managing health plan quotes and related insurance operations. It follows Clean Architecture principles and includes complete health plan management functionality.

### What version of .NET is required?

The project requires **.NET 8.0 SDK**. You can download it from [Microsoft's website](https://dotnet.microsoft.com/download/dotnet/8.0).

### What database is supported?

The application supports MySQL 8.0+ and MariaDB 10.5+.

## Development Questions

### How do I run the tests?

```bash
dotnet test Solution/HealthPlanSuite.sln
```

Or use the convenience scripts:
```bash
scripts/test.sh        # Linux/Mac
scripts/test.bat       # Windows
```

### How do I build the project?

```bash
dotnet build Solution/HealthPlanSuite.sln --configuration Release
```

### Where is the API documentation?

After running the application, access Swagger UI at https://localhost:7001

## Troubleshooting

### Build errors related to .NET version

Make sure you have .NET 8.0 SDK installed:
```bash
dotnet --version
```

If not, download and install from: https://dotnet.microsoft.com/download/dotnet/8.0

### Database connection errors

1. Verify MySQL is running
2. Check connection string in `appsettings.Development.json`
3. Ensure database exists and user has permissions

### Tests failing

1. Clean and rebuild: `dotnet clean && dotnet build`
2. Check if database migrations are up to date
3. Review test logs for specific errors

## Getting Help

- Check the [Troubleshooting Guide](TROUBLESHOOTING.md)
- Open an issue on GitHub
- Start a discussion in the repository

## Related Documentation

- [Installation Guide](../getting-started/INSTALLATION.md)
- [Quick Start Guide](../getting-started/QUICK_START.md)
- [Development Guide](../guides/DEVELOPMENT.md)
