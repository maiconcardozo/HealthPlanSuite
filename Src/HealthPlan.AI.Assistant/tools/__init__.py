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
from .plancoverage_tools import (
    get_all_plancoverages,
    get_plancoverage_by_id,
    create_plancoverage,
    update_plancoverage,
    delete_plancoverage,
)
from .acceptancerule_tools import (
    get_all_acceptancerules,
    get_acceptancerule_by_id,
    create_acceptancerule,
    update_acceptancerule,
    delete_acceptancerule,
)
from .adhesionfee_tools import (
    get_all_adhesionfees,
    get_adhesionfee_by_id,
    create_adhesionfee,
    update_adhesionfee,
    delete_adhesionfee,
)
from .promotionaldiscount_tools import (
    get_all_promotionaldiscounts,
    get_promotionaldiscount_by_id,
    create_promotionaldiscount,
    update_promotionaldiscount,
    delete_promotionaldiscount,
)
from .procedurecoparticipation_tools import (
    get_all_procedurecoparticipations,
    get_procedurecoparticipation_by_id,
    create_procedurecoparticipation,
    update_procedurecoparticipation,
    delete_procedurecoparticipation,
)
from .planpricerange_tools import (
    get_all_planpriceranges,
    get_planpricerange_by_id,
    create_planpricerange,
    update_planpricerange,
    delete_planpricerange,
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
    # PlanCoverage tools
    get_all_plancoverages,
    get_plancoverage_by_id,
    create_plancoverage,
    update_plancoverage,
    delete_plancoverage,
    # AcceptanceRule tools
    get_all_acceptancerules,
    get_acceptancerule_by_id,
    create_acceptancerule,
    update_acceptancerule,
    delete_acceptancerule,
    # AdhesionFee tools
    get_all_adhesionfees,
    get_adhesionfee_by_id,
    create_adhesionfee,
    update_adhesionfee,
    delete_adhesionfee,
    # PromotionalDiscount tools
    get_all_promotionaldiscounts,
    get_promotionaldiscount_by_id,
    create_promotionaldiscount,
    update_promotionaldiscount,
    delete_promotionaldiscount,
    # ProcedureCoparticipation tools
    get_all_procedurecoparticipations,
    get_procedurecoparticipation_by_id,
    create_procedurecoparticipation,
    update_procedurecoparticipation,
    delete_procedurecoparticipation,
    # PlanPriceRange tools
    get_all_planpriceranges,
    get_planpricerange_by_id,
    create_planpricerange,
    update_planpricerange,
    delete_planpricerange,
]

__all__ = ["ALL_TOOLS"]
