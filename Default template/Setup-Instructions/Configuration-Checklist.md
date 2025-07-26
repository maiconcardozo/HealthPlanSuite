# ✅ Checklist de Configuração - Novo Projeto

Use este checklist para garantir que você implementou todos os componentes necessários seguindo os padrões estabelecidos.

## 📋 Estrutura Base do Projeto

### ✅ 1. Configuração Inicial
- [ ] **Diretórios criados** seguindo a estrutura padrão
- [ ] **Foundation.Base clonado** no diretório pai correto
- [ ] **Solução Visual Studio** (.sln) criada
- [ ] **Projetos .NET** criados (API, Domain, Tests)
- [ ] **Referências entre projetos** configuradas corretamente
- [ ] **Referência ao Foundation.Base** adicionada

### ✅ 2. Dependências NuGet
- [ ] **Microsoft.EntityFrameworkCore** (9.0.0)
- [ ] **Microsoft.EntityFrameworkCore.Design** (9.0.0)
- [ ] **Pomelo.EntityFrameworkCore.MySql** (9.0.0)
- [ ] **Microsoft.AspNetCore.Authentication.JwtBearer** (9.0.0)
- [ ] **Swashbuckle.AspNetCore** (7.2.0)
- [ ] **FluentValidation.AspNetCore** (11.3.0)
- [ ] **Swashbuckle.AspNetCore.Filters** (8.0.2)
- [ ] **Swashbuckle.AspNetCore.Annotations** (7.2.0)

## 🏛️ Domain Layer

### ✅ 3. Entidades de Domínio
- [ ] **Account.cs** (entidade de usuário obrigatória)
- [ ] **JwtSettings.cs** (configurações JWT)
- [ ] **Token.cs** (entidade de token)
- [ ] **[SuasEntidades].cs** (suas entidades específicas)

### ✅ 4. Interfaces de Domínio
- [ ] **IAccount.cs**
- [ ] **IJwtSettings.cs**
- [ ] **IToken.cs**
- [ ] **I[SuasEntidades].cs**

### ✅ 5. Serviços de Aplicação
- [ ] **AccountService.cs** (gerenciamento de usuários)
- [ ] **AuthenticationService.cs** (autenticação JWT)
- [ ] **[SuasEntidades]Service.cs** (serviços específicos)

### ✅ 6. Interfaces de Serviços
- [ ] **IAccountService.cs**
- [ ] **IAuthenticationService.cs**
- [ ] **I[SuasEntidades]Service.cs**

### ✅ 7. Repositórios
- [ ] **AccountRepository.cs**
- [ ] **[SuasEntidades]Repository.cs**

### ✅ 8. Interfaces de Repositórios
- [ ] **IAccountRepository.cs**
- [ ] **I[SuasEntidades]Repository.cs**

### ✅ 9. Infrastructure
- [ ] **[SeuProjeto]Context.cs** (contexto principal)
- [ ] **I[SeuProjeto]Context.cs** (interface do contexto)
- [ ] **AccountMap.cs** (mapeamento EF Core)
- [ ] **[SuasEntidades]Map.cs** (mapeamentos específicos)

### ✅ 10. Unit of Work
- [ ] **[SeuProjeto]UnitOfWork.cs**
- [ ] **I[SeuProjeto]UnitOfWork.cs**

### ✅ 11. DTOs
- [ ] **LoginRequestDTO.cs**
- [ ] **TokenResponseDTO.cs**
- [ ] **CreateUserRequestDTO.cs**
- [ ] **AccountResponseDTO.cs**
- [ ] **[SuasEntidades]RequestDTO.cs**
- [ ] **[SuasEntidades]ResponseDTO.cs**

### ✅ 12. Validators
- [ ] **LoginRequestValidator.cs**
- [ ] **CreateUserRequestValidator.cs**
- [ ] **[SuasEntidades]Validator.cs**

### ✅ 13. Extensions
- [ ] **ServiceCollectionExtensions.cs** (configuração DI)
- [ ] **[SuasEntidades]Extensions.cs** (mapeamentos)

## 🌐 API Layer

### ✅ 14. Controllers
- [ ] **AuthenticationController.cs** (login, register, validate)
- [ ] **[SuasEntidades]Controller.cs** (CRUD completo)

### ✅ 15. Middleware
- [ ] **ExceptionHandlingMiddleware.cs** (tratamento de exceções)
- [ ] **JwtMiddleware.cs** (opcional, para custom JWT handling)

### ✅ 16. Swagger Configuration
- [ ] **[SeuProjeto]Routes.cs** (constantes de rotas)
- [ ] **TokenResponseExample.cs** (exemplos para documentação)
- [ ] **LocalizedSwaggerOperationFilter.cs** (se usar localização)

### ✅ 17. Data Contexts
- [ ] **ApiContextDevelopment.cs** (contexto para desenvolvimento)
- [ ] **ApiContextProduction.cs** (contexto para produção)

## 📁 Arquivos de Configuração

### ✅ 18. App Settings
- [ ] **appsettings.json** (configurações base)
- [ ] **appsettings.Development.json** (desenvolvimento)
- [ ] **appsettings.Production.json** (produção)
- [ ] **ConnectionStrings** configuradas
- [ ] **JwtSettings** configuradas (Issuer, Audience, SecretKey, ExpirationMinutes)

### ✅ 19. Program.cs
- [ ] **Configuração de serviços** (AddAuthenticationServices)
- [ ] **Configuração JWT** (AddAuthentication, AddJwtBearer)
- [ ] **Configuração Swagger** (AddSwaggerGen com JWT)
- [ ] **Configuração CORS** (AddCors)
- [ ] **Pipeline configurado** (UseAuthentication, UseAuthorization)

### ✅ 20. Project Files
- [ ] **[SeuProjeto].API.csproj** com dependências corretas
- [ ] **[SeuProjeto].Domain.csproj** com dependências corretas
- [ ] **[SeuProjeto].Tests.csproj** com dependências de teste
- [ ] **XML Documentation** habilitado no projeto API

## 🧪 Tests Layer

### ✅ 21. Testes Unitários
- [ ] **[SuasEntidades]ServiceTests.cs** (testes dos serviços)
- [ ] **[SuasEntidades]RepositoryTests.cs** (testes dos repositórios)
- [ ] **[SuasEntidades]Tests.cs** (testes das entidades)

### ✅ 22. Testes de Integração
- [ ] **[SuasEntidades]ControllerTests.cs** (testes dos controllers)
- [ ] **AuthenticationControllerTests.cs** (testes de autenticação)
- [ ] **DatabaseTests.cs** (testes de banco de dados)

### ✅ 23. Test Fixtures
- [ ] **[SuasEntidades]Fixture.cs** (dados de teste)
- [ ] **AccountFixture.cs** (dados de usuário para testes)

### ✅ 24. Test Helpers
- [ ] **TestHelper.cs** (funções auxiliares)
- [ ] **DatabaseHelper.cs** (setup de banco para testes)

## 🗃️ Banco de Dados

### ✅ 25. Entity Framework
- [ ] **DbContext** configurado corretamente
- [ ] **Entity mappings** implementados
- [ ] **Connection string** configurada
- [ ] **Database provider** configurado (MySQL/MariaDB)

### ✅ 26. Migrations
- [ ] **Migrations folder** criado
- [ ] **Initial migration** criada
- [ ] **Database** criado e atualizado
- [ ] **Seed data** configurado (se necessário)

## 🔐 Segurança e Autenticação

### ✅ 27. JWT Configuration
- [ ] **Secret key** configurada (mínimo 32 caracteres)
- [ ] **Issuer** configurado
- [ ] **Audience** configurado
- [ ] **Expiration time** configurado
- [ ] **Token validation** implementada

### ✅ 28. Password Security
- [ ] **Argon2 hashing** implementado (via Foundation.Base)
- [ ] **Password verification** implementada
- [ ] **Strong password policy** (se necessário)

### ✅ 29. Authorization
- [ ] **[Authorize] attributes** nos controllers protegidos
- [ ] **Claims-based authorization** (se usar RBAC)
- [ ] **Role-based access** (se necessário)

## 📚 Documentação

### ✅ 30. API Documentation
- [ ] **Swagger UI** configurado e funcionando
- [ ] **XML comments** nos controllers
- [ ] **SwaggerOperation attributes** nos endpoints
- [ ] **SwaggerResponse attributes** para status codes
- [ ] **Example responses** configurados

### ✅ 31. Project Documentation
- [ ] **README.md** do projeto atualizado
- [ ] **Architecture documentation** criada
- [ ] **API documentation** detalhada
- [ ] **Setup instructions** documentadas

## 🚀 Build e Deploy

### ✅ 32. Build Configuration
- [ ] **Solution compila** sem erros
- [ ] **Debug configuration** funciona
- [ ] **Release configuration** funciona
- [ ] **XML documentation** gerada

### ✅ 33. Runtime Testing
- [ ] **API inicia** corretamente
- [ ] **Swagger UI** acessível
- [ ] **Database connection** funciona
- [ ] **JWT authentication** funciona
- [ ] **CRUD operations** funcionam

### ✅ 34. Environment Testing
- [ ] **Development environment** testado
- [ ] **Production configuration** validada
- [ ] **Connection strings** testadas
- [ ] **Environment variables** configuradas

## 📋 Validação Final

### ✅ 35. Funcionalidades Core
- [ ] **User registration** funciona
- [ ] **User login** funciona
- [ ] **JWT token generation** funciona
- [ ] **Protected endpoints** funcionam
- [ ] **Token validation** funciona

### ✅ 36. CRUD Operations
- [ ] **Create** operations funcionam
- [ ] **Read** operations funcionam
- [ ] **Update** operations funcionam
- [ ] **Delete** operations funcionam
- [ ] **Validation** funciona corretamente

### ✅ 37. Error Handling
- [ ] **Global exception handling** implementado
- [ ] **Validation errors** retornados corretamente
- [ ] **HTTP status codes** corretos
- [ ] **Error responses** padronizados

### ✅ 38. Code Quality
- [ ] **Naming conventions** seguidas
- [ ] **SOLID principles** aplicados
- [ ] **Clean Architecture** respeitada
- [ ] **Dependency Injection** configurada corretamente

## 🎯 Scripts de Verificação

### Comandos para Validar a Implementação:
```bash
# 1. Compilar solução
dotnet build Solution/[SeuProjeto].sln

# 2. Executar testes
dotnet test Src/[SeuProjeto].Tests/

# 3. Executar API
dotnet run --project Src/[SeuProjeto].API

# 4. Verificar Swagger
# Acesse: https://localhost:7001/swagger

# 5. Testar autenticação
curl -X POST "https://localhost:7001/Authentication/login" \
  -H "Content-Type: application/json" \
  -d '{"userName": "admin", "password": "password123"}'
```

---

## 🚨 Problemas Comuns e Soluções

### ❌ "Foundation.Base não encontrado"
**Solução**: Verifique se o repositório Foundation foi clonado no diretório pai correto.

### ❌ "Connection string inválida"
**Solução**: Verifique se o MySQL está rodando e a string de conexão está correta.

### ❌ "JWT Secret Key muito curta"
**Solução**: Use uma chave com pelo menos 32 caracteres no appsettings.json.

### ❌ "Swagger não carrega"
**Solução**: Verifique se o XML documentation está habilitado e o arquivo está sendo gerado.

### ❌ "Migrations falham"
**Solução**: Certifique-se de estar no diretório do projeto API ao executar comandos EF.

---

## ✅ Conclusão

Quando todos os itens estiverem marcados, você terá um projeto completamente funcional seguindo todos os padrões arquiteturais estabelecidos. Seu projeto será:

- 🏛️ **Bem arquitetado** (Clean Architecture + DDD)
- 🔐 **Seguro** (JWT + Argon2 + Validações)
- 📊 **Testável** (Testes unitários e de integração)
- 📚 **Documentado** (Swagger + Comentários)
- 🔧 **Manutenível** (SOLID + Dependency Injection)
- 🚀 **Escalável** (Padrões estabelecidos)

Use este checklist sempre que criar um novo projeto!