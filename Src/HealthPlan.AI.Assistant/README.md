# 🤖 HealthPlan.AI.Assistant

Assistente inteligente baseado em LangChain para gerenciar operações CRUD do HealthPlan Suite através de linguagem natural.

## 📋 Visão Geral

O HealthPlan.AI.Assistant é um agente conversacional que permite interagir com a API do HealthPlan Suite usando linguagem natural em português. Ele utiliza LangChain para processar comandos e executar operações CRUD em empresas, planos de saúde, beneficiários, cotações, coberturas, faixas etárias e acomodações.

## ✨ Funcionalidades

- 🗣️ **Interface conversacional**: Interaja com a API usando linguagem natural
- 🔧 **CRUD completo**: Suporte para todas as entidades do sistema
- 🧠 **LLM flexível**: Suporte para Ollama (local) e OpenAI
- 💾 **Memória de conversa**: Mantém contexto durante a sessão
- 🎨 **CLI colorida**: Interface de linha de comando amigável
- ✅ **Validação**: Valida dados antes de enviar para a API
- 🔍 **Formatação inteligente**: Respostas formatadas e legíveis

## 🏗️ Arquitetura

```
HealthPlan.AI.Assistant/
├── config/              # Configurações e prompts
│   ├── settings.py      # Configurações do ambiente
│   ├── prompts.py       # Templates de prompts
│   └── __init__.py
├── utils/               # Utilitários
│   ├── api_client.py    # Cliente HTTP para API
│   ├── formatters.py    # Formatadores de resposta
│   ├── validators.py    # Validadores de dados
│   └── __init__.py
├── tools/               # Ferramentas LangChain
│   ├── company_tools.py
│   ├── healthplan_tools.py
│   ├── beneficiary_tools.py
│   ├── quote_tools.py
│   ├── coverage_tools.py
│   ├── agerange_tools.py
│   ├── accommodation_tools.py
│   └── __init__.py
├── agents/              # Agentes LangChain
│   ├── healthplan_agent.py
│   └── __init__.py
├── interfaces/          # Interfaces de usuário
│   ├── cli.py
│   └── __init__.py
├── tests/               # Testes
│   ├── conftest.py
│   ├── test_api_client.py
│   └── test_tools.py
├── main.py              # Ponto de entrada
├── requirements.txt
├── requirements-dev.txt
├── .env.example
├── .gitignore
└── README.md
```

## 🚀 Instalação

### Pré-requisitos

- Python 3.11 ou superior
- API HealthPlan Suite rodando (padrão: http://localhost:5000)
- Ollama (para LLM local) ou chave OpenAI API

### Instalação do Ollama (Opcional)

Se você deseja usar LLM local:

```bash
# Linux/Mac
curl -fsSL https://ollama.com/install.sh | sh

# Iniciar Ollama
ollama serve

# Baixar modelo (em outro terminal)
ollama pull llama2
```

### Configuração do Projeto

```bash
# 1. Navegar até o diretório
cd Src/HealthPlan.AI.Assistant

# 2. Criar ambiente virtual
python -m venv venv

# 3. Ativar ambiente virtual
# Linux/Mac:
source venv/bin/activate
# Windows:
venv\Scripts\activate

# 4. Instalar dependências
pip install -r requirements.txt

# 5. Copiar arquivo de configuração
cp .env.example .env

# 6. Editar .env com suas configurações
nano .env  # ou seu editor preferido
```

### Configuração do .env

```bash
# API Configuration
API_BASE_URL=http://localhost:5000/api
API_TIMEOUT=30

# Para Ollama (LLM local)
LLM_PROVIDER=ollama
OLLAMA_BASE_URL=http://localhost:11434
OLLAMA_MODEL=llama2

# OU para OpenAI
LLM_PROVIDER=openai
OPENAI_API_KEY=sk-...
OPENAI_MODEL=gpt-3.5-turbo
```

## 💻 Uso

### Iniciar o Assistente

```bash
python main.py
```

### Exemplos de Comandos

```
🤖 HealthPlan AI Assistant

Você: liste todas as empresas
Assistente: Encontrei 3 empresas cadastradas:
1. Bradesco Saúde (ID: 1)
2. Amil (ID: 2)
3. SulAmérica (ID: 3)

Você: crie uma nova empresa chamada Unimed
Assistente: ✅ Empresa "Unimed" criada com sucesso! ID: 4

Você: mostre os planos de saúde da empresa 1
Assistente: Planos da empresa Bradesco Saúde:
- Plano Executivo (ID: 10) - R$ 850,00
- Plano Familiar (ID: 11) - R$ 1200,00

Você: sair
Assistente: Até logo! 👋
```

### Comandos Especiais

- `ajuda` ou `help`: Mostra comandos disponíveis
- `limpar` ou `clear`: Limpa o histórico da conversa
- `resetar` ou `reset`: Reinicia o agente
- `sair` ou `exit`: Sai do programa

## 🧪 Testes

```bash
# Instalar dependências de desenvolvimento
pip install -r requirements-dev.txt

# Executar todos os testes
pytest

# Com cobertura
pytest --cov=. --cov-report=html

# Testes específicos
pytest tests/test_api_client.py
pytest tests/test_tools.py
```

## 🔧 Desenvolvimento

### Estrutura de Código

- **Type hints**: Todas as funções incluem type hints
- **Docstrings**: Documentação em PT-BR para todas as classes e funções
- **Error handling**: Tratamento robusto de erros
- **Logging**: Sistema de logs configurável
- **Testing**: Testes com mocks para evitar requisições reais

### Code Quality

```bash
# Formatação
black .

# Linting
flake8 .

# Type checking
mypy .

# Import sorting
isort .
```

## 📚 Entidades Suportadas

O assistente suporta operações CRUD para as seguintes entidades:

### Entidades Principais
- **Companies** (Empresas)
- **HealthPlans** (Planos de Saúde)
- **Beneficiaries** (Beneficiários)
- **Quotes** (Cotações)
- **Coverages** (Coberturas)
- **AgeRanges** (Faixas Etárias)
- **Accommodations** (Acomodações)

### Entidades de Configuração de Planos
- **PlanCoverages** (Coberturas de Planos) - Relaciona planos com coberturas
- **AcceptanceRules** (Regras de Aceitação) - Define regras para aceitar beneficiários
- **AdhesionFees** (Taxas de Adesão) - Taxas cobradas na adesão ao plano
- **PromotionalDiscounts** (Descontos Promocionais) - Descontos temporários
- **ProcedureCoparticipations** (Coparticipações) - Valores de coparticipação por procedimento
- **PlanPriceRanges** (Faixas de Preços) - Preços por faixa etária e tipo de contrato

**Total: 13 entidades com 65 ferramentas LangChain (5 operações CRUD × 13 entidades)**

### Endpoints da API

Todos os endpoints seguem o padrão REST:

- `GET /{entity}` - Listar todos
- `GET /{entity}/{id}` - Buscar por ID
- `POST /{entity}` - Criar novo
- `PUT /{entity}/{id}` - Atualizar
- `DELETE /{entity}/{id}` - Deletar

## 🔐 Segurança

- ❌ **Não inclua** chaves de API no código
- ✅ Use arquivo `.env` para configurações sensíveis
- ✅ O arquivo `.env` está no `.gitignore`
- ✅ Use `.env.example` como template

## 🐛 Troubleshooting

### API não está respondendo

```bash
# Verificar se a API está rodando
curl http://localhost:5000/health

# Verificar configuração no .env
cat .env | grep API_BASE_URL
```

### Ollama não está funcionando

```bash
# Verificar se Ollama está rodando
curl http://localhost:11434/api/tags

# Iniciar Ollama
ollama serve

# Verificar modelo instalado
ollama list
```

### Erros de importação

```bash
# Reinstalar dependências
pip install -r requirements.txt --upgrade
```

## 📖 Documentação Adicional

- [Documentação da API HealthPlan Suite](../../docs/API.md)
- [LangChain Documentation](https://python.langchain.com/)
- [Ollama Documentation](https://ollama.com/docs)

## 🤝 Contribuindo

1. Siga as convenções de código existentes
2. Adicione testes para novas funcionalidades
3. Mantenha a documentação atualizada
4. Use type hints e docstrings em PT-BR

## 📝 Licença

Este projeto faz parte do HealthPlan Suite e está sob a mesma licença MIT.

## 🔗 Links

- [HealthPlan Suite - Repositório Principal](https://github.com/maiconcardozo/HealthPlanSuite)
- [Issue #43 - AI Assistant Implementation](https://github.com/maiconcardozo/HealthPlanSuite/issues/43)

---

Desenvolvido com ❤️ por [Maicon Cardozo](https://github.com/maiconcardozo)
