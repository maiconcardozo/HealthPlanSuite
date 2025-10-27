"""
Ferramentas LangChain para operações com taxas de adesão (AdhesionFees).
"""

from langchain.tools import tool
from utils.api_client import HealthPlanAPIClient
from utils.formatters import format_adhesionfee, format_adhesionfees, format_error, format_success
from utils.validators import validate_adhesionfee_data, validate_id


api_client = HealthPlanAPIClient()


@tool
def get_all_adhesionfees() -> str:
    """Busca todas as taxas de adesão cadastradas."""
    try:
        adhesionfees = api_client.get_adhesionfees()
        return format_adhesionfees(adhesionfees)
    except Exception as e:
        return format_error(e)


@tool
def get_adhesionfee_by_id(adhesionfee_id: int) -> str:
    """
    Busca uma taxa de adesão específica por ID.

    Args:
        adhesionfee_id: ID da taxa de adesão.
    """
    try:
        valid, error_msg = validate_id(adhesionfee_id)
        if not valid:
            return format_error(Exception(error_msg))

        adhesionfee = api_client.get_adhesionfee(adhesionfee_id)
        return format_adhesionfee(adhesionfee)
    except Exception as e:
        return format_error(e)


@tool
def create_adhesionfee(
    health_plan_id: int, value: float, validity_start: str, validity_end: str
) -> str:
    """
    Cria uma nova taxa de adesão.

    Args:
        health_plan_id: ID do plano de saúde (obrigatório).
        value: Valor da taxa de adesão (obrigatório).
        validity_start: Data de início da validade no formato ISO (obrigatório). Ex: "2024-01-01T00:00:00".
        validity_end: Data de fim da validade no formato ISO (obrigatório). Ex: "2024-12-31T23:59:59".
    """
    try:
        data = {
            "healthPlanId": health_plan_id,
            "value": value,
            "validityStart": validity_start,
            "validityEnd": validity_end,
            "createdBy": "AI Assistant",
        }

        valid, error_msg = validate_adhesionfee_data(data)
        if not valid:
            return format_error(Exception(error_msg))

        result = api_client.create_adhesionfee(data)
        return format_success(
            f"Taxa de adesão criada com sucesso! ID: {result.get('id', 'N/A')}"
        )
    except Exception as e:
        return format_error(e)


@tool
def update_adhesionfee(
    adhesionfee_id: int,
    value: float = None,
    validity_start: str = "",
    validity_end: str = "",
) -> str:
    """
    Atualiza uma taxa de adesão existente.

    Args:
        adhesionfee_id: ID da taxa de adesão a ser atualizada.
        value: Novo valor da taxa (opcional).
        validity_start: Nova data de início da validade (opcional).
        validity_end: Nova data de fim da validade (opcional).
    """
    try:
        valid, error_msg = validate_id(adhesionfee_id)
        if not valid:
            return format_error(Exception(error_msg))

        data = {}
        if value is not None:
            data["value"] = value
        if validity_start:
            data["validityStart"] = validity_start
        if validity_end:
            data["validityEnd"] = validity_end

        if not data:
            return format_error(Exception("Nenhum campo fornecido para atualização."))

        data["updatedBy"] = "AI Assistant"
        api_client.update_adhesionfee(adhesionfee_id, data)
        return format_success(f"Taxa de adesão ID {adhesionfee_id} atualizada com sucesso!")
    except Exception as e:
        return format_error(e)


@tool
def delete_adhesionfee(adhesionfee_id: int) -> str:
    """
    Deleta uma taxa de adesão.

    Args:
        adhesionfee_id: ID da taxa de adesão a ser deletada.
    """
    try:
        valid, error_msg = validate_id(adhesionfee_id)
        if not valid:
            return format_error(Exception(error_msg))

        api_client.delete_adhesionfee(adhesionfee_id)
        return format_success(f"Taxa de adesão ID {adhesionfee_id} deletada com sucesso!")
    except Exception as e:
        return format_error(e)
