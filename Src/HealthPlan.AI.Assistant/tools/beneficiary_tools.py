"""
Ferramentas LangChain para operações com beneficiários (Beneficiaries).
"""

from langchain.tools import tool
from utils.api_client import HealthPlanAPIClient
from utils.formatters import format_beneficiary, format_beneficiaries, format_error, format_success
from utils.validators import validate_beneficiary_data, validate_id


api_client = HealthPlanAPIClient()


@tool
def get_all_beneficiaries() -> str:
    """Busca todos os beneficiários cadastrados."""
    try:
        beneficiaries = api_client.get_beneficiaries()
        return format_beneficiaries(beneficiaries)
    except Exception as e:
        return format_error(e)


@tool
def get_beneficiary_by_id(beneficiary_id: int) -> str:
    """Busca um beneficiário específico por ID."""
    try:
        valid, error_msg = validate_id(beneficiary_id)
        if not valid:
            return format_error(Exception(error_msg))

        beneficiary = api_client.get_beneficiary(beneficiary_id)
        return format_beneficiary(beneficiary)
    except Exception as e:
        return format_error(e)


@tool
def create_beneficiary(
    name: str, cpf: str, birth_date: str, phone: str = "", email: str = ""
) -> str:
    """
    Cria um novo beneficiário.

    Args:
        name: Nome completo (obrigatório).
        cpf: CPF do beneficiário (obrigatório).
        birth_date: Data de nascimento formato YYYY-MM-DD (obrigatório).
        phone: Telefone (opcional).
        email: Email (opcional).
    """
    try:
        data = {
            "name": name,
            "cpf": cpf,
            "birthDate": birth_date,
            "phone": phone,
            "email": email,
        }

        valid, error_msg = validate_beneficiary_data(data)
        if not valid:
            return format_error(Exception(error_msg))

        result = api_client.create_beneficiary(data)
        return format_success(
            f"Beneficiário '{name}' criado com sucesso! ID: {result.get('id', 'N/A')}"
        )
    except Exception as e:
        return format_error(e)


@tool
def update_beneficiary(
    beneficiary_id: int,
    name: str = "",
    phone: str = "",
    email: str = "",
) -> str:
    """Atualiza um beneficiário existente."""
    try:
        valid, error_msg = validate_id(beneficiary_id)
        if not valid:
            return format_error(Exception(error_msg))

        data = {}
        if name:
            data["name"] = name
        if phone:
            data["phone"] = phone
        if email:
            data["email"] = email

        if not data:
            return format_error(Exception("Nenhum campo fornecido para atualização."))

        api_client.update_beneficiary(beneficiary_id, data)
        return format_success(f"Beneficiário ID {beneficiary_id} atualizado com sucesso!")
    except Exception as e:
        return format_error(e)


@tool
def delete_beneficiary(beneficiary_id: int) -> str:
    """Deleta um beneficiário."""
    try:
        valid, error_msg = validate_id(beneficiary_id)
        if not valid:
            return format_error(Exception(error_msg))

        api_client.delete_beneficiary(beneficiary_id)
        return format_success(f"Beneficiário ID {beneficiary_id} deletado com sucesso!")
    except Exception as e:
        return format_error(e)
