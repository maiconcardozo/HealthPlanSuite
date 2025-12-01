"""
Ferramentas LangChain para operações com cotações (Quotes).
"""

from langchain.tools import tool
from utils.api_client import HealthPlanAPIClient
from utils.formatters import format_quote, format_quotes, format_error, format_success
from utils.validators import validate_quote_data, validate_id


api_client = HealthPlanAPIClient()


@tool
def get_all_quotes() -> str:
    """Busca todas as cotações cadastradas."""
    try:
        quotes = api_client.get_quotes()
        return format_quotes(quotes)
    except Exception as e:
        return format_error(e)


@tool
def get_quote_by_id(quote_id: int) -> str:
    """Busca uma cotação específica por ID."""
    try:
        valid, error_msg = validate_id(quote_id)
        if not valid:
            return format_error(Exception(error_msg))

        quote = api_client.get_quote(quote_id)
        return format_quote(quote)
    except Exception as e:
        return format_error(e)


@tool
def create_quote(company_id: int, healthplan_id: int, total_value: float = 0.0) -> str:
    """
    Cria uma nova cotação.

    Args:
        company_id: ID da empresa (obrigatório).
        healthplan_id: ID do plano de saúde (obrigatório).
        total_value: Valor total da cotação (opcional).
    """
    try:
        data = {
            "companyId": company_id,
            "healthPlanId": healthplan_id,
            "totalValue": total_value,
        }

        valid, error_msg = validate_quote_data(data)
        if not valid:
            return format_error(Exception(error_msg))

        result = api_client.create_quote(data)
        return format_success(
            f"Cotação criada com sucesso! ID: {result.get('id', 'N/A')}"
        )
    except Exception as e:
        return format_error(e)


@tool
def update_quote(quote_id: int, total_value: float = None, status: str = "") -> str:
    """
    Atualiza uma cotação existente.

    Args:
        quote_id: ID da cotação.
        total_value: Novo valor total (opcional).
        status: Novo status (opcional).
    """
    try:
        valid, error_msg = validate_id(quote_id)
        if not valid:
            return format_error(Exception(error_msg))

        data = {}
        if total_value is not None:
            data["totalValue"] = total_value
        if status:
            data["status"] = status

        if not data:
            return format_error(Exception("Nenhum campo fornecido para atualização."))

        api_client.update_quote(quote_id, data)
        return format_success(f"Cotação ID {quote_id} atualizada com sucesso!")
    except Exception as e:
        return format_error(e)


@tool
def delete_quote(quote_id: int) -> str:
    """Deleta uma cotação."""
    try:
        valid, error_msg = validate_id(quote_id)
        if not valid:
            return format_error(Exception(error_msg))

        api_client.delete_quote(quote_id)
        return format_success(f"Cotação ID {quote_id} deletada com sucesso!")
    except Exception as e:
        return format_error(e)
