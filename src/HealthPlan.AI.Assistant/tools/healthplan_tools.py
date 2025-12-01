"""
Ferramentas LangChain para operações com planos de saúde (HealthPlans).

Este módulo define as ferramentas que permitem ao agente LangChain
realizar operações CRUD em planos de saúde.
"""

from langchain.tools import tool
from utils.api_client import HealthPlanAPIClient
from utils.formatters import format_healthplan, format_healthplans, format_error, format_success
from utils.validators import validate_healthplan_data, validate_id


api_client = HealthPlanAPIClient()


@tool
def get_all_healthplans() -> str:
    """Busca todos os planos de saúde cadastrados."""
    try:
        healthplans = api_client.get_healthplans()
        return format_healthplans(healthplans)
    except Exception as e:
        return format_error(e)


@tool
def get_healthplan_by_id(healthplan_id: int) -> str:
    """
    Busca um plano de saúde específico por ID.

    Args:
        healthplan_id: ID do plano de saúde.
    """
    try:
        valid, error_msg = validate_id(healthplan_id)
        if not valid:
            return format_error(Exception(error_msg))

        healthplan = api_client.get_healthplan(healthplan_id)
        return format_healthplan(healthplan)
    except Exception as e:
        return format_error(e)


@tool
def create_healthplan(
    name: str, company_id: int, base_price: float = 0.0, description: str = ""
) -> str:
    """
    Cria um novo plano de saúde.

    Args:
        name: Nome do plano (obrigatório).
        company_id: ID da empresa (obrigatório).
        base_price: Preço base do plano (opcional).
        description: Descrição do plano (opcional).
    """
    try:
        data = {
            "name": name,
            "companyId": company_id,
            "basePrice": base_price,
            "description": description,
        }

        valid, error_msg = validate_healthplan_data(data)
        if not valid:
            return format_error(Exception(error_msg))

        result = api_client.create_healthplan(data)
        return format_success(
            f"Plano de saúde '{name}' criado com sucesso! ID: {result.get('id', 'N/A')}"
        )
    except Exception as e:
        return format_error(e)


@tool
def update_healthplan(
    healthplan_id: int,
    name: str = "",
    base_price: float = None,
    description: str = "",
) -> str:
    """
    Atualiza um plano de saúde existente.

    Args:
        healthplan_id: ID do plano a ser atualizado.
        name: Novo nome (opcional).
        base_price: Novo preço base (opcional).
        description: Nova descrição (opcional).
    """
    try:
        valid, error_msg = validate_id(healthplan_id)
        if not valid:
            return format_error(Exception(error_msg))

        data = {}
        if name:
            data["name"] = name
        if base_price is not None:
            data["basePrice"] = base_price
        if description:
            data["description"] = description

        if not data:
            return format_error(Exception("Nenhum campo fornecido para atualização."))

        api_client.update_healthplan(healthplan_id, data)
        return format_success(f"Plano de saúde ID {healthplan_id} atualizado com sucesso!")
    except Exception as e:
        return format_error(e)


@tool
def delete_healthplan(healthplan_id: int) -> str:
    """
    Deleta um plano de saúde.

    Args:
        healthplan_id: ID do plano a ser deletado.
    """
    try:
        valid, error_msg = validate_id(healthplan_id)
        if not valid:
            return format_error(Exception(error_msg))

        api_client.delete_healthplan(healthplan_id)
        return format_success(f"Plano de saúde ID {healthplan_id} deletado com sucesso!")
    except Exception as e:
        return format_error(e)
