"""
Cliente HTTP para interagir com a API do HealthPlan Suite.

Este módulo fornece uma interface simplificada para todas as
operações CRUD das entidades do sistema.
"""

from typing import Any, Dict, List, Optional
import requests
from config.settings import settings


class HealthPlanAPIClient:
    """Cliente para interação com a API do HealthPlan Suite."""

    def __init__(self, base_url: Optional[str] = None, timeout: Optional[int] = None):
        """
        Inicializa o cliente da API.

        Args:
            base_url: URL base da API. Se None, usa settings.API_BASE_URL.
            timeout: Timeout para requisições. Se None, usa settings.API_TIMEOUT.
        """
        self.base_url = base_url or settings.API_BASE_URL
        self.timeout = timeout or settings.API_TIMEOUT
        self.session = requests.Session()

    def _make_request(
        self,
        method: str,
        endpoint: str,
        data: Optional[Dict[str, Any]] = None,
        params: Optional[Dict[str, Any]] = None,
    ) -> Dict[str, Any]:
        """
        Realiza uma requisição HTTP para a API.

        Args:
            method: Método HTTP (GET, POST, PUT, DELETE).
            endpoint: Endpoint da API (sem barra inicial).
            data: Dados para enviar no corpo da requisição.
            params: Parâmetros de query string.

        Returns:
            Dict contendo a resposta da API.

        Raises:
            requests.exceptions.RequestException: Se houver erro na requisição.
        """
        url = f"{self.base_url}/{endpoint}"
        response = self.session.request(
            method=method,
            url=url,
            json=data,
            params=params,
            timeout=self.timeout,
        )
        response.raise_for_status()
        return response.json() if response.content else {}

    def health_check(self) -> bool:
        """
        Verifica se a API está respondendo.

        Returns:
            bool: True se a API está acessível, False caso contrário.
        """
        try:
            response = requests.get(
                f"{self.base_url.replace('/api', '')}/health",
                timeout=5,
            )
            return response.status_code == 200
        except Exception:
            return False

    # ==================== COMPANIES ====================

    def get_companies(self) -> List[Dict[str, Any]]:
        """Busca todas as empresas."""
        return self._make_request("GET", "Company/GetCompanies")

    def get_company(self, company_id: int) -> Dict[str, Any]:
        """Busca uma empresa por ID."""
        return self._make_request("GET", f"Company/GetCompanyById/{company_id}")

    def create_company(self, data: Dict[str, Any]) -> Dict[str, Any]:
        """Cria uma nova empresa."""
        return self._make_request("POST", "Company/CreateCompany", data=data)

    def update_company(self, company_id: int, data: Dict[str, Any]) -> Dict[str, Any]:
        """Atualiza uma empresa existente."""
        return self._make_request("PUT", f"Company/UpdateCompany/{company_id}", data=data)

    def delete_company(self, company_id: int) -> Dict[str, Any]:
        """Deleta uma empresa."""
        return self._make_request("DELETE", f"Company/DeleteCompany/{company_id}")

    # ==================== HEALTHPLANS ====================

    def get_healthplans(self) -> List[Dict[str, Any]]:
        """Busca todos os planos de saúde."""
        return self._make_request("GET", "HealthPlan/GetHealthPlans")

    def get_healthplan(self, healthplan_id: int) -> Dict[str, Any]:
        """Busca um plano de saúde por ID."""
        return self._make_request("GET", f"HealthPlan/GetHealthPlanById/{healthplan_id}")

    def create_healthplan(self, data: Dict[str, Any]) -> Dict[str, Any]:
        """Cria um novo plano de saúde."""
        return self._make_request("POST", "HealthPlan/CreateHealthPlan", data=data)

    def update_healthplan(self, healthplan_id: int, data: Dict[str, Any]) -> Dict[str, Any]:
        """Atualiza um plano de saúde existente."""
        return self._make_request("PUT", f"HealthPlan/UpdateHealthPlan/{healthplan_id}", data=data)

    def delete_healthplan(self, healthplan_id: int) -> Dict[str, Any]:
        """Deleta um plano de saúde."""
        return self._make_request("DELETE", f"HealthPlan/DeleteHealthPlan/{healthplan_id}")

    # ==================== BENEFICIARIES ====================

    def get_beneficiaries(self) -> List[Dict[str, Any]]:
        """Busca todos os beneficiários."""
        return self._make_request("GET", "Beneficiary/GetBeneficiaries")

    def get_beneficiary(self, beneficiary_id: int) -> Dict[str, Any]:
        """Busca um beneficiário por ID."""
        return self._make_request("GET", f"Beneficiary/GetBeneficiaryById/{beneficiary_id}")

    def create_beneficiary(self, data: Dict[str, Any]) -> Dict[str, Any]:
        """Cria um novo beneficiário."""
        return self._make_request("POST", "Beneficiary/CreateBeneficiary", data=data)

    def update_beneficiary(self, beneficiary_id: int, data: Dict[str, Any]) -> Dict[str, Any]:
        """Atualiza um beneficiário existente."""
        return self._make_request("PUT", f"Beneficiary/UpdateBeneficiary/{beneficiary_id}", data=data)

    def delete_beneficiary(self, beneficiary_id: int) -> Dict[str, Any]:
        """Deleta um beneficiário."""
        return self._make_request("DELETE", f"Beneficiary/DeleteBeneficiary/{beneficiary_id}")

    # ==================== QUOTES ====================

    def get_quotes(self) -> List[Dict[str, Any]]:
        """Busca todas as cotações."""
        return self._make_request("GET", "Quote/GetQuotes")

    def get_quote(self, quote_id: int) -> Dict[str, Any]:
        """Busca uma cotação por ID."""
        return self._make_request("GET", f"Quote/GetQuoteById/{quote_id}")

    def create_quote(self, data: Dict[str, Any]) -> Dict[str, Any]:
        """Cria uma nova cotação."""
        return self._make_request("POST", "Quote/CreateQuote", data=data)

    def update_quote(self, quote_id: int, data: Dict[str, Any]) -> Dict[str, Any]:
        """Atualiza uma cotação existente."""
        return self._make_request("PUT", f"Quote/UpdateQuote/{quote_id}", data=data)

    def delete_quote(self, quote_id: int) -> Dict[str, Any]:
        """Deleta uma cotação."""
        return self._make_request("DELETE", f"Quote/DeleteQuote/{quote_id}")

    # ==================== COVERAGES ====================

    def get_coverages(self) -> List[Dict[str, Any]]:
        """Busca todas as coberturas."""
        return self._make_request("GET", "Coverage/GetCoverages")

    def get_coverage(self, coverage_id: int) -> Dict[str, Any]:
        """Busca uma cobertura por ID."""
        return self._make_request("GET", f"Coverage/GetCoverageById/{coverage_id}")

    def create_coverage(self, data: Dict[str, Any]) -> Dict[str, Any]:
        """Cria uma nova cobertura."""
        return self._make_request("POST", "Coverage/CreateCoverage", data=data)

    def update_coverage(self, coverage_id: int, data: Dict[str, Any]) -> Dict[str, Any]:
        """Atualiza uma cobertura existente."""
        return self._make_request("PUT", f"Coverage/UpdateCoverage/{coverage_id}", data=data)

    def delete_coverage(self, coverage_id: int) -> Dict[str, Any]:
        """Deleta uma cobertura."""
        return self._make_request("DELETE", f"Coverage/DeleteCoverage/{coverage_id}")

    # ==================== AGERANGES ====================

    def get_ageranges(self) -> List[Dict[str, Any]]:
        """Busca todas as faixas etárias."""
        return self._make_request("GET", "AgeRange/GetAgeRanges")

    def get_agerange(self, agerange_id: int) -> Dict[str, Any]:
        """Busca uma faixa etária por ID."""
        return self._make_request("GET", f"AgeRange/GetAgeRangeById/{agerange_id}")

    def create_agerange(self, data: Dict[str, Any]) -> Dict[str, Any]:
        """Cria uma nova faixa etária."""
        return self._make_request("POST", "AgeRange/CreateAgeRange", data=data)

    def update_agerange(self, agerange_id: int, data: Dict[str, Any]) -> Dict[str, Any]:
        """Atualiza uma faixa etária existente."""
        return self._make_request("PUT", f"AgeRange/UpdateAgeRange/{agerange_id}", data=data)

    def delete_agerange(self, agerange_id: int) -> Dict[str, Any]:
        """Deleta uma faixa etária."""
        return self._make_request("DELETE", f"AgeRange/DeleteAgeRange/{agerange_id}")

    # ==================== ACCOMMODATIONS ====================

    def get_accommodations(self) -> List[Dict[str, Any]]:
        """Busca todas as acomodações."""
        return self._make_request("GET", "Accommodation/GetAccommodations")

    def get_accommodation(self, accommodation_id: int) -> Dict[str, Any]:
        """Busca uma acomodação por ID."""
        return self._make_request("GET", f"Accommodation/GetAccommodationById/{accommodation_id}")

    def create_accommodation(self, data: Dict[str, Any]) -> Dict[str, Any]:
        """Cria uma nova acomodação."""
        return self._make_request("POST", "Accommodation/CreateAccommodation", data=data)

    def update_accommodation(
        self, accommodation_id: int, data: Dict[str, Any]
    ) -> Dict[str, Any]:
        """Atualiza uma acomodação existente."""
        return self._make_request(
            "PUT", f"Accommodation/UpdateAccommodation/{accommodation_id}", data=data
        )

    def delete_accommodation(self, accommodation_id: int) -> Dict[str, Any]:
        """Deleta uma acomodação."""
        return self._make_request("DELETE", f"Accommodation/DeleteAccommodation/{accommodation_id}")
