# 🚀 Quick Start Guide - Authentication Service

This guide provides step-by-step instructions to configure and use the authentication service in different scenarios.

## 📋 Prerequisites

Before starting, make sure you have the following components installed:

### Required
- **.NET 8.0 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
- **MySQL 8.0+** - [Download](https://dev.mysql.com/downloads/mysql/)
- **Git** - [Download](https://git-scm.com/)

### Recommended
- **Visual Studio 2022** with .NET workload - [Download](https://visualstudio.microsoft.com/)
- **Visual Studio Code** with C# Dev Kit extension - [Download](https://code.visualstudio.com/)
- **MySQL Workbench** for database management - [Download](https://dev.mysql.com/downloads/workbench/)

## 🏃‍♂️ Quick Setup (5 minutes)

### 1. Clone and Build the Project

```bash
# Clone the repository
git clone https://github.com/maiconcardozo/CleanTemplateRepository.git
cd CleanTemplateRepository

# Restore dependencies
dotnet restore Solution/Authentication.sln

# Build the project
dotnet build Solution/Authentication.sln --configuration Debug
```

### 2. Configure the Database

#### Option A: Local MySQL
```bash
# Start MySQL and create a database
mysql -u root -p
CREATE DATABASE AuthenticationDB;
CREATE USER 'authuser'@'localhost' IDENTIFIED BY 'password123';
GRANT ALL PRIVILEGES ON AuthenticationDB.* TO 'authuser'@'localhost';
FLUSH PRIVILEGES;
exit;
```

#### Option B: Docker MySQL (Faster)
```bash
# Run MySQL in Docker container
docker run --name mysql-auth \
  -e MYSQL_ROOT_PASSWORD=rootpass \
  -e MYSQL_DATABASE=AuthenticationDB \
  -e MYSQL_USER=authuser \
  -e MYSQL_PASSWORD=password123 \
  -p 3306:3306 \
  -d mysql:8.0
```

### 3. Configure the Connection String

Edit `Src/Authentication.API/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=AuthenticationDB;Uid=authuser;Pwd=password123;SslMode=none;"
  },
  "JwtSettings": {
    "Issuer": "Authentication",
    "Audience": "AuthenticationClients",
    "SecretKey": "super-secret-jwt-key-minimum-32-characters-long",
    "ExpirationMinutes": 60
  }
}
```

### 4. Run Database Migrations

```bash
cd Src/Authentication.API
dotnet ef database update --context ApiContextDevelopment
```

### 5. Run the Application

```bash
# Run in development mode
dotnet run --configuration Debug

# API will be available at: https://localhost:7001
# Swagger Documentation: https://localhost:7001
```

## 🔐 First Use - Testing the API

### 1. Access Swagger Documentation

Open your browser and go to: **https://localhost:7001**

You will see two documented APIs:
- **Authentication API** - Login and token generation
- **Access Control API** - RBAC management (Claims, Actions, etc.)

### 2. Create Your First Account

```bash
# Using curl
curl -X POST "https://localhost:7001/Authentication/AddAccount" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "admin",
    "password": "AdminPass123!"
  }'
```

**Expected response (200 OK):**
```json
{
  "type": "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.1",
  "title": "OK.",
  "status": 200,
  "detail": "Request was successful.",
  "data": {
    "userId": 1,
    "userName": "admin"
  }
}
```

### 3. Generate a JWT Token

```bash
curl -X POST "https://localhost:7001/Authentication/GenerateToken" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "admin",
    "password": "AdminPass123!"
  }'
```

**Expected response (200 OK):**
```json
{
  "data": {
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "expiresIn": 3600,
    "userName": "admin",
    "tokenType": "Bearer"
  }
}
```

### 4. Use the Token to Access Protected Endpoints

```bash
# Save the token in a variable
TOKEN="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."

# List all claims
curl -X GET "https://localhost:7001/Claim/GetClaims" \
  -H "Authorization: Bearer $TOKEN"
```

## 🔒 Configuring RBAC (Access Control)

### 1. Create a Claim (Permission)

```bash
curl -X POST "https://localhost:7001/Claim/AddClaim" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "type": "Permission",
    "value": "UserManagement",
    "description": "Permission to manage users"
  }'
```

### 2. Create an Action (System Action)

```bash
curl -X POST "https://localhost:7001/Action/AddAction" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Create",
    "description": "Create new records"
  }'
```

### 3. Map Claim to Action

```bash
curl -X POST "https://localhost:7001/ClaimAction/AddClaimAction" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "claimId": 1,
    "actionId": 1
  }'
```

### 4. Assign Permission to User

```bash
curl -X POST "https://localhost:7001/AccountClaimAction/AddAccountClaimAction" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "accountId": 1,
    "claimActionId": 1
  }'
```

## 🧪 Validating the Configuration

### Run the Tests

```bash
# Run all tests
dotnet test Src/Authentication.Tests/Authentication.Tests.csproj

# Run only unit tests
dotnet test --filter "FullyQualifiedName~Unit"

# Run with convenience scripts
scripts/run-tests.sh unit    # Linux/Mac
scripts/run-tests.bat unit     # Windows
```

### Verify Token Generation with Claims

After configuring RBAC, generate a new token:

```bash
curl -X POST "https://localhost:7001/Authentication/GenerateToken" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "admin",
    "password": "AdminPass123!"
  }'
```

The token should now include claims in the format `"UserManagement:Create"`.

## 🔧 Frontend Integration

### JavaScript/React Example

```javascript
class AuthService {
  constructor() {
    this.baseURL = 'https://localhost:7001';
    this.token = localStorage.getItem('authToken');
  }

  async login(userName, password) {
    const response = await fetch(`${this.baseURL}/Authentication/GenerateToken`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ userName, password })
    });
    
    if (response.ok) {
      const data = await response.json();
      this.token = data.data.accessToken;
      localStorage.setItem('authToken', this.token);
      return data;
    }
    throw new Error('Login failed');
  }

  async apiCall(endpoint, options = {}) {
    return fetch(`${this.baseURL}${endpoint}`, {
      ...options,
      headers: {
        'Authorization': `Bearer ${this.token}`,
        'Content-Type': 'application/json',
        ...options.headers
      }
    });
  }
}

// Usage
const auth = new AuthService();
await auth.login('admin', 'AdminPass123!');
const claims = await auth.apiCall('/Claim/GetClaims');
```

### C# Client Example

```csharp
public class AuthenticationClient
{
    private readonly HttpClient _httpClient;
    private string? _token;

    public AuthenticationClient()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7001") };
    }

    public async Task<bool> LoginAsync(string userName, string password)
    {
        var request = new { userName, password };
        var response = await _httpClient.PostAsJsonAsync("/Authentication/GenerateToken", request);
        
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            _token = result?.Data?.AccessToken;
            _httpClient.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<Claim>> GetClaimsAsync()
    {
        var response = await _httpClient.GetAsync("/Claim/GetClaims");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<Claim>>();
    }
}
```

## 📚 Next Steps

1. **Read complete documentation**: [docs/DEVELOPMENT.md](DEVELOPMENT.md)
2. **Configure for production**: [docs/DEPLOYMENT.md](DEPLOYMENT.md)
3. **Understand the architecture**: [docs/ARCHITECTURE.md](ARCHITECTURE.md)
4. **See more examples**: [docs/EXAMPLES.md](EXAMPLES.md)
5. **Configure security**: [docs/SECURITY.md](SECURITY.md)

## 🆘 Common Issues

### ❌ Database Connection Error
```
Unable to connect to any of the specified MySQL hosts
```
**Solution**: Check if MySQL is running and the connection string is correct.

### ❌ Migration Error
```
Unable to create an object of type 'ApiContextDevelopment'
```
**Solution**: 
```bash
cd Src/Authentication.API
dotnet ef database update --context ApiContextDevelopment --verbose
```

### ❌ Invalid Token
```
401 Unauthorized
```
**Solution**: Verify that the token is being sent in the `Authorization: Bearer {token}` header.

### ❌ CORS Error (Frontend)
```
Access to fetch at 'https://localhost:7001' has been blocked by CORS policy
```
**Solution**: The API is already configured with CORS allowing all origins. Check if you're using HTTPS.

## 💡 Development Tips

- Use `dotnet watch run` for hot reload during development
- Configure environment variables for different environments
- Use Swagger UI to test endpoints interactively
- Monitor logs with `dotnet run --verbosity detailed`
- Use tools like Postman or Insomnia for API testing

---

🎉 **Parabéns!** Você configurou com sucesso o Authentication Service. Para dúvidas, consulte a [documentação completa](../README.md) ou abra uma [issue](https://github.com/maiconcardozo/CleanTemplateRepository/issues).