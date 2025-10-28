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


def validate_plancoverage_data(data: Dict[str, Any]) -> tuple[bool, Optional[str]]:
    """
    Valida dados de uma cobertura de plano.

    Args:
        data: Dicionário com dados da cobertura de plano.

    Returns:
        Tupla (válido, mensagem_erro).
    """
    required_fields = ["healthPlanId", "coverageId"]

    for field in required_fields:
        if field not in data or data[field] is None:
            return False, f"Campo obrigatório ausente: {field}"

    # Validar valor premium se fornecido
    if "premiumValue" in data and data["premiumValue"] is not None:
        try:
            value = float(data["premiumValue"])
            if value < 0:
                return False, "Valor premium não pode ser negativo."
        except (ValueError, TypeError):
            return False, "Valor premium inválido."

    return True, None


def validate_acceptancerule_data(data: Dict[str, Any]) -> tuple[bool, Optional[str]]:
    """
    Valida dados de uma regra de aceitação.

    Args:
        data: Dicionário com dados da regra de aceitação.

    Returns:
        Tupla (válido, mensagem_erro).
    """
    required_fields = ["healthPlanId", "ruleType", "operator", "description"]

    for field in required_fields:
        if field not in data or not data[field]:
            return False, f"Campo obrigatório ausente: {field}"

    # Validar operador
    valid_operators = ["=", ">", "<", ">=", "<=", "BETWEEN", "IN"]
    if data["operator"] not in valid_operators:
        return False, f"Operador inválido. Valores permitidos: {', '.join(valid_operators)}"

    return True, None


def validate_adhesionfee_data(data: Dict[str, Any]) -> tuple[bool, Optional[str]]:
    """
    Valida dados de uma taxa de adesão.

    Args:
        data: Dicionário com dados da taxa de adesão.

    Returns:
        Tupla (válido, mensagem_erro).
    """
    required_fields = ["healthPlanId", "value", "validityStart", "validityEnd"]

    for field in required_fields:
        if field not in data or data[field] is None:
            return False, f"Campo obrigatório ausente: {field}"

    # Validar valor
    try:
        value = float(data["value"])
        if value < 0:
            return False, "Valor não pode ser negativo."
    except (ValueError, TypeError):
        return False, "Valor inválido."

    return True, None


def validate_promotionaldiscount_data(data: Dict[str, Any]) -> tuple[bool, Optional[str]]:
    """
    Valida dados de um desconto promocional.

    Args:
        data: Dicionário com dados do desconto promocional.

    Returns:
        Tupla (válido, mensagem_erro).
    """
    required_fields = ["healthPlanId", "discountPercentage", "validityStart", "validityEnd"]

    for field in required_fields:
        if field not in data or data[field] is None:
            return False, f"Campo obrigatório ausente: {field}"

    # Validar percentual de desconto
    try:
        discount = float(data["discountPercentage"])
        if discount < 0 or discount > 100:
            return False, "Percentual de desconto deve estar entre 0 e 100."
    except (ValueError, TypeError):
        return False, "Percentual de desconto inválido."

    return True, None


def validate_procedurecoparticipation_data(data: Dict[str, Any]) -> tuple[bool, Optional[str]]:
    """
    Valida dados de uma coparticipação de procedimento.

    Args:
        data: Dicionário com dados da coparticipação de procedimento.

    Returns:
        Tupla (válido, mensagem_erro).
    """
    required_fields = ["healthPlanId", "coparticipationType", "procedure", "value"]

    for field in required_fields:
        if field not in data or not data[field]:
            return False, f"Campo obrigatório ausente: {field}"

    # Validar valor
    try:
        value = float(data["value"])
        if value < 0:
            return False, "Valor não pode ser negativo."
    except (ValueError, TypeError):
        return False, "Valor inválido."

    # Validar limite se fornecido
    if "limit" in data and data["limit"] is not None:
        try:
            limit = float(data["limit"])
            if limit < 0:
                return False, "Limite não pode ser negativo."
        except (ValueError, TypeError):
            return False, "Limite inválido."

    return True, None


def validate_planpricerange_data(data: Dict[str, Any]) -> tuple[bool, Optional[str]]:
    """
    Valida dados de uma faixa de preços de plano.

    Args:
        data: Dicionário com dados da faixa de preços de plano.

    Returns:
        Tupla (válido, mensagem_erro).
    """
    required_fields = ["healthPlanId", "ageRangeId", "contractType", "coparticipationType", "originalValue", "validityStart", "validityEnd"]

    for field in required_fields:
        if field not in data or data[field] is None:
            return False, f"Campo obrigatório ausente: {field}"

    # Validar valor original
    try:
        original_value = float(data["originalValue"])
        if original_value < 0:
            return False, "Valor original não pode ser negativo."
    except (ValueError, TypeError):
        return False, "Valor original inválido."

    # Validar valor de desconto se fornecido
    if "discountValue" in data and data["discountValue"] is not None:
        try:
            discount = float(data["discountValue"])
            if discount < 0:
                return False, "Valor de desconto não pode ser negativo."
        except (ValueError, TypeError):
            return False, "Valor de desconto inválido."

    return True, None
