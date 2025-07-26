# 🏛️ Clean Architecture - Implementação Detalhada

Esta documentação detalha como a Clean Architecture foi implementada no projeto, servindo como guia para replicar a mesma estrutura em novos projetos.

## 📋 Índice

- [Visão Geral das Camadas](#visão-geral-das-camadas)
- [Fluxo de Dependências](#fluxo-de-dependências)
- [Implementação das Camadas](#implementação-das-camadas)
- [Padrões Arquiteturais](#padrões-arquiteturais)
- [Exemplos Práticos](#exemplos-práticos)

## 🏗️ Visão Geral das Camadas

### 1. **Domain Layer (Núcleo)**
**Responsabilidade**: Contém as regras de negócio fundamentais e entidades do domínio.

```
Domain/
├── Implementation/     # Entidades concretas do domínio
│   ├── Account.cs     # Entidade usuário
│   ├── Claim.cs       # Entidade permissão
│   ├── Action.cs      # Entidade ação
│   └── ...           # Outras entidades
└── Interface/         # Contratos de domínio
    ├── IAccount.cs    # Interface da entidade
    └── ...           # Outras interfaces
```

**Características**:
- ❌ **Sem dependências externas** (nem mesmo do Framework .NET)
- ✅ **Entidades ricas** com regras de negócio
- ✅ **Interfaces** para abstrações
- ✅ **Value Objects** quando apropriado

### 2. **Application Layer (Serviços)**
**Responsabilidade**: Coordena o fluxo de trabalho e implementa casos de uso.

```
Services/
├── Implementation/          # Implementações dos serviços
│   ├── AccountService.cs   # Lógica de negócio para usuários
│   ├── ClaimService.cs     # Lógica de negócio para permissões
│   └── ...                # Outros serviços
└── Interface/              # Contratos de serviços
    ├── IAccountService.cs  # Interface do serviço
    └── ...                # Outras interfaces
```

**Características**:
- ✅ **Orchestração** entre repositórios
- ✅ **Validação** de regras de negócio
- ✅ **Transformação** de dados (DTOs)
- ✅ **Transações** via Unit of Work

### 3. **Infrastructure Layer (Persistência)**
**Responsabilidade**: Implementa acesso a dados e serviços externos.

```
Infrastructure/
├── Repository/
│   ├── Implementation/           # Implementações concretas
│   │   ├── AccountRepository.cs # Acesso a dados de usuários
│   │   └── ...                 # Outros repositórios
│   └── Interface/               # Contratos de repositório
│       ├── IAccountRepository.cs
│       └── ...
├── Implementation/              # Configurações EF Core
│   ├── AccountMap.cs           # Mapeamento entidade-tabela
│   └── LoginContext.cs         # Contexto do banco
└── UnitOfWork/                 # Gerenciamento de transações
    ├── Implementation/
    │   └── LoginUnitOfWork.cs
    └── Interface/
        └── ILoginUnitOfWork.cs
```

**Características**:
- ✅ **Repository Pattern** para abstração de dados
- ✅ **Unit of Work** para transações
- ✅ **Entity Framework** para ORM
- ✅ **Mapeamentos explícitos** das entidades

### 4. **Presentation Layer (API)**
**Responsabilidade**: Expõe funcionalidades via REST API.

```
API/
├── Controllers/              # Endpoints REST
│   ├── AuthenticationController.cs
│   ├── ClaimController.cs
│   └── ...
├── Middleware/              # Middleware customizado
│   ├── ExceptionHandlingMiddleware.cs
│   └── ...
├── Swagger/                 # Documentação API
│   ├── Routes/             # Constantes de rotas
│   └── Examples/           # Exemplos para documentação
└── Program.cs              # Configuração da aplicação
```

**Características**:
- ✅ **Controllers lean** (apenas coordenação)
- ✅ **Middleware** para cross-cutting concerns
- ✅ **Swagger** para documentação
- ✅ **Dependency Injection** configurada

## 🔄 Fluxo de Dependências

```
API → Services → Repository → Domain
 ↓       ↓          ↓         ↑
DTOs   Logic   Data Access  Entities
```

### Regras de Dependência:
1. **API** pode depender de **Services** e **DTOs**
2. **Services** podem depender de **Repository** e **Domain**
3. **Repository** pode depender de **Domain**
4. **Domain** NÃO depende de nada (núcleo independente)

## 🎯 Padrões Arquiteturais Implementados

### 1. **Dependency Inversion**
```csharp
// ❌ ERRADO: Dependência concreta
public class AccountService
{
    private AccountRepository _repository; // Classe concreta
}

// ✅ CORRETO: Dependência de abstração
public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository; // Interface
    
    public AccountService(IAccountRepository repository)
    {
        _repository = repository;
    }
}
```

### 2. **Repository Pattern**
```csharp
// Interface (Infrastructure/Interface)
public interface IAccountRepository : IEntityRepository<Account>
{
    Task<Account?> GetByUserNameAsync(string userName);
    Task<bool> ExistsAsync(string userName);
}

// Implementação (Infrastructure/Implementation)
public class AccountRepository : EntityRepository<Account>, IAccountRepository
{
    public AccountRepository(ILoginContext context) : base(context) { }
    
    public async Task<Account?> GetByUserNameAsync(string userName)
    {
        return await Context.Set<Account>()
            .FirstOrDefaultAsync(a => a.UserName == userName);
    }
}
```

### 3. **Unit of Work**
```csharp
public interface ILoginUnitOfWork : IBaseUnitOfWork
{
    IAccountRepository AccountRepository { get; }
    IClaimRepository ClaimRepository { get; }
    // ... outros repositórios
}

public class LoginUnitOfWork : BaseUnitOfWork, ILoginUnitOfWork
{
    public IAccountRepository AccountRepository { get; private set; }
    public IClaimRepository ClaimRepository { get; private set; }
    
    public LoginUnitOfWork(ILoginContext context) : base(context)
    {
        AccountRepository = new AccountRepository(context);
        ClaimRepository = new ClaimRepository(context);
    }
}
```

### 4. **Service Layer**
```csharp
public class AccountService : IAccountService
{
    private readonly ILoginUnitOfWork _unitOfWork;
    
    public AccountService(ILoginUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AccountResponseDTO> CreateAsync(AccountRequestDTO request)
    {
        // 1. Validações de negócio
        await ValidateBusinessRules(request);
        
        // 2. Criar entidade de domínio
        var account = new Account(request.UserName, request.Password);
        
        // 3. Persistir via repositório
        await _unitOfWork.AccountRepository.AddAsync(account);
        await _unitOfWork.CommitAsync();
        
        // 4. Retornar DTO
        return account.MapToResponseDTO();
    }
}
```

## 📊 Sistema RBAC (Role-Based Access Control)

### Entidades do Sistema de Permissões:

```
Account (Usuário)
    ↓ possui
AccountClaimAction (Permissões do Usuário)
    ↓ referencia
ClaimAction (Mapeamento Permissão-Ação)
    ↓ conecta
Claim (Permissão) ←→ Action (Ação do Sistema)
```

### Fluxo de Autorização:
1. **Usuário autentica** → Recebe JWT token
2. **Sistema consulta** AccountClaimAction do usuário
3. **Verifica se** a Claim permite a Action solicitada
4. **Autoriza ou nega** acesso

## 🔧 Configuração e Extensibilidade

### Extensions para Dependency Injection:
```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationLoginServices(
        this IServiceCollection services, 
        string connectionString)
    {
        // Configurar contexto de dados
        services.AddDbContext<LoginContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        
        // Registrar repositórios
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IClaimRepository, ClaimRepository>();
        
        // Registrar serviços
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IClaimService, ClaimService>();
        
        // Registrar Unit of Work
        services.AddScoped<ILoginUnitOfWork, LoginUnitOfWork>();
        
        return services;
    }
}
```

## 📝 Princípios de Design

### 1. **Single Responsibility Principle (SRP)**
- Cada classe tem uma única responsabilidade
- Controllers apenas coordenam
- Services implementam lógica de negócio
- Repositories apenas acessam dados

### 2. **Open/Closed Principle (OCP)**
- Aberto para extensão via interfaces
- Fechado para modificação da implementação base

### 3. **Interface Segregation Principle (ISP)**
- Interfaces específicas e coesas
- Clientes não dependem de métodos que não usam

### 4. **Dependency Inversion Principle (DIP)**
- Módulos de alto nível não dependem de módulos de baixo nível
- Ambos dependem de abstrações

---

## 💡 Benefícios desta Arquitetura

✅ **Testabilidade**: Todas as dependências são injetáveis e podem ser mockadas  
✅ **Manutenibilidade**: Código organizado e separação clara de responsabilidades  
✅ **Extensibilidade**: Fácil adição de novas funcionalidades  
✅ **Independência**: Core business independente de frameworks  
✅ **Reusabilidade**: Componentes podem ser reutilizados em outros contextos  

Use esta estrutura como base para todos os seus projetos .NET!