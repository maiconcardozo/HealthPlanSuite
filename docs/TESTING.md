# Authentication Tests Documentation

## Visão Geral

Este documento descreve como executar os testes do projeto de autenticação, que segue a arquitetura TDD (Test-Driven Development) e fornece cobertura abrangente para todos os endpoints da API.

## Estrutura dos Testes

Os testes estão organizados na seguinte estrutura:

```
Src/Authentication.Tests/
├── Controllers/              # Testes específicos de controladores (futuros)
├── Fixtures/                 # Configurações de teste e factories
├── Helpers/                  # Utilitários e helpers para testes
├── Integration/              # Testes de integração de endpoints
├── Unit/                     # Testes unitários de lógica de negócio
└── Authentication.Tests.csproj
```

## Tipos de Testes Implementados

### 1. Testes de Integração

Localizados em `Integration/`, testam os endpoints da API end-to-end:

- **AuthenticationControllerTests**: Testa geração de token e criação de contas
- **ClaimControllerTests**: Testa CRUD de claims/permissões
- **ActionControllerTests**: Testa CRUD de ações
- **ClaimActionControllerTests**: Testa mapeamento claim-ação
- **AccountClaimActionControllerTests**: Testa permissões de usuários

### 2. Testes Unitários

Localizados em `Unit/`, testam lógica de negócio isolada:

- **TokenGenerationTests**: Testa geração e validação de tokens JWT
- **PasswordHashingTests**: Testa hash e verificação de senhas
- **ValidationTests**: Testa validação de entrada de dados
- **ClaimsAndTokenTests**: Testa integração entre claims e tokens

## Cenários de Teste Cobertos

### Para Cada Endpoint:

#### ✅ Casos de Sucesso (200 OK)
- Dados válidos
- Operações bem-sucedidas
- Respostas corretas

#### ❌ Casos de Exceção
- **400 Bad Request**: Dados inválidos, JSON malformado
- **401 Unauthorized**: Falha de autenticação
- **404 Not Found**: Recursos não encontrados
- **500 Internal Server Error**: Erros de servidor
- **405 Method Not Allowed**: Métodos HTTP não suportados

#### 🔍 Casos Específicos
- Validação de entrada de dados
- Teste de limites e valores extremos
- Cenários de erro específicos de cada endpoint

## Como Executar os Testes

### Pré-requisitos
- .NET 8.0 SDK
- Todas as dependências restauradas

### Comandos Básicos

```bash
# Navegar para o diretório do projeto
cd /home/runner/work/Authentication/Authentication

# Restaurar dependências
dotnet restore Solution/Authentication.sln

# Executar todos os testes
dotnet test Src/Authentication.Tests/Authentication.Tests.csproj

# Executar testes com verbosidade detalhada
dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --verbosity normal

# Executar apenas testes de integração
dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --filter "FullyQualifiedName~Integration"

# Executar apenas testes unitários
dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --filter "FullyQualifiedName~Unit"

# Executar testes com cobertura de código
dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --collect:"XPlat Code Coverage"

# Executar testes de um controlador específico
dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --filter "FullyQualifiedName~AuthenticationControllerTests"
```

### Executar no Visual Studio

1. Abrir a solução `Solution/Authentication.sln`
2. Compilar a solução (Ctrl+Shift+B)
3. Abrir Test Explorer (Test > Test Explorer)
4. Executar todos os testes ou testes específicos

### Executar no Visual Studio Code

1. Instalar extensão C# Dev Kit
2. Abrir a pasta do projeto
3. Usar Command Palette (Ctrl+Shift+P) > ".NET: Run Tests"

## Estrutura de um Teste Típico

### Exemplo de Teste de Integração

```csharp
[Fact]
public async Task GenerateToken_WithValidCredentials_ShouldReturnOk()
{
    // Arrange
    var request = new
    {
        userName = "testuser",
        password = "testpassword123"
    };

    var content = new StringContent(
        JsonSerializer.Serialize(request),
        Encoding.UTF8,
        "application/json");

    // Act
    var response = await _client.PostAsync("/Authentication/GenerateToken", content);

    // Assert
    response.StatusCode.Should().BeOneOf(
        HttpStatusCode.OK,           // Success case
        HttpStatusCode.BadRequest,   // Validation error
        HttpStatusCode.Unauthorized, // Invalid credentials
        HttpStatusCode.InternalServerError // Configuration issues
    );
}
```

### Exemplo de Teste Unitário

```csharp
[Fact]
public void ComputeHash_WithSamePassword_ShouldReturnConsistentHash()
{
    // Arrange
    var password = "testpassword123";

    // Act
    var hash1 = ComputeTestHash(password);
    var hash2 = ComputeTestHash(password);

    // Assert
    hash1.Should().Be(hash2);
    hash1.Should().NotBeNullOrEmpty();
}
```

## Padrões de Teste Seguidos

### 1. Arrange-Act-Assert (AAA)
Todos os testes seguem o padrão AAA para clareza e consistência.

### 2. Naming Convention
- Testes de integração: `[Method]_[Scenario]_Should[ExpectedResult]`
- Testes unitários: `[Method]_[Input]_Should[ExpectedOutput]`

### 3. FluentAssertions
Uso consistente do FluentAssertions para assertions mais legíveis.

### 4. Test Data
Uso de helpers e factories para dados de teste consistentes.

## Ferramentas e Frameworks Utilizados

- **xUnit**: Framework de teste principal
- **FluentAssertions**: Assertions mais expressivas
- **Moq**: Mocking framework para isolamento
- **Microsoft.AspNetCore.Mvc.Testing**: Testes de integração web
- **Microsoft.EntityFrameworkCore.InMemory**: Banco de dados em memória

## Cobertura de Testes

Os testes cobrem:

### ✅ Endpoints Testados
- `/Authentication/GenerateToken` (POST)
- `/Authentication/AddAccount` (POST)
- `/Claim/*` (GET, POST, PUT, DELETE)
- `/Action/*` (GET, POST, PUT, DELETE)
- `/ClaimAction/*` (GET, POST, PUT, DELETE)
- `/AccountClaimAction/*` (GET, POST, PUT, DELETE)

### ✅ Funcionalidades Testadas
- Geração de tokens JWT
- Validação de dados de entrada
- Hash de senhas
- Mapeamento de claims para ações
- Permissões de usuários
- Validação de métodos HTTP
- Tratamento de erros

### ✅ Cenários de Status Codes
- 200 OK (sucesso)
- 400 Bad Request (dados inválidos)
- 401 Unauthorized (não autorizado)
- 404 Not Found (não encontrado)
- 405 Method Not Allowed (método não permitido)
- 500 Internal Server Error (erro interno)

## Resolução de Problemas

### Problemas Comuns

1. **Falhas de Build**: Verificar se todas as dependências estão restauradas
2. **Testes Falham por Timeout**: Aumentar timeout ou verificar configuração de banco
3. **Falhas de Conexão**: Verificar se o banco de dados em memória está configurado
4. **Dependências Ausentes**: Executar `dotnet restore`

### Debugging

```bash
# Executar com logs detalhados
dotnet test --logger "console;verbosity=detailed"

# Executar um teste específico para debugging
dotnet test --filter "FullyQualifiedName~SpecificTestName"
```

## Continuous Integration

Os testes são projetados para serem executados em pipelines CI/CD:

```yaml
# Exemplo para GitHub Actions
- name: Run Tests
  run: dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --no-build --verbosity normal
```

## Contribuindo com Novos Testes

Ao adicionar novos testes:

1. Seguir a estrutura de pastas existente
2. Usar os padrões de naming estabelecidos
3. Incluir casos de sucesso e falha
4. Adicionar documentação quando necessário
5. Garantir que os testes sejam independentes

## Métricas de Qualidade

- **Cobertura de Código**: Objetivo > 80%
- **Tempo de Execução**: Todos os testes < 30 segundos
- **Confiabilidade**: 0% de testes flaky
- **Manutenibilidade**: Testes legíveis e bem documentados