"""
Configuração do pytest para os testes do HealthPlan.AI.Assistant.

Este módulo define fixtures e configurações compartilhadas entre os testes.
"""

import pytest
from unittest.mock import Mock, MagicMock
from utils.api_client import HealthPlanAPIClient


@pytest.fixture
def mock_api_client():
    """
    Fixture que retorna um cliente API mockado.

    Returns:
        Mock do HealthPlanAPIClient.
    """
    client = Mock(spec=HealthPlanAPIClient)

    # Mock de métodos comuns
    client.health_check.return_value = True
    client.get_companies.return_value = [
        {"id": 1, "name": "Empresa Teste", "cnpj": "12345678901234"}
    ]
    client.get_company.return_value = {
        "id": 1,
        "name": "Empresa Teste",
        "cnpj": "12345678901234",
    }
    client.create_company.return_value = {"id": 1, "name": "Nova Empresa"}
    client.update_company.return_value = {"id": 1, "name": "Empresa Atualizada"}
    client.delete_company.return_value = {}

    return client


@pytest.fixture
def mock_response_data():
    """
    Fixture que retorna dados de resposta mockados.

    Returns:
        Dict com dados de exemplo.
    """
    return {
        "companies": [
            {"id": 1, "name": "Bradesco Saúde", "cnpj": "11111111111111"},
            {"id": 2, "name": "Amil", "cnpj": "22222222222222"},
        ],
        "healthplans": [
            {
                "id": 1,
                "name": "Plano Executivo",
                "companyId": 1,
                "basePrice": 850.0,
            },
            {"id": 2, "name": "Plano Familiar", "companyId": 1, "basePrice": 1200.0},
        ],
        "beneficiaries": [
            {
                "id": 1,
                "name": "João Silva",
                "cpf": "12345678901",
                "age": 35,
                "birthDate": "1989-01-15",
            }
        ],
    }


@pytest.fixture
def sample_company():
    """Fixture com dados de uma empresa de exemplo."""
    return {
        "id": 1,
        "name": "Empresa Teste",
        "cnpj": "12345678901234",
        "phone": "11999999999",
        "email": "contato@empresa.com",
    }


@pytest.fixture
def sample_healthplan():
    """Fixture com dados de um plano de saúde de exemplo."""
    return {
        "id": 1,
        "name": "Plano Teste",
        "companyId": 1,
        "basePrice": 500.0,
        "description": "Plano de teste",
    }


@pytest.fixture
def sample_beneficiary():
    """Fixture com dados de um beneficiário de exemplo."""
    return {
        "id": 1,
        "name": "João Silva",
        "cpf": "12345678901",
        "birthDate": "1990-01-01",
        "age": 34,
        "phone": "11999999999",
        "email": "joao@email.com",
    }
