"""
Ferramentas LangChain para operações com acomodações (Accommodations).
"""

from langchain.tools import tool
from utils.api_client import HealthPlanAPIClient
from utils.formatters import format_accommodation, format_accommodations, format_error, format_success
from utils.validators import validate_accommodation_data, validate_id


api_client = HealthPlanAPIClient()


@tool
def get_all_accommodations() -> str:
    """Busca todas as acomodações cadastradas."""
    try:
        accommodations = api_client.get_accommodations()
        return format_accommodations(accommodations)
    except Exception as e:
        return format_error(e)


@tool
def get_accommodation_by_id(accommodation_id: int) -> str:
    """Busca uma acomodação específica por ID."""
    try:
        valid, error_msg = validate_id(accommodation_id)
        if not valid:
            return format_error(Exception(error_msg))

        accommodation = api_client.get_accommodation(accommodation_id)
        return format_accommodation(accommodation)
    except Exception as e:
        return format_error(e)


@tool
def create_accommodation(
    name: str, accommodation_type: str, description: str = ""
) -> str:
    """
    Cria uma nova acomodação.

    Args:
        name: Nome da acomodação (obrigatório).
        accommodation_type: Tipo da acomodação (obrigatório).
        description: Descrição (opcional).
    """
    try:
        data = {
            "name": name,
            "type": accommodation_type,
            "description": description,
        }

        valid, error_msg = validate_accommodation_data(data)
        if not valid:
            return format_error(Exception(error_msg))

        result = api_client.create_accommodation(data)
        return format_success(
            f"Acomodação '{name}' criada com sucesso! ID: {result.get('id', 'N/A')}"
        )
    except Exception as e:
        return format_error(e)


@tool
def update_accommodation(
    accommodation_id: int,
    name: str = "",
    accommodation_type: str = "",
    description: str = "",
) -> str:
    """Atualiza uma acomodação existente."""
    try:
        valid, error_msg = validate_id(accommodation_id)
        if not valid:
            return format_error(Exception(error_msg))

        data = {}
        if name:
            data["name"] = name
        if accommodation_type:
            data["type"] = accommodation_type
        if description:
            data["description"] = description

        if not data:
            return format_error(Exception("Nenhum campo fornecido para atualização."))

        api_client.update_accommodation(accommodation_id, data)
        return format_success(f"Acomodação ID {accommodation_id} atualizada com sucesso!")
    except Exception as e:
        return format_error(e)


@tool
def delete_accommodation(accommodation_id: int) -> str:
    """Deleta uma acomodação."""
    try:
        valid, error_msg = validate_id(accommodation_id)
        if not valid:
            return format_error(Exception(error_msg))

        api_client.delete_accommodation(accommodation_id)
        return format_success(f"Acomodação ID {accommodation_id} deletada com sucesso!")
    except Exception as e:
        return format_error(e)
