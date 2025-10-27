"""
Testes para o módulo utils.api_client.

Este módulo testa o cliente HTTP que interage com a API do HealthPlan Suite.
"""

import pytest
from unittest.mock import Mock, patch, MagicMock
from utils.api_client import HealthPlanAPIClient


class TestHealthPlanAPIClient:
    """Testes para a classe HealthPlanAPIClient."""

    def test_initialization(self):
        """Testa a inicialização do cliente."""
        client = HealthPlanAPIClient()
        assert client.base_url is not None
        assert client.timeout > 0
        assert client.session is not None

    def test_initialization_with_custom_params(self):
        """Testa inicialização com parâmetros customizados."""
        client = HealthPlanAPIClient(
            base_url="http://custom-url.com/api", timeout=60
        )
        assert client.base_url == "http://custom-url.com/api"
        assert client.timeout == 60

    @patch("utils.api_client.requests.Session.request")
    def test_make_request_success(self, mock_request):
        """Testa requisição bem-sucedida."""
        # Configurar mock
        mock_response = Mock()
        mock_response.status_code = 200
        mock_response.json.return_value = {"id": 1, "name": "Teste"}
        mock_response.content = b'{"id": 1}'
        mock_request.return_value = mock_response

        # Executar
        client = HealthPlanAPIClient()
        result = client._make_request("GET", "test/endpoint")

        # Verificar
        assert result == {"id": 1, "name": "Teste"}
        mock_request.assert_called_once()

    @patch("utils.api_client.requests.get")
    def test_health_check_success(self, mock_get):
        """Testa verificação de saúde bem-sucedida."""
        mock_response = Mock()
        mock_response.status_code = 200
        mock_get.return_value = mock_response

        client = HealthPlanAPIClient()
        result = client.health_check()

        assert result is True

    @patch("utils.api_client.requests.get")
    def test_health_check_failure(self, mock_get):
        """Testa verificação de saúde com falha."""
        mock_get.side_effect = Exception("Connection error")

        client = HealthPlanAPIClient()
        result = client.health_check()

        assert result is False

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_get_companies(self, mock_request):
        """Testa busca de todas as empresas."""
        mock_request.return_value = [{"id": 1, "name": "Empresa 1"}]

        client = HealthPlanAPIClient()
        result = client.get_companies()

        assert len(result) == 1
        assert result[0]["name"] == "Empresa 1"
        mock_request.assert_called_once_with("GET", "Company/GetCompanies")

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_get_company(self, mock_request):
        """Testa busca de empresa por ID."""
        mock_request.return_value = {"id": 1, "name": "Empresa Teste"}

        client = HealthPlanAPIClient()
        result = client.get_company(1)

        assert result["id"] == 1
        assert result["name"] == "Empresa Teste"
        mock_request.assert_called_once_with("GET", "Company/GetCompanyById/1")

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_create_company(self, mock_request):
        """Testa criação de empresa."""
        mock_request.return_value = {"id": 1, "name": "Nova Empresa"}
        data = {"name": "Nova Empresa", "cnpj": "12345678901234"}

        client = HealthPlanAPIClient()
        result = client.create_company(data)

        assert result["id"] == 1
        mock_request.assert_called_once_with(
            "POST", "Company/CreateCompany", data=data
        )

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_update_company(self, mock_request):
        """Testa atualização de empresa."""
        mock_request.return_value = {"id": 1, "name": "Empresa Atualizada"}
        data = {"name": "Empresa Atualizada"}

        client = HealthPlanAPIClient()
        result = client.update_company(1, data)

        assert result["name"] == "Empresa Atualizada"
        mock_request.assert_called_once_with(
            "PUT", "Company/UpdateCompany/1", data=data
        )

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_delete_company(self, mock_request):
        """Testa exclusão de empresa."""
        mock_request.return_value = {}

        client = HealthPlanAPIClient()
        result = client.delete_company(1)

        assert result == {}
        mock_request.assert_called_once_with("DELETE", "Company/DeleteCompany/1")

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_get_healthplans(self, mock_request):
        """Testa busca de todos os planos de saúde."""
        mock_request.return_value = [{"id": 1, "name": "Plano 1"}]

        client = HealthPlanAPIClient()
        result = client.get_healthplans()

        assert len(result) == 1
        mock_request.assert_called_once_with("GET", "HealthPlan/GetHealthPlans")

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_get_beneficiaries(self, mock_request):
        """Testa busca de todos os beneficiários."""
        mock_request.return_value = [{"id": 1, "name": "João"}]

        client = HealthPlanAPIClient()
        result = client.get_beneficiaries()

        assert len(result) == 1
        mock_request.assert_called_once_with("GET", "Beneficiary/GetBeneficiaries")

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_get_quotes(self, mock_request):
        """Testa busca de todas as cotações."""
        mock_request.return_value = [{"id": 1, "totalValue": 500.0}]

        client = HealthPlanAPIClient()
        result = client.get_quotes()

        assert len(result) == 1
        mock_request.assert_called_once_with("GET", "Quote/GetQuotes")

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_get_coverages(self, mock_request):
        """Testa busca de todas as coberturas."""
        mock_request.return_value = [{"id": 1, "name": "Cobertura 1"}]

        client = HealthPlanAPIClient()
        result = client.get_coverages()

        assert len(result) == 1
        mock_request.assert_called_once_with("GET", "Coverage/GetCoverages")

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_get_ageranges(self, mock_request):
        """Testa busca de todas as faixas etárias."""
        mock_request.return_value = [{"id": 1, "minAge": 0, "maxAge": 18}]

        client = HealthPlanAPIClient()
        result = client.get_ageranges()

        assert len(result) == 1
        mock_request.assert_called_once_with("GET", "AgeRange/GetAgeRanges")

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_get_accommodations(self, mock_request):
        """Testa busca de todas as acomodações."""
        mock_request.return_value = [{"id": 1, "name": "Quarto Simples"}]

        client = HealthPlanAPIClient()
        result = client.get_accommodations()

        assert len(result) == 1
        mock_request.assert_called_once_with("GET", "Accommodation/GetAccommodations")

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_get_plancoverages(self, mock_request):
        """Testa busca de todas as coberturas de planos."""
        mock_request.return_value = [{"id": 1, "healthPlanId": 1}]

        client = HealthPlanAPIClient()
        result = client.get_plancoverages()

        assert len(result) == 1
        mock_request.assert_called_once_with("GET", "PlanCoverage/plan-coverages")

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_get_acceptancerules(self, mock_request):
        """Testa busca de todas as regras de aceitação."""
        mock_request.return_value = [{"id": 1, "ruleType": "Age"}]

        client = HealthPlanAPIClient()
        result = client.get_acceptancerules()

        assert len(result) == 1
        mock_request.assert_called_once_with("GET", "AcceptanceRule")

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_get_adhesionfees(self, mock_request):
        """Testa busca de todas as taxas de adesão."""
        mock_request.return_value = [{"id": 1, "value": 150.0}]

        client = HealthPlanAPIClient()
        result = client.get_adhesionfees()

        assert len(result) == 1
        mock_request.assert_called_once_with("GET", "AdhesionFee")

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_get_promotionaldiscounts(self, mock_request):
        """Testa busca de todos os descontos promocionais."""
        mock_request.return_value = [{"id": 1, "discountPercentage": 10.0}]

        client = HealthPlanAPIClient()
        result = client.get_promotionaldiscounts()

        assert len(result) == 1
        mock_request.assert_called_once_with("GET", "PromotionalDiscount")

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_get_procedurecoparticipations(self, mock_request):
        """Testa busca de todas as coparticipações de procedimentos."""
        mock_request.return_value = [{"id": 1, "procedure": "Consulta"}]

        client = HealthPlanAPIClient()
        result = client.get_procedurecoparticipations()

        assert len(result) == 1
        mock_request.assert_called_once_with("GET", "ProcedureCoparticipation")

    @patch.object(HealthPlanAPIClient, "_make_request")
    def test_get_planpriceranges(self, mock_request):
        """Testa busca de todas as faixas de preços de planos."""
        mock_request.return_value = [{"id": 1, "originalValue": 500.0}]

        client = HealthPlanAPIClient()
        result = client.get_planpriceranges()

        assert len(result) == 1
        mock_request.assert_called_once_with("GET", "PlanPriceRange")
