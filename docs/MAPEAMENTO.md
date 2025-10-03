# 🗺️ Mapeamento do Projeto HealthPlan Suite

Este documento fornece um mapeamento completo dos controllers, endpoints, classes principais e arquitetura do projeto HealthPlan Suite.

## 📋 Índice
- [Controllers e Endpoints](#controllers-e-endpoints)
- [Classes Principais](#classes-principais)
- [Organização do Projeto](#organização-do-projeto)
- [Observações](#observações)

---

## 🎮 Controllers e Endpoints

### 1. PlanCoverageController
**Descrição**: Gerencia operações CRUD de coberturas de plano.

**Rota Base**: `/PlanCoverage`

**Endpoints**:
- `GET /PlanCoverage/plan-coverages` - Recupera todas as coberturas de plano ativas
- `GET /PlanCoverage/{id}` - Recupera uma cobertura de plano específica por ID
- `GET /PlanCoverage/health-plan/{healthPlanId}` - Recupera coberturas por ID do plano de saúde
- `POST /PlanCoverage` - Cria uma nova cobertura de plano
- `PUT /PlanCoverage/{id}` - Atualiza uma cobertura de plano existente
- `DELETE /PlanCoverage/{id}` - Remove uma cobertura de plano

**Arquivo**: `Src/HealthPlan.API/Controllers/PlanCoverageController.cs`

---

### 2. CoverageController
**Descrição**: Gerencia as coberturas disponíveis no sistema.

**Rota Base**: `/Coverage`

**Endpoints**:
- `GET /Coverage/coverages` - Lista todas as coberturas
- `GET /Coverage/{id}` - Recupera uma cobertura por ID
- `GET /Coverage/type/{coverageType}` - Filtra coberturas por tipo
- `POST /Coverage` - Cria nova cobertura
- `PUT /Coverage/{id}` - Atualiza cobertura existente
- `DELETE /Coverage/{id}` - Remove cobertura

**Arquivo**: `Src/HealthPlan.API/Controllers/CoverageController.cs`

---

### 3. QuoteController
**Descrição**: Gerencia cotações de planos de saúde.

**Rota Base**: `/Quote`

**Endpoints**:
- `GET /Quote/quotes` - Lista todas as cotações
- `GET /Quote/{id}` - Recupera cotação por ID
- `POST /Quote` - Cria nova cotação
- `PUT /Quote/{id}` - Atualiza cotação
- `DELETE /Quote/{id}` - Remove cotação

**Arquivo**: `Src/HealthPlan.API/Controllers/QuoteController.cs`

---

### 4. HealthPlanController
**Descrição**: Gerencia planos de saúde.

**Rota Base**: `/HealthPlan`

**Endpoints**:
- `GET /HealthPlan/healthplans` - Lista todos os planos
- `GET /HealthPlan/{id}` - Recupera plano por ID
- `GET /HealthPlan/company/{companyId}` - Filtra planos por operadora
- `GET /HealthPlan/code/{code}` - Busca plano por código
- `POST /HealthPlan` - Cria novo plano
- `PUT /HealthPlan/{id}` - Atualiza plano
- `DELETE /HealthPlan/{id}` - Remove plano

**Arquivo**: `Src/HealthPlan.API/Controllers/HealthPlanController.cs`

---

### 5. CompanyController
**Descrição**: Gerencia operadoras de planos de saúde.

**Rota Base**: `/Company`

**Endpoints**:
- `GET /Company/companies` - Lista todas as operadoras
- `GET /Company/{id}` - Recupera operadora por ID
- `GET /Company/cnpj/{cnpj}` - Busca operadora por CNPJ
- `POST /Company` - Cria nova operadora
- `PUT /Company/{id}` - Atualiza operadora
- `DELETE /Company/{id}` - Remove operadora

**Arquivo**: `Src/HealthPlan.API/Controllers/CompanyController.cs`

---

### 6. BeneficiaryController
**Descrição**: Gerencia beneficiários dos planos.

**Rota Base**: `/Beneficiary`

**Endpoints**:
- `GET /Beneficiary/beneficiaries` - Lista todos os beneficiários
- `GET /Beneficiary/{id}` - Recupera beneficiário por ID
- `GET /Beneficiary/cpf/{cpf}` - Busca beneficiário por CPF
- `POST /Beneficiary` - Cria novo beneficiário
- `PUT /Beneficiary/{id}` - Atualiza beneficiário
- `DELETE /Beneficiary/{id}` - Remove beneficiário

**Arquivo**: `Src/HealthPlan.API/Controllers/BeneficiaryController.cs`

---

### 7. AgeRangeController
**Descrição**: Gerencia faixas etárias para cálculo de preços.

**Rota Base**: `/AgeRange`

**Endpoints**:
- `GET /AgeRange/age-ranges` - Lista todas as faixas etárias
- `GET /AgeRange/{id}` - Recupera faixa etária por ID
- `POST /AgeRange` - Cria nova faixa etária
- `PUT /AgeRange/{id}` - Atualiza faixa etária
- `DELETE /AgeRange/{id}` - Remove faixa etária

**Arquivo**: `Src/HealthPlan.API/Controllers/AgeRangeController.cs`

---

### 8. AccommodationController
**Descrição**: Gerencia tipos de acomodação hospitalar.

**Rota Base**: `/Accommodation`

**Endpoints**:
- `GET /Accommodation/accommodations` - Lista todas as acomodações
- `GET /Accommodation/{id}` - Recupera acomodação por ID
- `GET /Accommodation/type/{type}` - Filtra acomodações por tipo
- `POST /Accommodation` - Cria nova acomodação
- `PUT /Accommodation/{id}` - Atualiza acomodação
- `DELETE /Accommodation/{id}` - Remove acomodação

**Arquivo**: `Src/HealthPlan.API/Controllers/AccommodationController.cs`

---

### 9. AcceptanceRuleController
**Descrição**: Gerencia regras de aceitação de planos.

**Rota Base**: `/AcceptanceRule`

**Endpoints**:
- `GET /AcceptanceRule/acceptance-rules` - Lista todas as regras de aceitação
- `GET /AcceptanceRule/{id}` - Recupera regra por ID
- `GET /AcceptanceRule/health-plan/{healthPlanId}` - Filtra regras por plano
- `POST /AcceptanceRule` - Cria nova regra
- `PUT /AcceptanceRule/{id}` - Atualiza regra
- `DELETE /AcceptanceRule/{id}` - Remove regra

**Arquivo**: `Src/HealthPlan.API/Controllers/AcceptanceRuleController.cs`

---

### 10. QuoteHistoryController
**Descrição**: Gerencia histórico de cotações.

**Rota Base**: `/QuoteHistory`

**Endpoints**:
- `GET /QuoteHistory/quote-histories` - Lista todo o histórico
- `GET /QuoteHistory/{id}` - Recupera histórico por ID
- `GET /QuoteHistory/quote/{quoteId}` - Filtra histórico por cotação
- `POST /QuoteHistory` - Cria novo registro de histórico
- `PUT /QuoteHistory/{id}` - Atualiza histórico
- `DELETE /QuoteHistory/{id}` - Remove histórico

**Arquivo**: `Src/HealthPlan.API/Controllers/QuoteHistoryController.cs`

---

### 11. TaxaAdesaoController
**Descrição**: Gerencia taxas de adesão aos planos.

**Rota Base**: `/TaxaAdesao`

**Endpoints**:
- `GET /TaxaAdesao/taxas-adesao` - Lista todas as taxas
- `GET /TaxaAdesao/{id}` - Recupera taxa por ID
- `POST /TaxaAdesao` - Cria nova taxa
- `PUT /TaxaAdesao/{id}` - Atualiza taxa
- `DELETE /TaxaAdesao/{id}` - Remove taxa

**Arquivo**: `Src/HealthPlan.API/Controllers/TaxaAdesaoController.cs`

---

### 12. DescontoPromocionalController
**Descrição**: Gerencia descontos promocionais.

**Rota Base**: `/DescontoPromocional`

**Endpoints**:
- `GET /DescontoPromocional/descontos-promocionais` - Lista todos os descontos
- `GET /DescontoPromocional/{id}` - Recupera desconto por ID
- `POST /DescontoPromocional` - Cria novo desconto
- `PUT /DescontoPromocional/{id}` - Atualiza desconto
- `DELETE /DescontoPromocional/{id}` - Remove desconto

**Arquivo**: `Src/HealthPlan.API/Controllers/DescontoPromocionalController.cs`

---

### 13. CoparticipacaoProcedimentoController
**Descrição**: Gerencia coparticipação em procedimentos.

**Rota Base**: `/CoparticipacaoProcedimento`

**Endpoints**:
- `GET /CoparticipacaoProcedimento/coparticipacoes` - Lista todas as coparticipações
- `GET /CoparticipacaoProcedimento/{id}` - Recupera coparticipação por ID
- `POST /CoparticipacaoProcedimento` - Cria nova coparticipação
- `PUT /CoparticipacaoProcedimento/{id}` - Atualiza coparticipação
- `DELETE /CoparticipacaoProcedimento/{id}` - Remove coparticipação

**Arquivo**: `Src/HealthPlan.API/Controllers/CoparticipacaoProcedimentoController.cs`

---

### 14. PrecoPlanoFaixaController
**Descrição**: Gerencia preços de planos por faixa etária.

**Rota Base**: `/PrecoPlanoFaixa`

**Endpoints**:
- `GET /PrecoPlanoFaixa/precos` - Lista todos os preços
- `GET /PrecoPlanoFaixa/{id}` - Recupera preço por ID
- `POST /PrecoPlanoFaixa` - Cria novo preço
- `PUT /PrecoPlanoFaixa/{id}` - Atualiza preço
- `DELETE /PrecoPlanoFaixa/{id}` - Remove preço

**Arquivo**: `Src/HealthPlan.API/Controllers/PrecoPlanoFaixaController.cs`

---

## 🔧 Classes Principais

### 1. ApplicationConstants
**Localização**: 
- `Src/HealthPlan.API/Constants/ApplicationConstants.cs`
- `Src/HealthPlan.Quote/Constants/ApplicationConstants.cs`

**Descrição**: Define constantes utilizadas em toda a aplicação.

**Principais Constantes**:
- `DefaultCreatedByUser`: Usuário padrão para criação de registros
- `DefaultConnectionStringName`: Nome padrão da string de conexão
- `ClaimTypes.Permission`: Tipo de claim para permissões
- `Environment.Production/Development`: Ambientes de execução
- `Cors.AllowAllPolicy`: Política de CORS
- `Api.Title`, `Api.Version`: Informações da API
- `Api.SwaggerEndpoint`: Endpoint do Swagger

**Finalidade**: Centralizar valores constantes e configurações da aplicação, facilitando manutenção e padronização.

---

### 2. BaseApiContext
**Localização**: `Src/HealthPlan.API/Data/BaseApiContext.cs`

**Descrição**: Classe base abstrata para contextos de banco de dados da API.

**Funcionalidades**:
- Herda de `DbContext` do Entity Framework Core
- Configuração automática de conexão com banco de dados
- Suporte a MySQL em produção
- Suporte a InMemoryDatabase para testes
- Carregamento automático do modelo de dados via `ApplicationContext`

**Responsabilidades**:
- Gerenciar conexões com banco de dados
- Aplicar configurações do Entity Framework
- Facilitar testes com banco em memória

---

### 3. SucessDetails
**Localização**: `Src/HealthPlan.API/Swagger/SucessDetails.cs`

**Descrição**: Classe para padronização de respostas de sucesso da API.

**Propriedades**:
- `Status`: Código HTTP de status (herda de ProblemDetails)
- `Title`: Título da resposta
- `Detail`: Detalhes adicionais
- `Type`: URI do RFC que define o tipo de resposta
- `Data`: Objeto com os dados da resposta
- `Instance`: Caminho da requisição

**Uso**: Retornar respostas consistentes e padronizadas em endpoints de sucesso.

**Exemplo Factory**: `SuccessResponseExampleFactory.ForSuccess()` cria instâncias configuradas.

---

### 4. Utils
**Localização**: `Src/HealthPlan.API/Util/Utils.cs`

**Descrição**: Classe utilitária com métodos auxiliares.

**Métodos Principais**:
- `GetConnectionString()`: Retorna a string de conexão apropriada
  - Detecta automaticamente ambiente de teste
  - Retorna InMemoryDatabase para testes
  - Retorna connection string configurada para produção/desenvolvimento

**Finalidade**: Fornecer funções auxiliares reutilizáveis em diferentes partes da aplicação.

---

### 5. Outras Classes Importantes

#### ProblemDetailsExampleFactory
**Localização**: `Src/HealthPlan.API/Swagger/ProblemDetailsExampleFactory.cs`

**Descrição**: Factory para criar respostas de erro padronizadas.

**Métodos**:
- `ForBadRequest()`: Erros de validação (400)
- `ForUnauthorized()`: Erros de autorização (401)
- `ForNotFound()`: Recursos não encontrados (404)
- `ForConflict()`: Conflitos de dados (409)
- `ForInternalServerError()`: Erros internos (500)

#### CleanTemplateApplicationMapperInitializer
**Localização**: `Src/HealthPlan.Quote/Mapping/`

**Descrição**: Inicializa e configura o AutoMapper para mapeamento entre DTOs e entidades de domínio.

#### Route Classes
**Localização**: `Src/HealthPlan.API/Swagger/*Routes.cs`

**Descrição**: Definem constantes para rotas de cada controller, garantindo consistência e facilitando refatoração.

---

## 🏗️ Organização do Projeto

O projeto HealthPlan Suite segue os princípios da **Clean Architecture**, promovendo separação de responsabilidades, testabilidade e manutenibilidade.

### Camadas da Arquitetura

```
HealthPlanSuite/
├── Src/
│   ├── HealthPlan.API/              # Camada de Apresentação
│   │   ├── Controllers/             # Endpoints da API
│   │   ├── Middleware/              # Middlewares HTTP
│   │   ├── Swagger/                 # Documentação e exemplos da API
│   │   │   ├── Routes/              # Constantes de rotas
│   │   │   └── Examples/            # Exemplos para Swagger
│   │   ├── Data/                    # Contextos específicos da API
│   │   ├── Constants/               # Constantes da API
│   │   ├── Util/                    # Utilitários
│   │   └── Resource/                # Recursos de localização
│   │
│   └── HealthPlan.Quote/            # Camadas de Domínio, Aplicação e Infraestrutura
│       ├── Domain/                  # Camada de Domínio
│       │   ├── Interface/           # Interfaces de entidades
│       │   └── Implementation/      # Entidades de domínio
│       │
│       ├── Services/                # Camada de Aplicação
│       │   ├── Interface/           # Interfaces de serviços
│       │   └── Implementation/      # Lógica de negócio
│       │
│       ├── Repository/              # Camada de Infraestrutura - Dados
│       │   ├── Interface/           # Interfaces de repositórios
│       │   └── Implementation/      # Acesso a dados
│       │
│       ├── Infrastructure/          # Camada de Infraestrutura
│       │   ├── Data/                # Configurações de contexto
│       │   └── Implementation/      # Mapeamentos EF Core
│       │
│       ├── DTO/                     # Data Transfer Objects
│       ├── Mapping/                 # Configurações do AutoMapper
│       ├── UnitOfWork/              # Padrão Unit of Work
│       ├── Constants/               # Constantes do domínio
│       └── Validation/              # Regras de validação
│
└── HealthPlan.Test/                 # Camada de Testes
    ├── Unit/                        # Testes unitários
    ├── Integration/                 # Testes de integração
    └── Helpers/                     # Utilitários de teste
```

### Princípios Aplicados

#### 1. **Separation of Concerns**
Cada camada tem responsabilidades bem definidas:
- **Presentation (API)**: Recebe requisições HTTP, valida entrada, retorna respostas
- **Application (Services)**: Contém lógica de negócio e orquestração
- **Domain**: Define entidades e regras de negócio fundamentais
- **Infrastructure**: Implementa acesso a dados e integrações externas

#### 2. **Dependency Inversion**
- Camadas internas não dependem de camadas externas
- Interfaces definem contratos entre camadas
- Injeção de dependência gerenciada pelo ASP.NET Core

#### 3. **Single Responsibility**
- Cada classe tem uma única responsabilidade
- Controllers apenas gerenciam requisições/respostas
- Services contêm lógica de negócio
- Repositories gerenciam persistência

#### 4. **Clean Code**
- DTOs separam representações de dados da API do domínio
- Mapeamento automático com AutoMapper
- Validações centralizadas
- Tratamento de erros padronizado

---

## 📚 Padrões de Design Utilizados

### 1. Repository Pattern
**Localização**: `Src/HealthPlan.Quote/Repository/`

Abstrai o acesso a dados, permitindo:
- Trocar implementação de persistência sem afetar lógica de negócio
- Facilitar testes com repositórios mock
- Centralizar queries e operações de dados

### 2. Unit of Work Pattern
**Localização**: `Src/HealthPlan.Quote/UnitOfWork/`

Gerencia transações:
- Garante consistência em operações múltiplas
- Controla commit/rollback de transações
- Coordena múltiplos repositórios

### 3. Dependency Injection
Configurado em `Program.cs`:
- Registro de serviços e repositórios
- Controle de ciclo de vida (Scoped, Singleton, Transient)
- Facilita testes e desacoplamento

### 4. DTO Pattern
**Localização**: `Src/HealthPlan.Quote/DTO/`

Separa representações:
- `*PayLoadDTO`: Dados de entrada (POST/PUT)
- `*ResponseDTO`: Dados de saída (GET)
- Protege modelo de domínio
- Controla quais dados são expostos

### 5. Factory Pattern
**Localização**: `Src/HealthPlan.API/Swagger/*Factory.cs`

Cria objetos complexos:
- `SuccessResponseExampleFactory`: Respostas de sucesso
- `ProblemDetailsExampleFactory`: Respostas de erro
- Garante consistência e facilita manutenção

---

## 🔍 Observações

### Para Navegar pelo Código

1. **Explorar Controllers**: Comece por `Src/HealthPlan.API/Controllers/` para entender os endpoints disponíveis

2. **Entender Entidades**: Veja `Src/HealthPlan.Quote/Domain/Implementation/` para conhecer o modelo de domínio

3. **Revisar Serviços**: Analise `Src/HealthPlan.Quote/Services/Implementation/` para a lógica de negócio

4. **Verificar DTOs**: Confira `Src/HealthPlan.Quote/DTO/` para estruturas de entrada/saída

5. **Consultar Documentação**: Use o Swagger em `/swagger` quando a aplicação estiver rodando

### Busca no GitHub

Para encontrar controllers, classes e funcionalidades específicas:
- Use a busca de código do GitHub: Pressione `/` e digite o termo
- Filtre por tipo de arquivo: `filename:Controller.cs`
- Busque por classes específicas: `class:ApplicationConstants`
- Encontre interfaces: `interface:IService`

### Links Úteis

- **Arquitetura Completa**: [docs/ARCHITECTURE.md](./ARCHITECTURE.md)
- **Guia de Desenvolvimento**: [docs/DEVELOPMENT.md](./DEVELOPMENT.md)
- **Documentação da API**: [docs/API.md](./API.md)
- **Guia de Testes**: [docs/TESTING.md](./TESTING.md)
- **Início Rápido**: [docs/QUICK_START.md](./QUICK_START.md)

### Contribuindo

Para adicionar novos recursos ou modificar existentes:
1. Leia [CONTRIBUTING.md](../CONTRIBUTING.md) para diretrizes
2. Siga os padrões arquiteturais estabelecidos
3. Mantenha a coerência com o código existente
4. Adicione testes para novas funcionalidades
5. Atualize a documentação conforme necessário

---

## 📞 Suporte

Para dúvidas ou problemas:
- Abra uma issue no GitHub
- Consulte a documentação completa na pasta `docs/`
- Revise os exemplos em `docs/EXAMPLES.md`

---

**Última Atualização**: Janeiro 2025
**Versão do Documento**: 1.0
