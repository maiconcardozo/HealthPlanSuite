"""
Ferramentas LangChain para operações com regras de aceitação (AcceptanceRules).
"""

from langchain.tools import tool
from utils.api_client import HealthPlanAPIClient
from utils.formatters import format_acceptancerule, format_acceptancerules, format_error, format_success
from utils.validators import validate_acceptancerule_data, validate_id


api_client = HealthPlanAPIClient()


@tool
def get_all_acceptancerules() -> str:
    """Busca todas as regras de aceitação cadastradas."""
    try:
        acceptancerules = api_client.get_acceptancerules()
        return format_acceptancerules(acceptancerules)
    except Exception as e:
        return format_error(e)


@tool
def get_acceptancerule_by_id(acceptancerule_id: int) -> str:
    """
    Busca uma regra de aceitação específica por ID.

    Args:
        acceptancerule_id: ID da regra de aceitação.
    """
    try:
        valid, error_msg = validate_id(acceptancerule_id)
        if not valid:
            return format_error(Exception(error_msg))

        acceptancerule = api_client.get_acceptancerule(acceptancerule_id)
        return format_acceptancerule(acceptancerule)
    except Exception as e:
        return format_error(e)


@tool
def create_acceptancerule(
    health_plan_id: int,
    rule_type: str,
    operator: str,
    description: str,
    min_value: str = "",
    max_value: str = "",
    values_list: str = "",
    rejection_message: str = "",
    is_mandatory: bool = True,
) -> str:
    """
    Cria uma nova regra de aceitação.

    Args:
        health_plan_id: ID do plano de saúde (obrigatório).
        rule_type: Tipo de regra (obrigatório). Ex: "Age", "Income", "Profession".
        operator: Operador da regra (obrigatório). Ex: "=", ">", "<", ">=", "<=", "BETWEEN", "IN".
        description: Descrição da regra (obrigatório).
        min_value: Valor mínimo (opcional).
        max_value: Valor máximo (opcional).
        values_list: Lista de valores aceitos em formato JSON (opcional).
        rejection_message: Mensagem de rejeição (opcional).
        is_mandatory: Se a regra é obrigatória (opcional, padrão: True).
    """
    try:
        data = {
            "healthPlanId": health_plan_id,
            "ruleType": rule_type,
            "operator": operator,
            "description": description,
            "minValue": min_value if min_value else None,
            "maxValue": max_value if max_value else None,
            "valuesList": values_list if values_list else None,
            "rejectionMessage": rejection_message if rejection_message else None,
            "isMandatory": is_mandatory,
            "createdBy": "AI Assistant",
        }

        valid, error_msg = validate_acceptancerule_data(data)
        if not valid:
            return format_error(Exception(error_msg))

        result = api_client.create_acceptancerule(data)
        return format_success(
            f"Regra de aceitação '{rule_type}' criada com sucesso! ID: {result.get('id', 'N/A')}"
        )
    except Exception as e:
        return format_error(e)


@tool
def update_acceptancerule(
    acceptancerule_id: int,
    rule_type: str = "",
    operator: str = "",
    description: str = "",
    min_value: str = "",
    max_value: str = "",
    is_mandatory: bool = None,
) -> str:
    """
    Atualiza uma regra de aceitação existente.

    Args:
        acceptancerule_id: ID da regra de aceitação a ser atualizada.
        rule_type: Novo tipo de regra (opcional).
        operator: Novo operador (opcional).
        description: Nova descrição (opcional).
        min_value: Novo valor mínimo (opcional).
        max_value: Novo valor máximo (opcional).
        is_mandatory: Nova flag de obrigatoriedade (opcional).
    """
    try:
        valid, error_msg = validate_id(acceptancerule_id)
        if not valid:
            return format_error(Exception(error_msg))

        data = {}
        if rule_type:
            data["ruleType"] = rule_type
        if operator:
            data["operator"] = operator
        if description:
            data["description"] = description
        if min_value:
            data["minValue"] = min_value
        if max_value:
            data["maxValue"] = max_value
        if is_mandatory is not None:
            data["isMandatory"] = is_mandatory

        if not data:
            return format_error(Exception("Nenhum campo fornecido para atualização."))

        data["updatedBy"] = "AI Assistant"
        api_client.update_acceptancerule(acceptancerule_id, data)
        return format_success(f"Regra de aceitação ID {acceptancerule_id} atualizada com sucesso!")
    except Exception as e:
        return format_error(e)


@tool
def delete_acceptancerule(acceptancerule_id: int) -> str:
    """
    Deleta uma regra de aceitação.

    Args:
        acceptancerule_id: ID da regra de aceitação a ser deletada.
    """
    try:
        valid, error_msg = validate_id(acceptancerule_id)
        if not valid:
            return format_error(Exception(error_msg))

        api_client.delete_acceptancerule(acceptancerule_id)
        return format_success(f"Regra de aceitação ID {acceptancerule_id} deletada com sucesso!")
    except Exception as e:
        return format_error(e)
