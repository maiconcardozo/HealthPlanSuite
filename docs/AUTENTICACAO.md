# 🔐 Documentação de Autenticação JWT

## Índice
- [Visão Geral](#visão-geral)
- [Funcionamento do JWT](#funcionamento-do-jwt)
- [Como Obter e Usar o Token](#como-obter-e-usar-o-token)
- [Validação do Token](#validação-do-token)
- [Exemplos de Comunicação](#exemplos-de-comunicação)
- [Segurança e Boas Práticas](#segurança-e-boas-práticas)
- [Extensões para Outros Métodos](#extensões-para-outros-métodos)

---

## Visão Geral

O **HealthPlan Suite** utiliza autenticação baseada em **JWT (JSON Web Token)** integrada ao **Authentication Service** para proteger os endpoints da API e gerenciar o controle de acesso dos usuários.

### Por que JWT?

- **Stateless**: Não requer armazenamento de sessão no servidor
- **Escalável**: Facilita a distribuição horizontal da aplicação
- **Seguro**: Assinatura criptográfica garante integridade do token
- **Portátil**: Pode ser usado entre diferentes domínios e serviços
- **Auto-contido**: Contém todas as informações necessárias sobre o usuário

### Arquitetura de Autenticação

```
┌─────────────┐         ┌──────────────────┐         ┌─────────────┐
│   Cliente   │────1───>│ Authentication   │────2───>│  Database   │
│             │         │    Service       │         │             │
│             │<───4────│                  │<───3────│             │
└─────────────┘         └──────────────────┘         └─────────────┘
      │
      │ 5. Requisições com Token JWT
      ↓
┌─────────────────────────────────────────────────────────┐
│              API Protegida (Endpoints)                   │
│  - JWT Middleware valida token                          │
│  - Extrai claims e permissões                           │
│  - Autoriza acesso aos recursos                         │
└─────────────────────────────────────────────────────────┘
```

**Fluxo:**
1. Cliente envia credenciais (usuário/senha)
2. Authentication Service valida credenciais no banco de dados
3. Banco retorna dados do usuário e suas permissões
4. Serviço gera JWT token e retorna ao cliente
5. Cliente usa token JWT para acessar endpoints protegidos

---

## Funcionamento do JWT

### Estrutura do Token JWT

Um token JWT é composto por três partes separadas por pontos (`.`):

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbiIsImp0aSI6IjEyMzQ1Njc4LTkwYWItY2RlZi0xMjM0LTU2Nzg5MGFiY2RlZiIsImlhdCI6MTY0MjY4MDAwMCwiZXhwIjoxNjQyNjgzNjAwLCJpc3MiOiJBdXRoZW50aWNhdGlvbiIsImF1ZCI6IkF1dGhlbnRpY2F0aW9uQ2xpZW50cyJ9.signature_hash_value

│                  Header                  │                          Payload                                    │  Signature  │
```

#### 1. Header (Cabeçalho)
Contém informações sobre o tipo de token e algoritmo de assinatura:

```json
{
  "alg": "HS256",
  "typ": "JWT"
}
```

- **alg**: Algoritmo de criptografia usado (HMAC-SHA256)
- **typ**: Tipo do token (JWT)

#### 2. Payload (Carga Útil)
Contém as claims (declarações) sobre o usuário e metadados do token:

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

**Claims Padrão (Registered Claims):**
- **sub** (Subject): Identificador do usuário
- **jti** (JWT ID): ID único do token
- **iat** (Issued At): Timestamp de quando o token foi criado
- **exp** (Expiration): Timestamp de quando o token expira
- **iss** (Issuer): Emissor do token (Authentication Service)
- **aud** (Audience): Destinatário do token (quem pode usá-lo)

**Claims Customizadas:**
- **userName**: Nome do usuário
- **userId**: ID do usuário no sistema
- **claims**: Array de permissões do usuário

#### 3. Signature (Assinatura)
Garante a integridade do token e verifica que não foi alterado:

```
HMACSHA256(
  base64UrlEncode(header) + "." + base64UrlEncode(payload),
  secret_key
)
```

### Configuração JWT no HealthPlan Suite

A configuração JWT está definida no `appsettings.json`:

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

**Parâmetros:**
- **Issuer**: Identifica quem emitiu o token
- **Audience**: Define para quem o token é válido
- **SecretKey**: Chave secreta para assinar tokens (mínimo 32 caracteres)
- **ExpirationMinutes**: Tempo de validade do token em minutos

⚠️ **IMPORTANTE**: Em produção, NUNCA armazene a `SecretKey` diretamente no arquivo de configuração. Use variáveis de ambiente ou Azure Key Vault.

### Validação do Token pelo Servidor

O servidor valida automaticamente os seguintes aspectos:

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
        ValidateIssuer = true,              // Valida o emissor
        ValidateAudience = true,            // Valida o destinatário
        ValidateLifetime = true,            // Valida expiração
        ValidateIssuerSigningKey = true,    // Valida assinatura
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero           // Sem tolerância de tempo
    };
});
```

---

## Como Obter e Usar o Token

### Passo 1: Criar uma Conta de Usuário

Antes de autenticar, você precisa ter uma conta cadastrada.

**Endpoint:** `POST /Account/AddAccount`

**Requisição:**
```bash
curl -X POST "https://localhost:7001/Account/AddAccount" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "usuario_exemplo",
    "password": "SenhaSegura123!",
    "email": "usuario@exemplo.com"
  }'
```

**Resposta de Sucesso (200):**
```json
{
  "type": "https://datatracker.ietf.org/doc/html/rfc7231#section-6.3.1",
  "title": "OK",
  "status": 200,
  "detail": "Request was successful.",
  "instance": "/Account/AddAccount",
  "data": {
    "userId": 123,
    "userName": "usuario_exemplo",
    "email": "usuario@exemplo.com"
  }
}
```

### Passo 2: Autenticar e Obter Token JWT

Uma vez que você tem uma conta, pode autenticar para obter o token JWT.

**Endpoint:** `POST /Authentication/GenerateToken`

**Requisição:**
```bash
curl -X POST "https://localhost:7001/Authentication/GenerateToken" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "usuario_exemplo",
    "password": "SenhaSegura123!"
  }'
```

**Resposta de Sucesso (200):**
```json
{
  "data": {
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c3VhcmlvX2V4ZW1wbG8iLCJqdGkiOiIxMjM0NTY3OC05MGFiLWNkZWYtMTIzNC01Njc4OTBhYmNkZWYiLCJpYXQiOjE2NDI2ODAwMDAsImV4cCI6MTY0MjY4MzYwMCwiaXNzIjoiQXV0aGVudGljYXRpb24iLCJhdWQiOiJBdXRoZW50aWNhdGlvbkNsaWVudHMifQ.signature",
    "expiresIn": 3600,
    "userName": "usuario_exemplo",
    "claims": [
      "user:read",
      "user:write"
    ]
  }
}
```

**Campos da Resposta:**
- **accessToken**: Token JWT para usar nas requisições
- **expiresIn**: Tempo de validade em segundos (3600 = 1 hora)
- **userName**: Nome do usuário autenticado
- **claims**: Permissões do usuário

### Passo 3: Usar o Token em Requisições

Inclua o token JWT no header `Authorization` com o prefixo `Bearer`:

```bash
curl -X GET "https://localhost:7001/Quote/GetQuotes" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

**Formato do Header:**
```
Authorization: Bearer <seu_token_jwt>
```

### Exemplo Completo em Diferentes Linguagens

#### JavaScript/TypeScript (Fetch API)

```javascript
// 1. Função para autenticar e obter token
async function authenticate(userName, password) {
  const response = await fetch('https://localhost:7001/Authentication/GenerateToken', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ userName, password })
  });

  if (!response.ok) {
    throw new Error('Autenticação falhou');
  }

  const data = await response.json();
  return data.data.accessToken;
}

// 2. Função para fazer requisição autenticada
async function getQuotes(token) {
  const response = await fetch('https://localhost:7001/Quote/GetQuotes', {
    method: 'GET',
    headers: {
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    }
  });

  if (!response.ok) {
    throw new Error('Erro ao buscar cotações');
  }

  return await response.json();
}

// 3. Uso
async function main() {
  try {
    // Obter token
    const token = await authenticate('usuario_exemplo', 'SenhaSegura123!');
    console.log('Token obtido:', token);

    // Armazenar token (localStorage, sessionStorage, etc.)
    localStorage.setItem('jwt_token', token);

    // Usar token para fazer requisição
    const quotes = await getQuotes(token);
    console.log('Cotações:', quotes);
  } catch (error) {
    console.error('Erro:', error);
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

    // 1. Autenticar e obter token
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

    // 2. Fazer requisição autenticada
    public async Task<List<Quote>> GetQuotesAsync()
    {
        if (string.IsNullOrEmpty(_token))
            throw new InvalidOperationException("Não autenticado. Chame AuthenticateAsync primeiro.");

        _httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", _token);

        var response = await _httpClient.GetAsync("/Quote/GetQuotes");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Quote>>(json);
    }
}

// Classes de modelo
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

// Uso
var client = new AuthenticationClient();
await client.AuthenticateAsync("usuario_exemplo", "SenhaSegura123!");
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
        """Autentica e obtém token JWT"""
        url = f"{self.base_url}/Authentication/GenerateToken"
        payload = {
            "userName": username,
            "password": password
        }
        
        response = self.session.post(url, json=payload, verify=False)
        response.raise_for_status()
        
        data = response.json()
        self.token = data['data']['accessToken']
        
        # Configura header de autenticação para próximas requisições
        self.session.headers.update({
            'Authorization': f'Bearer {self.token}'
        })
        
        return self.token

    def get_quotes(self):
        """Busca cotações usando token JWT"""
        if not self.token:
            raise ValueError("Não autenticado. Chame authenticate() primeiro.")
        
        url = f"{self.base_url}/Quote/GetQuotes"
        response = self.session.get(url, verify=False)
        response.raise_for_status()
        
        return response.json()

# Uso
client = AuthenticationClient()
token = client.authenticate("usuario_exemplo", "SenhaSegura123!")
print(f"Token obtido: {token[:50]}...")

quotes = client.get_quotes()
print(f"Cotações encontradas: {len(quotes)}")
```

---

## Validação do Token

### Validação Automática

O middleware JWT do ASP.NET Core valida automaticamente todos os tokens recebidos:

```csharp
// Configurado em Startup.cs
app.UseAuthentication();  // Middleware de autenticação JWT
app.UseAuthorization();   // Middleware de autorização
```

### O que é Validado?

1. **Assinatura**: Verifica se o token foi assinado com a chave secreta correta
2. **Emissor (Issuer)**: Confirma que o token foi emitido pelo servidor esperado
3. **Destinatário (Audience)**: Verifica se o token é destinado a esta aplicação
4. **Expiração (Expiration)**: Garante que o token ainda não expirou
5. **Formato**: Valida a estrutura do token JWT

### Fluxo de Validação

```
Cliente envia requisição
        ↓
┌───────────────────────────────────┐
│   JWT Middleware Intercepta       │
└───────────────────────────────────┘
        ↓
┌───────────────────────────────────┐
│   Extrai token do header          │
│   Authorization: Bearer <token>   │
└───────────────────────────────────┘
        ↓
┌───────────────────────────────────┐
│   Valida Assinatura               │
│   ✓ Token foi assinado com        │
│     SecretKey correto?             │
└───────────────────────────────────┘
        ↓
┌───────────────────────────────────┐
│   Valida Claims                   │
│   ✓ Issuer correto?               │
│   ✓ Audience correto?             │
│   ✓ Token não expirado?           │
└───────────────────────────────────┘
        ↓
    ┌───────┐
    │Válido?│
    └───┬───┘
        │
    ┌───┴────────────────┐
    │                    │
   Sim                  Não
    │                    │
    ↓                    ↓
┌────────┐       ┌──────────────┐
│ 200 OK │       │ 401 Unauthorized │
└────────┘       └──────────────┘
```

### Respostas de Erro de Validação

#### Token Expirado (401)
```json
{
  "title": "Unauthorized",
  "status": 401,
  "detail": "Token has expired",
  "type": "https://tools.ietf.org/html/rfc7235#section-3.1"
}
```

#### Token Inválido ou Assinatura Incorreta (401)
```json
{
  "title": "Unauthorized",
  "status": 401,
  "detail": "Invalid token signature",
  "type": "https://tools.ietf.org/html/rfc7235#section-3.1"
}
```

#### Token Ausente (401)
```json
{
  "title": "Unauthorized",
  "status": 401,
  "detail": "Authorization header is missing",
  "type": "https://tools.ietf.org/html/rfc7235#section-3.1"
}
```

### Validação Manual (Opcional)

Se você precisar validar manualmente um token JWT (por exemplo, em um serviço externo):

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

## Exemplos de Comunicação

### Cenário 1: Fluxo Completo de Autenticação

```bash
# 1. Criar conta
curl -X POST "https://localhost:7001/Account/AddAccount" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "joao_silva",
    "password": "MinhaSenh@123",
    "email": "joao@exemplo.com"
  }'

# Resposta:
# {
#   "status": 200,
#   "data": {
#     "userId": 456,
#     "userName": "joao_silva",
#     "email": "joao@exemplo.com"
#   }
# }

# 2. Autenticar e obter token
TOKEN=$(curl -X POST "https://localhost:7001/Authentication/GenerateToken" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "joao_silva",
    "password": "MinhaSenh@123"
  }' | jq -r '.data.accessToken')

echo "Token: $TOKEN"

# 3. Usar token para acessar recurso protegido
curl -X GET "https://localhost:7001/Quote/GetQuotes" \
  -H "Authorization: Bearer $TOKEN"
```

### Cenário 2: Renovação de Token

Quando o token expira, você precisa autenticar novamente:

```javascript
class TokenManager {
  constructor() {
    this.token = null;
    this.expiresAt = null;
  }

  async getValidToken(userName, password) {
    // Verifica se o token ainda é válido
    if (this.token && this.expiresAt && Date.now() < this.expiresAt) {
      return this.token;
    }

    // Token expirado ou inexistente, obter novo
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
    
    // Define tempo de expiração (diminui 5 minutos para margem de segurança)
    this.expiresAt = Date.now() + (data.data.expiresIn - 300) * 1000;
    
    return this.token;
  }
}

// Uso
const tokenManager = new TokenManager();

async function makeAuthenticatedRequest() {
  const token = await tokenManager.getValidToken('joao_silva', 'MinhaSenh@123');
  
  const response = await fetch('https://localhost:7001/Quote/GetQuotes', {
    headers: { 'Authorization': `Bearer ${token}` }
  });
  
  return await response.json();
}
```

### Cenário 3: Tratamento de Erros de Autenticação

```javascript
async function authenticatedFetch(url, options = {}) {
  const token = localStorage.getItem('jwt_token');
  
  if (!token) {
    throw new Error('Token não encontrado. Por favor, faça login.');
  }

  // Adiciona token ao header
  const headers = {
    ...options.headers,
    'Authorization': `Bearer ${token}`
  };

  const response = await fetch(url, { ...options, headers });

  // Trata erro de autenticação
  if (response.status === 401) {
    // Token inválido ou expirado
    localStorage.removeItem('jwt_token');
    throw new Error('Sessão expirada. Por favor, faça login novamente.');
  }

  // Trata erro de autorização (sem permissão)
  if (response.status === 403) {
    throw new Error('Você não tem permissão para acessar este recurso.');
  }

  if (!response.ok) {
    const error = await response.json();
    throw new Error(error.detail || 'Erro na requisição');
  }

  return await response.json();
}

// Uso com tratamento de erro
try {
  const quotes = await authenticatedFetch('https://localhost:7001/Quote/GetQuotes');
  console.log('Cotações:', quotes);
} catch (error) {
  console.error('Erro:', error.message);
  // Redirecionar para página de login se necessário
  if (error.message.includes('login')) {
    window.location.href = '/login';
  }
}
```

### Cenário 4: Sistema RBAC - Verificação de Permissões

```bash
# 1. Autenticar como administrador
TOKEN=$(curl -X POST "https://localhost:7001/Authentication/GenerateToken" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "admin",
    "password": "AdminPassword123!"
  }' | jq -r '.data.accessToken')

# 2. Criar uma permissão (Claim)
CLAIM_ID=$(curl -X POST "https://localhost:7001/Claim/AddClaim" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "type": "Permission",
    "value": "quote:manage",
    "description": "Gerenciar cotações"
  }' | jq -r '.data.claimId')

# 3. Criar uma ação
ACTION_ID=$(curl -X POST "https://localhost:7001/Action/AddAction" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "CreateQuote",
    "description": "Criar nova cotação"
  }' | jq -r '.data.actionId')

# 4. Associar Claim à Ação
CLAIM_ACTION_ID=$(curl -X POST "https://localhost:7001/ClaimAction/AddClaimAction" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "claimId": '$CLAIM_ID',
    "actionId": '$ACTION_ID'
  }' | jq -r '.data.claimActionId')

# 5. Atribuir permissão a um usuário
curl -X POST "https://localhost:7001/AccountClaimAction/AddAccountClaimAction" \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "accountId": 456,
    "claimActionId": '$CLAIM_ACTION_ID'
  }'

# Agora o usuário com accountId 456 tem permissão para criar cotações
```

---

## Segurança e Boas Práticas

### 🔒 Configuração Segura da SecretKey

#### ❌ NUNCA FAÇA ISSO (Produção)
```json
// appsettings.json
{
  "JwtSettings": {
    "SecretKey": "minha-chave-secreta-123"  // ❌ INSEGURO!
  }
}
```

#### ✅ FAÇA ISSO

**Opção 1: Variáveis de Ambiente**
```bash
# Linux/Mac
export JwtSettings__SecretKey="sua-chave-muito-segura-com-no-minimo-32-caracteres-aleatorios"

# Windows PowerShell
$env:JwtSettings__SecretKey="sua-chave-muito-segura-com-no-minimo-32-caracteres-aleatorios"

# Docker
docker run -e JwtSettings__SecretKey="sua-chave-segura" myapp
```

**Opção 2: Azure Key Vault**
```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// Adiciona Azure Key Vault
if (!builder.Environment.IsDevelopment())
{
    var keyVaultEndpoint = new Uri(builder.Configuration["KeyVaultEndpoint"]);
    builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
}
```

**Opção 3: User Secrets (Desenvolvimento)**
```bash
# Inicializar user secrets
dotnet user-secrets init --project Src/HealthPlan.API

# Adicionar secret
dotnet user-secrets set "JwtSettings:SecretKey" "chave-de-desenvolvimento-32-chars" --project Src/HealthPlan.API

# Listar secrets
dotnet user-secrets list --project Src/HealthPlan.API
```

### 🛡️ Segurança da SecretKey

**Requisitos da Chave:**
- Mínimo de **32 caracteres**
- Use caracteres aleatórios (letras, números, símbolos)
- Nunca compartilhe ou versione no Git
- Rotacione periodicamente (a cada 90 dias recomendado)

**Gerar Chave Segura:**
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

### 🔐 Armazenamento Seguro do Token (Cliente)

#### ❌ NÃO Armazene em localStorage (Vulnerável a XSS)
```javascript
// ❌ INSEGURO - Vulnerável a ataques XSS
localStorage.setItem('jwt_token', token);
```

#### ✅ Armazene em httpOnly Cookie
```javascript
// Servidor (ASP.NET Core) - Define cookie httpOnly
Response.Cookies.Append("jwt_token", token, new CookieOptions
{
    HttpOnly = true,    // Não acessível via JavaScript
    Secure = true,      // Apenas HTTPS
    SameSite = SameSiteMode.Strict,  // Proteção CSRF
    Expires = DateTimeOffset.UtcNow.AddHours(1)
});

// Cliente - O cookie é enviado automaticamente
fetch('https://localhost:7001/Quote/GetQuotes', {
    credentials: 'include'  // Inclui cookies na requisição
});
```

#### ✅ Alternativa: sessionStorage (Mais Seguro que localStorage)
```javascript
// Melhor que localStorage, mas ainda vulnerável a XSS
// Use apenas se não puder usar httpOnly cookies
sessionStorage.setItem('jwt_token', token);
```

### ⏱️ Tempo de Expiração Apropriado

| Ambiente | Tempo Recomendado | Motivo |
|----------|-------------------|--------|
| **Desenvolvimento** | 60 minutos | Conveniência para testes |
| **Produção (Público)** | 15-30 minutos | Balance entre segurança e UX |
| **Produção (Admin)** | 5-15 minutos | Alta segurança para operações críticas |
| **API Interna** | 1-2 horas | Comunicação entre serviços confiáveis |

```json
{
  "JwtSettings": {
    "ExpirationMinutes": 15  // 15 minutos para produção
  }
}
```

### 🔒 HTTPS Obrigatório

**Desenvolvimento:**
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

**Produção:**
```csharp
// Startup.cs
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();  // HTTP Strict Transport Security
    app.UseHttpsRedirection();  // Redireciona HTTP para HTTPS
}
```

### 🛡️ Validação de Entrada

Sempre valide entradas para prevenir ataques:

```csharp
public class LoginRequestValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username é obrigatório")
            .Length(3, 50).WithMessage("Username deve ter entre 3 e 50 caracteres")
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("Username deve conter apenas letras, números e underscore");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password é obrigatório")
            .MinimumLength(8).WithMessage("Password deve ter no mínimo 8 caracteres");
    }
}
```

### 🔐 Hash de Senha (Argon2)

O sistema usa **Argon2** para hash de senhas (melhor que bcrypt/SHA):

```csharp
// A senha nunca é armazenada em texto puro
public void AddAccount(Account account)
{
    // Gera hash seguro com Argon2
    account.Password = _passwordHasher.Hash(account.Password);
    _unitOfWork.AccountRepository.Add(account);
    _unitOfWork.Complete();
}

// Verificação segura
public Account GetAccountByUserNameAndPassword(Account account)
{
    var dbAccount = _unitOfWork.AccountRepository.GetByUserName(account.UserName);
    if (dbAccount == null)
        throw new InvalidOperationException("Conta não encontrada");
    
    // Verifica hash de forma segura (constant-time comparison)
    if (_passwordHasher.Verify(account.Password, dbAccount.Password))
        return dbAccount;
    
    throw new UnauthorizedAccessException("Senha inválida");
}
```

### 📋 Checklist de Segurança

- [ ] **SecretKey** possui no mínimo 32 caracteres aleatórios
- [ ] **SecretKey** armazenada em variável de ambiente ou Key Vault
- [ ] **HTTPS** habilitado em produção
- [ ] **Tempo de expiração** apropriado (15-30min produção)
- [ ] **Token** armazenado em httpOnly cookie (não localStorage)
- [ ] **Validação de entrada** implementada (FluentValidation)
- [ ] **Hash de senha** usando Argon2
- [ ] **Rate limiting** configurado para prevenir brute force
- [ ] **CORS** configurado adequadamente
- [ ] **Security headers** adicionados (HSTS, X-Frame-Options, etc.)
- [ ] **Logging** de tentativas de autenticação falhadas
- [ ] **Rotação de chaves** agendada (90 dias)

### 🚨 Monitoramento e Logging

```csharp
public async Task<IActionResult> GenerateToken([FromBody] LoginRequestDTO request)
{
    try
    {
        _logger.LogInformation("Tentativa de login para usuário: {UserName}", request.UserName);
        
        var response = await _authService.AuthenticateAsync(request);
        
        _logger.LogInformation("Login bem-sucedido para usuário: {UserName}", request.UserName);
        return Ok(response);
    }
    catch (UnauthorizedAccessException ex)
    {
        _logger.LogWarning("Falha de autenticação para usuário: {UserName}. Motivo: {Reason}", 
            request.UserName, ex.Message);
        return Unauthorized("Credenciais inválidas");
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Erro durante autenticação para usuário: {UserName}", request.UserName);
        return StatusCode(500, "Erro interno do servidor");
    }
}
```

---

## Extensões para Outros Métodos

O sistema atual usa JWT com autenticação por usuário/senha, mas pode ser estendido para suportar outros métodos de autenticação.

### 1. OAuth 2.0 / OpenID Connect

Integração com provedores externos (Google, Microsoft, Facebook):

```csharp
// Startup.cs
services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => { /* configuração JWT existente */ })
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

**Fluxo OAuth:**
```
Cliente → Redireciona para Google
Google → Usuário autentica
Google → Redireciona de volta com código
Servidor → Troca código por token
Servidor → Cria conta/sessão do usuário
Servidor → Gera JWT token próprio
Cliente → Recebe JWT token
```

### 2. Two-Factor Authentication (2FA)

Adicionar segunda camada de segurança:

```csharp
public class TwoFactorAuthService
{
    private readonly IMemoryCache _cache;
    private readonly IEmailService _emailService;

    // Gerar código 2FA
    public string GenerateTwoFactorCode(string userName)
    {
        var code = new Random().Next(100000, 999999).ToString();
        _cache.Set($"2fa:{userName}", code, TimeSpan.FromMinutes(5));
        return code;
    }

    // Enviar código por email
    public async Task SendTwoFactorCodeAsync(string userName, string email)
    {
        var code = GenerateTwoFactorCode(userName);
        await _emailService.SendEmailAsync(email, "Código de Verificação", 
            $"Seu código de verificação é: {code}");
    }

    // Validar código
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
    // 1. Valida usuário/senha
    var account = await _authService.ValidateCredentialsAsync(request);
    
    // 2. Se 2FA está habilitado, envia código
    if (account.TwoFactorEnabled)
    {
        await _twoFactorService.SendTwoFactorCodeAsync(account.UserName, account.Email);
        return Ok(new { requiresTwoFactor = true });
    }
    
    // 3. Gera token normalmente se 2FA não está habilitado
    return Ok(await _authService.GenerateTokenAsync(account));
}

[HttpPost("VerifyTwoFactor")]
public async Task<IActionResult> VerifyTwoFactor([FromBody] TwoFactorRequestDTO request)
{
    // Valida código 2FA
    if (!_twoFactorService.ValidateTwoFactorCode(request.UserName, request.Code))
    {
        return Unauthorized("Código inválido ou expirado");
    }
    
    // Gera token JWT
    var account = await _authService.GetAccountByUserNameAsync(request.UserName);
    return Ok(await _authService.GenerateTokenAsync(account));
}
```

### 3. API Keys

Para autenticação de serviços externos:

```csharp
public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
{
    private const string ApiKeyHeaderName = "X-API-Key";

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
        {
            return AuthenticateResult.Fail("API Key header não encontrado");
        }

        var providedApiKey = apiKeyHeaderValues.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(providedApiKey))
        {
            return AuthenticateResult.Fail("API Key vazia");
        }

        // Validar API Key no banco de dados
        var apiKey = await _apiKeyRepository.ValidateApiKeyAsync(providedApiKey);
        if (apiKey == null)
        {
            return AuthenticateResult.Fail("API Key inválida");
        }

        // Criar claims e identity
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

// Uso
[ApiController]
[Route("[controller]")]
public class ExternalApiController : ControllerBase
{
    [HttpGet("data")]
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public IActionResult GetData()
    {
        return Ok("Dados protegidos por API Key");
    }
}
```

### 4. Refresh Tokens

Implementar tokens de longa duração para renovar access tokens:

```csharp
public class TokenService
{
    // Gerar access token e refresh token
    public TokenResponseDTO GenerateTokens(Account account)
    {
        // Access token (curta duração - 15 minutos)
        var accessToken = GenerateJwtToken(account, TimeSpan.FromMinutes(15));
        
        // Refresh token (longa duração - 7 dias)
        var refreshToken = GenerateRefreshToken();
        StoreRefreshToken(account.Id, refreshToken, TimeSpan.FromDays(7));
        
        return new TokenResponseDTO
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = 900 // 15 minutos
        };
    }

    // Renovar access token usando refresh token
    public async Task<TokenResponseDTO> RefreshAccessTokenAsync(string refreshToken)
    {
        var storedToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
        
        if (storedToken == null || storedToken.ExpiresAt < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Refresh token inválido ou expirado");
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

// Endpoint para renovar token
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
        return Unauthorized("Refresh token inválido");
    }
}
```

### 5. Autenticação por Certificado (mTLS)

Para comunicação segura entre serviços:

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
                // Validar certificado personalizado
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

### 6. Biometria / WebAuthn

Autenticação sem senha usando biometria:

```csharp
// Requer biblioteca Fido2.AspNet
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
    // Cria desafio de registro
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

Integração com SAML ou OpenID Connect para SSO corporativo:

```csharp
// Adicionar Sustainsys.Saml2 ou IdentityServer
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

### Comparação de Métodos

| Método | Segurança | Complexidade | Uso Recomendado |
|--------|-----------|--------------|-----------------|
| **JWT (atual)** | ⭐⭐⭐⭐ | ⭐⭐ | APIs REST, SPAs |
| **OAuth 2.0** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ | Login social, delegação |
| **2FA** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ | Operações sensíveis |
| **API Keys** | ⭐⭐⭐ | ⭐ | Integração B2B |
| **Refresh Tokens** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ | Aplicações móveis |
| **mTLS** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ | Microsserviços |
| **WebAuthn** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐ | Autenticação sem senha |
| **SSO/SAML** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ | Empresas corporativas |

---

## Recursos Adicionais

### Documentação Relacionada

- **[API.md](./API.md)** - Documentação completa da API
- **[SECURITY.md](./SECURITY.md)** - Configuração de segurança detalhada
- **[EXAMPLES.md](./EXAMPLES.md)** - Exemplos práticos de integração
- **[DEVELOPMENT.md](./DEVELOPMENT.md)** - Guia de desenvolvimento

### Links Externos

- [JWT.io](https://jwt.io/) - Debugger e documentação JWT
- [RFC 7519](https://tools.ietf.org/html/rfc7519) - Especificação JWT
- [OWASP Authentication Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Authentication_Cheat_Sheet.html)
- [ASP.NET Core Authentication](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/)
- [Argon2 Password Hashing](https://github.com/P-H-C/phc-winner-argon2)

### Ferramentas Úteis

- **Postman** - Testar API com autenticação JWT
- **JWT.io Debugger** - Decodificar e validar tokens
- **Azure Key Vault** - Gerenciamento seguro de chaves
- **HashiCorp Vault** - Alternativa open-source para gerenciamento de secrets

---

## Suporte

Para questões, sugestões ou reportar problemas:
- Abra uma [issue](https://github.com/maiconcardozo/HealthPlanSuite/issues)
- Entre em contato através do GitHub

---

⭐ Se esta documentação foi útil, considere dar uma estrela no projeto!
