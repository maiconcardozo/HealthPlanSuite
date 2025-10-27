"""
Módulo de configurações do HealthPlan.AI.Assistant.

Este módulo carrega e gerencia todas as configurações da aplicação
a partir de variáveis de ambiente.
"""

import os
from typing import Literal
from dotenv import load_dotenv

# Carregar variáveis de ambiente
load_dotenv()


class Settings:
    """Classe de configurações da aplicação."""

    # API Configuration
    API_BASE_URL: str = os.getenv("API_BASE_URL", "http://localhost:5000/api")
    API_TIMEOUT: int = int(os.getenv("API_TIMEOUT", "30"))

    # LLM Provider
    LLM_PROVIDER: Literal["ollama", "openai"] = os.getenv(
        "LLM_PROVIDER", "ollama"
    )  # type: ignore

    # Ollama Configuration
    OLLAMA_BASE_URL: str = os.getenv("OLLAMA_BASE_URL", "http://localhost:11434")
    OLLAMA_MODEL: str = os.getenv("OLLAMA_MODEL", "llama2")

    # OpenAI Configuration
    OPENAI_API_KEY: str = os.getenv("OPENAI_API_KEY", "")
    OPENAI_MODEL: str = os.getenv("OPENAI_MODEL", "gpt-3.5-turbo")
    OPENAI_TEMPERATURE: float = float(os.getenv("OPENAI_TEMPERATURE", "0.7"))

    # Logging
    LOG_LEVEL: str = os.getenv("LOG_LEVEL", "INFO")

    # Agent Configuration
    MAX_ITERATIONS: int = int(os.getenv("MAX_ITERATIONS", "10"))
    AGENT_VERBOSE: bool = os.getenv("AGENT_VERBOSE", "true").lower() == "true"

    @classmethod
    def validate(cls) -> None:
        """
        Valida as configurações necessárias.

        Raises:
            ValueError: Se alguma configuração obrigatória estiver faltando.
        """
        if cls.LLM_PROVIDER == "openai" and not cls.OPENAI_API_KEY:
            raise ValueError(
                "OPENAI_API_KEY é obrigatória quando LLM_PROVIDER='openai'"
            )

        if cls.LLM_PROVIDER not in ["ollama", "openai"]:
            raise ValueError(
                f"LLM_PROVIDER inválido: {cls.LLM_PROVIDER}. Use 'ollama' ou 'openai'"
            )

    @classmethod
    def display(cls) -> str:
        """
        Retorna uma representação string das configurações (sem dados sensíveis).

        Returns:
            str: String formatada com as configurações.
        """
        return f"""
🔧 Configurações do HealthPlan.AI.Assistant
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
API Base URL: {cls.API_BASE_URL}
API Timeout: {cls.API_TIMEOUT}s
LLM Provider: {cls.LLM_PROVIDER}
{"Ollama URL: " + cls.OLLAMA_BASE_URL if cls.LLM_PROVIDER == "ollama" else ""}
{"Ollama Model: " + cls.OLLAMA_MODEL if cls.LLM_PROVIDER == "ollama" else ""}
{"OpenAI Model: " + cls.OPENAI_MODEL if cls.LLM_PROVIDER == "openai" else ""}
Log Level: {cls.LOG_LEVEL}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        """.strip()


# Instância global de configurações
settings = Settings()
