# 🤝 Contributing Guide

Thank you for your interest in contributing to the HealthPlan Suite project!

## 📋 Getting Started

### Prerequisites

- .NET 8.0 SDK
- MySQL 8.0+ (for integration tests)
- Git
- Your favorite IDE (Visual Studio, VS Code, or JetBrains Rider)

### Setting Up Your Development Environment

1. **Fork the repository**
   ```bash
   # Fork via GitHub UI, then clone your fork
   git clone https://github.com/YOUR_USERNAME/HealthPlanSuite.git
   cd HealthPlanSuite
   ```

2. **Add upstream remote**
   ```bash
   git remote add upstream https://github.com/maiconcardozo/HealthPlanSuite.git
   ```

3. **Install dependencies**
   ```bash
   dotnet restore Solution/HealthPlanSuite.sln
   ```

4. **Build the project**
   ```bash
   dotnet build Solution/HealthPlanSuite.sln
   ```

5. **Run tests**
   ```bash
   dotnet test Solution/HealthPlanSuite.sln
   ```

## 🔄 Development Workflow

### Creating a Feature Branch

```bash
git checkout main
git pull upstream main
git checkout -b feature/your-feature-name
```

### Making Changes

1. Make your changes
2. Write or update tests
3. Update documentation if needed
4. Ensure all tests pass
5. Check code formatting

### Committing Changes

Follow conventional commits format:
- `feat:` - New feature
- `fix:` - Bug fix
- `docs:` - Documentation changes
- `refactor:` - Code refactoring
- `test:` - Test changes
- `chore:` - Maintenance tasks

Example:
```bash
git commit -m "feat: add health plan comparison endpoint"
```

### Submitting a Pull Request

1. Push your changes to your fork
   ```bash
   git push origin feature/your-feature-name
   ```

2. Create a Pull Request via GitHub UI

3. Fill in the PR template with:
   - Description of changes
   - Related issues
   - Testing performed
   - Screenshots (if UI changes)

## 📝 Code Standards

### General Guidelines

- Follow existing code style and patterns
- Use meaningful variable and method names
- Write self-documenting code
- Add comments only when necessary

### C# Specific

- Use `var` when type is obvious
- Prefer expression-bodied members when appropriate
- Follow SOLID principles
- **Avoid else statements** - use conditional expressions

### Testing

- Write unit tests for all business logic
- Write integration tests for API endpoints
- Maintain >80% code coverage
- Use FluentAssertions for readable assertions

## 🔍 Code Review Process

All contributions go through code review:

1. **Automated checks** - CI/CD pipeline validates build, tests, and code quality
2. **Human review** - Maintainers review code changes
3. **Feedback** - Address review comments
4. **Approval** - Once approved, changes are merged

## 📚 Additional Resources

- [Development Guide](DEVELOPMENT.md)
- [Testing Guide](TESTING.md)
- [Architecture Guide](../architecture/ARCHITECTURE.md)
- [API Documentation](../api/API.md)

## 🙋 Getting Help

- Open an issue for bugs or feature requests
- Start a discussion for questions
- Check existing documentation first

Thank you for contributing to HealthPlan Suite! 🎉
