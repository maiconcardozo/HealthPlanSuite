"""
Ferramentas LangChain para operações com coberturas de planos (PlanCoverages).
"""

from langchain.tools import tool
from utils.api_client import HealthPlanAPIClient
from utils.formatters import format_plancoverage, format_plancoverages, format_error, format_success
from utils.validators import validate_plancoverage_data, validate_id


api_client = HealthPlanAPIClient()


@tool
def get_all_plancoverages() -> str:
    """Busca todas as coberturas de planos cadastradas."""
    try:
        plancoverages = api_client.get_plancoverages()
        return format_plancoverages(plancoverages)
    except Exception as e:
        return format_error(e)


@tool
def get_plancoverage_by_id(plancoverage_id: int) -> str:
    """
    Busca uma cobertura de plano específica por ID.

    Args:
        plancoverage_id: ID da cobertura de plano.
    """
    try:
        valid, error_msg = validate_id(plancoverage_id)
        if not valid:
            return format_error(Exception(error_msg))

        plancoverage = api_client.get_plancoverage(plancoverage_id)
        return format_plancoverage(plancoverage)
    except Exception as e:
        return format_error(e)


@tool
def create_plancoverage(
    health_plan_id: int, coverage_id: int, premium_value: float = 0.0, is_included: bool = True
) -> str:
    """
    Cria uma nova cobertura de plano.

    Args:
        health_plan_id: ID do plano de saúde (obrigatório).
        coverage_id: ID da cobertura (obrigatório).
        premium_value: Valor premium da cobertura (opcional, padrão: 0.0).
        is_included: Se a cobertura está incluída (opcional, padrão: True).
    """
    try:
        data = {
            "healthPlanId": health_plan_id,
            "coverageId": coverage_id,
            "premiumValue": premium_value,
            "isIncluded": is_included,
            "createdBy": "AI Assistant",
        }

        valid, error_msg = validate_plancoverage_data(data)
        if not valid:
            return format_error(Exception(error_msg))

        result = api_client.create_plancoverage(data)
        return format_success(
            f"Cobertura de plano criada com sucesso! ID: {result.get('id', 'N/A')}"
        )
    except Exception as e:
        return format_error(e)


@tool
def update_plancoverage(
    plancoverage_id: int,
    premium_value: float = None,
    is_included: bool = None,
) -> str:
    """
    Atualiza uma cobertura de plano existente.

    Args:
        plancoverage_id: ID da cobertura de plano a ser atualizada.
        premium_value: Novo valor premium (opcional).
        is_included: Nova flag de inclusão (opcional).
    """
    try:
        valid, error_msg = validate_id(plancoverage_id)
        if not valid:
            return format_error(Exception(error_msg))

        data = {}
        if premium_value is not None:
            data["premiumValue"] = premium_value
        if is_included is not None:
            data["isIncluded"] = is_included

        if not data:
            return format_error(Exception("Nenhum campo fornecido para atualização."))

        data["updatedBy"] = "AI Assistant"
        api_client.update_plancoverage(plancoverage_id, data)
        return format_success(f"Cobertura de plano ID {plancoverage_id} atualizada com sucesso!")
    except Exception as e:
        return format_error(e)


@tool
def delete_plancoverage(plancoverage_id: int) -> str:
    """
    Deleta uma cobertura de plano.

    Args:
        plancoverage_id: ID da cobertura de plano a ser deletada.
    """
    try:
        valid, error_msg = validate_id(plancoverage_id)
        if not valid:
            return format_error(Exception(error_msg))

        api_client.delete_plancoverage(plancoverage_id)
        return format_success(f"Cobertura de plano ID {plancoverage_id} deletada com sucesso!")
    except Exception as e:
        return format_error(e)
