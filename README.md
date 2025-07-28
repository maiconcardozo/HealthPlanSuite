# HealthPlan Suite - Sistema de Planos de Saúde para .NET

[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-blue.svg)](https://docs.microsoft.com/en-us/ef/core/)

## 📋 Visão Geral

O **HealthPlan Suite** é um sistema .NET que fornece funcionalidades completas para gerenciamento de planos de saúde seguindo os princípios de Domain-Driven Design (DDD). Este serviço implementa um sistema abrangente de gestão de operadoras de saúde, planos, tabelas de preços, coberturas e ajustes.

### 🏥 Funcionalidades Principais

- **Gerenciamento de Operadoras**: Cadastro e administração de operadoras de planos de saúde
- **Gestão de Planos**: Criação e configuração de diferentes tipos de planos de saúde
- **Tabelas de Preços**: Configuração de preços por faixa etária e plano
- **Coberturas**: Definição de coberturas disponíveis para cada plano
- **Ajustes de Planos**: Sistema de ajustes e reajustes de valores
- **Estabelecimentos de Saúde**: Gestão de rede credenciada
- **API RESTful Completa**: Endpoints CRUD para todas as entidades

## 🏗️ Arquitetura

O serviço está organizado em camadas bem definidas seguindo os princípios de Clean Architecture:

```
HealthPlanSuite/
├── Src/
│   ├── HealthPlan.Api/                # Camada de API
│   │   ├── Controllers/               # Controllers da API
│   │   │   └── HealthInsuranceOperatorController.cs  # Gerenciamento de operadoras
│   │   ├── Middleware/                # Middleware customizado  
│   │   ├── Swagger/                   # Documentação da API
│   │   └── Data/                      # Contextos do banco de dados
│   │
│   └── HealthPlan.Quote/              # Domínio & Lógica de Negócio
│       ├── Domain/                    # Entidades de domínio
│       │   └── HealthPlan/           # Entidades relacionadas a planos de saúde
│       │       ├── Implementation/    # Implementações concretas
│       │       │   ├── HealthPlan.cs           # Plano de saúde
│       │       │   ├── HealthInsuranceOperator.cs # Operadora
│       │       │   ├── PriceTable.cs          # Tabela de preços
│       │       │   ├── PlanCoverage.cs        # Cobertura do plano
│       │       │   ├── PlanType.cs            # Tipo de plano
│       │       │   ├── AgeRange.cs            # Faixa etária
│       │       │   ├── HealthEstablishment.cs # Estabelecimento de saúde
│       │       │   └── PlanAdjustment.cs      # Ajustes do plano
│       │       └── Interface/         # Interfaces de domínio
│       ├── Services/                  # Serviços de negócio
│       │   └── HealthPlan/           # Serviços de planos de saúde
│       │       ├── Implementation/    # Implementações de serviços
│       │       └── Interface/        # Contratos de serviços
│       ├── Repository/                # Camada de acesso a dados
│       │   └── HealthPlan/           # Repositórios de planos de saúde
│       │       ├── Implementation/    # Implementações de repositórios
│       │       └── Interface/        # Contratos de repositórios
│       ├── DTO/                      # Objetos de transferência
│       │   └── HealthPlan/           # DTOs de planos de saúde
│       ├── Infrastructure/           # Configurações de entidade
│       │   └── HealthPlan/           # Mapeamentos EF Core para planos
│       │       ├── Data/             # Contexto de dados
│       │       └── Implementation/   # Mapeamentos de entidades
│       ├── UnitOfWork/               # Padrão Unit of Work
│       │   ├── Implementation/       # Implementação do UoW
│       │   └── Interface/           # Contrato do UoW
│       ├── Extensions/               # Extensões de configuração
│       └── Validation/               # Validações
│           └── HealthPlan/          # Validações de planos de saúde
│
└── Solution/                         # Configuração da solução
```

## 🔧 Tecnologias Utilizadas

- **.NET 8.0** - Framework principal
- **ASP.NET Core 8.0** - Framework para API RESTful
- **Entity Framework Core 8.0** - ORM para acesso a dados
- **FluentValidation** - Validação de entrada
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
cd Src/HealthPlan.Api
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
  }
}
```

### 2. Inicializando o Banco de Dados

```bash
cd Src/HealthPlan.Api
dotnet ef database update --context ApiContextDevelopment
```

### 3. Verificação da Instalação

- 🌐 **API Endpoint**: https://localhost:7001  
- 📖 **Documentação da API**: https://localhost:7001 (redireciona automaticamente para Swagger UI)
- ❤️ **Health Check**: https://localhost:7001/health

## 📚 Componentes Principais

### 🏛️ Camada de API

- **`HealthInsuranceOperatorController`**: Controller para gerenciamento de operadoras de saúde
- **`Middleware`**: Middleware customizado para tratamento de exceções e autenticação do Swagger

### 🏥 Camada de Serviços

- **`IHealthInsuranceOperatorService`**: Interface para serviços de operadoras de saúde
- **`HealthInsuranceOperatorService`**: Implementação dos serviços de operadoras

### 🗃️ Camada de Repositório

- **`IHealthInsuranceOperatorRepository`**: Interface para acesso a dados de operadoras
- **`HealthInsuranceOperatorRepository`**: Implementação com operações CRUD de operadoras

### 🏗️ Entidades Principais

- **`HealthPlan`**: Plano de saúde principal
- **`HealthInsuranceOperator`**: Operadora de planos de saúde  
- **`PriceTable`**: Tabela de preços por faixa etária
- **`PlanCoverage`**: Coberturas disponíveis
- **`PlanType`**: Tipos de plano
- **`AgeRange`**: Faixas etárias para precificação
- **`HealthEstablishment`**: Estabelecimentos credenciados
- **`PlanAdjustment`**: Ajustes e reajustes

## 🌐 API Endpoints

### Endpoints Principais de Operadoras de Saúde

| Método | Endpoint | Descrição | Autenticação |
|--------|----------|-----------|--------------|
| **GET** | `/HealthInsuranceOperator/GetAll` | 🏥 Listar todas as operadoras | ❌ |
| **GET** | `/HealthInsuranceOperator/GetById/{id}` | 🔍 Obter operadora por ID | ❌ |
| **POST** | `/HealthInsuranceOperator/Add` | ➕ Criar nova operadora | ❌ |
| **PUT** | `/HealthInsuranceOperator/Update/{id}` | ✏️ Atualizar operadora | ❌ |
| **DELETE** | `/HealthInsuranceOperator/Delete/{id}` | ❌ Excluir operadora | ❌ |

### 🏥 Listar Todas as Operadoras

Recupera uma lista de todas as operadoras de planos de saúde:

```bash
curl -X GET "https://localhost:7001/HealthInsuranceOperator/GetAll" \
  -H "Accept: application/json"
```

**Resposta de Sucesso (200):**
```json
[
  {
    "id": 1,
    "name": "Unimed Nacional",
    "cnpj": "12.345.678/0001-90",
    "website": "https://www.unimed.com.br",
    "phone": "(11) 1234-5678",
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-20T14:45:00Z"
  }
]
```

### 👤 Criar Nova Operadora

Registra uma nova operadora de planos de saúde:

```bash
curl -X POST "https://localhost:7001/HealthInsuranceOperator/Add" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Nova Operadora",
    "cnpj": "98.765.432/0001-01",
    "website": "https://www.novaoperadora.com.br",
    "phone": "(11) 9876-5432"
  }'
```

## ✅ Validação

O sistema inclui validações para garantir a integridade dos dados:

```csharp
// Exemplo de validação de operadora
public class HealthInsuranceOperatorValidator : AbstractValidator<HealthInsuranceOperatorPayLoadDTO>
{
    public HealthInsuranceOperatorValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.CNPJ).NotEmpty().Matches(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$");
        RuleFor(x => x.Website).Must(BeAValidUrl).When(x => !string.IsNullOrEmpty(x.Website));
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(20);
    }
    
    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
```

## 🧪 Executar Testes

O projeto inclui uma suíte completa de testes seguindo arquitetura TDD:

```bash
# Executar todos os testes
dotnet test Solution/HealthPlan.sln

# Executar testes com verbosidade
dotnet test Solution/HealthPlan.sln --verbosity normal

# Executar apenas testes de integração
dotnet test --filter "FullyQualifiedName~Integration"

# Executar apenas testes unitários
dotnet test --filter "FullyQualifiedName~Unit"
```

### 📊 Cobertura de Testes

- ✅ **Testes de Integração**: Endpoints da API de planos de saúde
- ✅ **Testes Unitários**: Lógica de negócio e validações
- ✅ **Cenários de Sucesso**: Casos 200 OK
- ✅ **Cenários de Exceção**: Codes 400, 404, 500
- ✅ **Validação de Dados**: Entrada e formato
- ✅ **Operações CRUD**: Planos e operadoras

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
dotnet run --project Src/HealthPlan.Api

# Executar testes
dotnet test Solution/HealthPlan.sln
```

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
