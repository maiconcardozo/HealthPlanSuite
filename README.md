# HealthPlan - Serviço de Gestão de Planos de Saúde para .NET

[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-blue.svg)](https://docs.microsoft.com/en-us/ef/core/)
[![JWT](https://img.shields.io/badge/JWT-Authentication-green.svg)](https://jwt.io/)

## 📋 Visão Geral

O **HealthPlan** é um serviço .NET que fornece gestão completa de planos de saúde seguindo os princípios de Domain-Driven Design (DDD). Este serviço implementa autenticação JWT, gestão de beneficiários, planos de saúde, e um sistema completo de **Role-Based Access Control (RBAC)** com gerenciamento de permissões, ações e controle de acesso de usuários.

### 🏥 Funcionalidades Principais

- **Gestão de Planos de Saúde**: Criação e administração de planos
- **Gestão de Beneficiários**: Cadastro e controle de beneficiários
- **Autenticação JWT**: Geração e validação de tokens seguros
- **Sistema RBAC Completo**: 
  - **Claims**: Definição de permissões e roles
  - **Actions**: Ações disponíveis no sistema de saúde
  - **ClaimActions**: Mapeamento de permissões para ações
  - **AccountClaimActions**: Atribuição de permissões a usuários
- **API RESTful Completa**: CRUD endpoints para todas as entidades
- **Segurança Avançada**: Hash Argon2, validação de entrada, middleware de segurança

## 🏗️ Arquitetura

O serviço está organizado em camadas bem definidas seguindo os princípios de Clean Architecture:

```
HealthPlan/
├── Src/
│   ├── HealthPlan.API/                 # Camada de API
│   │   ├── Controllers/               # Controllers da API
│   │   │   ├── HealthPlanController.cs        # Gestão de planos de saúde
│   │   │   ├── BeneficiaryController.cs       # Gestão de beneficiários
│   │   │   ├── AuthenticationController.cs    # Autenticação básica
│   │   │   ├── ClaimController.cs             # Gerenciamento de claims
│   │   │   ├── ActionController.cs            # Gerenciamento de ações
│   │   │   ├── ClaimActionController.cs       # Mapeamento claim-ação
│   │   │   └── AccountClaimActionController.cs # Permissões de usuários
│   │   ├── Middleware/                # Middleware customizado
│   │   ├── Swagger/                   # Documentação da API
│   │   └── Data/                      # Contextos do banco de dados
│   │
│   └── HealthPlan.Login/              # Domínio & Lógica de Negócio
│       ├── Domain/                    # Entidades de domínio
│       │   ├── Implementation/        # Implementações concretas
│       │   │   ├── HealthPlan.cs     # Entidade de plano de saúde
│       │   │   ├── Beneficiary.cs    # Entidade de beneficiário
│       │   │   ├── Account.cs        # Entidade de usuário
│       │   │   ├── Claim.cs          # Claims/Permissões
│       │   │   ├── Action.cs         # Ações do sistema
│       │   │   ├── ClaimAction.cs    # Relação claim-ação
│       │   │   └── AccountClaimAction.cs # Permissões do usuário
│       │   └── Interface/            # Interfaces de domínio
│       ├── Services/                 # Serviços de negócio
│       │   ├── Implementation/       # Implementações de serviços
│       │   └── Interface/           # Contratos de serviços
│       ├── Repository/               # Camada de acesso a dados
│       │   ├── Implementation/       # Implementações de repositórios
│       │   └── Interface/           # Contratos de repositórios
│       ├── DTO/                     # Objetos de transferência
│       ├── Infrastructure/          # Configurações de entidade
│       │   ├── Implementation/      # Mapeamentos EF Core
│       │   └── Interface/          # Contratos de contexto
│       └── UnitOfWork/             # Padrão Unit of Work
│           ├── Implementation/      # Implementação do UoW
│           └── Interface/          # Contrato do UoW
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

- **.NET 8.0** - Framework principal
- **ASP.NET Core 8.0** - Framework para API RESTful
- **Entity Framework Core 8.0** - ORM para acesso a dados
- **JWT Bearer** - Autenticação baseada em tokens
- **FluentValidation** - Validação de entrada
- **Argon2** - Hash seguro de senhas
- **MySQL/MariaDB** - Suporte a banco de dados
- **Swagger/OpenAPI** - Documentação da API

## 🚀 Desenvolvimento (Início Rápido)

### Configuração do Ambiente de Desenvolvimento

```bash
# 1. Clone o repositório
git clone https://github.com/maiconcardozo/HealthPlanSuite.git
cd HealthPlanSuite

# 2. Instalar .NET 8.0 SDK (se não tiver)
# Baixe de: https://dotnet.microsoft.com/download/dotnet/8.0

# 3. Restaurar dependências
dotnet restore Solution/HealthPlan.sln

# 4. Compilar em modo Debug (desenvolvimento)
dotnet build Solution/HealthPlan.sln --configuration Debug

# 5. Executar a API
cd Src/HealthPlan.API
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
- .NET 8.0 SDK ou superior
- MySQL 8.0+ ou superior
- Entity Framework Core 8.0

### Clonando e compilando localmente
```bash
git clone https://github.com/maiconcardozo/HealthPlanSuite.git
cd HealthPlanSuite
dotnet build Solution/HealthPlan.sln --configuration Release
```

## 🚀 Uso Rápido (Desenvolvimento)

> **💡 Foco em Desenvolvimento**: Todos os exemplos priorizam configurações e práticas de desenvolvimento para facilitar a experiência do desenvolvedor.

### 1. Configurando o Banco de Dados (Desenvolvimento)

Atualize a string de conexão em `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HealthPlanDB;Uid=seu_usuario;Pwd=sua_senha;"
  },
  "JwtSettings": {
    "Issuer": "HealthPlan",
    "Audience": "HealthPlanClients",
    "SecretKey": "sua-chave-secreta-de-32-caracteres-minimo",
    "ExpirationMinutes": 60
  }
}
```

### 2. Inicializando o Banco de Dados

```bash
cd Src/HealthPlan.API
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

### 4. Usando o Serviço de Planos de Saúde (Com Debug)

```csharp
using HealthPlan.Login.Services;
using HealthPlan.Login.DTO;
using Microsoft.Extensions.Logging;

public class HealthPlanController : ControllerBase
{
    private readonly IHealthPlanService _healthPlanService;
    private readonly ILogger<HealthPlanController> _logger;
    
    public HealthPlanController(
        IHealthPlanService healthPlanService,
        ILogger<HealthPlanController> logger)
    {
        _healthPlanService = healthPlanService;
        _logger = logger;
    }
    
    [HttpPost("CreateHealthPlan")]
    public async Task<IActionResult> CreateHealthPlan([FromBody] CreateHealthPlanRequestDTO request)
    {
        #if DEBUG
        _logger.LogDebug("Criando plano de saúde: {PlanName} em {Time}", 
            request.Name, DateTime.Now);
        #endif
        
        try
        {
            var response = await _healthPlanService.CreateHealthPlanAsync(request);
            
            #if DEBUG
            _logger.LogDebug("Plano de saúde criado com sucesso: {PlanName}", request.Name);
            #endif
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            #if DEBUG
            _logger.LogError(ex, "Erro na criação do plano: {PlanName}", request.Name);
            #endif
            
            return BadRequest("Erro ao criar plano de saúde");
        }
    }
    
    [HttpPost("AddBeneficiary")]
    public async Task<IActionResult> AddBeneficiary([FromBody] CreateBeneficiaryRequestDTO request)
    {
        #if DEBUG
        _logger.LogDebug("Criando beneficiário: {BeneficiaryName}", request.Name);
        #endif
        
        var result = await _healthPlanService.CreateBeneficiaryAsync(request);
        
        #if DEBUG
        _logger.LogDebug("Beneficiário criado com sucesso: {BeneficiaryName}", request.Name);
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

- **`HealthPlanController`**: Controller principal para gestão de planos de saúde
- **`BeneficiaryController`**: Controller para gestão de beneficiários
- **`AuthenticationController`**: Controller para autenticação
- **`Middleware`**: Middleware customizado para JWT e logging

### 🔐 Camada de Serviços

- **`IHealthPlanService`**: Interface para serviços de planos de saúde
- **`HealthPlanService`**: Implementação dos serviços de planos
- **`IBeneficiaryService`**: Interface para serviços de beneficiários
- **`BeneficiaryService`**: Implementação dos serviços de beneficiários

### 🗃️ Camada de Repositório

- **`IHealthPlanRepository`**: Interface para acesso a dados de planos
- **`HealthPlanRepository`**: Implementação com operações CRUD de planos
- **`IBeneficiaryRepository`**: Interface para acesso a dados de beneficiários
- **`BeneficiaryRepository`**: Implementação com operações CRUD de beneficiários

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

public class CreateHealthPlanRequestValidator : AbstractValidator<CreateHealthPlanRequestDTO>
{
    public CreateHealthPlanRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome do plano é obrigatório");
        RuleFor(x => x.Type).NotEmpty().WithMessage("Tipo do plano é obrigatório");
        RuleFor(x => x.Coverage).NotEmpty().WithMessage("Cobertura do plano é obrigatória");
        RuleFor(x => x.MonthlyFee).GreaterThan(0).WithMessage("Mensalidade deve ser maior que zero");
    }
}

public class CreateBeneficiaryRequestValidator : AbstractValidator<CreateBeneficiaryRequestDTO>
{
    public CreateBeneficiaryRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.CPF).NotEmpty().Matches(@"^\d{11}$").WithMessage("CPF deve conter 11 dígitos");
        RuleFor(x => x.BirthDate).LessThan(DateTime.Now).WithMessage("Data de nascimento deve ser no passado");
        RuleFor(x => x.HealthPlanId).GreaterThan(0).WithMessage("Plano de saúde é obrigatório");
    }
}

// No controller
var validationResult = await ValidationHelper.ValidateEntityAsync(request, serviceProvider, this);
if (validationResult != null) return validationResult;
```

## 🔐 Sistema RBAC - Exemplo Prático para Planos de Saúde

### Configuração de Permissões Passo a Passo

```bash
# 1. Autenticar e obter token JWT
curl -X POST "https://localhost:7001/Authentication/GenerateToken" \
  -H "Content-Type: application/json" \
  -d '{"userName": "admin", "password": "password123"}'

# 2. Criar uma claim (permissão) para planos de saúde
curl -X POST "https://localhost:7001/Claim/AddClaim" \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d '{"type": "Permission", "value": "healthplan:manage", "description": "Gerenciar planos de saúde"}'

# 3. Criar uma ação específica para planos
curl -X POST "https://localhost:7001/Action/AddAction" \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d '{"name": "CreateHealthPlan"}'

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

### Fluxo de Verificação de Permissões para Planos de Saúde

1. **Usuário faz login** → Recebe JWT token
2. **Sistema verifica permissões** → Consulta `AccountClaimAction`
3. **Validação de ação** → Verifica se a claim permite a ação desejada no plano
4. **Execução autorizada** → Usuário pode executar operações no plano de saúde

## 🌐 API Endpoints

### Endpoints Principais de Planos de Saúde

| Método | Endpoint | Descrição | Autenticação |
|--------|----------|-----------|--------------|
| **GET** | `/HealthPlan/GetHealthPlans` | 📋 Listar todos os planos | ✅ |
| **GET** | `/HealthPlan/GetHealthPlanById/{id}` | 🔍 Obter plano por ID | ✅ |
| **POST** | `/HealthPlan/CreateHealthPlan` | ➕ Criar novo plano | ✅ |
| **PUT** | `/HealthPlan/UpdateHealthPlan/{id}` | ✏️ Atualizar plano | ✅ |
| **DELETE** | `/HealthPlan/DeleteHealthPlan/{id}` | ❌ Excluir plano | ✅ |

### Endpoints de Gestão de Beneficiários

| Método | Endpoint | Descrição | Autenticação |
|--------|----------|-----------|--------------|
| **GET** | `/Beneficiary/GetBeneficiaries` | 📋 Listar todos os beneficiários | ✅ |
| **GET** | `/Beneficiary/GetBeneficiaryById/{id}` | 🔍 Obter beneficiário por ID | ✅ |
| **POST** | `/Beneficiary/CreateBeneficiary` | 👤 Criar novo beneficiário | ✅ |
| **PUT** | `/Beneficiary/UpdateBeneficiary/{id}` | ✏️ Atualizar beneficiário | ✅ |
| **DELETE** | `/Beneficiary/DeleteBeneficiary/{id}` | ❌ Excluir beneficiário | ✅ |
| **GET** | `/Beneficiary/GetBeneficiariesByHealthPlan/{planId}` | 👥 Listar beneficiários por plano | ✅ |

### Endpoints de Autenticação

| Método | Endpoint | Descrição | Autenticação |
|--------|----------|-----------|--------------|
| **POST** | `/Authentication/GenerateToken` | 🔑 Gerar token JWT | ❌ |
| **POST** | `/Authentication/AddAccount` | 👤 Criar conta de usuário | ❌ |
| **GET** | `/health` | ❤️ Verificação de saúde | ❌ |

### 🔑 Criar Plano de Saúde

Cria um novo plano de saúde:

```bash
curl -X POST "https://localhost:7001/HealthPlan/CreateHealthPlan" \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Plano Básico Familiar",
    "type": "Individual",
    "coverage": "Nacional",
    "monthlyFee": 299.90,
    "description": "Plano básico com cobertura nacional"
  }'
```

**Resposta de Sucesso (200):**
```json
{
  "id": 1,
  "name": "Plano Básico Familiar",
  "type": "Individual",
  "coverage": "Nacional",
  "monthlyFee": 299.90,
  "description": "Plano básico com cobertura nacional",
  "createdAt": "2024-01-15T10:30:00Z",
  "isActive": true
}
```

### 👤 Criar Beneficiário

Registra um novo beneficiário:

```bash
curl -X POST "https://localhost:7001/Beneficiary/CreateBeneficiary" \
  -H "Authorization: Bearer <token>" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "João Silva",
    "cpf": "12345678901",
    "birthDate": "1985-05-15",
    "healthPlanId": 1,
    "relationship": "Titular"
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
- [Gestão de Planos de Saúde](docs/HEALTHPLANS.md)
- [Gestão de Beneficiários](docs/BENEFICIARIES.md)

> **🎯 Importante**: O projeto segue uma abordagem **development-first**. Sempre comece pelo [Guia de Desenvolvimento](docs/DEVELOPMENT.md)!

## 🤝 Contribuição

Contribuições são bem-vindas! Por favor, leia o [guia de contribuição](CONTRIBUTING.md) antes de submeter pull requests.

### Configuração do Ambiente de Contribuição

```bash
# Clone o repositório
git clone https://github.com/maiconcardozo/HealthPlanSuite.git
cd HealthPlanSuite

# Instalar dependências
dotnet restore

# Executar em modo de desenvolvimento
dotnet run --project Src/HealthPlan.API

# Executar testes
dotnet test Src/HealthPlan.Tests/HealthPlan.Tests.csproj
```

## 🧪 Executar Testes

O projeto inclui uma suíte completa de testes seguindo arquitetura TDD:

```bash
# Executar todos os testes
dotnet test Src/HealthPlan.Tests/HealthPlan.Tests.csproj

# Executar testes com verbosidade
dotnet test Src/HealthPlan.Tests/HealthPlan.Tests.csproj --verbosity normal

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
- ✅ **Gestão de Planos**: CRUD de planos de saúde
- ✅ **Gestão de Beneficiários**: CRUD de beneficiários

Ver [documentação completa de testes](docs/TESTING.md) para mais detalhes.

## 📄 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).

## 👨‍💻 Autor

**Maicon Cardozo**
- GitHub: [@maiconcardozo](https://github.com/maiconcardozo)

## 📞 Suporte

Para dúvidas, sugestões ou reportar problemas:
- Abra uma [issue](https://github.com/maiconcardozo/HealthPlanSuite/issues)
- Entre em contato através do GitHub

---

⭐ Se este projeto foi útil para você, considere dar uma estrela no repositório!