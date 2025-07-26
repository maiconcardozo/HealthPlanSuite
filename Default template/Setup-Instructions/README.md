# 🚀 Instruções de Setup - Novo Projeto

Este guia fornece instruções passo a passo para criar um novo projeto seguindo exatamente os mesmos padrões arquiteturais do projeto Authentication.

## 📋 Pré-requisitos

- ✅ [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- ✅ [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- ✅ [MySQL 8.0+](https://dev.mysql.com/downloads/mysql/) ou [MariaDB](https://mariadb.org/)
- ✅ [Git](https://git-scm.com/)
- ✅ **Repository Foundation** - Clone obrigatório: `https://github.com/maiconcardozo/Foundation`

## 🎯 Passo 1: Configuração Inicial

### 1.1 Estrutura de Diretórios
```bash
# Crie a estrutura base seguindo este padrão obrigatório:
mkdir Parent-Directory
cd Parent-Directory

# Clone o Foundation (OBRIGATÓRIO)
git clone https://github.com/maiconcardozo/Foundation.git

# Crie seu novo projeto
mkdir [SeuProjeto]
cd [SeuProjeto]

# Estrutura final deve ficar:
# Parent-Directory/
# ├── Foundation/
# │   └── Src/Foundation.Base/
# └── [SeuProjeto]/
#     ├── Solution/
#     └── Src/
```

### 1.2 Criar Estrutura de Pastas
```bash
cd [SeuProjeto]

# Criar estrutura completa
mkdir -p Solution
mkdir -p Src/[SeuProjeto].API/Controllers
mkdir -p Src/[SeuProjeto].API/Data  
mkdir -p Src/[SeuProjeto].API/Middleware
mkdir -p Src/[SeuProjeto].API/Swagger
mkdir -p Src/[SeuProjeto].API/Resource
mkdir -p Src/[SeuProjeto].API/Util
mkdir -p Src/[SeuProjeto].API/Properties

mkdir -p Src/[SeuProjeto].Domain/Domain/Implementation
mkdir -p Src/[SeuProjeto].Domain/Domain/Interface
mkdir -p Src/[SeuProjeto].Domain/Services/Implementation
mkdir -p Src/[SeuProjeto].Domain/Services/Interface
mkdir -p Src/[SeuProjeto].Domain/Repository/Implementation
mkdir -p Src/[SeuProjeto].Domain/Repository/Interface
mkdir -p Src/[SeuProjeto].Domain/Infrastructure/Implementation
mkdir -p Src/[SeuProjeto].Domain/Infrastructure/Interface
mkdir -p Src/[SeuProjeto].Domain/UnitOfWork/Implementation
mkdir -p Src/[SeuProjeto].Domain/UnitOfWork/Interface
mkdir -p Src/[SeuProjeto].Domain/DTO
mkdir -p Src/[SeuProjeto].Domain/Mapping
mkdir -p Src/[SeuProjeto].Domain/Util
mkdir -p Src/[SeuProjeto].Domain/Extensions
mkdir -p Src/[SeuProjeto].Domain/Resource

mkdir -p Src/[SeuProjeto].Tests/Unit
mkdir -p Src/[SeuProjeto].Tests/Integration
mkdir -p Src/[SeuProjeto].Tests/Fixtures
mkdir -p Src/[SeuProjeto].Tests/Helpers

mkdir -p docs
mkdir -p "Default template"
```

## 🎯 Passo 2: Criar Solução e Projetos

### 2.1 Criar Arquivo de Solução
```bash
cd Solution
dotnet new sln -n [SeuProjeto]
```

### 2.2 Criar Projetos .NET
```bash
# Projeto API (ASP.NET Core Web API)
cd ../Src/[SeuProjeto].API
dotnet new webapi -n [SeuProjeto].API

# Projeto Domain (Class Library)
cd ../[SeuProjeto].Domain
dotnet new classlib -n [SeuProjeto].Domain

# Projeto Tests (xUnit Test Project)
cd ../[SeuProjeto].Tests
dotnet new xunit -n [SeuProjeto].Tests

# Adicionar projetos à solução
cd ../../Solution
dotnet sln add ../Src/[SeuProjeto].API/[SeuProjeto].API.csproj
dotnet sln add ../Src/[SeuProjeto].Domain/[SeuProjeto].Domain.csproj
dotnet sln add ../Src/[SeuProjeto].Tests/[SeuProjeto].Tests.csproj

# Adicionar referência ao Foundation.Base
dotnet sln add ../../Foundation/Src/Foundation.Base/Foundation.Base.csproj
```

### 2.3 Configurar Referências entre Projetos
```bash
# API referencia Domain
cd ../Src/[SeuProjeto].API
dotnet add reference ../[SeuProjeto].Domain/[SeuProjeto].Domain.csproj

# Domain referencia Foundation.Base
cd ../[SeuProjeto].Domain
dotnet add reference ../../../Foundation/Src/Foundation.Base/Foundation.Base.csproj

# Tests referencia API e Domain
cd ../[SeuProjeto].Tests
dotnet add reference ../[SeuProjeto].API/[SeuProjeto].API.csproj
dotnet add reference ../[SeuProjeto].Domain/[SeuProjeto].Domain.csproj
```

## 🎯 Passo 3: Configurar Dependências NuGet

### 3.1 Projeto API - Pacotes Obrigatórios
```bash
cd ../[SeuProjeto].API

# Pacotes essenciais
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.0
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 9.0.0
dotnet add package Swashbuckle.AspNetCore --version 7.2.0
dotnet add package FluentValidation.AspNetCore --version 11.3.0
dotnet add package Swashbuckle.AspNetCore.Filters --version 8.0.2
dotnet add package Swashbuckle.AspNetCore.Annotations --version 7.2.0
```

### 3.2 Projeto Domain - Pacotes Obrigatórios
```bash
cd ../[SeuProjeto].Domain

# Pacotes essenciais
dotnet add package Microsoft.EntityFrameworkCore --version 9.0.0
dotnet add package FluentValidation --version 11.9.0
dotnet add package Microsoft.Extensions.DependencyInjection.Abstractions --version 9.0.0
dotnet add package System.IdentityModel.Tokens.Jwt --version 8.2.1
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 9.0.0
```

### 3.3 Projeto Tests - Pacotes Obrigatórios
```bash
cd ../[SeuProjeto].Tests

# Pacotes para testes
dotnet add package Microsoft.AspNetCore.Mvc.Testing --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 9.0.0
dotnet add package Moq --version 4.20.70
dotnet add package FluentAssertions --version 6.12.1
```

## 🎯 Passo 4: Configurar Arquivo de Projeto

### 4.1 Atualizar [SeuProjeto].API.csproj
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="9.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.0" />
    <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="9.0.0" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="7.2.0" />
    <PackageReference Include="FluentValidation.AspNetCore" Version="11.3.0" />
    <PackageReference Include="Swashbuckle.AspNetCore.Filters" Version="8.0.2" />
    <PackageReference Include="Swashbuckle.AspNetCore.Annotations" Version="7.2.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="../[SeuProjeto].Domain/[SeuProjeto].Domain.csproj" />
  </ItemGroup>

</Project>
```

### 4.2 Atualizar [SeuProjeto].Domain.csproj
```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.0" />
    <PackageReference Include="FluentValidation" Version="11.9.0" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="9.0.0" />
    <PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="8.2.1" />
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="9.0.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="../../../Foundation/Src/Foundation.Base/Foundation.Base.csproj" />
  </ItemGroup>

</Project>
```

## 🎯 Passo 5: Configurar Arquivos de Configuração

### 5.1 appsettings.json (Base)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=[SeuProjeto]DB;Uid=root;Pwd=;"
  },
  "JwtSettings": {
    "Issuer": "[SeuProjeto]",
    "Audience": "[SeuProjeto]Clients",
    "SecretKey": "sua-chave-secreta-de-pelo-menos-32-caracteres-aqui",
    "ExpirationMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### 5.2 appsettings.Development.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=[SeuProjeto]DB_Dev;Uid=root;Pwd=;"
  },
  "JwtSettings": {
    "Issuer": "[SeuProjeto]",
    "Audience": "[SeuProjeto]Clients",
    "SecretKey": "development-secret-key-32-characters-minimum",
    "ExpirationMinutes": 120
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information"
    }
  }
}
```

### 5.3 appsettings.Production.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-server;Database=[SeuProjeto]DB;Uid=produser;Pwd=prodpassword;"
  },
  "JwtSettings": {
    "Issuer": "[SeuProjeto]",
    "Audience": "[SeuProjeto]Clients",
    "SecretKey": "SUBSTITUA-POR-CHAVE-SECRETA-FORTE-EM-PRODUCAO",
    "ExpirationMinutes": 30
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## 🎯 Passo 6: Implementar Arquivos Base

### 6.1 Entidade Account (Obrigatória)
```csharp
// Src/[SeuProjeto].Domain/Domain/Implementation/Account.cs
using Foundation.Base.Domain.Implementation;
using Foundation.Base.Util;

namespace [SeuProjeto].Domain.Implementation
{
    public class Account : Entity
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        
        private Account() { }
        
        public Account(string userName, string password)
        {
            UserName = userName;
            Password = StringHelper.ComputeArgon2Hash(password);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
        
        public bool VerifyPassword(string password)
        {
            return StringHelper.VerifyArgon2Hash(password, Password);
        }
        
        public void UpdatePassword(string newPassword)
        {
            Password = StringHelper.ComputeArgon2Hash(newPassword);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
```

### 6.2 Interface Account
```csharp
// Src/[SeuProjeto].Domain/Domain/Interface/IAccount.cs
namespace [SeuProjeto].Domain.Interface
{
    public interface IAccount
    {
        int Id { get; }
        string UserName { get; }
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
        
        bool VerifyPassword(string password);
        void UpdatePassword(string newPassword);
    }
}
```

### 6.3 Configurações JWT
```csharp
// Src/[SeuProjeto].Domain/Domain/Implementation/JwtSettings.cs
using [SeuProjeto].Domain.Interface;

namespace [SeuProjeto].Domain.Implementation
{
    public class JwtSettings : IJwtSettings
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public int ExpirationMinutes { get; set; }
    }
}

// Src/[SeuProjeto].Domain/Domain/Interface/IJwtSettings.cs
namespace [SeuProjeto].Domain.Interface
{
    public interface IJwtSettings
    {
        string Issuer { get; set; }
        string Audience { get; set; }
        string SecretKey { get; set; }
        int ExpirationMinutes { get; set; }
    }
}
```

### 6.4 Contexto de Dados
```csharp
// Src/[SeuProjeto].Domain/Infrastructure/Implementation/[SeuProjeto]Context.cs
using Foundation.Base.Infrastructure.Data;
using [SeuProjeto].Infrastructure.Interface;
using [SeuProjeto].Domain.Implementation;
using Microsoft.EntityFrameworkCore;

namespace [SeuProjeto].Infrastructure.Implementation
{
    public class [SeuProjeto]Context : EntityContext, I[SeuProjeto]Context
    {
        public [SeuProjeto]Context(DbContextOptions<[SeuProjeto]Context> options) : base(options) { }
        
        public DbSet<Account> Accounts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new AccountMap());
        }
    }
}

// Src/[SeuProjeto].Domain/Infrastructure/Interface/I[SeuProjeto]Context.cs
using Foundation.Base.Infrastructure.Data;

namespace [SeuProjeto].Infrastructure.Interface
{
    public interface I[SeuProjeto]Context : IEntityContext
    {
        // Adicione propriedades específicas se necessário
    }
}
```

### 6.5 Mapeamento de Entidade
```csharp
// Src/[SeuProjeto].Domain/Infrastructure/Implementation/AccountMap.cs
using Foundation.Base.Infrastructure.Data;
using [SeuProjeto].Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace [SeuProjeto].Infrastructure.Implementation
{
    public class AccountMap : EntityConfiguration<Account>
    {
        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            base.Configure(builder);
            
            builder.ToTable("accounts");
            
            builder.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("user_name");
                
            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("password");
                
            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");
                
            builder.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasColumnName("updated_at");
            
            builder.HasIndex(e => e.UserName)
                .IsUnique()
                .HasDatabaseName("IX_Accounts_UserName");
        }
    }
}
```

### 6.6 Program.cs Base
```csharp
// Src/[SeuProjeto].API/Program.cs
using [SeuProjeto].Domain.Implementation;
using [SeuProjeto].Domain.Interface;
using [SeuProjeto].Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

var appsettings = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

// Configurar serviços do domínio
builder.Services.Add[SeuProjeto]Services(GetConnectionString(appsettings));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configurar Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "[SeuProjeto] API",
        Version = "v1",
        Description = "API para [SeuProjeto]"
    });
    
    // Configurar JWT no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Método auxiliar
static string GetConnectionString(ConfigurationBuilder appsettings)
{
    var config = appsettings.Build();
    return config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found");
}
```

## 🎯 Passo 7: Configurar Injeção de Dependência

### 7.1 ServiceCollectionExtensions
```csharp
// Src/[SeuProjeto].Domain/Extensions/ServiceCollectionExtensions.cs
using [SeuProjeto].Domain.Implementation;
using [SeuProjeto].Domain.Interface;
using [SeuProjeto].Infrastructure.Implementation;
using [SeuProjeto].Infrastructure.Interface;
using [SeuProjeto].Repository.Implementation;
using [SeuProjeto].Repository.Interface;
using [SeuProjeto].Services.Implementation;
using [SeuProjeto].Services.Interface;
using [SeuProjeto].UnitOfWork.Implementation;
using [SeuProjeto].UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace [SeuProjeto].Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Add[SeuProjeto]Services(
            this IServiceCollection services, 
            string connectionString)
        {
            // Configurar Entity Framework
            services.AddDbContext<[SeuProjeto]Context>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            
            // Registrar contextos
            services.AddScoped<I[SeuProjeto]Context, [SeuProjeto]Context>();
            
            // Registrar repositórios
            services.AddScoped<IAccountRepository, AccountRepository>();
            
            // Registrar serviços
            services.AddScoped<IAccountService, AccountService>();
            
            // Registrar Unit of Work
            services.AddScoped<I[SeuProjeto]UnitOfWork, [SeuProjeto]UnitOfWork>();
            
            // Configurar JWT
            var jwtSettings = new JwtSettings();
            services.AddSingleton<IJwtSettings>(jwtSettings);
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    };
                });
            
            return services;
        }
    }
}
```

## 🎯 Passo 8: Configurar Banco de Dados

### 8.1 Criar Migration Inicial
```bash
cd Src/[SeuProjeto].API

# Instalar ferramenta EF (se não tiver)
dotnet tool install --global dotnet-ef

# Criar migration inicial
dotnet ef migrations add InitialCreate --context [SeuProjeto]Context

# Aplicar migration
dotnet ef database update --context [SeuProjeto]Context
```

## 🎯 Passo 9: Testar a Configuração

### 9.1 Compilar o Projeto
```bash
cd ../../Solution
dotnet build [SeuProjeto].sln
```

### 9.2 Executar o Projeto
```bash
cd ../Src/[SeuProjeto].API
dotnet run

# Acessar:
# https://localhost:7001 (API)
# https://localhost:7001/swagger (Documentação)
```

## 🎯 Passo 10: Próximos Passos

Agora que você tem a estrutura base configurada:

1. ✅ Implemente suas entidades de domínio específicas
2. ✅ Crie os serviços correspondentes
3. ✅ Desenvolva os controllers da API
4. ✅ Adicione testes unitários e de integração
5. ✅ Configure CI/CD se necessário
6. ✅ Documente sua API específica

## 📚 Recursos Adicionais

- 📖 [Code Templates](../Code-Templates/README.md) - Templates para suas entidades
- 🏗️ [Architecture Guide](../Architecture/Clean-Architecture-Implementation.md) - Documentação da arquitetura
- 📁 [Project Structure](../Project-Structure/README.md) - Estrutura completa de pastas

---

## 🆘 Solução de Problemas Comuns

### Erro: "Foundation.Base.csproj not found"
**Solução**: Verifique se o repositório Foundation foi clonado no diretório pai correto.

### Erro de Conexão com Banco
**Solução**: Verifique se o MySQL está rodando e a string de conexão está correta.

### Erro de Compilação
**Solução**: Execute `dotnet restore` em todos os projetos antes de compilar.

### Migrations não funcionam
**Solução**: Certifique-se de estar no diretório do projeto API ao executar comandos EF.

Com este setup, você terá um projeto seguindo exatamente os mesmos padrões do Authentication!