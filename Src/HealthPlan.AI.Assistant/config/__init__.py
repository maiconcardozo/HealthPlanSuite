"""Módulo de configurações do HealthPlan.AI.Assistant."""

from .settings import settings, Settings
from .prompts import (
    SYSTEM_PROMPT,
    WELCOME_MESSAGE,
    HELP_MESSAGE,
    ERROR_MESSAGES,
    SUCCESS_MESSAGES,
)

__all__ = [
    "settings",
    "Settings",
    "SYSTEM_PROMPT",
    "WELCOME_MESSAGE",
    "HELP_MESSAGE",
    "ERROR_MESSAGES",
    "SUCCESS_MESSAGES",
]
