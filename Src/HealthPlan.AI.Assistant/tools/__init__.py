"""Módulo de ferramentas LangChain para o HealthPlan.AI.Assistant."""

from .company_tools import (
    get_all_companies,
    get_company_by_id,
    create_company,
    update_company,
    delete_company,
)
from .healthplan_tools import (
    get_all_healthplans,
    get_healthplan_by_id,
    create_healthplan,
    update_healthplan,
    delete_healthplan,
)
from .beneficiary_tools import (
    get_all_beneficiaries,
    get_beneficiary_by_id,
    create_beneficiary,
    update_beneficiary,
    delete_beneficiary,
)
from .quote_tools import (
    get_all_quotes,
    get_quote_by_id,
    create_quote,
    update_quote,
    delete_quote,
)
from .coverage_tools import (
    get_all_coverages,
    get_coverage_by_id,
    create_coverage,
    update_coverage,
    delete_coverage,
)
from .agerange_tools import (
    get_all_ageranges,
    get_agerange_by_id,
    create_agerange,
    update_agerange,
    delete_agerange,
)
from .accommodation_tools import (
    get_all_accommodations,
    get_accommodation_by_id,
    create_accommodation,
    update_accommodation,
    delete_accommodation,
)

# Lista de todas as ferramentas disponíveis
ALL_TOOLS = [
    # Company tools
    get_all_companies,
    get_company_by_id,
    create_company,
    update_company,
    delete_company,
    # HealthPlan tools
    get_all_healthplans,
    get_healthplan_by_id,
    create_healthplan,
    update_healthplan,
    delete_healthplan,
    # Beneficiary tools
    get_all_beneficiaries,
    get_beneficiary_by_id,
    create_beneficiary,
    update_beneficiary,
    delete_beneficiary,
    # Quote tools
    get_all_quotes,
    get_quote_by_id,
    create_quote,
    update_quote,
    delete_quote,
    # Coverage tools
    get_all_coverages,
    get_coverage_by_id,
    create_coverage,
    update_coverage,
    delete_coverage,
    # AgeRange tools
    get_all_ageranges,
    get_agerange_by_id,
    create_agerange,
    update_agerange,
    delete_agerange,
    # Accommodation tools
    get_all_accommodations,
    get_accommodation_by_id,
    create_accommodation,
    update_accommodation,
    delete_accommodation,
]

__all__ = ["ALL_TOOLS"]
