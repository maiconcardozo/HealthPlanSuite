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


def format_plancoverage(plancoverage: Dict[str, Any]) -> str:
    """Formata os dados de uma cobertura de plano."""
    return f"""
🔗 Cobertura de Plano
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
ID: {plancoverage.get('id', 'N/A')}
Plano de Saúde ID: {plancoverage.get('healthPlanId', 'N/A')}
Cobertura ID: {plancoverage.get('coverageId', 'N/A')}
Valor Premium: R$ {plancoverage.get('premiumValue', 'N/A')}
Incluída: {'Sim' if plancoverage.get('isIncluded') else 'Não'}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    """.strip()


def format_plancoverages(plancoverages: List[Dict[str, Any]]) -> str:
    """Formata uma lista de coberturas de planos."""
    if not plancoverages:
        return "Nenhuma cobertura de plano encontrada."

    result = f"📋 Encontradas {len(plancoverages)} cobertura(s) de plano:\n\n"
    for pc in plancoverages:
        premium = pc.get('premiumValue', 'N/A')
        result += f"• Plano {pc.get('healthPlanId', 'N/A')} - Cobertura {pc.get('coverageId', 'N/A')} - R$ {premium} (ID: {pc.get('id', 'N/A')})\n"
    return result.strip()


def format_acceptancerule(acceptancerule: Dict[str, Any]) -> str:
    """Formata os dados de uma regra de aceitação."""
    return f"""
📋 Regra de Aceitação
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
ID: {acceptancerule.get('id', 'N/A')}
Plano de Saúde ID: {acceptancerule.get('healthPlanId', 'N/A')}
Tipo de Regra: {acceptancerule.get('ruleType', 'N/A')}
Operador: {acceptancerule.get('operator', 'N/A')}
Valor Mínimo: {acceptancerule.get('minValue', 'N/A')}
Valor Máximo: {acceptancerule.get('maxValue', 'N/A')}
Descrição: {acceptancerule.get('description', 'N/A')}
Obrigatória: {'Sim' if acceptancerule.get('isMandatory') else 'Não'}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    """.strip()


def format_acceptancerules(acceptancerules: List[Dict[str, Any]]) -> str:
    """Formata uma lista de regras de aceitação."""
    if not acceptancerules:
        return "Nenhuma regra de aceitação encontrada."

    result = f"📋 Encontradas {len(acceptancerules)} regra(s) de aceitação:\n\n"
    for ar in acceptancerules:
        result += f"• {ar.get('ruleType', 'N/A')} - Plano {ar.get('healthPlanId', 'N/A')} (ID: {ar.get('id', 'N/A')})\n"
    return result.strip()


def format_adhesionfee(adhesionfee: Dict[str, Any]) -> str:
    """Formata os dados de uma taxa de adesão."""
    return f"""
💵 Taxa de Adesão
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
ID: {adhesionfee.get('id', 'N/A')}
Plano de Saúde ID: {adhesionfee.get('healthPlanId', 'N/A')}
Valor: R$ {adhesionfee.get('value', 'N/A')}
Validade Início: {adhesionfee.get('validityStart', 'N/A')}
Validade Fim: {adhesionfee.get('validityEnd', 'N/A')}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    """.strip()


def format_adhesionfees(adhesionfees: List[Dict[str, Any]]) -> str:
    """Formata uma lista de taxas de adesão."""
    if not adhesionfees:
        return "Nenhuma taxa de adesão encontrada."

    result = f"📋 Encontradas {len(adhesionfees)} taxa(s) de adesão:\n\n"
    for af in adhesionfees:
        value = af.get('value', 'N/A')
        result += f"• Plano {af.get('healthPlanId', 'N/A')} - R$ {value} (ID: {af.get('id', 'N/A')})\n"
    return result.strip()


def format_promotionaldiscount(promotionaldiscount: Dict[str, Any]) -> str:
    """Formata os dados de um desconto promocional."""
    return f"""
🎁 Desconto Promocional
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
ID: {promotionaldiscount.get('id', 'N/A')}
Plano de Saúde ID: {promotionaldiscount.get('healthPlanId', 'N/A')}
Percentual de Desconto: {promotionaldiscount.get('discountPercentage', 'N/A')}%
Validade Início: {promotionaldiscount.get('validityStart', 'N/A')}
Validade Fim: {promotionaldiscount.get('validityEnd', 'N/A')}
Observação: {promotionaldiscount.get('observation', 'N/A')}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    """.strip()


def format_promotionaldiscounts(promotionaldiscounts: List[Dict[str, Any]]) -> str:
    """Formata uma lista de descontos promocionais."""
    if not promotionaldiscounts:
        return "Nenhum desconto promocional encontrado."

    result = f"📋 Encontrados {len(promotionaldiscounts)} desconto(s) promocional(is):\n\n"
    for pd in promotionaldiscounts:
        discount = pd.get('discountPercentage', 'N/A')
        result += f"• Plano {pd.get('healthPlanId', 'N/A')} - {discount}% (ID: {pd.get('id', 'N/A')})\n"
    return result.strip()


def format_procedurecoparticipation(procedurecoparticipation: Dict[str, Any]) -> str:
    """Formata os dados de uma coparticipação de procedimento."""
    return f"""
🏥 Coparticipação de Procedimento
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
ID: {procedurecoparticipation.get('id', 'N/A')}
Plano de Saúde ID: {procedurecoparticipation.get('healthPlanId', 'N/A')}
Tipo de Coparticipação: {procedurecoparticipation.get('coparticipationType', 'N/A')}
Procedimento: {procedurecoparticipation.get('procedure', 'N/A')}
Valor: R$ {procedurecoparticipation.get('value', 'N/A')}
Limite: {procedurecoparticipation.get('limit', 'N/A')}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    """.strip()


def format_procedurecoparticipations(procedurecoparticipations: List[Dict[str, Any]]) -> str:
    """Formata uma lista de coparticipações de procedimentos."""
    if not procedurecoparticipations:
        return "Nenhuma coparticipação de procedimento encontrada."

    result = f"📋 Encontradas {len(procedurecoparticipations)} coparticipação(ões) de procedimento:\n\n"
    for pc in procedurecoparticipations:
        procedure = pc.get('procedure', 'N/A')
        result += f"• {procedure} - Plano {pc.get('healthPlanId', 'N/A')} (ID: {pc.get('id', 'N/A')})\n"
    return result.strip()


def format_planpricerange(planpricerange: Dict[str, Any]) -> str:
    """Formata os dados de uma faixa de preços de plano."""
    return f"""
💰 Faixa de Preços de Plano
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
ID: {planpricerange.get('id', 'N/A')}
Plano de Saúde ID: {planpricerange.get('healthPlanId', 'N/A')}
Faixa Etária ID: {planpricerange.get('ageRangeId', 'N/A')}
Tipo de Contrato: {planpricerange.get('contractType', 'N/A')}
Tipo de Coparticipação: {planpricerange.get('coparticipationType', 'N/A')}
Valor Original: R$ {planpricerange.get('originalValue', 'N/A')}
Valor Desconto: R$ {planpricerange.get('discountValue', 'N/A')}
Validade Início: {planpricerange.get('validityStart', 'N/A')}
Validade Fim: {planpricerange.get('validityEnd', 'N/A')}
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
    """.strip()


def format_planpriceranges(planpriceranges: List[Dict[str, Any]]) -> str:
    """Formata uma lista de faixas de preços de planos."""
    if not planpriceranges:
        return "Nenhuma faixa de preços de plano encontrada."

    result = f"📋 Encontradas {len(planpriceranges)} faixa(s) de preços de plano:\n\n"
    for ppr in planpriceranges:
        original = ppr.get('originalValue', 'N/A')
        result += f"• Plano {ppr.get('healthPlanId', 'N/A')} - R$ {original} (ID: {ppr.get('id', 'N/A')})\n"
    return result.strip()
