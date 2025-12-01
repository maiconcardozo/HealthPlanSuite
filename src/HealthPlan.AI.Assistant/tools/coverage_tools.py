"""
Ferramentas LangChain para operações com coberturas (Coverages).
"""

from langchain.tools import tool
from utils.api_client import HealthPlanAPIClient
from utils.formatters import format_coverage, format_coverages, format_error, format_success
from utils.validators import validate_coverage_data, validate_id


api_client = HealthPlanAPIClient()


@tool
def get_all_coverages() -> str:
    """Busca todas as coberturas cadastradas."""
    try:
        coverages = api_client.get_coverages()
        return format_coverages(coverages)
    except Exception as e:
        return format_error(e)


@tool
def get_coverage_by_id(coverage_id: int) -> str:
    """Busca uma cobertura específica por ID."""
    try:
        valid, error_msg = validate_id(coverage_id)
        if not valid:
            return format_error(Exception(error_msg))

        coverage = api_client.get_coverage(coverage_id)
        return format_coverage(coverage)
    except Exception as e:
        return format_error(e)


@tool
def create_coverage(name: str, description: str = "", coverage_type: str = "") -> str:
    """
    Cria uma nova cobertura.

    Args:
        name: Nome da cobertura (obrigatório).
        description: Descrição (opcional).
        coverage_type: Tipo de cobertura (opcional).
    """
    try:
        data = {
            "name": name,
            "description": description,
            "type": coverage_type,
        }

        valid, error_msg = validate_coverage_data(data)
        if not valid:
            return format_error(Exception(error_msg))

        result = api_client.create_coverage(data)
        return format_success(
            f"Cobertura '{name}' criada com sucesso! ID: {result.get('id', 'N/A')}"
        )
    except Exception as e:
        return format_error(e)


@tool
def update_coverage(
    coverage_id: int, name: str = "", description: str = "", coverage_type: str = ""
) -> str:
    """Atualiza uma cobertura existente."""
    try:
        valid, error_msg = validate_id(coverage_id)
        if not valid:
            return format_error(Exception(error_msg))

        data = {}
        if name:
            data["name"] = name
        if description:
            data["description"] = description
        if coverage_type:
            data["type"] = coverage_type

        if not data:
            return format_error(Exception("Nenhum campo fornecido para atualização."))

        api_client.update_coverage(coverage_id, data)
        return format_success(f"Cobertura ID {coverage_id} atualizada com sucesso!")
    except Exception as e:
        return format_error(e)


@tool
def delete_coverage(coverage_id: int) -> str:
    """Deleta uma cobertura."""
    try:
        valid, error_msg = validate_id(coverage_id)
        if not valid:
            return format_error(Exception(error_msg))

        api_client.delete_coverage(coverage_id)
        return format_success(f"Cobertura ID {coverage_id} deletada com sucesso!")
    except Exception as e:
        return format_error(e)
