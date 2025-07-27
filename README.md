# Authentication - Serviço de Autenticação para .NET

[![.NET 9.0](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/9.0)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-9.0-blue.svg)](https://docs.microsoft.com/en-us/ef/core/)
[![JWT](https://img.shields.io/badge/JWT-Authentication-green.svg)](https://jwt.io/)

## 📋 Visão Geral

O **Authentication** é um serviço .NET que fornece autenticação segura e autorização para aplicações seguindo os princípios de Domain-Driven Design (DDD). Este serviço implementa autenticação JWT, hash seguro de senhas com Argon2, e um sistema completo de **Role-Based Access Control (RBAC)** com gerenciamento de claims, ações e permissões de usuários.

### 🔐 Funcionalidades Principais

- **Autenticação JWT**: Geração e validação de tokens seguros
- **Gerenciamento de Usuários**: Criação e administração de contas
- **Sistema RBAC Completo**: 
  - **Claims**: Definição de permissões e roles
  - **Actions**: Ações disponíveis no sistema
  - **ClaimActions**: Mapeamento de permissões para ações
  - **AccountClaimActions**: Atribuição de permissões a usuários
- **API RESTful Completa**: CRUD endpoints para todas as entidades
- **Segurança Avançada**: Hash Argon2, validação de entrada, middleware de segurança

## 🏗️ Arquitetura

O serviço está organizado em camadas bem definidas seguindo os princípios de Clean Architecture:

```
Authentication/
├── Src/
│   ├── Authentication.API/           # Camada de API
│   │   ├── Controllers/             # Controllers da API
│   │   │   ├── AuthenticationController.cs  # Autenticação básica
│   │   │   ├── ClaimController.cs          # Gerenciamento de claims
│   │   │   ├── ActionController.cs         # Gerenciamento de ações
│   │   │   ├── ClaimActionController.cs    # Mapeamento claim-ação
│   │   │   └── AccountClaimActionController.cs # Permissões de usuários
│   │   ├── Middleware/              # Middleware customizado
│   │   ├── Swagger/                 # Documentação da API
│   │   └── Data/                    # Contextos do banco de dados
│   │
│   └── Authentication.Login/        # Domínio & Lógica de Negócio
│       ├── Domain/                  # Entidades de domínio
│       │   ├── Implementation/      # Implementações concretas
│       │   │   ├── Account.cs      # Entidade de usuário
│       │   │   ├── Claim.cs        # Claims/Permissões
│       │   │   ├── Action.cs       # Ações do sistema
│       │   │   ├── ClaimAction.cs  # Relação claim-ação
│       │   │   └── AccountClaimAction.cs # Permissões do usuário
│       │   └── Interface/          # Interfaces de domínio
│       ├── Services/               # Serviços de negócio
│       │   ├── Implementation/     # Implementações de serviços
│       │   └── Interface/         # Contratos de serviços
│       ├── Repository/             # Camada de acesso a dados
│       │   ├── Implementation/     # Implementações de repositórios
│       │   └── Interface/         # Contratos de repositórios
│       ├── DTO/                   # Objetos de transferência
│       ├── Infrastructure/        # Configurações de entidade
│       │   ├── Implementation/    # Mapeamentos EF Core
│       │   └── Interface/        # Contratos de contexto
│       └── UnitOfWork/           # Padrão Unit of Work
│           ├── Implementation/    # Implementação do UoW
│           └── Interface/        # Contrato do UoW
│
└── Foundation.Base/                 # Biblioteca base compartilhada
│   ├── Domain/                      # Entidades base de domínio
│   ├── Repository/                  # Padrões de repositório genéricos
│   ├── UnitOfWork/                  # Gerenciamento de transações
│   └── Util/                        # Utilitários comuns
│
└── Solution/                        # Configuração da solução
```

## 🔧 Tecnologias Utilizadas

- **.NET 9.0** - Framework principal
- **ASP.NET Core 9.0** - Framework para API RESTful
- **Entity Framework Core 9.0** - ORM para acesso a dados
- **JWT Bearer** - Autenticação baseada em tokens
- **FluentValidation** - Validação de entrada
- **Argon2** - Hash seguro de senhas
- **MySQL/MariaDB** - Suporte a banco de dados
- **Swagger/OpenAPI** - Documentação da API

## 🚀 Desenvolvimento (Início Rápido)

### Configuração do Ambiente de Desenvolvimento

```bash
# 1. Clone o repositório
git clone https://github.com/maiconcardozo/Authentication.git
cd Authentication

# 2. Instalar .NET 9.0 SDK (se não tiver)
# Baixe de: https://dotnet.microsoft.com/download/dotnet/9.0

# 3. Restaurar dependências
dotnet restore Solution/Authentication.sln

# 4. Compilar em modo Debug (desenvolvimento)
dotnet build Solution/Authentication.sln --configuration Debug

# 5. Executar a API
cd Src/Authentication.API
dotnet run --configuration Debug
```

### 🎯 Configuração Recomendada para Desenvolvimento

O projeto está otimizado para desenvolvimento local com **Debug** como configuração padrão:

```bash
# Configuração de desenvolvimento ativa por padrão
export ASPNETCORE_ENVIRONMENT=Development
export DOTNET_ENVIRONMENT=Development

# Build contínuo durante desenvolvimento
dotnet watch run --configuration Debug
```

### 💻 IDEs Recomendadas
- **Visual Studio 2022** (17.14+) com workload .NET
- **Visual Studio Code** com extensão C# Dev Kit
- **JetBrains Rider** 2024.1+

## 📦 Instalação para Produção

### Pré-requisitos
- .NET 9.0 SDK ou superior
- MySQL 8.0+ ou superior
- Entity Framework Core 9.0

### Clonando e compilando localmente
```bash
git clone https://github.com/maiconcardozo/Authentication.git
cd Authentication
dotnet build Solution/Authentication.sln --configuration Release
```

## 🚀 Uso Rápido (Desenvolvimento)

> **💡 Foco em Desenvolvimento**: Todos os exemplos priorizam configurações e práticas de desenvolvimento para facilitar a experiência do desenvolvedor.

### 1. Configurando o Banco de Dados (Desenvolvimento)

Atualize a string de conexão em `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=AuthenticationDB;Uid=seu_usuario;Pwd=sua_senha;"
  },
  "JwtSettings": {
    "Issuer": "Authentication",
    "Audience": "AuthenticationClients",
    "SecretKey": "sua-chave-secreta-de-32-caracteres-minimo",
    "ExpirationMinutes": 60
  }
}
```

### 2. Inicializando o Banco de Dados

```bash
cd Src/Authentication.API
dotnet ef database update --context ApiContextDevelopment
```

### 3. Configuração do JWT para Desenvolvimento

```csharp
// Program.cs - Configuração específica para desenvolvimento
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Configuração específica para desenvolvimento
        if (builder.Environment.IsDevelopment())
        {
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            builder.Logging.SetMinimumLevel(LogLevel.Debug);
        }
        
        // Configuração JWT para desenvolvimento
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
                };
                
                #if DEBUG
                // Configurações específicas para debugging
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine($"Token validado para: {context.Principal?.Identity?.Name}");
                        return Task.CompletedTask;
                    }
                };
                #endif
            });
        
        var app = builder.Build();
        
        // Middleware específico para desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.Run();
    }
}
```

### 4. Usando o Serviço de Autenticação (Com Debug)

```csharp
using Authentication.Login.Services;
using Authentication.Login.DTO;
using Microsoft.Extensions.Logging;

public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;
    private readonly ILogger<AuthController> _logger;
    
    public AuthController(
        IAuthenticationService authService,
        ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }
    
    [HttpPost("GenerateToken")]
    public async Task<IActionResult> GenerateToken([FromBody] LoginRequestDTO request)
    {
        #if DEBUG
        _logger.LogDebug("Tentativa de login para usuário: {UserName} em {Time}", 
            request.UserName, DateTime.Now);
        #endif
        
        try
        {
            var response = await _authService.AuthenticateAsync(request);
            
            #if DEBUG
            _logger.LogDebug("Token gerado com sucesso para: {UserName}", request.UserName);
            #endif
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            #if DEBUG
            _logger.LogError(ex, "Erro na autenticação para: {UserName}", request.UserName);
            #endif
            
            return Unauthorized("Credenciais inválidas");
        }
    }
    
    [HttpPost("AddAccount")]
    public async Task<IActionResult> AddAccount([FromBody] CreateUserRequestDTO request)
    {
        #if DEBUG
        _logger.LogDebug("Criando conta para usuário: {UserName}", request.UserName);
        #endif
        
        var result = await _authService.CreateUserAsync(request);
        
        #if DEBUG
        _logger.LogDebug("Conta criada com sucesso: {UserName}", request.UserName);
        #endif
        
        return Ok(result);
    }
}
```

### 5. Verificação da Instalação

- 🌐 **API Endpoint**: https://localhost:7001
- 📖 **Documentação da API**: https://localhost:7001 (redireciona automaticamente para Swagger UI)
- ❤️ **Health Check**: https://localhost:7001/health

## 📚 Componentes Principais

### 🏛️ Camada de API

- **`AuthenticationController`**: Controller principal para autenticação
- **`Middleware`**: Middleware customizado para JWT e logging

### 🔐 Camada de Serviços

- **`IAuthenticationService`**: Interface para serviços de autenticação
- **`AuthenticationService`**: Implementação dos serviços de autenticação

### 🗃️ Camada de Repositório

- **`IUserRepository`**: Interface para acesso a dados de usuários
- **`UserRepository`**: Implementação com operações CRUD de usuários

### 🛠️ Utilitários

- **`JwtTokenGenerator`**: Geração e validação de tokens JWT
- **`PasswordHasher`**: Hash e verificação de senhas com Argon2

## 🔐 Segurança

O serviço inclui funcionalidades de segurança robustas:

```csharp
using Foundation.Base.Util;

// Hash de senha com Argon2
string senhaHash = StringHelper.ComputeArgon2Hash("minhaSenha123");

// Verificação de senha
bool senhaValida = StringHelper.VerifyArgon2Hash("minhaSenha123", senhaHash);

// Geração de JWT Token
var tokenDescriptor = new SecurityTokenDescriptor
{
    Subject = new ClaimsIdentity(new[]
    {
        new Claim(ClaimTypes.Name, usuario.UserName),
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
    }),
    Expires = DateTime.UtcNow.AddMinutes(60),
    SigningCredentials = new SigningCredentials(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), 
        SecurityAlgorithms.HmacSha256Signature)
};
```

## ✅ Validação

Integração nativa com FluentValidation:

```csharp
using FluentValidation;

public class LoginRequestValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Nome de usuário é obrigatório");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Senha deve ter no mínimo 6 caracteres");
    }
}

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequestDTO>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]")
            .WithMessage("Senha deve conter pelo menos: 1 letra minúscula, 1 maiúscula, 1 número e 1 caractere especial");
    }
}

// No controller
var validationResult = await ValidationHelper.ValidateEntityAsync(request, serviceProvider, this);
if (validationResult != null) return validationResult;
```

## 🔐 Sistema RBAC - Exemplo Prático

### Configuração de Permissões Passo a Passo

```bash
# 1. Autenticar e obter token JWT
curl -X POST "https://localhost:7001/Authentication/GenerateToken" \
  -H "Content-Type: application/json" \
  -d '{"userName": "admin", "password": "password123"}'

# 2. Criar uma claim (permissão)
curl -X POST "https://localhost:7001/Claim/AddClaim" \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d '{"type": "Permission", "value": "user:manage", "description": "Gerenciar usuários"}'

# 3. Criar uma ação
curl -X POST "https://localhost:7001/Action/AddAction" \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d '{"name": "CreateUser"}'

# 4. Mapear claim para ação
curl -X POST "https://localhost:7001/ClaimAction/AddClaimAction" \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d '{"claimId": 1, "actionId": 1}'

# 5. Atribuir permissão a um usuário
curl -X POST "https://localhost:7001/AccountClaimAction/AddAccountClaimAction" \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d '{"accountId": 123, "claimActionId": 1}'
```

### Fluxo de Verificação de Permissões

1. **Usuário faz login** → Recebe JWT token
2. **Sistema verifica permissões** → Consulta `AccountClaimAction`
3. **Validação de ação** → Verifica se a claim permite a ação desejada
4. **Execução autorizada** → Usuário pode executar a operação

## 🌐 API Endpoints

### Endpoints Principais de Autenticação

| Método | Endpoint | Descrição | Autenticação |
|--------|----------|-----------|--------------|
| **POST** | `/Authentication/GenerateToken` | 🔑 Gerar token JWT | ❌ |
| **POST** | `/Authentication/AddAccount` | 👤 Criar conta de usuário | ❌ |
| **GET** | `/health` | ❤️ Verificação de saúde | ❌ |

### Endpoints de Gerenciamento de Permissões (RBAC)

| Método | Endpoint | Descrição | Autenticação |
|--------|----------|-----------|--------------|
| **GET** | `/Claim/GetClaims` | 📋 Listar todas as claims | ✅ |
| **GET** | `/Claim/GetClaimById/{id}` | 🔍 Obter claim por ID | ✅ |
| **POST** | `/Claim/AddClaim` | ➕ Criar nova claim | ✅ |
| **PUT** | `/Claim/UpdateClaim/{id}` | ✏️ Atualizar claim | ✅ |
| **DELETE** | `/Claim/DeleteClaim/{id}` | ❌ Excluir claim | ✅ |
| **GET** | `/Action/GetActions` | 📋 Listar todas as ações | ✅ |
| **GET** | `/Action/GetActionById/{id}` | 🔍 Obter ação por ID | ✅ |
| **POST** | `/Action/AddAction` | ➕ Criar nova ação | ✅ |
| **PUT** | `/Action/UpdateAction/{id}` | ✏️ Atualizar ação | ✅ |
| **DELETE** | `/Action/DeleteAction/{id}` | ❌ Excluir ação | ✅ |
| **GET** | `/ClaimAction/GetClaimActions` | 🔗 Listar mapeamentos claim-ação | ✅ |
| **POST** | `/ClaimAction/AddClaimAction` | 🔗 Mapear claim a ação | ✅ |
| **PUT** | `/ClaimAction/UpdateClaimAction/{id}` | ✏️ Atualizar mapeamento | ✅ |
| **DELETE** | `/ClaimAction/DeleteClaimAction/{id}` | ❌ Excluir mapeamento | ✅ |
| **GET** | `/AccountClaimAction/GetAccountClaimActions` | 👥 Listar permissões de usuários | ✅ |
| **POST** | `/AccountClaimAction/AddAccountClaimAction` | 👤 Atribuir permissão a usuário | ✅ |
| **DELETE** | `/AccountClaimAction/DeleteAccountClaimAction/{idAccount}/{idClaimAction}` | ❌ Remover permissão de usuário | ✅ |

### 🔑 Gerar Token de Autenticação

Gera um token JWT para credenciais válidas de usuário:

```bash
curl -X POST "https://localhost:7001/Authentication/GenerateToken" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "admin",
    "password": "password123"
  }'
```

**Resposta de Sucesso (200):**
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiresIn": 3600,
  "userName": "admin",
  "claims": ["user:read", "user:write"],
  "tokenType": "Bearer"
}
```

### 👤 Criar Conta de Usuário

Registra uma nova conta de usuário:

```bash
curl -X POST "https://localhost:7001/Authentication/AddAccount" \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "novoUsuario",
    "password": "SenhaSegura123!",
    "email": "usuario@exemplo.com"
  }'
```

## 📖 Documentação Detalhada

### 🚀 **Para Desenvolvedores (COMECE AQUI)**
- **[Guia de Desenvolvimento](docs/DEVELOPMENT.md)** - **Setup completo e workflow development-first**

### 📚 **Documentação Técnica**
- [Arquitetura do Serviço](docs/ARCHITECTURE.md)
- [Guia de Autenticação JWT](docs/JWT.md)
- [Configuração de Segurança](docs/SECURITY.md)
- [API Reference Completa](docs/API.md)
- [Guia de Deployment](docs/DEPLOYMENT.md)
- [Exemplos Práticos](docs/EXAMPLES.md)

> **🎯 Importante**: O projeto segue uma abordagem **development-first**. Sempre comece pelo [Guia de Desenvolvimento](docs/DEVELOPMENT.md)!

## 🤝 Contribuição

Contribuições são bem-vindas! Por favor, leia o [guia de contribuição](CONTRIBUTING.md) antes de submeter pull requests.

### Configuração do Ambiente de Contribuição

```bash
# Clone o repositório
git clone https://github.com/maiconcardozo/Authentication.git
cd Authentication

# Instalar dependências
dotnet restore

# Executar em modo de desenvolvimento
dotnet run --project Src/Authentication.API

# Executar testes
dotnet test Src/Authentication.Tests/Authentication.Tests.csproj
```

## 🧪 Executar Testes

O projeto inclui uma suíte completa de testes seguindo arquitetura TDD:

```bash
# Executar todos os testes
dotnet test Src/Authentication.Tests/Authentication.Tests.csproj

# Executar testes com verbosidade
dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --verbosity normal

# Executar apenas testes de integração
dotnet test --filter "FullyQualifiedName~Integration"

# Executar apenas testes unitários
dotnet test --filter "FullyQualifiedName~Unit"
```

### 📊 Cobertura de Testes

- ✅ **Testes de Integração**: Todos os endpoints da API
- ✅ **Testes Unitários**: Lógica de negócio e validações
- ✅ **Cenários de Sucesso**: Casos 200 OK
- ✅ **Cenários de Exceção**: Codes 400, 401, 404, 500
- ✅ **Validação de Dados**: Entrada e formato
- ✅ **Geração de Tokens**: JWT com claims
- ✅ **Hash de Senhas**: Segurança Argon2

Ver [documentação completa de testes](docs/TESTING.md) para mais detalhes.
```

## 📄 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).

## 👨‍💻 Autor

**Maicon Cardozo**
- GitHub: [@maiconcardozo](https://github.com/maiconcardozo)

## 📞 Suporte

Para dúvidas, sugestões ou reportar problemas:
- Abra uma [issue](https://github.com/maiconcardozo/Authentication/issues)
- Entre em contato através do GitHub

---

⭐ Se este projeto foi útil para você, considere dar uma estrela no repositório!
