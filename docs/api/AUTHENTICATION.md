# 🔐 JWT Authentication Documentation

## Table of Contents
- [Overview](#overview)
- [How JWT Works](#how-jwt-works)
- [How to Obtain and Use the Token](#how-to-obtain-and-use-the-token)
- [Token Validation](#token-validation)
- [Communication Examples](#communication-examples)
- [Security and Best Practices](#security-and-best-practices)
- [Extensions for Other Methods](#extensions-for-other-methods)

---

## Overview

**HealthPlan Suite** uses **JWT (JSON Web Token)**-based authentication integrated with the **Authentication Service** to protect API endpoints and manage user access control.

### Why JWT?

- **Stateless**: No need for server-side session storage
- **Scalable**: Facilitates horizontal application distribution
- **Secure**: Cryptographic signature ensures token integrity
- **Portable**: Can be used across different domains and services
- **Self-contained**: Contains all necessary information about the user

### Authentication Architecture

```
┌─────────────┐         ┌──────────────────┐         ┌─────────────┐
│   Client    │────1───>│ Authentication   │────2───>│  Database   │
│             │         │    Service       │         │             │
│             │<───4────│                  │<───3────│             │
└─────────────┘         └──────────────────┘         └─────────────┘
      │
      │ 5. Requests with JWT Token
      ↓
┌─────────────────────────────────────────────────────────┐
│              Protected API (Endpoints)                   │
│  - JWT Middleware validates token                       │
│  - Extracts claims and permissions                      │
│  - Authorizes access to resources                       │
└─────────────────────────────────────────────────────────┘
```

**Flow:**
1. Client sends credentials (username/password)
2. Authentication Service validates credentials in the database
3. Database returns user data and their permissions
4. Service generates JWT token and returns it to the client
5. Client uses JWT token to access protected endpoints

---

## How JWT Works

### JWT Token Structure

A JWT token consists of three parts separated by dots (`.`):

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbiIsImp0aSI6IjEyMzQ1Njc4LTkwYWItY2RlZi0xMjM0LTU2Nzg5MGFiY2RlZiIsImlhdCI6MTY0MjY4MDAwMCwiZXhwIjoxNjQyNjgzNjAwLCJpc3MiOiJBdXRoZW50aWNhdGlvbiIsImF1ZCI6IkF1dGhlbnRpY2F0aW9uQ2xpZW50cyJ9.signature_hash_value

│                  Header                  │                          Payload                                    │  Signature  │
```

#### 1. Header
Contains information about the token type and signing algorithm:

```json
{
  "alg": "HS256",
  "typ": "JWT"
}
```

- **alg**: Encryption algorithm used (HMAC-SHA256)
- **typ**: Token type (JWT)

#### 2. Payload
Contains the claims (declarations) about the user and token metadata:

```json
{
  "sub": "admin",
  "jti": "12345678-90ab-cdef-1234-567890abcdef",
  "iat": 1642680000,
  "exp": 1642683600,
  "iss": "Authentication",
  "aud": "AuthenticationClients",
  "userName": "admin",
  "userId": "123",
  "claims": ["user:read", "user:write", "admin:access"]
}
```

**Standard Claims (Registered Claims):**
- **sub** (Subject): User identifier
- **jti** (JWT ID): Unique token ID
- **iat** (Issued At): Timestamp when the token was created
- **exp** (Expiration): Timestamp when the token expires
- **iss** (Issuer): Token issuer (Authentication Service)
- **aud** (Audience): Token recipient (who can use it)

**Custom Claims:**
- **userName**: User name
- **userId**: User ID in the system
- **claims**: Array of user permissions

#### 3. Signature
Ensures the token integrity and verifies that it hasn't been altered:

```
HMACSHA256(
  base64UrlEncode(header) + "." + base64UrlEncode(payload),
  secret_key
)
```

### JWT Configuration in HealthPlan Suite

The JWT configuration is defined in `appsettings.json`:

```json
{
  "JwtSettings": {
    "Issuer": "Authentication",
    "Audience": "AuthenticationClients",
    "SecretKey": "REPLACE-WITH-SECURE-KEY-MIN-32-CHARS-USE-ENV-VAR-OR-KEY-VAULT",
    "ExpirationMinutes": 60
  }
}
```

**Parameters:**
- **Issuer**: Identifies who issued the token
- **Audience**: Defines for whom the token is valid
- **SecretKey**: Secret key for signing tokens (minimum 32 characters)
- **ExpirationMinutes**: Token validity time in minutes

⚠️ **IMPORTANT**: In production, NEVER store the `SecretKey` directly in the configuration file. Use environment variables or Azure Key Vault.

### Token Validation by the Server

The server automatically validates the following aspects:

```csharp
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = !_environment.IsDevelopment();
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,              // Validates the issuer
        ValidateAudience = true,            // Validates the audience
        ValidateLifetime = true,            // Validates expiration
        ValidateIssuerSigningKey = true,    // Validates signature
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero           // No time tolerance
    };
});
```

---

## How to Obtain and Use the Token

### Step 1: Create a User Account

Before authenticating, you need to have a registered account.

**Endpoint:** `POST /Account/AddAccount`

**Request:**
```bash
curl -X POST "https://localhost:7001/Account/AddAccount" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "example_user",
    "password": "SecurePassword123!",
    "email": "user@example.com"
  }'
```

**Success Response (200):**
```json
{
  "type": "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.1",
  "title": "OK",
  "status": 200,
  "detail": "Request was successful.",
  "instance": "/Account/AddAccount",
  "data": {
    "userId": 123,
    "userName": "example_user",
    "email": "user@example.com"
  }
}
```

### Step 2: Authenticate and Obtain JWT Token

Once you have an account, you can authenticate to obtain the JWT token.

**Endpoint:** `POST /Authentication/GenerateToken`

**Request:**
```bash
curl -X POST "https://localhost:7001/Authentication/GenerateToken" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "example_user",
    "password": "SecurePassword123!"
  }'
```

**Success Response (200):**
```json
{
  "data": {
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJleGFtcGxlX3VzZXIiLCJqdGkiOiIxMjM0NTY3OC05MGFiLWNkZWYtMTIzNC01Njc4OTBhYmNkZWYiLCJpYXQiOjE2NDI2ODAwMDAsImV4cCI6MTY0MjY4MzYwMCwiaXNzIjoiQXV0aGVudGljYXRpb24iLCJhdWQiOiJBdXRoZW50aWNhdGlvbkNsaWVudHMifQ.signature",
    "expiresIn": 3600,
    "userName": "example_user",
    "claims": [
      "user:read",
      "user:write"
    ]
  }
}
```

**Response Fields:**
- **accessToken**: JWT token to use in requests
- **expiresIn**: Validity time in seconds (3600 = 1 hour)
- **userName**: Authenticated user name
- **claims**: User permissions

### Step 3: Use the Token in Requests

Include the JWT token in the `Authorization` header with the `Bearer` prefix:

```bash
curl -X GET "https://localhost:7001/Quote/GetQuotes" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

**Header Format:**
```
Authorization: Bearer <your_jwt_token>
```

### Complete Example in Different Languages

#### JavaScript/TypeScript (Fetch API)

```javascript
// 1. Function to authenticate and obtain token
async function authenticate(userName, password) {
  const response = await fetch('https://localhost:7001/Authentication/GenerateToken', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ userName, password })
  });

  if (!response.ok) {
    throw new Error('Authentication failed');
  }

  const data = await response.json();
  return data.data.accessToken;
}

// 2. Function to make authenticated request
async function getQuotes(token) {
  const response = await fetch('https://localhost:7001/Quote/GetQuotes', {
    method: 'GET',
    headers: {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    }
  });

  if (!response.ok) {
    throw new Error('Failed to fetch quotes');
  }

  return await response.json();
}

// 3. Usage
async function main() {
  try {
    // Obtain token
    const token = await authenticate('example_user', 'SecurePassword123!');
    console.log('Token obtained:', token);

    // Store token (localStorage, sessionStorage, etc.)
    localStorage.setItem('jwt_token', token);

    // Use token to make request
    const quotes = await getQuotes(token);
    console.log('Quotes:', quotes);
  } catch (error) {
    console.error('Error:', error);
  }
}

main();
```

#### C# (.NET)

```csharp
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class AuthenticationClient
{
    private readonly HttpClient _httpClient;
    private string _token;

    public AuthenticationClient(string baseUrl = "https://localhost:7001")
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    // 1. Authenticate and obtain token
    public async Task<string> AuthenticateAsync(string userName, string password)
    {
        var loginRequest = new { userName, password };
        var json = JsonSerializer.Serialize(loginRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/Authentication/GenerateToken", content);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();
        var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseJson);
        
        _token = authResponse.Data.AccessToken;
        return _token;
    }

    // 2. Make authenticated request
    public async Task<List<Quote>> GetQuotesAsync()
    {
        if (string.IsNullOrEmpty(_token))
            throw new InvalidOperationException("Not authenticated. Call AuthenticateAsync first.");

        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", _token);

        var response = await _httpClient.GetAsync("/Quote/GetQuotes");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Quote>>(json);
    }
}

// Model classes
public class AuthResponse
{
    public AuthData Data { get; set; }
}

public class AuthData
{
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string UserName { get; set; }
    public List<string> Claims { get; set; }
}

// Usage
var client = new AuthenticationClient();
await client.AuthenticateAsync("example_user", "SecurePassword123!");
var quotes = await client.GetQuotesAsync();
```

#### Python (requests)

```python
import requests
import json

class AuthenticationClient:
    def __init__(self, base_url="https://localhost:7001"):
        self.base_url = base_url
        self.token = None
        self.session = requests.Session()

    def authenticate(self, username, password):
        """Authenticate and obtain JWT token"""
        url = f"{self.base_url}/Authentication/GenerateToken"
        payload = {
            "userName": username,
            "password": password
        }
        
        response = self.session.post(url, json=payload, verify=False)
        response.raise_for_status()
        
        data = response.json()
        self.token = data['data']['accessToken']
        
        # Configure authentication header for next requests
        self.session.headers.update({
            'Authorization': f'Bearer {self.token}'
        })
        
        return self.token

    def get_quotes(self):
        """Fetch quotes using JWT token"""
        if not self.token:
            raise ValueError("Not authenticated. Call authenticate() first.")
        
        url = f"{self.base_url}/Quote/GetQuotes"
        response = self.session.get(url, verify=False)
        response.raise_for_status()
        
        return response.json()

# Usage
client = AuthenticationClient()
token = client.authenticate("example_user", "SecurePassword123!")
print(f"Token obtained: {token[:50]}...")

quotes = client.get_quotes()
print(f"Quotes found: {len(quotes)}")
```

---

## Token Validation

### Automatic Validation

The ASP.NET Core JWT middleware automatically validates all received tokens:

```csharp
// Configured in Startup.cs
app.UseAuthentication();  // JWT authentication middleware
app.UseAuthorization();   // Authorization middleware
```

### What is Validated?

1. **Signature**: Verifies if the token was signed with the correct secret key
2. **Issuer**: Confirms that the token was issued by the expected server
3. **Audience**: Verifies if the token is intended for this application
4. **Expiration**: Ensures that the token hasn't expired yet
5. **Format**: Validates the JWT token structure

### Validation Flow

```
Client sends request
        ↓
┌───────────────────────────────────┐
│   JWT Middleware Intercepts       │
└───────────────────────────────────┘
        ↓
┌───────────────────────────────────┐
│   Extracts token from header      │
│   Authorization: Bearer <token>   │
└───────────────────────────────────┘
        ↓
┌───────────────────────────────────┐
│   Validates Signature             │
│   ✓ Token signed with             │
│     correct SecretKey?            │
└───────────────────────────────────┘
        ↓
┌───────────────────────────────────┐
│   Validates Claims                │
│   ✓ Correct Issuer?               │
│   ✓ Correct Audience?             │
│   ✓ Token not expired?            │
└───────────────────────────────────┘
        ↓
    ┌───────┐
    │Valid? │
    └───┬───┘
        │
    ┌───┴────────────────┐
    │                    │
   Yes                  No
    │                    │
    ↓                    ↓
┌────────┐       ┌──────────────┐
│ 200 OK │       │ 401 Unauthorized │
└────────┘       └──────────────┘
```

### Validation Error Responses

#### Expired Token (401)
```json
{
  "title": "Unauthorized",
  "status": 401,
  "detail": "Token has expired",
  "type": "https://tools.ietf.org/html/rfc7235#section-3.1"
}
```

#### Invalid Token or Incorrect Signature (401)
```json
{
  "title": "Unauthorized",
  "status": 401,
  "detail": "Invalid token signature",
  "type": "https://tools.ietf.org/html/rfc7235#section-3.1"
}
```

#### Missing Token (401)
```json
{
  "title": "Unauthorized",
  "status": 401,
  "detail": "Authorization header is missing",
  "type": "https://tools.ietf.org/html/rfc7235#section-3.1"
}
```

### Manual Validation (Optional)

If you need to manually validate a JWT token (for example, in an external service):

```csharp
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public bool ValidateToken(string token, string secretKey)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.UTF8.GetBytes(secretKey);

    try
    {
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "Authentication",
            ValidAudience = "AuthenticationClients",
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        return true;
    }
    catch
    {
        return false;
    }
}
```

---

## Communication Examples

### Scenario 1: Complete Authentication Flow

```bash
# 1. Create account
curl -X POST "https://localhost:7001/Account/AddAccount" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "john_silva",
    "password": "MyPass@123",
    "email": "john@example.com"
  }'

# Response:
# {
#   "status": 200,
#   "data": {
#     "userId": 456,
#     "userName": "john_silva",
#     "email": "john@example.com"
#   }
# }

# 2. Authenticate and obtain token
TOKEN=$(curl -X POST "https://localhost:7001/Authentication/GenerateToken" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "john_silva",
    "password": "MyPass@123"
  }' | jq -r '.data.accessToken')

echo "Token: $TOKEN"

# 3. Use token to access protected resource
curl -X GET "https://localhost:7001/Quote/GetQuotes" \
  -H "Authorization: Bearer $TOKEN"
```

### Scenario 2: Token Renewal

When the token expires, you need to authenticate again:

```javascript
class TokenManager {
  constructor() {
    this.token = null;
    this.expiresAt = null;
  }

  async getValidToken(userName, password) {
    // Check if the token is still valid
    if (this.token && this.expiresAt && Date.now() < this.expiresAt) {
      return this.token;
    }

    // Token expired or non-existent, obtain new one
    return await this.refreshToken(userName, password);
  }

  async refreshToken(userName, password) {
    const response = await fetch('https://localhost:7001/Authentication/GenerateToken', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ userName, password })
    });

    const data = await response.json();
    this.token = data.data.accessToken;
    
    // Set expiration time (subtract 5 minutes for safety margin)
    this.expiresAt = Date.now() + (data.data.expiresIn - 300) * 1000;
    
    return this.token;
  }
}

// Usage
const tokenManager = new TokenManager();

async function makeAuthenticatedRequest() {
  const token = await tokenManager.getValidToken('john_silva', 'MyPass@123');
  
  const response = await fetch('https://localhost:7001/Quote/GetQuotes', {
    headers: { 'Authorization': `Bearer ${token}` }
  });
  
  return await response.json();
}
```

### Scenario 3: Authentication Error Handling

```javascript
async function authenticatedFetch(url, options = {}) {
  const token = localStorage.getItem('jwt_token');
  
  if (!token) {
    throw new Error('Token not found. Please log in.');
  }

  // Add token to header
  const headers = {
    ...options.headers,
    'Authorization': `Bearer ${token}`
  };

  const response = await fetch(url, { ...options, headers });

  // Handle authentication error
  if (response.status === 401) {
    // Invalid or expired token
    localStorage.removeItem('jwt_token');
    throw new Error('Session expired. Please log in again.');
  }

  // Handle authorization error (no permission)
  if (response.status === 403) {
    throw new Error('You do not have permission to access this resource.');
  }

  if (!response.ok) {
    const error = await response.json();
    throw new Error(error.detail || 'Request error');
  }

  return await response.json();
}

// Usage with error handling
try {
  const quotes = await authenticatedFetch('https://localhost:7001/Quote/GetQuotes');
  console.log('Quotes:', quotes);
} catch (error) {
  console.error('Error:', error.message);
  // Redirect to login page if necessary
  if (error.message.includes('login')) {
    window.location.href = '/login';
  }
}
```

### Scenario 4: RBAC System - Permission Verification

```bash
# 1. Authenticate as administrator
TOKEN=$(curl -X POST "https://localhost:7001/Authentication/GenerateToken" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "admin",
    "password": "AdminPassword123!"
  }' | jq -r '.data.accessToken')

# 2. Create a permission (Claim)
CLAIM_ID=$(curl -X POST "https://localhost:7001/Claim/AddClaim" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "type": "Permission",
    "value": "quote:manage",
    "description": "Manage quotes"
  }' | jq -r '.data.claimId')

# 3. Create an action
ACTION_ID=$(curl -X POST "https://localhost:7001/Action/AddAction" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "CreateQuote",
    "description": "Create new quote"
  }' | jq -r '.data.actionId')

# 4. Associate Claim with Action
CLAIM_ACTION_ID=$(curl -X POST "https://localhost:7001/ClaimAction/AddClaimAction" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "claimId": '$CLAIM_ID',
    "actionId": '$ACTION_ID'
  }' | jq -r '.data.claimActionId')

# 5. Assign permission to a user
curl -X POST "https://localhost:7001/AccountClaimAction/AddAccountClaimAction" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "accountId": 456,
    "claimActionId": '$CLAIM_ACTION_ID'
  }'

# Now the user with accountId 456 has permission to create quotes
```

---

## Security and Best Practices

### 🔒 Secure SecretKey Configuration

#### ❌ NEVER DO THIS (Production)
```json
// appsettings.json
{
  "JwtSettings": {
    "SecretKey": "my-secret-key-123"  // ❌ INSECURE!
  }
}
```

#### ✅ DO THIS

**Option 1: Environment Variables**
```bash
# Linux/Mac
export JwtSettings__SecretKey="your-very-secure-key-with-at-least-32-random-characters"

# Windows PowerShell
$env:JwtSettings__SecretKey="your-very-secure-key-with-at-least-32-random-characters"

# Docker
docker run -e JwtSettings__SecretKey="your-secure-key" myapp
```

**Option 2: Azure Key Vault**
```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Add Azure Key Vault
if (!builder.Environment.IsDevelopment())
{
    var keyVaultEndpoint = new Uri(builder.Configuration["KeyVaultEndpoint"]);
    builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
}
```

**Option 3: User Secrets (Development)**
```bash
# Initialize user secrets
dotnet user-secrets init --project Src/HealthPlan.API

# Add secret
dotnet user-secrets set "JwtSettings:SecretKey" "development-key-32-chars" --project Src/HealthPlan.API

# List secrets
dotnet user-secrets list --project Src/HealthPlan.API
```

### 🛡️ SecretKey Security

**Key Requirements:**
- Minimum of **32 characters**
- Use random characters (letters, numbers, symbols)
- Never share or version in Git
- Rotate periodically (every 90 days recommended)

**Generate Secure Key:**
```bash
# Linux/Mac
openssl rand -base64 48

# PowerShell
$bytes = New-Object byte[] 48
[Security.Cryptography.RandomNumberGenerator]::Create().GetBytes($bytes)
[Convert]::ToBase64String($bytes)

# Python
python -c "import secrets; print(secrets.token_urlsafe(48))"
```

### 🔐 Secure Token Storage (Client)

#### ❌ DO NOT Store in localStorage (Vulnerable to XSS)
```javascript
// ❌ INSECURE - Vulnerable to XSS attacks
localStorage.setItem('jwt_token', token);
```

#### ✅ Store in httpOnly Cookie
```javascript
// Server (ASP.NET Core) - Set httpOnly cookie
Response.Cookies.Append("jwt_token", token, new CookieOptions
{
    HttpOnly = true,    // Not accessible via JavaScript
    Secure = true,      // HTTPS only
    SameSite = SameSiteMode.Strict,  // CSRF protection
    Expires = DateTimeOffset.UtcNow.AddHours(1)
});

// Client - Cookie is sent automatically
fetch('https://localhost:7001/Quote/GetQuotes', {
    credentials: 'include'  // Include cookies in request
});
```

#### ✅ Alternative: sessionStorage (More Secure than localStorage)
```javascript
// Better than localStorage, but still vulnerable to XSS
// Use only if you can't use httpOnly cookies
sessionStorage.setItem('jwt_token', token);
```

### ⏱️ Appropriate Expiration Time

| Environment | Recommended Time | Reason |
|----------|-------------------|--------|
| **Development** | 60 minutes | Convenience for testing |
| **Production (Public)** | 15-30 minutes | Balance between security and UX |
| **Production (Admin)** | 5-15 minutes | High security for critical operations |
| **Internal API** | 1-2 hours | Communication between trusted services |

```json
{
  "JwtSettings": {
    "ExpirationMinutes": 15  // 15 minutes for production
  }
}
```

### 🔒 HTTPS Required

**Development:**
```json
{
  "Kestrel": {
    "Endpoints": {
      "Https": {
        "Url": "https://localhost:7001"
      }
    }
  }
}
```

**Production:**
```csharp
// Startup.cs
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();  // HTTP Strict Transport Security
    app.UseHttpsRedirection();  // Redirect HTTP to HTTPS
}
```

### 🛡️ Input Validation

Always validate inputs to prevent attacks:

```csharp
public class LoginRequestValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters")
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("Username must contain only letters, numbers and underscore");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters");
    }
}
```

### 🔐 Password Hashing (Argon2)

The system uses **Argon2** for password hashing (better than bcrypt/SHA):

```csharp
// Password is never stored in plain text
public void AddAccount(Account account)
{
    // Generate secure hash with Argon2
    account.Password = _passwordHasher.Hash(account.Password);
    _unitOfWork.AccountRepository.Add(account);
    _unitOfWork.Complete();
}

// Secure verification
public Account GetAccountByUserNameAndPassword(Account account)
{
    var dbAccount = _unitOfWork.AccountRepository.GetByUserName(account.UserName);
    if (dbAccount == null)
        throw new InvalidOperationException("Account not found");
    
    // Verify hash securely (constant-time comparison)
    if (_passwordHasher.Verify(account.Password, dbAccount.Password))
        return dbAccount;
    
    throw new UnauthorizedAccessException("Invalid password");
}
```

### 📋 Security Checklist

- [ ] **SecretKey** has at least 32 random characters
- [ ] **SecretKey** stored in environment variable or Key Vault
- [ ] **HTTPS** enabled in production
- [ ] **Expiration time** appropriate (15-30min production)
- [ ] **Token** stored in httpOnly cookie (not localStorage)
- [ ] **Input validation** implemented (FluentValidation)
- [ ] **Password hashing** using Argon2
- [ ] **Rate limiting** configured to prevent brute force
- [ ] **CORS** configured appropriately
- [ ] **Security headers** added (HSTS, X-Frame-Options, etc.)
- [ ] **Logging** of failed authentication attempts
- [ ] **Key rotation** scheduled (90 days)

### 🚨 Monitoring and Logging

```csharp
public async Task<IActionResult> GenerateToken([FromBody] LoginRequestDTO request)
{
    try
    {
        _logger.LogInformation("Login attempt for user: {UserName}", request.UserName);
        
        var response = await _authService.AuthenticateAsync(request);
        
        _logger.LogInformation("Successful login for user: {UserName}", request.UserName);
        return Ok(response);
    }
    catch (UnauthorizedAccessException ex)
    {
        _logger.LogWarning("Authentication failure for user: {UserName}. Reason: {Reason}", 
            request.UserName, ex.Message);
        return Unauthorized("Invalid credentials");
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error during authentication for user: {UserName}", request.UserName);
        return StatusCode(500, "Internal server error");
    }
}
```

---

## Extensions for Other Methods

The current system uses JWT with username/password authentication, but can be extended to support other authentication methods.

### 1. OAuth 2.0 / OpenID Connect

Integration with external providers (Google, Microsoft, Facebook):

```csharp
// Startup.cs
services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => { /* existing JWT configuration */ })
.AddGoogle(options =>
{
    options.ClientId = Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
})
.AddMicrosoftAccount(options =>
{
    options.ClientId = Configuration["Authentication:Microsoft:ClientId"];
    options.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
});
```

**OAuth Flow:**
```
Client → Redirect to Google
Google → User authenticates
Google → Redirect back with code
Server → Exchange code for token
Server → Create user account/session
Server → Generate own JWT token
Client → Receive JWT token
```

### 2. Two-Factor Authentication (2FA)

Add second layer of security:

```csharp
public class TwoFactorAuthService
{
    private readonly IMemoryCache _cache;
    private readonly IEmailService _emailService;

    // Generate 2FA code
    public string GenerateTwoFactorCode(string userName)
    {
        var code = new Random().Next(100000, 999999).ToString();
        _cache.Set($"2fa:{userName}", code, TimeSpan.FromMinutes(5));
        return code;
    }

    // Send code by email
    public async Task SendTwoFactorCodeAsync(string userName, string email)
    {
        var code = GenerateTwoFactorCode(userName);
        await _emailService.SendEmailAsync(email, "Verification Code", 
            $"Your verification code is: {code}");
    }

    // Validate code
    public bool ValidateTwoFactorCode(string userName, string code)
    {
        if (_cache.TryGetValue($"2fa:{userName}", out string cachedCode))
        {
            return cachedCode == code;
        }
        return false;
    }
}

// Controller
[HttpPost("GenerateToken")]
public async Task<IActionResult> GenerateToken([FromBody] LoginRequestDTO request)
{
    // 1. Validate username/password
    var account = await _authService.ValidateCredentialsAsync(request);
    
    // 2. If 2FA is enabled, send code
    if (account.TwoFactorEnabled)
    {
        await _twoFactorService.SendTwoFactorCodeAsync(account.UserName, account.Email);
        return Ok(new { requiresTwoFactor = true });
    }
    
    // 3. Generate token normally if 2FA is not enabled
    return Ok(await _authService.GenerateTokenAsync(account));
}

[HttpPost("VerifyTwoFactor")]
public async Task<IActionResult> VerifyTwoFactor([FromBody] TwoFactorRequestDTO request)
{
    // Validate 2FA code
    if (!_twoFactorService.ValidateTwoFactorCode(request.UserName, request.Code))
    {
        return Unauthorized("Invalid or expired code");
    }
    
    // Generate JWT token
    var account = await _authService.GetAccountByUserNameAsync(request.UserName);
    return Ok(await _authService.GenerateTokenAsync(account));
}
```

### 3. API Keys

For external service authentication:

```csharp
public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
{
    private const string ApiKeyHeaderName = "X-API-Key";

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
        {
            return AuthenticateResult.Fail("API Key header not found");
        }

        var providedApiKey = apiKeyHeaderValues.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(providedApiKey))
        {
            return AuthenticateResult.Fail("Empty API Key");
        }

        // Validate API Key in database
        var apiKey = await _apiKeyRepository.ValidateApiKeyAsync(providedApiKey);
        if (apiKey == null)
        {
            return AuthenticateResult.Fail("Invalid API Key");
        }

        // Create claims and identity
        var claims = new[] {
            new Claim(ClaimTypes.Name, apiKey.ClientName),
            new Claim("ApiKeyId", apiKey.Id.ToString())
        };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}

// Usage
[ApiController]
[Route("[controller]")]
public class ExternalApiController : ControllerBase
{
    [HttpGet("data")]
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public IActionResult GetData()
    {
        return Ok("Data protected by API Key");
    }
}
```

### 4. Refresh Tokens

Implement long-duration tokens to renew access tokens:

```csharp
public class TokenService
{
    // Generate access token and refresh token
    public TokenResponseDTO GenerateTokens(Account account)
    {
        // Access token (short duration - 15 minutes)
        var accessToken = GenerateJwtToken(account, TimeSpan.FromMinutes(15));
        
        // Refresh token (long duration - 7 days)
        var refreshToken = GenerateRefreshToken();
        StoreRefreshToken(account.Id, refreshToken, TimeSpan.FromDays(7));
        
        return new TokenResponseDTO
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = 900 // 15 minutes
        };
    }

    // Renew access token using refresh token
    public async Task<TokenResponseDTO> RefreshAccessTokenAsync(string refreshToken)
    {
        var storedToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
        
        if (storedToken == null || storedToken.ExpiresAt < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Invalid or expired refresh token");
        }

        var account = await _accountRepository.GetByIdAsync(storedToken.AccountId);
        return GenerateTokens(account);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}

// Endpoint to renew token
[HttpPost("RefreshToken")]
public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO request)
{
    try
    {
        var tokens = await _tokenService.RefreshAccessTokenAsync(request.RefreshToken);
        return Ok(tokens);
    }
    catch (UnauthorizedAccessException)
    {
        return Unauthorized("Invalid refresh token");
    }
}
```

### 5. Certificate Authentication (mTLS)

For secure communication between services:

```csharp
// Startup.cs
services.AddAuthentication()
    .AddCertificate(options =>
    {
        options.AllowedCertificateTypes = CertificateTypes.All;
        options.RevocationMode = X509RevocationMode.NoCheck;
        
        options.Events = new CertificateAuthenticationEvents
        {
            OnCertificateValidated = context =>
            {
                // Validate custom certificate
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, context.ClientCertificate.Subject),
                    new Claim("Thumbprint", context.ClientCertificate.Thumbprint)
                };
                
                context.Principal = new ClaimsPrincipal(
                    new ClaimsIdentity(claims, context.Scheme.Name));
                context.Success();
                
                return Task.CompletedTask;
            }
        };
    });
```

### 6. Biometrics / WebAuthn

Passwordless authentication using biometrics:

```csharp
// Requires Fido2.AspNet library
services.AddFido2(options =>
{
    options.ServerDomain = "localhost";
    options.ServerName = "HealthPlan Suite";
    options.Origin = "https://localhost:7001";
});

// Controller
[HttpPost("RegisterBiometric")]
public async Task<IActionResult> RegisterBiometric([FromBody] BiometricRegistrationDTO request)
{
    // Create registration challenge
    var options = _fido2.RequestNewCredential(
        user: request.User,
        excludeCredentials: new List<PublicKeyCredentialDescriptor>(),
        authenticatorSelection: new AuthenticatorSelection
        {
            RequireResidentKey = false,
            UserVerification = UserVerificationRequirement.Required
        },
        attestationPreference: AttestationConveyancePreference.None
    );
    
    return Ok(options);
}
```

### 7. Single Sign-On (SSO)

Integration with SAML or OpenID Connect for corporate SSO:

```csharp
// Add Sustainsys.Saml2 or IdentityServer
services.AddAuthentication()
    .AddSaml2(options =>
    {
        options.SPOptions.EntityId = new EntityId("https://localhost:7001");
        options.IdentityProviders.Add(new IdentityProvider(
            new EntityId("https://idp.example.com"),
            options.SPOptions)
        {
            MetadataLocation = "https://idp.example.com/metadata",
            LoadMetadata = true
        });
    });
```

### Method Comparison

| Method | Security | Complexity | Recommended Use |
|--------|-----------|--------------|-----------------|
| **JWT (current)** | ⭐⭐⭐⭐ | ⭐⭐ | REST APIs, SPAs |
| **OAuth 2.0** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ | Social login, delegation |
| **2FA** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ | Sensitive operations |
| **API Keys** | ⭐⭐⭐ | ⭐ | B2B integration |
| **Refresh Tokens** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ | Mobile applications |
| **mTLS** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ | Microservices |
| **WebAuthn** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ | Passwordless auth |
| **SSO/SAML** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ | Corporate enterprises |

---

## Additional Resources

### Related Documentation

- **[API.md](./API.md)** - Complete API documentation
- **[SECURITY.md](../../SECURITY.md)** - Detailed security configuration
- **[EXAMPLES.md](./EXAMPLES.md)** - Practical integration examples
- **[DEVELOPMENT.md](../guides/DEVELOPMENT.md)** - Development guide

### External Links

- [JWT.io](https://jwt.io/) - JWT Debugger and documentation
- [RFC 7519](https://tools.ietf.org/html/rfc7519) - JWT Specification
- [OWASP Authentication Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Authentication_Cheat_Sheet.html)
- [ASP.NET Core Authentication](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/)
- [Argon2 Password Hashing](https://github.com/P-H-C/phc-winner-argon2)

### Useful Tools

- **Postman** - Test API with JWT authentication
- **JWT.io Debugger** - Decode and validate tokens
- **Azure Key Vault** - Secure key management
- **HashiCorp Vault** - Open-source alternative for secrets management

---

## Support

For questions, suggestions, or to report issues:
- Open an [issue](https://github.com/maiconcardozo/HealthPlanSuite/issues)
- Contact through GitHub

---

⭐ If this documentation was helpful, consider giving the project a star!
