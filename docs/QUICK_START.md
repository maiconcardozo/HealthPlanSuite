# 🚀 Quick Start Guide - Authentication Service

Este guia fornece instruções passo a passo para configurar e usar o serviço de autenticação em diferentes cenários.

## 📋 Pré-requisitos

Antes de começar, certifique-se de ter os seguintes componentes instalados:

### Obrigatórios
- **.NET 9.0 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/9.0)
- **MySQL 8.0+** - [Download](https://dev.mysql.com/downloads/mysql/)
- **Git** - [Download](https://git-scm.com/)

### Recomendados
- **Visual Studio 2022** com workload .NET - [Download](https://visualstudio.microsoft.com/)
- **Visual Studio Code** com extensão C# Dev Kit - [Download](https://code.visualstudio.com/)
- **MySQL Workbench** para gerenciamento do banco - [Download](https://dev.mysql.com/downloads/workbench/)

## 🏃‍♂️ Configuração Rápida (5 minutos)

### 1. Clone e Construa o Projeto

```bash
# Clone o repositório
git clone https://github.com/maiconcardozo/CleanTemplateRepository.git
cd CleanTemplateRepository

# Restaure as dependências
dotnet restore Solution/CleanTemplate.sln

# Construa o projeto
dotnet build Solution/CleanTemplate.sln --configuration Debug
```

### 2. Configure o Banco de Dados

#### Opção A: MySQL Local
```bash
# Inicie o MySQL e crie um banco
mysql -u root -p
CREATE DATABASE CleanTemplateDB;
CREATE USER 'cleanuser'@'localhost' IDENTIFIED BY 'password123';
GRANT ALL PRIVILEGES ON CleanTemplateDB.* TO 'cleanuser'@'localhost';
FLUSH PRIVILEGES;
exit;
```

#### Opção B: Docker MySQL (Mais Rápido)
```bash
# Execute MySQL em container Docker
docker run --name mysql-clean \
  -e MYSQL_ROOT_PASSWORD=rootpass \
  -e MYSQL_DATABASE=CleanTemplateDB \
  -e MYSQL_USER=cleanuser \
  -e MYSQL_PASSWORD=password123 \
  -p 3306:3306 \
  -d mysql:8.0
```

### 3. Configure a Connection String

Edite `Src/CleanTemplate.API/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=CleanTemplateDB;Uid=cleanuser;Pwd=password123;SslMode=none;"
  },
  "JwtSettings": {
    "Issuer": "CleanTemplate",
    "Audience": "CleanTemplateClients",
    "SecretKey": "super-secret-jwt-key-minimum-32-characters-long",
    "ExpirationMinutes": 60
  }
}
```

### 4. Execute as Migrações do Banco

```bash
cd Src/CleanTemplate.API
dotnet ef database update --context ApiContextDevelopment
```

### 5. Execute a Aplicação

```bash
# Execute em modo de desenvolvimento
dotnet run --configuration Debug

# A API estará disponível em: https://localhost:7001
# Documentação Swagger: https://localhost:7001
```

## 🔐 Primeiro Uso - Testando a API

### 1. Acesse a Documentação Swagger

Abra seu navegador e vá para: **https://localhost:7001**

Você verá a API documentada com endpoints para:
- **CleanEntity Management** - CRUD operations example
- **Authentication** - Login e geração de tokens (if implemented)

### 2. Teste os Endpoints

```bash
# Health check
curl -X GET "https://localhost:7001/health"

# Get all CleanEntities (example)
curl -X GET "https://localhost:7001/api/CleanEntity"

# Create a new CleanEntity
curl -X POST "https://localhost:7001/api/CleanEntity" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Test Entity",
    "description": "Test Description"
  }'
```

## 🧪 Validando a Configuração

### Execute os Testes

```bash
# Execute todos os testes
dotnet test Solution/CleanTemplate.sln

# Execute com scripts de conveniência
scripts/build.sh verify    # Linux/Mac - build and test
scripts/build.bat verify   # Windows - build and test
```

## 🔧 Integração com Frontend

### JavaScript/React Example

```javascript
class CleanTemplateApiService {
  constructor() {
    this.baseURL = 'https://localhost:7001';
  }

  async getHealthStatus() {
    const response = await fetch(`${this.baseURL}/health`);
    return response.json();
  }

  async getCleanEntities() {
    const response = await fetch(`${this.baseURL}/api/CleanEntity`);
    return response.json();
  }

  async createCleanEntity(entity) {
    const response = await fetch(`${this.baseURL}/api/CleanEntity`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(entity)
    });
    return response.json();
  }
}
```

## 📚 Próximos Passos

1. **Leia a documentação completa**: [docs/DEVELOPMENT.md](DEVELOPMENT.md)
2. **Configure para produção**: [docs/DEPLOYMENT.md](DEPLOYMENT.md)
3. **Entenda a arquitetura**: [docs/ARCHITECTURE.md](ARCHITECTURE.md)
4. **Veja mais exemplos**: [docs/EXAMPLES.md](EXAMPLES.md)
5. **Configure segurança**: [docs/SECURITY.md](SECURITY.md)

## 🆘 Problemas Comuns

### ❌ Erro de Conexão com Banco
```
Unable to connect to any of the specified MySQL hosts
```
**Solução**: Verifique se o MySQL está rodando e a connection string está correta.

### ❌ Erro de Build
```
NETSDK1045: The current .NET SDK does not support targeting .NET 9.0
```
**Solução**: Instale o .NET 9.0 SDK de https://dotnet.microsoft.com/download/dotnet/9.0

## 💡 Dicas de Desenvolvimento

- Use `dotnet watch run` para hot reload durante desenvolvimento
- Configure variáveis de ambiente para diferentes ambientes
- Use o Swagger UI para testar endpoints interativamente
- Monitore logs com `dotnet run --verbosity detailed`
- Use ferramentas como Postman ou Insomnia para testes de API

---

🎉 **Parabéns!** Você configurou com sucesso o CleanTemplate Service. Para dúvidas, consulte a [documentação completa](../README.md).