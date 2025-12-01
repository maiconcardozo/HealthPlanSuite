"""
Ferramentas LangChain para operações com faixas etárias (AgeRanges).
"""

from langchain.tools import tool
from utils.api_client import HealthPlanAPIClient
from utils.formatters import format_agerange, format_ageranges, format_error, format_success
from utils.validators import validate_agerange_data, validate_id


api_client = HealthPlanAPIClient()


@tool
def get_all_ageranges() -> str:
    """Busca todas as faixas etárias cadastradas."""
    try:
        ageranges = api_client.get_ageranges()
        return format_ageranges(ageranges)
    except Exception as e:
        return format_error(e)


@tool
def get_agerange_by_id(agerange_id: int) -> str:
    """Busca uma faixa etária específica por ID."""
    try:
        valid, error_msg = validate_id(agerange_id)
        if not valid:
            return format_error(Exception(error_msg))

        agerange = api_client.get_agerange(agerange_id)
        return format_agerange(agerange)
    except Exception as e:
        return format_error(e)


@tool
def create_agerange(min_age: int, max_age: int, factor: float) -> str:
    """
    Cria uma nova faixa etária.

    Args:
        min_age: Idade mínima (obrigatório).
        max_age: Idade máxima (obrigatório).
        factor: Fator de multiplicação (obrigatório).
    """
    try:
        data = {
            "minAge": min_age,
            "maxAge": max_age,
            "factor": factor,
        }

        valid, error_msg = validate_agerange_data(data)
        if not valid:
            return format_error(Exception(error_msg))

        result = api_client.create_agerange(data)
        return format_success(
            f"Faixa etária {min_age}-{max_age} anos criada com sucesso! ID: {result.get('id', 'N/A')}"
        )
    except Exception as e:
        return format_error(e)


@tool
def update_agerange(
    agerange_id: int, min_age: int = None, max_age: int = None, factor: float = None
) -> str:
    """Atualiza uma faixa etária existente."""
    try:
        valid, error_msg = validate_id(agerange_id)
        if not valid:
            return format_error(Exception(error_msg))

        data = {}
        if min_age is not None:
            data["minAge"] = min_age
        if max_age is not None:
            data["maxAge"] = max_age
        if factor is not None:
            data["factor"] = factor

        if not data:
            return format_error(Exception("Nenhum campo fornecido para atualização."))

        api_client.update_agerange(agerange_id, data)
        return format_success(f"Faixa etária ID {agerange_id} atualizada com sucesso!")
    except Exception as e:
        return format_error(e)


@tool
def delete_agerange(agerange_id: int) -> str:
    """Deleta uma faixa etária."""
    try:
        valid, error_msg = validate_id(agerange_id)
        if not valid:
            return format_error(Exception(error_msg))

        api_client.delete_agerange(agerange_id)
        return format_success(f"Faixa etária ID {agerange_id} deletada com sucesso!")
    except Exception as e:
        return format_error(e)
