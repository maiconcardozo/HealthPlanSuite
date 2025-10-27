"""
Validadores de dados para as ferramentas LangChain.

Este módulo fornece funções para validar dados antes de
enviá-los para a API.
"""

from typing import Any, Dict, List, Optional


def validate_company_data(data: Dict[str, Any]) -> tuple[bool, Optional[str]]:
    """
    Valida dados de uma empresa.

    Args:
        data: Dicionário com dados da empresa.

    Returns:
        Tupla (válido, mensagem_erro).
    """
    required_fields = ["name"]

    for field in required_fields:
        if field not in data or not data[field]:
            return False, f"Campo obrigatório ausente: {field}"

    # Validar CNPJ se fornecido
    if "cnpj" in data and data["cnpj"]:
        cnpj = data["cnpj"].replace(".", "").replace("/", "").replace("-", "")
        if len(cnpj) != 14 or not cnpj.isdigit():
            return False, "CNPJ inválido. Deve conter 14 dígitos."

    return True, None


def validate_healthplan_data(data: Dict[str, Any]) -> tuple[bool, Optional[str]]:
    """
    Valida dados de um plano de saúde.

    Args:
        data: Dicionário com dados do plano.

    Returns:
        Tupla (válido, mensagem_erro).
    """
    required_fields = ["name", "companyId"]

    for field in required_fields:
        if field not in data or data[field] is None:
            return False, f"Campo obrigatório ausente: {field}"

    # Validar preço base se fornecido
    if "basePrice" in data and data["basePrice"] is not None:
        try:
            price = float(data["basePrice"])
            if price < 0:
                return False, "Preço base não pode ser negativo."
        except (ValueError, TypeError):
            return False, "Preço base inválido."

    return True, None


def validate_beneficiary_data(data: Dict[str, Any]) -> tuple[bool, Optional[str]]:
    """
    Valida dados de um beneficiário.

    Args:
        data: Dicionário com dados do beneficiário.

    Returns:
        Tupla (válido, mensagem_erro).
    """
    required_fields = ["name", "cpf", "birthDate"]

    for field in required_fields:
        if field not in data or not data[field]:
            return False, f"Campo obrigatório ausente: {field}"

    # Validar CPF
    cpf = data["cpf"].replace(".", "").replace("-", "")
    if len(cpf) != 11 or not cpf.isdigit():
        return False, "CPF inválido. Deve conter 11 dígitos."

    return True, None


def validate_quote_data(data: Dict[str, Any]) -> tuple[bool, Optional[str]]:
    """
    Valida dados de uma cotação.

    Args:
        data: Dicionário com dados da cotação.

    Returns:
        Tupla (válido, mensagem_erro).
    """
    required_fields = ["companyId", "healthPlanId"]

    for field in required_fields:
        if field not in data or data[field] is None:
            return False, f"Campo obrigatório ausente: {field}"

    return True, None


def validate_coverage_data(data: Dict[str, Any]) -> tuple[bool, Optional[str]]:
    """
    Valida dados de uma cobertura.

    Args:
        data: Dicionário com dados da cobertura.

    Returns:
        Tupla (válido, mensagem_erro).
    """
    required_fields = ["name"]

    for field in required_fields:
        if field not in data or not data[field]:
            return False, f"Campo obrigatório ausente: {field}"

    return True, None


def validate_agerange_data(data: Dict[str, Any]) -> tuple[bool, Optional[str]]:
    """
    Valida dados de uma faixa etária.

    Args:
        data: Dicionário com dados da faixa etária.

    Returns:
        Tupla (válido, mensagem_erro).
    """
    required_fields = ["minAge", "maxAge", "factor"]

    for field in required_fields:
        if field not in data or data[field] is None:
            return False, f"Campo obrigatório ausente: {field}"

    # Validar idades
    try:
        min_age = int(data["minAge"])
        max_age = int(data["maxAge"])

        if min_age < 0 or max_age < 0:
            return False, "Idades não podem ser negativas."

        if min_age >= max_age:
            return False, "Idade mínima deve ser menor que idade máxima."

    except (ValueError, TypeError):
        return False, "Idades devem ser números inteiros."

    # Validar fator
    try:
        factor = float(data["factor"])
        if factor < 0:
            return False, "Fator não pode ser negativo."
    except (ValueError, TypeError):
        return False, "Fator deve ser um número."

    return True, None


def validate_accommodation_data(data: Dict[str, Any]) -> tuple[bool, Optional[str]]:
    """
    Valida dados de uma acomodação.

    Args:
        data: Dicionário com dados da acomodação.

    Returns:
        Tupla (válido, mensagem_erro).
    """
    required_fields = ["name", "type"]

    for field in required_fields:
        if field not in data or not data[field]:
            return False, f"Campo obrigatório ausente: {field}"

    return True, None


def validate_id(entity_id: Any) -> tuple[bool, Optional[str]]:
    """
    Valida um ID de entidade.

    Args:
        entity_id: ID a ser validado.

    Returns:
        Tupla (válido, mensagem_erro).
    """
    try:
        id_int = int(entity_id)
        if id_int <= 0:
            return False, "ID deve ser um número positivo."
        return True, None
    except (ValueError, TypeError):
        return False, "ID deve ser um número inteiro."
