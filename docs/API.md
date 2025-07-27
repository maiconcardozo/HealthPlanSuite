# 📖 API Documentation

## Overview

The Authentication API provides secure authentication and authorization services using JWT tokens. This documentation covers all available endpoints, request/response formats, and usage examples.

## Base Information

- **Base URL**: `https://localhost:7001` (Development) / `https://api.yourdomain.com` (Production)
- **API Version**: v1
- **Content Type**: `application/json`
- **Authentication**: JWT Bearer Token (for protected endpoints)

## Authentication Header

For protected endpoints, include the JWT token in the Authorization header:

```http
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

## Endpoints

### 🔐 Authentication

#### POST /Authentication/GenerateToken

Generates a JWT token for valid user credentials.

**Request:**

```http
POST /Authentication/GenerateToken
Content-Type: application/json

{
  "userName": "admin",
  "password": "password123"
}
```

**Request Body Schema:**

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `userName` | string | ✅ | User's login name (3-50 characters) |
| `password` | string | ✅ | User's password (minimum 6 characters) |

**Response (200 OK):**

```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbiIsImp0aSI6IjEyMzQ1Njc4LTkwYWItY2RlZi0xMjM0LTU2Nzg5MGFiY2RlZiIsImlhdCI6MTY0MjY4MDAwMCwiZXhwIjoxNjQyNjgzNjAwLCJpc3MiOiJBdXRoZW50aWNhdGlvblNlcnZpY2UiLCJhdWQiOiJBdXRoZW50aWNhdGlvbkNsaWVudHMifQ.signature",
  "expiresIn": 3600,
  "userName": "admin",
  "claims": [
    "user:read",
    "user:write",
    "admin:access"
  ]
}
```

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `accessToken` | string | JWT access token |
| `expiresIn` | number | Token expiration time in seconds |
| `userName` | string | Authenticated user's name |
| `claims` | string[] | User's permissions/claims |

**Error Responses:**

**400 Bad Request** - Invalid credentials:
```json
{
  "title": "Validation Error",
  "status": 400,
  "errors": {
    "UserName": ["UserName is required"],
    "Password": ["Password must be at least 6 characters"]
  }
}
```

**401 Unauthorized** - Authentication failed:
```json
{
  "title": "Authentication Failed",
  "status": 401,
  "detail": "Invalid username or password"
}
```

**500 Internal Server Error** - Server error:
```json
{
  "title": "Internal Server Error",
  "status": 500,
  "detail": "An error occurred while processing your request"
}
```

#### POST /Authentication/AddAccount

Creates a new user account.

**Request:**

```http
POST /Authentication/AddAccount
Content-Type: application/json

{
  "userName": "newuser",
  "password": "securepassword123"
}
```

**Request Body Schema:**

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| `userName` | string | ✅ | Unique username (3-50 characters) |
| `password` | string | ✅ | User's password (minimum 6 characters) |

**Response (200 OK):**

```json
{
  "id": 123,
  "userName": "newuser",
  "message": "Account created successfully",
  "success": true
}
```

**Response Schema:**

| Field | Type | Description |
|-------|------|-------------|
| `id` | number | Created account ID |
| `userName` | string | Created username |
| `message` | string | Success message |
| `success` | boolean | Operation success status |

**Error Responses:**

**400 Bad Request** - Validation error:
```json
{
  "title": "Validation Error",
  "status": 400,
  "errors": {
    "UserName": ["UserName already exists"]
  }
}
```

**409 Conflict** - Username already exists:
```json
{
  "title": "Conflict",
  "status": 409,
  "detail": "Username already exists"
}
```

### 🛡️ Claims Management

#### GET /Claim/GetClaims

Retrieves all available claims (permissions) in the system.

**Request:**

```http
GET /Claim/GetClaims
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Response (200 OK):**

```json
[
  {
    "id": 1,
    "type": "Permission",
    "value": "user:read",
    "description": "Permission to read user data",
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:30:00Z"
  },
  {
    "id": 2,
    "type": "Role",
    "value": "admin",
    "description": "Administrator role",
    "createdAt": "2024-01-15T10:35:00Z",
    "updatedAt": "2024-01-15T10:35:00Z"
  }
]
```

#### GET /Claim/GetClaimById/{id}

Retrieves a specific claim by its ID.

**Request:**

```http
GET /Claim/GetClaimById/1
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Response (200 OK):**

```json
{
  "id": 1,
  "type": "Permission",
  "value": "user:read",
  "description": "Permission to read user data",
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": "2024-01-15T10:30:00Z"
}
```

#### POST /Claim/AddClaim

Creates a new claim in the system.

**Request:**

```http
POST /Claim/AddClaim
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "type": "Permission",
  "value": "user:write",
  "description": "Permission to modify user data"
}
```

**Response (200 OK):**

```json
{
  "id": 3,
  "type": "Permission",
  "value": "user:write",
  "description": "Permission to modify user data",
  "createdAt": "2024-01-15T11:00:00Z",
  "updatedAt": "2024-01-15T11:00:00Z"
}
```

#### PUT /Claim/UpdateClaim/{id}

Updates an existing claim.

**Request:**

```http
PUT /Claim/UpdateClaim/1
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "type": "Permission",
  "value": "user:read",
  "description": "Updated: Permission to read user data"
}
```

#### DELETE /Claim/DeleteClaim/{id}

Deletes a claim from the system.

**Request:**

```http
DELETE /Claim/DeleteClaim/1
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Response (200 OK):**

```json
{
  "message": "Claim deleted successfully",
  "success": true
}
```

### ⚡ Actions Management

#### GET /Action/GetActions

Retrieves all system actions that can be performed.

**Request:**

```http
GET /Action/GetActions
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Response (200 OK):**

```json
[
  {
    "id": 1,
    "name": "CreateUser",
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:30:00Z"
  },
  {
    "id": 2,
    "name": "DeleteUser", 
    "createdAt": "2024-01-15T10:35:00Z",
    "updatedAt": "2024-01-15T10:35:00Z"
  }
]
```

#### GET /Action/GetActionById/{id}

Retrieves a specific action by its ID.

#### POST /Action/AddAction

Creates a new system action.

**Request:**

```http
POST /Action/AddAction
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "name": "UpdateUser"
}
```

#### PUT /Action/UpdateAction/{id}

Updates an existing action.

#### DELETE /Action/DeleteAction/{id}

Deletes an action from the system.

### 🔗 Claim-Action Relationships

#### GET /ClaimAction/GetClaimActions

Retrieves all mappings between claims and actions.

**Response (200 OK):**

```json
[
  {
    "id": 1,
    "claimId": 1,
    "actionId": 1,
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:30:00Z"
  }
]
```

#### POST /ClaimAction/AddClaimAction

Maps a claim to an action, defining what actions a claim can perform.

**Request:**

```http
POST /ClaimAction/AddClaimAction
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "claimId": 1,
  "actionId": 2
}
```

### 👤 User Permission Assignments

#### GET /AccountClaimAction/GetAccountClaimActions

Retrieves user permission assignments with optional filtering.

**Request:**

```http
GET /AccountClaimAction/GetAccountClaimActions?accountId=123
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

#### POST /AccountClaimAction/AddAccountClaimAction

Assigns permissions to a user account.

**Request:**

```http
POST /AccountClaimAction/AddAccountClaimAction
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json

{
  "accountId": 123,
  "claimActionId": 1
}
```

## 📝 Data Transfer Objects

### AccountPayLoadDTO

Used for authentication and account creation requests.

```json
{
  "userName": "string",
  "password": "string"
}
```

**Validation Rules:**
- `userName`: Required, 3-50 characters, alphanumeric and underscore only
- `password`: Required, minimum 6 characters

### TokenResponseDTO

Response object for successful token generation.

```json
{
  "accessToken": "string",
  "expiresIn": "number",
  "userName": "string",
  "claims": ["string"]
}
```

### AccountResponseDTO

Response object for account operations.

```json
{
  "id": "number",
  "userName": "string",
  "message": "string",
  "success": "boolean"
}
```

### ClaimPayLoadDTO

Used for creating and updating claims.

```json
{
  "type": "Permission|Role|Feature",
  "value": "string",
  "description": "string"
}
```

**Validation Rules:**
- `type`: Required, must be valid ClaimType enum value
- `value`: Required, unique claim value identifier
- `description`: Optional, claim description

### ClaimResponseDTO

Response object for claim operations.

```json
{
  "id": "number",
  "type": "Permission|Role|Feature",
  "value": "string",
  "description": "string",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

### ActionPayLoadDTO

Used for creating and updating actions.

```json
{
  "name": "string"
}
```

**Validation Rules:**
- `name`: Required, unique action name

### ActionResponseDTO

Response object for action operations.

```json
{
  "id": "number",
  "name": "string",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

### ClaimActionPayLoadDTO

Used for mapping claims to actions.

```json
{
  "claimId": "number",
  "actionId": "number"
}
```

**Validation Rules:**
- `claimId`: Required, must exist in Claims table
- `actionId`: Required, must exist in Actions table

### ClaimActionResponseDTO

Response object for claim-action mappings.

```json
{
  "id": "number",
  "claimId": "number",
  "actionId": "number",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

### AccountClaimActionPayLoadDTO

Used for assigning permissions to user accounts.

```json
{
  "accountId": "number",
  "claimActionId": "number"
}
```

**Validation Rules:**
- `accountId`: Required, must exist in Accounts table
- `claimActionId`: Required, must exist in ClaimActions table

### AccountClaimActionResponseDTO

Response object for user permission assignments.

```json
{
  "id": "number",
  "accountId": "number",
  "claimActionId": "number",
  "createdAt": "datetime",
  "updatedAt": "datetime"
}
```

## 🔒 Security

### JWT Token Structure

The JWT token contains the following claims:

```json
{
  "sub": "username",           // Subject (username)
  "jti": "unique-token-id",    // JWT ID
  "iat": 1642680000,           // Issued at (timestamp)
  "exp": 1642683600,           // Expiration (timestamp)
  "iss": "AuthenticationService",     // Issuer
  "aud": "AuthenticationClients",     // Audience
  "claims": ["user:read", "user:write"] // User permissions
}
```

### Password Security

- Passwords are hashed using Argon2 algorithm
- Minimum password length: 6 characters
- Passwords are never returned in API responses
- Salt is automatically generated for each password

### Rate Limiting

To prevent brute force attacks, the following rate limits apply:

| Endpoint | Rate Limit | Window |
|----------|------------|--------|
| `/Authentication/GenerateToken` | 5 requests | 1 minute |
| `/Authentication/AddAccount` | 3 requests | 5 minutes |

## 🧪 Examples

### cURL Examples

#### Generate Token

```bash
curl -X POST "https://localhost:7001/Authentication/GenerateToken" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "admin",
    "password": "password123"
  }'
```

#### Create Account

```bash
curl -X POST "https://localhost:7001/Authentication/AddAccount" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "newuser",
    "password": "securepassword123"
  }'
```

#### Create Claim

```bash
curl -X POST "https://localhost:7001/Claim/AddClaim" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "Content-Type: application/json" \
  -d '{
    "type": "Permission",
    "value": "user:write",
    "description": "Permission to modify user data"
  }'
```

#### Get All Claims

```bash
curl -X GET "https://localhost:7001/Claim/GetClaims" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

#### Create Action

```bash
curl -X POST "https://localhost:7001/Action/AddAction" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "Content-Type: application/json" \
  -d '{
    "name": "UpdateUser"
  }'
```

#### Map Claim to Action

```bash
curl -X POST "https://localhost:7001/ClaimAction/AddClaimAction" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "Content-Type: application/json" \
  -d '{
    "claimId": 1,
    "actionId": 2
  }'
```

#### Assign Permission to User

```bash
curl -X POST "https://localhost:7001/AccountClaimAction/AddAccountClaimAction" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." \
  -H "Content-Type: application/json" \
  -d '{
    "accountId": 123,
    "claimActionId": 1
  }'
```

### JavaScript Examples

#### Generate Token (Fetch API)

```javascript
const response = await fetch('https://localhost:7001/Authentication/GenerateToken', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify({
    userName: 'admin',
    password: 'password123'
  })
});

const data = await response.json();
console.log('Token:', data.accessToken);
```

#### Create Account (Axios)

```javascript
import axios from 'axios';

try {
  const response = await axios.post('https://localhost:7001/Authentication/AddAccount', {
    userName: 'newuser',
    password: 'securepassword123'
  });
  
  console.log('Account created:', response.data);
} catch (error) {
  console.error('Error:', error.response.data);
}
```

#### Complete RBAC Setup Example

```javascript
// Complete workflow to set up a user with specific permissions
const setupUserPermissions = async (token) => {
  try {
    // 1. Create a claim
    const claimResponse = await axios.post('/Claim/AddClaim', {
      type: 'Permission',
      value: 'reports:view',
      description: 'Permission to view reports'
    }, {
      headers: { Authorization: `Bearer ${token}` }
    });

    // 2. Create an action  
    const actionResponse = await axios.post('/Action/AddAction', {
      name: 'ViewReports'
    }, {
      headers: { Authorization: `Bearer ${token}` }
    });

    // 3. Map claim to action
    const claimActionResponse = await axios.post('/ClaimAction/AddClaimAction', {
      claimId: claimResponse.data.id,
      actionId: actionResponse.data.id
    }, {
      headers: { Authorization: `Bearer ${token}` }
    });

    // 4. Assign permission to user
    const userPermissionResponse = await axios.post('/AccountClaimAction/AddAccountClaimAction', {
      accountId: 123,
      claimActionId: claimActionResponse.data.id
    }, {
      headers: { Authorization: `Bearer ${token}` }
    });

    console.log('User permissions setup completed:', userPermissionResponse.data);
  } catch (error) {
    console.error('Setup failed:', error.response.data);
  }
};
```

### C# Examples

#### Generate Token

```csharp
using System.Text;
using System.Text.Json;

var client = new HttpClient();
var payload = new
{
    userName = "admin",
    password = "password123"
};

var json = JsonSerializer.Serialize(payload);
var content = new StringContent(json, Encoding.UTF8, "application/json");

var response = await client.PostAsync("https://localhost:7001/Authentication/GenerateToken", content);
var result = await response.Content.ReadAsStringAsync();

Console.WriteLine(result);
```

#### Using Generated Token

```csharp
var client = new HttpClient();
client.DefaultRequestHeaders.Authorization = 
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

var response = await client.GetAsync("https://localhost:7001/api/protected-endpoint");
```

## 🚨 Error Handling

### Standard Error Response Format

All error responses follow RFC 7807 (Problem Details for HTTP APIs):

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "Bad Request",
  "status": 400,
  "detail": "The request could not be understood by the server",
  "instance": "/Authentication/GenerateToken",
  "errors": {
    "fieldName": ["Error message"]
  }
}
```

### HTTP Status Codes

| Status Code | Description | When Used |
|-------------|-------------|-----------|
| 200 | OK | Successful operation |
| 400 | Bad Request | Invalid request data or validation errors |
| 401 | Unauthorized | Authentication failed |
| 403 | Forbidden | Valid authentication but insufficient permissions |
| 404 | Not Found | Resource not found |
| 409 | Conflict | Resource conflict (e.g., username already exists) |
| 422 | Unprocessable Entity | Valid syntax but semantic errors |
| 429 | Too Many Requests | Rate limit exceeded |
| 500 | Internal Server Error | Server-side error |

### Validation Errors

Validation errors return detailed field-level error messages:

```json
{
  "title": "Validation Error",
  "status": 400,
  "errors": {
    "UserName": [
      "UserName is required",
      "UserName must be between 3 and 50 characters"
    ],
    "Password": [
      "Password is required",
      "Password must be at least 6 characters"
    ]
  }
}
```

## 🔍 Testing

### Swagger UI

Interactive API documentation is available at:
- **Development**: `https://localhost:7001/`
- **Production**: `https://api.yourdomain.com/swagger`

The Swagger UI allows you to:
- Explore all available endpoints
- Test API calls directly from the browser
- View request/response schemas
- Copy cURL commands for each endpoint

### Health Check

Monitor API health using the health check endpoint:

```bash
curl -X GET "https://localhost:7001/health"
```

**Response:**
```json
{
  "status": "Healthy",
  "totalDuration": "00:00:00.0123456",
  "entries": {
    "database": {
      "status": "Healthy",
      "duration": "00:00:00.0012345"
    }
  }
}
```

## 📚 SDKs and Client Libraries

### Official SDKs

- **.NET SDK**: NuGet package `Authentication.Client`
- **JavaScript SDK**: NPM package `@yourorg/authentication-client`
- **Python SDK**: PyPI package `authentication-client`

### Community SDKs

- **PHP SDK**: Composer package `yourorg/authentication-php`
- **Go SDK**: Go module `github.com/yourorg/authentication-go`
- **Java SDK**: Maven artifact `com.yourorg:authentication-java`

## 🔄 Versioning

The API uses semantic versioning (SemVer) with the following strategy:

- **Major Version** (v1, v2): Breaking changes
- **Minor Version** (v1.1, v1.2): New features, backward compatible
- **Patch Version** (v1.1.1, v1.1.2): Bug fixes, backward compatible

### API Versioning Strategy

- URL path versioning: `/v1/Authentication/GenerateToken`
- Header versioning: `API-Version: 1.0`
- Accept header versioning: `Accept: application/vnd.api+json;version=1`

## 📞 Support

For API support and questions:

- **Documentation**: [API Docs](../../docs/)
- **Issues**: [GitHub Issues](../../issues)
- **Support Email**: api-support@yourdomain.com
- **Developer Forum**: [Community Forum](https://forum.yourdomain.com)