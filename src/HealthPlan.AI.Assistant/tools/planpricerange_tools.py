"""
Ferramentas LangChain para operações com faixas de preços de planos (PlanPriceRanges).
"""

from langchain.tools import tool
from utils.api_client import HealthPlanAPIClient
from utils.formatters import format_planpricerange, format_planpriceranges, format_error, format_success
from utils.validators import validate_planpricerange_data, validate_id


api_client = HealthPlanAPIClient()


@tool
def get_all_planpriceranges() -> str:
    """Busca todas as faixas de preços de planos cadastradas."""
    try:
        planpriceranges = api_client.get_planpriceranges()
        return format_planpriceranges(planpriceranges)
    except Exception as e:
        return format_error(e)


@tool
def get_planpricerange_by_id(planpricerange_id: int) -> str:
    """
    Busca uma faixa de preços de plano específica por ID.

    Args:
        planpricerange_id: ID da faixa de preços de plano.
    """
    try:
        valid, error_msg = validate_id(planpricerange_id)
        if not valid:
            return format_error(Exception(error_msg))

        planpricerange = api_client.get_planpricerange(planpricerange_id)
        return format_planpricerange(planpricerange)
    except Exception as e:
        return format_error(e)


@tool
def create_planpricerange(
    health_plan_id: int,
    age_range_id: int,
    contract_type: str,
    coparticipation_type: str,
    original_value: float,
    validity_start: str,
    validity_end: str,
    discount_value: float = 0.0,
) -> str:
    """
    Cria uma nova faixa de preços de plano.

    Args:
        health_plan_id: ID do plano de saúde (obrigatório).
        age_range_id: ID da faixa etária (obrigatório).
        contract_type: Tipo de contrato (obrigatório). Ex: "Individual", "Coletivo por Adesão", "Empresarial".
        coparticipation_type: Tipo de coparticipação (obrigatório). Ex: "Parcial", "Total", "Sem Coparticipação".
        original_value: Valor original do plano (obrigatório).
        validity_start: Data de início da validade no formato ISO (obrigatório). Ex: "2024-01-01T00:00:00".
        validity_end: Data de fim da validade no formato ISO (obrigatório). Ex: "2024-12-31T23:59:59".
        discount_value: Valor de desconto (opcional, padrão: 0.0).
    """
    try:
        data = {
            "healthPlanId": health_plan_id,
            "ageRangeId": age_range_id,
            "contractType": contract_type,
            "coparticipationType": coparticipation_type,
            "originalValue": original_value,
            "discountValue": discount_value,
            "validityStart": validity_start,
            "validityEnd": validity_end,
            "createdBy": "AI Assistant",
        }

        valid, error_msg = validate_planpricerange_data(data)
        if not valid:
            return format_error(Exception(error_msg))

        result = api_client.create_planpricerange(data)
        return format_success(
            f"Faixa de preços de plano criada com sucesso! ID: {result.get('id', 'N/A')}"
        )
    except Exception as e:
        return format_error(e)


@tool
def update_planpricerange(
    planpricerange_id: int,
    contract_type: str = "",
    coparticipation_type: str = "",
    original_value: float = None,
    discount_value: float = None,
    validity_start: str = "",
    validity_end: str = "",
) -> str:
    """
    Atualiza uma faixa de preços de plano existente.

    Args:
        planpricerange_id: ID da faixa de preços de plano a ser atualizada.
        contract_type: Novo tipo de contrato (opcional).
        coparticipation_type: Novo tipo de coparticipação (opcional).
        original_value: Novo valor original (opcional).
        discount_value: Novo valor de desconto (opcional).
        validity_start: Nova data de início da validade (opcional).
        validity_end: Nova data de fim da validade (opcional).
    """
    try:
        valid, error_msg = validate_id(planpricerange_id)
        if not valid:
            return format_error(Exception(error_msg))

        data = {}
        if contract_type:
            data["contractType"] = contract_type
        if coparticipation_type:
            data["coparticipationType"] = coparticipation_type
        if original_value is not None:
            data["originalValue"] = original_value
        if discount_value is not None:
            data["discountValue"] = discount_value
        if validity_start:
            data["validityStart"] = validity_start
        if validity_end:
            data["validityEnd"] = validity_end

        if not data:
            return format_error(Exception("Nenhum campo fornecido para atualização."))

        data["updatedBy"] = "AI Assistant"
        api_client.update_planpricerange(planpricerange_id, data)
        return format_success(f"Faixa de preços de plano ID {planpricerange_id} atualizada com sucesso!")
    except Exception as e:
        return format_error(e)


@tool
def delete_planpricerange(planpricerange_id: int) -> str:
    """
    Deleta uma faixa de preços de plano.

    Args:
        planpricerange_id: ID da faixa de preços de plano a ser deletada.
    """
    try:
        valid, error_msg = validate_id(planpricerange_id)
        if not valid:
            return format_error(Exception(error_msg))

        api_client.delete_planpricerange(planpricerange_id)
        return format_success(f"Faixa de preços de plano ID {planpricerange_id} deletada com sucesso!")
    except Exception as e:
        return format_error(e)
