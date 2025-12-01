"""
Ferramentas LangChain para operações com descontos promocionais (PromotionalDiscounts).
"""

from langchain.tools import tool
from utils.api_client import HealthPlanAPIClient
from utils.formatters import format_promotionaldiscount, format_promotionaldiscounts, format_error, format_success
from utils.validators import validate_promotionaldiscount_data, validate_id


api_client = HealthPlanAPIClient()


@tool
def get_all_promotionaldiscounts() -> str:
    """Busca todos os descontos promocionais cadastrados."""
    try:
        promotionaldiscounts = api_client.get_promotionaldiscounts()
        return format_promotionaldiscounts(promotionaldiscounts)
    except Exception as e:
        return format_error(e)


@tool
def get_promotionaldiscount_by_id(promotionaldiscount_id: int) -> str:
    """
    Busca um desconto promocional específico por ID.

    Args:
        promotionaldiscount_id: ID do desconto promocional.
    """
    try:
        valid, error_msg = validate_id(promotionaldiscount_id)
        if not valid:
            return format_error(Exception(error_msg))

        promotionaldiscount = api_client.get_promotionaldiscount(promotionaldiscount_id)
        return format_promotionaldiscount(promotionaldiscount)
    except Exception as e:
        return format_error(e)


@tool
def create_promotionaldiscount(
    health_plan_id: int,
    discount_percentage: float,
    validity_start: str,
    validity_end: str,
    observation: str = "",
) -> str:
    """
    Cria um novo desconto promocional.

    Args:
        health_plan_id: ID do plano de saúde (obrigatório).
        discount_percentage: Percentual de desconto (obrigatório). Ex: 10 para 10%.
        validity_start: Data de início da validade no formato ISO (obrigatório). Ex: "2024-01-01T00:00:00".
        validity_end: Data de fim da validade no formato ISO (obrigatório). Ex: "2024-12-31T23:59:59".
        observation: Observação sobre o desconto (opcional).
    """
    try:
        data = {
            "healthPlanId": health_plan_id,
            "discountPercentage": discount_percentage,
            "validityStart": validity_start,
            "validityEnd": validity_end,
            "observation": observation if observation else None,
            "createdBy": "AI Assistant",
        }

        valid, error_msg = validate_promotionaldiscount_data(data)
        if not valid:
            return format_error(Exception(error_msg))

        result = api_client.create_promotionaldiscount(data)
        return format_success(
            f"Desconto promocional de {discount_percentage}% criado com sucesso! ID: {result.get('id', 'N/A')}"
        )
    except Exception as e:
        return format_error(e)


@tool
def update_promotionaldiscount(
    promotionaldiscount_id: int,
    discount_percentage: float = None,
    validity_start: str = "",
    validity_end: str = "",
    observation: str = "",
) -> str:
    """
    Atualiza um desconto promocional existente.

    Args:
        promotionaldiscount_id: ID do desconto promocional a ser atualizado.
        discount_percentage: Novo percentual de desconto (opcional).
        validity_start: Nova data de início da validade (opcional).
        validity_end: Nova data de fim da validade (opcional).
        observation: Nova observação (opcional).
    """
    try:
        valid, error_msg = validate_id(promotionaldiscount_id)
        if not valid:
            return format_error(Exception(error_msg))

        data = {}
        if discount_percentage is not None:
            data["discountPercentage"] = discount_percentage
        if validity_start:
            data["validityStart"] = validity_start
        if validity_end:
            data["validityEnd"] = validity_end
        if observation:
            data["observation"] = observation

        if not data:
            return format_error(Exception("Nenhum campo fornecido para atualização."))

        data["updatedBy"] = "AI Assistant"
        api_client.update_promotionaldiscount(promotionaldiscount_id, data)
        return format_success(f"Desconto promocional ID {promotionaldiscount_id} atualizado com sucesso!")
    except Exception as e:
        return format_error(e)


@tool
def delete_promotionaldiscount(promotionaldiscount_id: int) -> str:
    """
    Deleta um desconto promocional.

    Args:
        promotionaldiscount_id: ID do desconto promocional a ser deletado.
    """
    try:
        valid, error_msg = validate_id(promotionaldiscount_id)
        if not valid:
            return format_error(Exception(error_msg))

        api_client.delete_promotionaldiscount(promotionaldiscount_id)
        return format_success(f"Desconto promocional ID {promotionaldiscount_id} deletado com sucesso!")
    except Exception as e:
        return format_error(e)
