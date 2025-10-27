"""
Ferramentas LangChain para operações com empresas (Companies).

Este módulo define as ferramentas que permitem ao agente LangChain
realizar operações CRUD em empresas.
"""

from typing import Any, Dict
from langchain.tools import tool
from utils.api_client import HealthPlanAPIClient
from utils.formatters import format_company, format_companies, format_error, format_success
from utils.validators import validate_company_data, validate_id


# Instância global do cliente API
api_client = HealthPlanAPIClient()


@tool
def get_all_companies() -> str:
    """
    Busca todas as empresas cadastradas no sistema.

    Returns:
        String formatada com a lista de empresas.
    """
    try:
        companies = api_client.get_companies()
        return format_companies(companies)
    except Exception as e:
        return format_error(e)


@tool
def get_company_by_id(company_id: int) -> str:
    """
    Busca uma empresa específica por ID.

    Args:
        company_id: ID da empresa a ser buscada.

    Returns:
        String formatada com informações da empresa.
    """
    try:
        valid, error_msg = validate_id(company_id)
        if not valid:
            return format_error(Exception(error_msg))

        company = api_client.get_company(company_id)
        return format_company(company)
    except Exception as e:
        return format_error(e)


@tool
def create_company(name: str, cnpj: str = "", phone: str = "", email: str = "") -> str:
    """
    Cria uma nova empresa no sistema.

    Args:
        name: Nome da empresa (obrigatório).
        cnpj: CNPJ da empresa (opcional).
        phone: Telefone da empresa (opcional).
        email: Email da empresa (opcional).

    Returns:
        String confirmando a criação ou mensagem de erro.
    """
    try:
        data = {
            "name": name,
            "cnpj": cnpj,
            "phone": phone,
            "email": email,
        }

        valid, error_msg = validate_company_data(data)
        if not valid:
            return format_error(Exception(error_msg))

        result = api_client.create_company(data)
        return format_success(
            f"Empresa '{name}' criada com sucesso! ID: {result.get('id', 'N/A')}"
        )
    except Exception as e:
        return format_error(e)


@tool
def update_company(
    company_id: int, name: str = "", cnpj: str = "", phone: str = "", email: str = ""
) -> str:
    """
    Atualiza os dados de uma empresa existente.

    Args:
        company_id: ID da empresa a ser atualizada.
        name: Novo nome da empresa (opcional).
        cnpj: Novo CNPJ da empresa (opcional).
        phone: Novo telefone da empresa (opcional).
        email: Novo email da empresa (opcional).

    Returns:
        String confirmando a atualização ou mensagem de erro.
    """
    try:
        valid, error_msg = validate_id(company_id)
        if not valid:
            return format_error(Exception(error_msg))

        data = {}
        if name:
            data["name"] = name
        if cnpj:
            data["cnpj"] = cnpj
        if phone:
            data["phone"] = phone
        if email:
            data["email"] = email

        if not data:
            return format_error(
                Exception("Nenhum campo fornecido para atualização.")
            )

        api_client.update_company(company_id, data)
        return format_success(f"Empresa ID {company_id} atualizada com sucesso!")
    except Exception as e:
        return format_error(e)


@tool
def delete_company(company_id: int) -> str:
    """
    Deleta uma empresa do sistema.

    Args:
        company_id: ID da empresa a ser deletada.

    Returns:
        String confirmando a exclusão ou mensagem de erro.
    """
    try:
        valid, error_msg = validate_id(company_id)
        if not valid:
            return format_error(Exception(error_msg))

        api_client.delete_company(company_id)
        return format_success(f"Empresa ID {company_id} deletada com sucesso!")
    except Exception as e:
        return format_error(e)
