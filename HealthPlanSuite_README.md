# 🏥 HealthPlanSuite - Sistema de Gestão de Planos de Saúde

## 📋 Visão Geral

O **HealthPlanSuite** é um sistema completo para gestão de planos de saúde, beneficiários e cotações, desenvolvido seguindo os padrões de Clean Architecture e Domain-Driven Design (DDD). O sistema é composto por dois projetos principais que implementam a modelagem relacional completa do domínio de planos de saúde.

## 🏗️ Arquitetura dos Projetos

### HealthPlanSuite.API
**Camada de Apresentação/API**
```
Src/HealthPlanSuite.API/
├── Controllers/                 # Controladores RESTful
│   ├── OperadoraController.cs   # CRUD para operadoras
│   └── CotacaoController.cs     # CRUD para cotações
├── Program.cs                   # Ponto de entrada da aplicação
└── Startup.cs                   # Configuração de serviços e middleware
```

### HealthPlanSuite.Quote
**Camada de Domínio e Lógica de Negócio**
```
Src/HealthPlanSuite.Quote/
├── Domain/                      # Entidades do domínio
│   ├── Implementation/          # Entidades concretas
│   │   ├── Operadora.cs        # Operadoras de planos
│   │   ├── Plano.cs            # Planos de saúde
│   │   ├── Beneficiario.cs     # Beneficiários/segurados
│   │   ├── Cotacao.cs          # Cotações de planos
│   │   ├── Cobertura.cs        # Coberturas médicas
│   │   └── ...                 # Outras entidades
│   └── Interface/              # Interfaces do domínio
├── DTO/                        # Data Transfer Objects
├── Services/                   # Serviços de negócio
│   ├── Interface/              # Contratos de serviços
│   └── Implementation/         # Implementações dos serviços
├── Repository/                 # Camada de acesso a dados
│   └── Interface/              # Contratos de repositórios
└── Mapping/                    # Profiles do AutoMapper
```

### HealthPlanSuite.Tests
**Testes Unitários**
```
Src/HealthPlanSuite.Tests/
└── Services/                   # Testes dos serviços
    └── OperadoraServiceTests.cs # Testes do serviço de operadoras
```

## 🗄️ Modelagem do Banco de Dados

A modelagem completa está documentada em `Docs/modelagem_completa_plano_saude.sql` e inclui:

### 📊 Entidades Principais

1. **Operadoras** - Operadoras de planos de saúde registradas na ANS
2. **TiposPlano** - Classificação dos tipos de planos (Ambulatorial, Hospitalar, etc.)
3. **Planos** - Planos de saúde oferecidos pelas operadoras
4. **FaixasEtarias** - Faixas etárias para precificação
5. **PrecosPlanos** - Preços dos planos por faixa etária com vigência
6. **Beneficiarios** - Beneficiários/segurados titulares
7. **Dependentes** - Dependentes dos beneficiários
8. **Cotacoes** - Cotações de planos solicitadas
9. **ItensCotacao** - Planos incluídos nas cotações
10. **Coberturas** - Serviços médicos disponíveis
11. **CoberturasPorPlano** - Coberturas específicas por plano

### 🔗 Relacionamentos

- Operadoras ←→ Planos (1:N)
- Planos ←→ PrecosPlanos (1:N)
- Beneficiarios ←→ Dependentes (1:N)
- Beneficiarios ←→ Cotacoes (1:N)
- Cotacoes ←→ ItensCotacao (1:N)
- Planos ←→ CoberturasPorPlano ←→ Coberturas (N:M)

## 🚀 Funcionalidades Implementadas

### ✅ Operadoras
- [x] CRUD completo de operadoras
- [x] Validação de registro ANS único
- [x] Validação de CNPJ único
- [x] Consulta por registro ANS
- [x] Controle de ativação/desativação

### ✅ Cotações
- [x] CRUD de cotações
- [x] Geração automática de protocolo
- [x] Controle de status (Pendente, Em Análise, Aprovada, Rejeitada, Expirada)
- [x] Cálculo automático de valores
- [x] Consultas por beneficiário e status
- [x] Processamento de expiração automática

### ✅ Estrutura Técnica
- [x] Clean Architecture com separação de camadas
- [x] Domain-Driven Design (DDD)
- [x] Repository Pattern
- [x] Service Layer
- [x] AutoMapper para mapeamento objeto-objeto
- [x] Controladores RESTful com documentação
- [x] Testes unitários com Moq e FluentAssertions
- [x] Swagger/OpenAPI para documentação da API

## 📋 Próximos Passos (Implementação Pendente)

### 🔲 Repositórios e Entity Framework
- [ ] Configuração do Entity Framework Core
- [ ] DbContext para HealthPlanSuite
- [ ] Implementação dos repositórios
- [ ] Migrations automáticas
- [ ] Configuração de relacionamentos

### 🔲 Serviços Adicionais
- [ ] Serviço completo de Cotações
- [ ] Serviço de Beneficiários
- [ ] Serviço de Planos
- [ ] Serviço de cálculo de preços

### 🔲 Controladores Adicionais
- [ ] BeneficiarioController
- [ ] PlanoController  
- [ ] CoberturaController
- [ ] RelatoriosController

### 🔲 Validações
- [ ] FluentValidation para DTOs
- [ ] Validações de negócio
- [ ] Validações de CPF/CNPJ

### 🔲 Testes
- [ ] Testes de integração
- [ ] Testes de controladores
- [ ] Testes de repositórios
- [ ] Cobertura de testes > 80%

## 🛠️ Como Usar

### Pré-requisitos
- .NET 9.0 SDK
- MySQL 8.0+
- Visual Studio 2022 ou VS Code

### Configuração

1. **Clone o repositório**
```bash
git clone <repository-url>
cd HealthPlanSuite
```

2. **Configure a string de conexão**
Edite `Src/HealthPlanSuite.API/appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HealthPlanSuiteDB;Uid=root;Pwd=password;"
  }
}
```

3. **Execute o script SQL**
```bash
mysql -u root -p < Docs/modelagem_completa_plano_saude.sql
```

4. **Execute a aplicação**
```bash
cd Src/HealthPlanSuite.API
dotnet run
```

5. **Acesse a documentação**
Abra: `https://localhost:7001`

### Endpoints Disponíveis

#### Operadoras
- `GET /api/operadora` - Lista todas as operadoras
- `GET /api/operadora/{id}` - Obtém operadora por ID  
- `GET /api/operadora/registro-ans/{registroANS}` - Obtém por registro ANS
- `POST /api/operadora` - Cria nova operadora
- `PUT /api/operadora/{id}` - Atualiza operadora
- `DELETE /api/operadora/{id}` - Remove operadora

#### Cotações
- `GET /api/cotacao` - Lista todas as cotações (resumo)
- `GET /api/cotacao/{id}` - Obtém cotação completa por ID
- `GET /api/cotacao/protocolo/{protocolo}` - Obtém por protocolo
- `GET /api/cotacao/beneficiario/{id}` - Cotações por beneficiário
- `GET /api/cotacao/status/{status}` - Cotações por status
- `POST /api/cotacao` - Cria nova cotação
- `PUT /api/cotacao/{id}` - Atualiza cotação
- `PATCH /api/cotacao/{id}/status` - Atualiza status
- `DELETE /api/cotacao/{id}` - Remove cotação
- `GET /api/cotacao/gerar-protocolo` - Gera novo protocolo
- `GET /api/cotacao/{id}/valor-total` - Calcula valor total
- `POST /api/cotacao/processar-expiracao` - Processa expiração automática

## 🧪 Executar Testes

```bash
cd Src/HealthPlanSuite.Tests
dotnet test
```

## 📚 Documentação Técnica

- **Arquitetura**: Clean Architecture + DDD
- **Framework**: ASP.NET Core 9.0
- **ORM**: Entity Framework Core 9.0
- **Banco**: MySQL 8.0+ / MariaDB
- **Mapeamento**: AutoMapper 15.0
- **Testes**: xUnit + Moq + FluentAssertions
- **Documentação**: Swagger/OpenAPI

## 🤝 Padrões Seguidos

1. **Clean Architecture** - Separação clara de responsabilidades
2. **Repository Pattern** - Abstração da camada de dados  
3. **Service Layer** - Lógica de negócio centralizada
4. **DTO Pattern** - Transfer objects para API
5. **Dependency Injection** - Inversão de controle
6. **SOLID Principles** - Código limpo e manutenível
7. **RESTful API** - Endpoints padronizados
8. **Unit Testing** - Cobertura de testes unitários

## 📝 Considerações

Este sistema demonstra a implementação completa da modelagem relacional para o domínio de planos de saúde, seguindo rigorosamente os padrões arquiteturais e de nomenclatura dos projetos de referência no repositório. A estrutura criada serve como base sólida para expansão e implementação das funcionalidades pendentes.