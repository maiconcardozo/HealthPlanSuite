# 📁 Estrutura de Projeto - Template Base

Esta seção contém a estrutura completa de pastas e arquivos que deve ser replicada em novos projetos para manter a consistência arquitetural.

## 📋 Estrutura Completa do Projeto

```
[NomeProjeto]/
├── 📁 Solution/                                    # Configuração da Solução
│   ├── 📄 [NomeProjeto].sln                       # Arquivo da solução Visual Studio
│   └── 📄 Claim.cs                                # Classe auxiliar (opcional)
│
├── 📁 Src/                                        # Código fonte principal
│   ├── 📁 [NomeProjeto].API/                      # 🌐 Camada de Apresentação
│   │   ├── 📁 Controllers/                        # Controllers REST API
│   │   │   ├── 📄 AuthenticationController.cs    # Controller de autenticação
│   │   │   ├── 📄 [Entidade]Controller.cs         # Controllers das entidades
│   │   │   └── 📄 ...                            # Outros controllers
│   │   │
│   │   ├── 📁 Data/                              # Contextos específicos da API
│   │   │   ├── 📄 ApiContextDevelopment.cs       # Contexto para desenvolvimento
│   │   │   └── 📄 ApiContextProduction.cs        # Contexto para produção
│   │   │
│   │   ├── 📁 Middleware/                        # Middleware customizado
│   │   │   ├── 📄 ExceptionHandlingMiddleware.cs # Tratamento de exceções
│   │   │   └── 📄 SwaggerAuthMiddleware.cs       # Middleware para Swagger
│   │   │
│   │   ├── 📁 Migrations/                        # Migrations do Entity Framework
│   │   │   ├── 📁 Development/                   # Migrations de desenvolvimento
│   │   │   └── 📁 Production/                    # Migrations de produção
│   │   │
│   │   ├── 📁 Resource/                          # Recursos de localização
│   │   │   └── 📄 ResourceAPI.cs                 # Strings localizadas da API
│   │   │
│   │   ├── 📁 Swagger/                           # Configuração e documentação API
│   │   │   ├── 📄 LocalizedSwaggerOperationFilter.cs # Filtro de localização
│   │   │   ├── 📄 [Entidade]Routes.cs            # Constantes de rotas
│   │   │   ├── 📄 TokenResponseExample.cs        # Exemplos para documentação
│   │   │   └── 📄 ...                           # Outros arquivos Swagger
│   │   │
│   │   ├── 📁 Util/                              # Utilitários da API
│   │   │   └── 📄 Utils.cs                       # Funções auxiliares
│   │   │
│   │   ├── 📁 Properties/                        # Propriedades do projeto
│   │   │   └── 📄 launchSettings.json            # Configurações de execução
│   │   │
│   │   ├── 📁 wwwroot/                          # Arquivos estáticos (se necessário)
│   │   │
│   │   ├── 📄 Program.cs                         # Ponto de entrada e configuração
│   │   ├── 📄 [NomeProjeto].API.csproj           # Arquivo do projeto
│   │   ├── 📄 appsettings.json                   # Configurações base
│   │   ├── 📄 appsettings.Development.json       # Configurações de desenvolvimento
│   │   ├── 📄 appsettings.Production.json        # Configurações de produção
│   │   └── 📄 appsettings.Production.Development.json # Configurações prod/dev
│   │
│   ├── 📁 [NomeProjeto].Domain/                  # 🏛️ Camada de Domínio
│   │   ├── 📁 Domain/                            # Entidades de domínio
│   │   │   ├── 📁 Implementation/                # Implementações das entidades
│   │   │   │   ├── 📄 Account.cs                 # Entidade usuário (sempre presente)
│   │   │   │   ├── 📄 [Entidade].cs              # Suas entidades de domínio
│   │   │   │   ├── 📄 JwtSettings.cs             # Configurações JWT
│   │   │   │   └── 📄 Token.cs                   # Entidade de token
│   │   │   │
│   │   │   └── 📁 Interface/                     # Interfaces de domínio
│   │   │       ├── 📄 IAccount.cs                # Interface da entidade usuário
│   │   │       ├── 📄 I[Entidade].cs             # Interfaces das suas entidades
│   │   │       ├── 📄 IJwtSettings.cs            # Interface JWT
│   │   │       └── 📄 IToken.cs                  # Interface do token
│   │   │
│   │   ├── 📁 Services/                          # Serviços de aplicação
│   │   │   ├── 📁 Implementation/                # Implementações dos serviços
│   │   │   │   ├── 📄 AccountService.cs          # Serviço de usuários
│   │   │   │   ├── 📄 AuthenticationService.cs   # Serviço de autenticação
│   │   │   │   ├── 📄 [Entidade]Service.cs       # Serviços das suas entidades
│   │   │   │   └── 📄 ...                       # Outros serviços
│   │   │   │
│   │   │   └── 📁 Interface/                     # Interfaces dos serviços
│   │   │       ├── 📄 IAccountService.cs         # Interface do serviço de usuários
│   │   │       ├── 📄 IAuthenticationService.cs  # Interface de autenticação
│   │   │       ├── 📄 I[Entidade]Service.cs      # Interfaces dos seus serviços
│   │   │       └── 📄 ...                       # Outras interfaces
│   │   │
│   │   ├── 📁 Repository/                        # Camada de acesso a dados
│   │   │   ├── 📁 Implementation/                # Implementações dos repositórios
│   │   │   │   ├── 📄 AccountRepository.cs       # Repositório de usuários
│   │   │   │   ├── 📄 [Entidade]Repository.cs    # Repositórios das suas entidades
│   │   │   │   └── 📄 ...                       # Outros repositórios
│   │   │   │
│   │   │   └── 📁 Interface/                     # Interfaces dos repositórios
│   │   │       ├── 📄 IAccountRepository.cs      # Interface do repositório de usuários
│   │   │       ├── 📄 I[Entidade]Repository.cs   # Interfaces dos seus repositórios
│   │   │       └── 📄 ...                       # Outras interfaces
│   │   │
│   │   ├── 📁 Infrastructure/                    # Configurações de persistência
│   │   │   ├── 📁 Implementation/                # Implementações de infraestrutura
│   │   │   │   ├── 📄 AccountMap.cs              # Mapeamento EF da entidade usuário
│   │   │   │   ├── 📄 [Entidade]Map.cs           # Mapeamentos EF das suas entidades
│   │   │   │   ├── 📄 [NomeProjeto]Context.cs    # Contexto principal do banco
│   │   │   │   └── 📄 ...                       # Outros mapeamentos
│   │   │   │
│   │   │   └── 📁 Interface/                     # Interfaces de infraestrutura
│   │   │       ├── 📄 I[NomeProjeto]Context.cs   # Interface do contexto
│   │   │       └── 📄 ...                       # Outras interfaces
│   │   │
│   │   ├── 📁 UnitOfWork/                        # Padrão Unit of Work
│   │   │   ├── 📁 Implementation/                # Implementação do UoW
│   │   │   │   └── 📄 [NomeProjeto]UnitOfWork.cs # Unit of Work principal
│   │   │   │
│   │   │   └── 📁 Interface/                     # Interface do UoW
│   │   │       └── 📄 I[NomeProjeto]UnitOfWork.cs # Interface do Unit of Work
│   │   │
│   │   ├── 📁 DTO/                               # Data Transfer Objects
│   │   │   ├── 📄 AccountPayLoadDTO.cs           # DTO para login
│   │   │   ├── 📄 TokenResponseDTO.cs            # DTO de resposta de token
│   │   │   ├── 📄 [Entidade]RequestDTO.cs        # DTOs de request das suas entidades
│   │   │   ├── 📄 [Entidade]ResponseDTO.cs       # DTOs de response das suas entidades
│   │   │   └── 📄 ...                           # Outros DTOs
│   │   │
│   │   ├── 📁 Mapping/                           # Mapeamentos de objetos
│   │   │   ├── 📄 AccountMapping.cs              # Mapeamentos da entidade usuário
│   │   │   ├── 📄 [Entidade]Extensions.cs        # Extensões de mapeamento
│   │   │   └── 📄 ...                           # Outros mapeamentos
│   │   │
│   │   ├── 📁 Util/                              # Utilitários específicos do domínio
│   │   │   ├── 📄 AccountPayloadValidator.cs     # Validador de login
│   │   │   ├── 📄 [Entidade]Validator.cs         # Validadores das suas entidades
│   │   │   └── 📄 ...                           # Outros validadores
│   │   │
│   │   ├── 📁 Enum/                              # Enumerações do domínio
│   │   │   └── 📄 [Nome]Enum.cs                  # Suas enumerações
│   │   │
│   │   ├── 📁 Extensions/                        # Métodos de extensão
│   │   │   └── 📄 ServiceCollectionExtensions.cs # Extensões para DI
│   │   │
│   │   ├── 📁 Resource/                          # Recursos de localização
│   │   │   └── 📄 Resource[NomeProjeto].cs       # Strings localizadas do domínio
│   │   │
│   │   └── 📄 [NomeProjeto].Domain.csproj        # Arquivo do projeto
│   │
│   └── 📁 [NomeProjeto].Tests/                   # 🧪 Testes
│       ├── 📁 Unit/                              # Testes unitários
│       │   ├── 📁 Services/                      # Testes dos serviços
│       │   ├── 📁 Repositories/                  # Testes dos repositórios
│       │   └── 📁 Domain/                        # Testes das entidades
│       │
│       ├── 📁 Integration/                       # Testes de integração
│       │   ├── 📁 Controllers/                   # Testes dos controllers
│       │   └── 📁 Database/                      # Testes de banco de dados
│       │
│       ├── 📁 Fixtures/                          # Dados de teste
│       │   ├── 📄 [Entidade]Fixture.cs           # Fixtures das entidades
│       │   └── 📄 ...                           # Outros fixtures
│       │
│       ├── 📁 Helpers/                           # Auxiliares de teste
│       │   ├── 📄 TestHelper.cs                  # Funções auxiliares
│       │   └── 📄 DatabaseHelper.cs              # Auxiliares de banco
│       │
│       └── 📄 [NomeProjeto].Tests.csproj         # Arquivo do projeto de testes
│
├── 📁 docs/                                      # 📚 Documentação técnica
│   ├── 📄 ARCHITECTURE.md                        # Documentação da arquitetura
│   ├── 📄 API.md                                # Documentação da API
│   ├── 📄 DEPLOYMENT.md                         # Guia de deployment
│   └── 📄 TESTING.md                            # Documentação de testes
│
├── 📁 Default template/                          # 🎯 Template para replicação
│   ├── 📄 README.md                             # Documentação do template
│   ├── 📁 Architecture/                          # Documentação da arquitetura
│   ├── 📁 Code-Templates/                        # Templates de código
│   ├── 📁 Project-Structure/                     # Este arquivo
│   └── 📁 Setup-Instructions/                    # Instruções de configuração
│
├── 📄 README.md                                  # Documentação principal do projeto
├── 📄 CHANGELOG.md                               # Histórico de mudanças
├── 📄 CONTRIBUTING.md                            # Guia de contribuição
├── 📄 IMPLEMENTATION_SUMMARY.md                  # Resumo da implementação
├── 📄 IMPROVEMENTS.md                            # Melhorias propostas
├── 📄 TESTING_SUMMARY.md                         # Resumo dos testes
├── 📄 .gitignore                                # Arquivos ignorados pelo Git
├── 📄 run-tests.sh                              # Script para executar testes (Linux/Mac)
├── 📄 run-tests.bat                             # Script para executar testes (Windows)
└── 📄 dotnet-install.sh                         # Script de instalação do .NET
```

## 📋 Arquivos Essenciais por Camada

### 🌐 API Layer - Arquivos Obrigatórios

```
Controllers/
├── AuthenticationController.cs    # ✅ Sempre necessário
├── [Entidade]Controller.cs        # ✅ Para cada entidade de domínio
└── HealthController.cs           # ⚠️  Recomendado para monitoramento

Data/
├── ApiContextDevelopment.cs      # ✅ Contexto para desenvolvimento
└── ApiContextProduction.cs       # ✅ Contexto para produção

Middleware/
├── ExceptionHandlingMiddleware.cs # ✅ Tratamento global de exceções
└── SwaggerAuthMiddleware.cs       # ⚠️  Se usar autenticação no Swagger

Swagger/
├── [Entidade]Routes.cs           # ✅ Constantes de rotas para cada entidade
├── TokenResponseExample.cs       # ✅ Se usar autenticação JWT
└── LocalizedSwaggerOperationFilter.cs # ⚠️  Se usar localização

Program.cs                        # ✅ Configuração da aplicação
appsettings.*.json               # ✅ Configurações por ambiente
```

### 🏛️ Domain Layer - Arquivos Obrigatórios

```
Domain/Implementation/
├── Account.cs                    # ✅ Entidade de usuário (sempre presente)
├── [SuaEntidade].cs             # ✅ Suas entidades de domínio
├── JwtSettings.cs               # ✅ Se usar JWT
└── Token.cs                     # ✅ Se usar JWT

Services/Implementation/
├── AccountService.cs            # ✅ Serviço de usuários
├── AuthenticationService.cs     # ✅ Se usar autenticação
└── [Entidade]Service.cs         # ✅ Para cada entidade

Repository/Implementation/
├── AccountRepository.cs         # ✅ Repositório de usuários
└── [Entidade]Repository.cs      # ✅ Para cada entidade

Infrastructure/Implementation/
├── AccountMap.cs                # ✅ Mapeamento EF do usuário
├── [Entidade]Map.cs             # ✅ Mapeamento EF de cada entidade
└── [Projeto]Context.cs          # ✅ Contexto principal

UnitOfWork/Implementation/
└── [Projeto]UnitOfWork.cs       # ✅ Unit of Work principal

Extensions/
└── ServiceCollectionExtensions.cs # ✅ Configuração de DI
```

### 🧪 Tests Layer - Arquivos Recomendados

```
Unit/
├── Services/[Entidade]ServiceTests.cs    # ✅ Teste unitário de cada serviço
├── Repositories/[Entidade]RepositoryTests.cs # ⚠️  Testes de repositório
└── Domain/[Entidade]Tests.cs             # ⚠️  Testes de entidades

Integration/
├── Controllers/[Entidade]ControllerTests.cs # ✅ Teste de integração de cada controller
└── Database/DatabaseTests.cs             # ⚠️  Testes de banco

Fixtures/
└── [Entidade]Fixture.cs                  # ✅ Dados de teste
```

## 🔧 Configuração de Dependências

### Dependências Obrigatórias no .csproj da API:
```xml
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.0" />
<PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="9.0.0" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="7.2.0" />
<PackageReference Include="FluentValidation.AspNetCore" Version="11.3.0" />
```

### Dependências Obrigatórias no .csproj do Domain:
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.0" />
<PackageReference Include="FluentValidation" Version="11.9.0" />
<PackageReference Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="9.0.0" />
```

### Referência Obrigatória ao Foundation.Base:
```xml
<ProjectReference Include="..\..\Foundation\Src\Foundation.Base\Foundation.Base.csproj" />
```

## 📝 Convenções de Nomenclatura

### Arquivos e Classes:
- **Entidades**: `[Nome].cs` (PascalCase, singular)
- **Interfaces**: `I[Nome].cs` (com prefixo I)
- **Serviços**: `[Nome]Service.cs`
- **Repositórios**: `[Nome]Repository.cs`
- **Controllers**: `[Nome]Controller.cs`
- **DTOs**: `[Nome]RequestDTO.cs` / `[Nome]ResponseDTO.cs`
- **Mapeamentos EF**: `[Nome]Map.cs`
- **Testes**: `[Nome]Tests.cs`

### Pastas:
- **PascalCase** para nomes de pastas
- **Implementation** / **Interface** para separação de contratos
- **Plural** para entidades relacionadas (ex: `Controllers`, `Services`)

### Namespaces:
```csharp
[NomeProjeto].API.Controllers          // Controllers
[NomeProjeto].Domain.Implementation    // Entidades
[NomeProjeto].Services.Interface       // Interfaces de serviços
[NomeProjeto].Repository.Implementation // Repositórios
[NomeProjeto].Infrastructure.Implementation // Mapeamentos EF
```

## 🚀 Scripts de Automação

### Comandos Essenciais (criar como arquivos .sh/.bat):

```bash
# build.sh / build.bat
dotnet build Solution/[NomeProjeto].sln --configuration Release

# run-dev.sh / run-dev.bat
dotnet run --project Src/[NomeProjeto].API --configuration Debug

# run-tests.sh / run-tests.bat
dotnet test Src/[NomeProjeto].Tests/[NomeProjeto].Tests.csproj

# create-migration.sh / create-migration.bat
dotnet ef migrations add [NomeMigration] --project Src/[NomeProjeto].API --context ApiContextDevelopment

# update-database.sh / update-database.bat
dotnet ef database update --project Src/[NomeProjeto].API --context ApiContextDevelopment
```

---

## 💡 Dicas de Implementação

1. **Comece sempre com** a estrutura básica de pastas
2. **Crie primeiro** as entidades de domínio
3. **Implemente** os repositórios e Unit of Work
4. **Desenvolva** os serviços de aplicação
5. **Crie** os controllers da API
6. **Configure** a injeção de dependência
7. **Adicione** testes conforme desenvolve
8. **Documente** as APIs com Swagger

Esta estrutura garante consistência e manutenibilidade em todos os seus projetos!