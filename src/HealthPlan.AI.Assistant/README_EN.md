# 🤖 HealthPlan.AI.Assistant

Intelligent assistant based on LangChain to manage CRUD operations of HealthPlan Suite through natural language.

## 📋 Overview

HealthPlan.AI.Assistant is a conversational agent that allows you to interact with the HealthPlan Suite API using natural language in English. It uses LangChain to process commands and execute CRUD operations on companies, health plans, beneficiaries, quotes, coverages, age ranges, and accommodations.

## ✨ Features

- 🗣️ **Conversational interface**: Interact with the API using natural language
- 🔧 **Complete CRUD**: Support for all system entities
- 🧠 **Flexible LLM**: Support for Ollama (local) and OpenAI
- 💾 **Conversation memory**: Maintains context during the session
- 🎨 **Colored CLI**: User-friendly command line interface
- ✅ **Validation**: Validates data before sending to the API
- 🔍 **Smart formatting**: Formatted and readable responses

## 🏗️ Architecture

```
HealthPlan.AI.Assistant/
├── config/              # Settings and prompts
│   ├── settings.py      # Environment settings
│   ├── prompts.py       # Prompt templates
│   └── __init__.py
├── utils/               # Utilities
│   ├── api_client.py    # HTTP client for API
│   ├── formatters.py    # Response formatters
│   ├── validators.py    # Data validators
│   └── __init__.py
├── tools/               # LangChain tools
│   ├── company_tools.py
│   ├── healthplan_tools.py
│   ├── beneficiary_tools.py
│   ├── quote_tools.py
│   ├── coverage_tools.py
│   ├── agerange_tools.py
│   ├── accommodation_tools.py
│   └── __init__.py
├── agents/              # LangChain agents
│   ├── healthplan_agent.py
│   └── __init__.py
├── interfaces/          # User interfaces
│   ├── cli.py
│   └── __init__.py
├── tests/               # Tests
│   ├── conftest.py
│   ├── test_api_client.py
│   └── test_tools.py
├── main.py              # Entry point
├── requirements.txt
├── requirements-dev.txt
├── .env.example
├── .gitignore
└── README.md
```

## 🚀 Installation

### Prerequisites

- Python 3.11 or higher
- HealthPlan Suite API running (default: http://localhost:5000)
- Ollama (for local LLM) or OpenAI API key

### Ollama Installation (Optional)

If you want to use local LLM:

```bash
# Linux/Mac
curl -fsSL https://ollama.com/install.sh | sh

# Start Ollama
ollama serve

# Download model (in another terminal)
ollama pull llama2
```

### Project Setup

```bash
# 1. Navigate to directory
cd Src/HealthPlan.AI.Assistant

# 2. Create virtual environment
python -m venv venv

# 3. Activate virtual environment
# Linux/Mac:
source venv/bin/activate
# Windows:
venv\Scripts\activate

# 4. Install dependencies
pip install -r requirements.txt

# 5. Copy configuration file
cp .env.example .env

# 6. Edit .env with your settings
nano .env  # or your preferred editor
```

### .env Configuration

```bash
# API Configuration
API_BASE_URL=http://localhost:5000/api
API_TIMEOUT=30

# For Ollama (local LLM)
LLM_PROVIDER=ollama
OLLAMA_BASE_URL=http://localhost:11434
OLLAMA_MODEL=llama2

# OR for OpenAI
LLM_PROVIDER=openai
OPENAI_API_KEY=sk-...
OPENAI_MODEL=gpt-3.5-turbo
```

## 💻 Usage

### Start the Assistant

```bash
python main.py
```

### Command Examples

```
🤖 HealthPlan AI Assistant

You: list all companies
Assistant: Found 3 registered companies:
1. Bradesco Saúde (ID: 1)
2. Amil (ID: 2)
3. SulAmérica (ID: 3)

You: create a new company called Unimed
Assistant: ✅ Company "Unimed" created successfully! ID: 4

You: show health plans for company 1
Assistant: Plans for company Bradesco Saúde:
- Executive Plan (ID: 10) - $850.00
- Family Plan (ID: 11) - $1200.00

You: exit
Assistant: Goodbye! 👋
```

### Special Commands

- `help`: Shows available commands
- `clear`: Clears conversation history
- `reset`: Restarts the agent
- `exit`: Exits the program

## 🧪 Tests

```bash
# Install development dependencies
pip install -r requirements-dev.txt

# Run all tests
pytest

# With coverage
pytest --cov=. --cov-report=html

# Specific tests
pytest tests/test_api_client.py
pytest tests/test_tools.py
```

## 🔧 Development

### Code Structure

- **Type hints**: All functions include type hints
- **Docstrings**: Documentation in English for all classes and functions
- **Error handling**: Robust error handling
- **Logging**: Configurable logging system
- **Testing**: Tests with mocks to avoid real requests

### Code Quality

```bash
# Formatting
black .

# Linting
flake8 .

# Type checking
mypy .

# Import sorting
isort .
```

## 📚 Supported Entities

The assistant supports CRUD operations for the following entities:

### Main Entities
- **Companies**
- **HealthPlans**
- **Beneficiaries**
- **Quotes**
- **Coverages**
- **AgeRanges**
- **Accommodations**

### Plan Configuration Entities
- **PlanCoverages** - Links plans with coverages
- **AcceptanceRules** - Defines rules for accepting beneficiaries
- **AdhesionFees** - Fees charged on plan adhesion
- **PromotionalDiscounts** - Temporary discounts
- **ProcedureCoparticipations** - Coparticipation values per procedure
- **PlanPriceRanges** - Prices by age range and contract type

**Total: 13 entities with 65 LangChain tools (5 CRUD operations × 13 entities)**

### API Endpoints

All endpoints follow REST pattern:

- `GET /{entity}` - List all
- `GET /{entity}/{id}` - Fetch by ID
- `POST /{entity}` - Create new
- `PUT /{entity}/{id}` - Update
- `DELETE /{entity}/{id}` - Delete

## 🔐 Security

- ❌ **Do not include** API keys in code
- ✅ Use `.env` file for sensitive settings
- ✅ The `.env` file is in `.gitignore`
- ✅ Use `.env.example` as template

## 🐛 Troubleshooting

### API is not responding

```bash
# Check if API is running
curl http://localhost:5000/health

# Check configuration in .env
cat .env | grep API_BASE_URL
```

### Ollama is not working

```bash
# Check if Ollama is running
curl http://localhost:11434/api/tags

# Start Ollama
ollama serve

# Check installed model
ollama list
```

### Import errors

```bash
# Reinstall dependencies
pip install -r requirements.txt --upgrade
```

## 📖 Additional Documentation

- [HealthPlan Suite API Documentation](../../docs/API.md)
- [LangChain Documentation](https://python.langchain.com/)
- [Ollama Documentation](https://ollama.com/docs)

## 🤝 Contributing

1. Follow existing code conventions
2. Add tests for new features
3. Keep documentation up to date
4. Use type hints and docstrings in English

## 📝 License

This project is part of HealthPlan Suite and is under the same MIT license.

## 🔗 Links

- [HealthPlan Suite - Main Repository](https://github.com/maiconcardozo/HealthPlanSuite)
- [Issue #43 - AI Assistant Implementation](https://github.com/maiconcardozo/HealthPlanSuite/issues/43)

---

Developed with ❤️ by [Maicon Cardozo](https://github.com/maiconcardozo)
