"""
Ferramentas LangChain para operações com coparticipações de procedimentos (ProcedureCoparticipations).
"""

from langchain.tools import tool
from utils.api_client import HealthPlanAPIClient
from utils.formatters import format_procedurecoparticipation, format_procedurecoparticipations, format_error, format_success
from utils.validators import validate_procedurecoparticipation_data, validate_id


api_client = HealthPlanAPIClient()


@tool
def get_all_procedurecoparticipations() -> str:
    """Busca todas as coparticipações de procedimentos cadastradas."""
    try:
        procedurecoparticipations = api_client.get_procedurecoparticipations()
        return format_procedurecoparticipations(procedurecoparticipations)
    except Exception as e:
        return format_error(e)


@tool
def get_procedurecoparticipation_by_id(procedurecoparticipation_id: int) -> str:
    """
    Busca uma coparticipação de procedimento específica por ID.

    Args:
        procedurecoparticipation_id: ID da coparticipação de procedimento.
    """
    try:
        valid, error_msg = validate_id(procedurecoparticipation_id)
        if not valid:
            return format_error(Exception(error_msg))

        procedurecoparticipation = api_client.get_procedurecoparticipation(procedurecoparticipation_id)
        return format_procedurecoparticipation(procedurecoparticipation)
    except Exception as e:
        return format_error(e)


@tool
def create_procedurecoparticipation(
    health_plan_id: int,
    coparticipation_type: str,
    procedure: str,
    value: float,
    limit: float = None,
) -> str:
    """
    Cria uma nova coparticipação de procedimento.

    Args:
        health_plan_id: ID do plano de saúde (obrigatório).
        coparticipation_type: Tipo de coparticipação (obrigatório). Ex: "Parcial", "Total".
        procedure: Nome ou descrição do procedimento (obrigatório).
        value: Valor da coparticipação (obrigatório).
        limit: Limite da coparticipação (opcional).
    """
    try:
        data = {
            "healthPlanId": health_plan_id,
            "coparticipationType": coparticipation_type,
            "procedure": procedure,
            "value": value,
            "limit": limit,
            "createdBy": "AI Assistant",
        }

        valid, error_msg = validate_procedurecoparticipation_data(data)
        if not valid:
            return format_error(Exception(error_msg))

        result = api_client.create_procedurecoparticipation(data)
        return format_success(
            f"Coparticipação de procedimento '{procedure}' criada com sucesso! ID: {result.get('id', 'N/A')}"
        )
    except Exception as e:
        return format_error(e)


@tool
def update_procedurecoparticipation(
    procedurecoparticipation_id: int,
    coparticipation_type: str = "",
    procedure: str = "",
    value: float = None,
    limit: float = None,
) -> str:
    """
    Atualiza uma coparticipação de procedimento existente.

    Args:
        procedurecoparticipation_id: ID da coparticipação de procedimento a ser atualizada.
        coparticipation_type: Novo tipo de coparticipação (opcional).
        procedure: Novo nome do procedimento (opcional).
        value: Novo valor da coparticipação (opcional).
        limit: Novo limite da coparticipação (opcional).
    """
    try:
        valid, error_msg = validate_id(procedurecoparticipation_id)
        if not valid:
            return format_error(Exception(error_msg))

        data = {}
        if coparticipation_type:
            data["coparticipationType"] = coparticipation_type
        if procedure:
            data["procedure"] = procedure
        if value is not None:
            data["value"] = value
        if limit is not None:
            data["limit"] = limit

        if not data:
            return format_error(Exception("Nenhum campo fornecido para atualização."))

        data["updatedBy"] = "AI Assistant"
        api_client.update_procedurecoparticipation(procedurecoparticipation_id, data)
        return format_success(f"Coparticipação de procedimento ID {procedurecoparticipation_id} atualizada com sucesso!")
    except Exception as e:
        return format_error(e)


@tool
def delete_procedurecoparticipation(procedurecoparticipation_id: int) -> str:
    """
    Deleta uma coparticipação de procedimento.

    Args:
        procedurecoparticipation_id: ID da coparticipação de procedimento a ser deletada.
    """
    try:
        valid, error_msg = validate_id(procedurecoparticipation_id)
        if not valid:
            return format_error(Exception(error_msg))

        api_client.delete_procedurecoparticipation(procedurecoparticipation_id)
        return format_success(f"Coparticipação de procedimento ID {procedurecoparticipation_id} deletada com sucesso!")
    except Exception as e:
        return format_error(e)
