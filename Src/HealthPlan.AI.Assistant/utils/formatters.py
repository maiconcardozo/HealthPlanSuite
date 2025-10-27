"""
Formatadores de resposta para as ferramentas LangChain.

Este módulo fornece funções para formatar dados da API
em strings legíveis para o usuário.
"""

from typing import Any, Dict, List


def format_company(company: Dict[str, Any]) -> str:
    """
    Formata os dados de uma empresa.

    Args:
        company: Dicionário com dados da empresa.

    Returns:
        String formatada com informações da empresa.
    """
    return f"""
🏢 Empresa
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
ID: {company.get('id', 'N/A')}
Nome: {company.get('name', 'N/A')}
CNPJ: {company.get('cnpj', 'N/A')}
Telefone: {company.get('phone', 'N/A')}
Email: {company.get('email', 'N/A')}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    """.strip()


def format_companies(companies: List[Dict[str, Any]]) -> str:
    """
    Formata uma lista de empresas.

    Args:
        companies: Lista de dicionários com dados das empresas.

    Returns:
        String formatada com a lista de empresas.
    """
    if not companies:
        return "Nenhuma empresa encontrada."

    result = f"📋 Encontradas {len(companies)} empresa(s):\n\n"
    for company in companies:
        result += f"• {company.get('name', 'N/A')} (ID: {company.get('id', 'N/A')})\n"
    return result.strip()


def format_healthplan(healthplan: Dict[str, Any]) -> str:
    """Formata os dados de um plano de saúde."""
    return f"""
💼 Plano de Saúde
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
ID: {healthplan.get('id', 'N/A')}
Nome: {healthplan.get('name', 'N/A')}
Empresa ID: {healthplan.get('companyId', 'N/A')}
Preço Base: R$ {healthplan.get('basePrice', 'N/A')}
Descrição: {healthplan.get('description', 'N/A')}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    """.strip()


def format_healthplans(healthplans: List[Dict[str, Any]]) -> str:
    """Formata uma lista de planos de saúde."""
    if not healthplans:
        return "Nenhum plano de saúde encontrado."

    result = f"📋 Encontrados {len(healthplans)} plano(s) de saúde:\n\n"
    for plan in healthplans:
        price = plan.get('basePrice', 'N/A')
        result += f"• {plan.get('name', 'N/A')} - R$ {price} (ID: {plan.get('id', 'N/A')})\n"
    return result.strip()


def format_beneficiary(beneficiary: Dict[str, Any]) -> str:
    """Formata os dados de um beneficiário."""
    return f"""
👤 Beneficiário
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
ID: {beneficiary.get('id', 'N/A')}
Nome: {beneficiary.get('name', 'N/A')}
CPF: {beneficiary.get('cpf', 'N/A')}
Data de Nascimento: {beneficiary.get('birthDate', 'N/A')}
Idade: {beneficiary.get('age', 'N/A')} anos
Telefone: {beneficiary.get('phone', 'N/A')}
Email: {beneficiary.get('email', 'N/A')}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    """.strip()


def format_beneficiaries(beneficiaries: List[Dict[str, Any]]) -> str:
    """Formata uma lista de beneficiários."""
    if not beneficiaries:
        return "Nenhum beneficiário encontrado."

    result = f"📋 Encontrados {len(beneficiaries)} beneficiário(s):\n\n"
    for ben in beneficiaries:
        result += f"• {ben.get('name', 'N/A')} - {ben.get('age', 'N/A')} anos (ID: {ben.get('id', 'N/A')})\n"
    return result.strip()


def format_quote(quote: Dict[str, Any]) -> str:
    """Formata os dados de uma cotação."""
    return f"""
📄 Cotação
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
ID: {quote.get('id', 'N/A')}
Empresa ID: {quote.get('companyId', 'N/A')}
Plano de Saúde ID: {quote.get('healthPlanId', 'N/A')}
Valor Total: R$ {quote.get('totalValue', 'N/A')}
Data: {quote.get('date', 'N/A')}
Status: {quote.get('status', 'N/A')}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    """.strip()


def format_quotes(quotes: List[Dict[str, Any]]) -> str:
    """Formata uma lista de cotações."""
    if not quotes:
        return "Nenhuma cotação encontrada."

    result = f"📋 Encontradas {len(quotes)} cotação(ões):\n\n"
    for quote in quotes:
        value = quote.get('totalValue', 'N/A')
        result += f"• Cotação #{quote.get('id', 'N/A')} - R$ {value}\n"
    return result.strip()


def format_coverage(coverage: Dict[str, Any]) -> str:
    """Formata os dados de uma cobertura."""
    return f"""
🛡️ Cobertura
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
ID: {coverage.get('id', 'N/A')}
Nome: {coverage.get('name', 'N/A')}
Descrição: {coverage.get('description', 'N/A')}
Tipo: {coverage.get('type', 'N/A')}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    """.strip()


def format_coverages(coverages: List[Dict[str, Any]]) -> str:
    """Formata uma lista de coberturas."""
    if not coverages:
        return "Nenhuma cobertura encontrada."

    result = f"📋 Encontradas {len(coverages)} cobertura(s):\n\n"
    for cov in coverages:
        result += f"• {cov.get('name', 'N/A')} (ID: {cov.get('id', 'N/A')})\n"
    return result.strip()


def format_agerange(agerange: Dict[str, Any]) -> str:
    """Formata os dados de uma faixa etária."""
    return f"""
📊 Faixa Etária
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
ID: {agerange.get('id', 'N/A')}
Idade Mínima: {agerange.get('minAge', 'N/A')} anos
Idade Máxima: {agerange.get('maxAge', 'N/A')} anos
Fator de Multiplicação: {agerange.get('factor', 'N/A')}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    """.strip()


def format_ageranges(ageranges: List[Dict[str, Any]]) -> str:
    """Formata uma lista de faixas etárias."""
    if not ageranges:
        return "Nenhuma faixa etária encontrada."

    result = f"📋 Encontradas {len(ageranges)} faixa(s) etária(s):\n\n"
    for age in ageranges:
        result += f"• {age.get('minAge', 'N/A')}-{age.get('maxAge', 'N/A')} anos (ID: {age.get('id', 'N/A')})\n"
    return result.strip()


def format_accommodation(accommodation: Dict[str, Any]) -> str:
    """Formata os dados de uma acomodação."""
    return f"""
🏨 Acomodação
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
ID: {accommodation.get('id', 'N/A')}
Nome: {accommodation.get('name', 'N/A')}
Tipo: {accommodation.get('type', 'N/A')}
Descrição: {accommodation.get('description', 'N/A')}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    """.strip()


def format_accommodations(accommodations: List[Dict[str, Any]]) -> str:
    """Formata uma lista de acomodações."""
    if not accommodations:
        return "Nenhuma acomodação encontrada."

    result = f"📋 Encontradas {len(accommodations)} acomodação(ões):\n\n"
    for acc in accommodations:
        result += f"• {acc.get('name', 'N/A')} - {acc.get('type', 'N/A')} (ID: {acc.get('id', 'N/A')})\n"
    return result.strip()


def format_error(error: Exception) -> str:
    """
    Formata uma mensagem de erro.

    Args:
        error: Exceção capturada.

    Returns:
        String formatada com a mensagem de erro.
    """
    return f"❌ Erro: {str(error)}"


def format_success(message: str) -> str:
    """
    Formata uma mensagem de sucesso.

    Args:
        message: Mensagem a ser formatada.

    Returns:
        String formatada com ícone de sucesso.
    """
    return f"✅ {message}"
