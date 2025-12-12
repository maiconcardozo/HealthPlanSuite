# 🔧 Troubleshooting Guide

This guide addresses the most common problems encountered when configuring and using the Authentication Service, with detailed solutions and prevention tips.

## 🚨 Installation and Configuration Issues

### ❌ Database Connection Error

**Error:**
```
Unable to connect to any of the specified MySQL hosts
MySqlConnector.MySqlException: Unable to connect to server
```

**Possible Causes:**
1. MySQL is not running
2. Incorrect connection string
3. Firewall blocking connection
4. Invalid credentials

**Solutions:**

#### 1. Check MySQL Status
```bash
# Linux/Mac
sudo systemctl status mysql
# ou
brew services list | grep mysql

# Windows
net start | findstr mysql
# ou via Services.msc
```

#### 2. Test Manual Connection
```bash
mysql -h localhost -u authuser -p
# Enter password when prompted
```

#### 3. Verify Connection String
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=AuthenticationDB;Uid=authuser;Pwd=password123;SslMode=none;AllowPublicKeyRetrieval=true;"
  }
}
```

#### 4. Recreate Database User
```sql
DROP USER IF EXISTS 'authuser'@'localhost';
CREATE USER 'authuser'@'localhost' IDENTIFIED BY 'password123';
GRANT ALL PRIVILEGES ON AuthenticationDB.* TO 'authuser'@'localhost';
FLUSH PRIVILEGES';
```

### ❌ Entity Framework Migration Error

**Error:**
```
Unable to create an object of type 'ApiContextDevelopment'
No database provider has been configured for this DbContext
```

**Solutions:**

#### 1. Verificar Context Registration
```csharp
// No Program.cs ou Startup.cs
builder.Services.AddDbContext<ApiContextDevelopment>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
```

#### 2. Run Migration with Verbose
```bash
cd Src/Authentication.API
dotnet ef database update --context ApiContextDevelopment --verbose
```

#### 3. Regenerate Migration if Necessary
```bash
# Remove corrupted migration
dotnet ef migrations remove --context ApiContextDevelopment

# Create new migration
dotnet ef migrations add InitialCreate --context ApiContextDevelopment

# Apply migration
dotnet ef database update --context ApiContextDevelopment
```

### ❌ Dependencies/Packages Error

**Error:**
```
Package 'Package.Name' is incompatible with 'net9.0'
Could not load file or assembly 'System.Text.Json'
```

**Solutions:**

#### 1. Clear Cache and Restore
```bash
dotnet nuget locals all --clear
dotnet restore Solution/Authentication.sln --force
dotnet build Solution/Authentication.sln --no-restore
```

#### 2. Check Package Versions
```bash
dotnet list package --outdated
dotnet list package --vulnerable
```

#### 3. Update Specific Packages
```bash
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.13
dotnet add package MySqlConnector --version 2.4.0
```

## 🔐 Authentication and JWT Issues

### ❌ Invalid or Expired Token

**Error:**
```
401 Unauthorized
{"type":"https://tools.ietf.org/html/rfc7231#section-6.3.1","title":"Unauthorized"}
```

**Diagnosis:**

#### 1. Check Token Format
```bash
# Token must be in format: Bearer {token}
curl -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

#### 2. Validate JWT Token Online
- Access [jwt.io](https://jwt.io)
- Paste your token to check structure
- Confirm it hasn't expired

#### 3. Verify JWT Configuration
```json
{
  "JwtSettings": {
    "Issuer": "Authentication",
    "Audience": "AuthenticationClients", 
    "SecretKey": "super-secret-jwt-key-minimum-32-characters-long",
    "ExpirationMinutes": 60
  }
}
```

**Important:** `SecretKey` must have at least 32 characters.

### ❌ Password Hash Error

**Error:**
```
Argon2 hashing failed
Invalid password verification
```

**Solutions:**

#### 1. Verify Argon2 Implementation
```csharp
// Correct
var hashedPassword = StringHelper.ComputeArgon2Hash(plainPassword);
var isValid = StringHelper.VerifyArgon2Hash(plainPassword, hashedPassword);
```

#### 2. Verify Encoding
Make sure passwords are in UTF-8:
```csharp
var passwordBytes = Encoding.UTF8.GetBytes(password);
```

### ❌ Claims Don't Appear in Token

**Problem:** Token is generated but doesn't contain expected claims/permissions.

**Diagnosis:**

#### 1. Check RBAC Mapping
```sql
-- Check if user has associated claims
SELECT a.UserName, c.Value, ac.Name 
FROM Account a 
JOIN AccountClaimAction aca ON a.Id = aca.IdAccount
JOIN ClaimAction ca ON aca.IdClaimAction = ca.Id  
JOIN Claim c ON ca.IdClaim = c.Id
JOIN Action ac ON ca.IdAction = ac.Id
WHERE a.UserName = 'admin';
```

#### 2. Debug Token Generation
```csharp
// Adicione logs no AccountService.GenerateToken
_logger.LogDebug("User {UserName} has {ClaimCount} claims", 
    account.UserName, accountClaimActions.Count);

foreach(var claim in accountClaimActions)
{
    _logger.LogDebug("Adding claim: {Claim}:{Action}", 
        claim.ClaimAction.Claim.Value, claim.ClaimAction.Action.Name);
}
```

## 🌐 API and HTTP Issues

### ❌ CORS Errors

**Error:**
```
Access to fetch at 'https://localhost:7001' from origin 'http://localhost:3000' 
has been blocked by CORS policy
```

**Solutions:**

#### 1. Verify CORS Configuration
```csharp
// Program.cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://yourfrontend.com")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // If using cookies
    });
});

// Apply middleware
app.UseCors("AllowSpecificOrigin");
```

#### 2. Middleware Order
```csharp
// Correct order
app.UseRouting();
app.UseCors(); // BEFORE UseAuthorization
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
```

### ❌ SSL/HTTPS Issues

**Error:**
```
The SSL connection could not be established
Certificate validation failed
```

**Solutions:**

#### 1. Local Development
```bash
# Trust development certificate
dotnet dev-certs https --trust
```

#### 2. Configure HTTP for Development
```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:5000"
      },
      "Https": {
        "Url": "https://localhost:5001"
      }
    }
  }
}
```

### ❌ Swagger Doesn't Load

**Error:**
```
Failed to load API definition
Swagger UI not accessible
```

**Solutions:**

#### 1. Verify Swagger Configuration
```csharp
// Program.cs
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);
});

// Only in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

#### 2. Verify XML Documentation
```xml
<!-- Authentication.API.csproj -->
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
  <DocumentationFile>bin\Debug\net9.0\Authentication.API.xml</DocumentationFile>
</PropertyGroup>
```

## 🧪 Testing Issues

### ❌ Tests Failing

**Error:**
```
Failed: Test method TestName threw exception
Connection string not found
```

**Solutions:**

#### 1. Configure Test Settings
```json
// appsettings.Testing.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=AuthenticationDB_Test;Uid=authuser;Pwd=password123;"
  }
}
```

#### 2. Mock Dependencies
```csharp
[Test]
public void Should_Authenticate_Valid_User()
{
    // Arrange
    var mockRepository = new Mock<IAccountRepository>();
    var mockUnitOfWork = new Mock<ILoginUnitOfWork>();
    
    mockRepository.Setup(r => r.GetByUserName("admin"))
               .Returns(new Account { UserName = "admin", Password = hashedPassword });
    
    mockUnitOfWork.Setup(u => u.AccountRepository).Returns(mockRepository.Object);
    
    var service = new AccountService(mockUnitOfWork.Object);
    
    // Act & Assert
    var result = service.GetAccountByUserNameAndPassword(account);
    Assert.IsNotNull(result);
}
```

### ❌ Integration Tests Failing

**Error:**
```
Database connection failed during integration test
Service not registered
```

**Solutions:**

#### 1. Test Database Setup
```csharp
public class IntegrationTestBase : IDisposable
{
    protected readonly TestServer Server;
    protected readonly HttpClient Client;
    
    public IntegrationTestBase()
    {
        var builder = new WebApplicationBuilder();
        
        // Use in-memory database for tests
        builder.Services.AddDbContext<ApiContextDevelopment>(options =>
            options.UseInMemoryDatabase("TestDatabase"));
        
        var app = builder.Build();
        Server = new TestServer(app);
        Client = Server.CreateClient();
    }
}
```

## 📊 Performance Issues

### ❌ Slow API

**Symptoms:**
- Endpoints take more than 2-3 seconds
- Frequent timeouts
- High CPU/memory usage

**Diagnosis:**

#### 1. Enable Detailed Logging
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  }
}
```

#### 2. Analyze SQL Queries
```bash
# Logs will show executed queries
dotnet run --verbosity detailed
```

#### 3. Monitor Performance
```csharp
// Add middleware for timing
app.Use(async (context, next) =>
{
    var stopwatch = Stopwatch.StartNew();
    await next();
    stopwatch.Stop();
    
    var responseTime = stopwatch.ElapsedMilliseconds;
    context.Response.Headers.Add("X-Response-Time", $"{responseTime}ms");
});
```

**Optimizations:**

#### 1. Database Indexing
```sql
-- Add indexes for frequently queried columns
CREATE INDEX idx_account_username ON Account(UserName);
CREATE INDEX idx_account_created ON Account(DtCreated);
```

#### 2. Connection Pooling
```csharp
builder.Services.AddDbContext<ApiContextDevelopment>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure(3)));
```

## 🛠️ Debug Tools

### 📈 Monitoring
```bash
# Application insights
dotnet add package Microsoft.ApplicationInsights.AspNetCore

# Health checks
dotnet add package Microsoft.Extensions.Diagnostics.HealthChecks
```

### 🔍 Logging
```csharp
// Structured logging with Serilog
builder.Host.UseSerilog((context, config) =>
    config.WriteTo.Console()
          .WriteTo.File("logs/app-.txt", rollingInterval: RollingInterval.Day));
```

### 📊 Profiling
```bash
# dotTrace (JetBrains)
# PerfView (Microsoft)
# Application Insights Profiler
```

## 📞 When to Seek Help

### 🆘 Before Opening an Issue
1. ✅ Consulted this troubleshooting guide
2. ✅ Checked detailed logs
3. ✅ Tested in clean environment
4. ✅ Reproduced the problem consistently

### 📝 Information to Include
```
**Environment:**
- OS: Windows 11 / Ubuntu 22.04 / macOS 13
- .NET Version: 8.0.118
- MySQL Version: 8.0.35
- IDE: Visual Studio 2022 17.8.0

**Issue:**
[Clear problem description]

**Steps to Reproduce:**
1. [Step 1]
2. [Step 2]
3. [Problem occurs]

**Expected vs Actual:**
Expected: [Expected behavior]
Actual: [What actually happened]

**Logs:**
[Paste relevant logs here]

**Configuration:**
[Relevant appsettings.json]
```

### 🔗 Help Resources
- **GitHub Issues**: [Create new issue](https://github.com/maiconcardozo/HealthPlanSuite/issues)
- **Stack Overflow**: Tag `healthplan-suite`
- **Documentation**: [docs/](../docs/)
- **Community**: GitHub Discussions

---

💡 **Tip**: Maintain structured logs and monitor metrics to detect problems before they affect end users.