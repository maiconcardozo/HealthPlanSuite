"""
Testes para o módulo tools.

Este módulo testa as ferramentas LangChain que o agente utiliza.
"""

import pytest
from unittest.mock import patch, Mock
from tools.company_tools import (
    get_all_companies,
    get_company_by_id,
    create_company,
    update_company,
    delete_company,
)


class TestCompanyTools:
    """Testes para as ferramentas de empresas."""

    @patch("tools.company_tools.api_client")
    def test_get_all_companies_success(self, mock_client):
        """Testa busca de todas as empresas com sucesso."""
        mock_client.get_companies.return_value = [
            {"id": 1, "name": "Empresa 1"},
            {"id": 2, "name": "Empresa 2"},
        ]

        result = get_all_companies.invoke({})

        assert "Empresa 1" in result
        assert "Empresa 2" in result
        assert "2 empresa(s)" in result

    @patch("tools.company_tools.api_client")
    def test_get_all_companies_empty(self, mock_client):
        """Testa busca de empresas quando não há resultados."""
        mock_client.get_companies.return_value = []

        result = get_all_companies.invoke({})

        assert "Nenhuma empresa encontrada" in result

    @patch("tools.company_tools.api_client")
    def test_get_all_companies_error(self, mock_client):
        """Testa busca de empresas com erro."""
        mock_client.get_companies.side_effect = Exception("API Error")

        result = get_all_companies.invoke({})

        assert "❌ Erro" in result
        assert "API Error" in result

    @patch("tools.company_tools.api_client")
    def test_get_company_by_id_success(self, mock_client):
        """Testa busca de empresa por ID com sucesso."""
        mock_client.get_company.return_value = {
            "id": 1,
            "name": "Empresa Teste",
            "cnpj": "12345678901234",
        }

        result = get_company_by_id.invoke({"company_id": 1})

        assert "Empresa Teste" in result
        assert "12345678901234" in result

    @patch("tools.company_tools.api_client")
    def test_get_company_by_id_invalid_id(self, mock_client):
        """Testa busca de empresa com ID inválido."""
        result = get_company_by_id.invoke({"company_id": -1})

        assert "❌ Erro" in result
        assert "ID deve ser um número positivo" in result

    @patch("tools.company_tools.api_client")
    def test_create_company_success(self, mock_client):
        """Testa criação de empresa com sucesso."""
        mock_client.create_company.return_value = {"id": 1, "name": "Nova Empresa"}

        result = create_company.invoke(
            {"name": "Nova Empresa", "cnpj": "12345678901234"}
        )

        assert "✅" in result
        assert "criada com sucesso" in result
        assert "Nova Empresa" in result

    @patch("tools.company_tools.api_client")
    def test_create_company_missing_name(self, mock_client):
        """Testa criação de empresa sem nome."""
        result = create_company.invoke({"name": ""})

        assert "❌ Erro" in result
        assert "Campo obrigatório" in result

    @patch("tools.company_tools.api_client")
    def test_create_company_invalid_cnpj(self, mock_client):
        """Testa criação de empresa com CNPJ inválido."""
        result = create_company.invoke({"name": "Teste", "cnpj": "123"})

        assert "❌ Erro" in result
        assert "CNPJ inválido" in result

    @patch("tools.company_tools.api_client")
    def test_update_company_success(self, mock_client):
        """Testa atualização de empresa com sucesso."""
        mock_client.update_company.return_value = {}

        result = update_company.invoke({"company_id": 1, "name": "Nome Atualizado"})

        assert "✅" in result
        assert "atualizada com sucesso" in result

    @patch("tools.company_tools.api_client")
    def test_update_company_no_fields(self, mock_client):
        """Testa atualização sem fornecer campos."""
        result = update_company.invoke({"company_id": 1})

        assert "❌ Erro" in result
        assert "Nenhum campo fornecido" in result

    @patch("tools.company_tools.api_client")
    def test_delete_company_success(self, mock_client):
        """Testa exclusão de empresa com sucesso."""
        mock_client.delete_company.return_value = {}

        result = delete_company.invoke({"company_id": 1})

        assert "✅" in result
        assert "deletada com sucesso" in result

    @patch("tools.company_tools.api_client")
    def test_delete_company_invalid_id(self, mock_client):
        """Testa exclusão com ID inválido."""
        result = delete_company.invoke({"company_id": 0})

        assert "❌ Erro" in result
        assert "ID deve ser um número positivo" in result


class TestToolsMetadata:
    """Testa metadados das ferramentas."""

    def test_tool_has_name(self):
        """Verifica se as ferramentas têm nome."""
        assert get_all_companies.name == "get_all_companies"
        assert create_company.name == "create_company"

    def test_tool_has_description(self):
        """Verifica se as ferramentas têm descrição."""
        assert get_all_companies.description is not None
        assert len(get_all_companies.description) > 0

    def test_tool_is_callable(self):
        """Verifica se as ferramentas são chamáveis."""
        assert callable(get_all_companies.invoke)
        assert callable(create_company.invoke)


class TestPlanCoverageTools:
    """Testes para as ferramentas de coberturas de planos."""

    @patch("tools.plancoverage_tools.api_client")
    def test_get_all_plancoverages_success(self, mock_client):
        """Testa busca de todas as coberturas de planos com sucesso."""
        from tools.plancoverage_tools import get_all_plancoverages
        
        mock_client.get_plancoverages.return_value = [
            {"id": 1, "healthPlanId": 1, "coverageId": 1, "premiumValue": 50.0},
            {"id": 2, "healthPlanId": 1, "coverageId": 2, "premiumValue": 100.0},
        ]

        result = get_all_plancoverages.invoke({})

        assert "2 cobertura(s) de plano" in result
        assert mock_client.get_plancoverages.called

    @patch("tools.plancoverage_tools.api_client")
    def test_create_plancoverage_success(self, mock_client):
        """Testa criação de cobertura de plano com sucesso."""
        from tools.plancoverage_tools import create_plancoverage
        
        mock_client.create_plancoverage.return_value = {"id": 1}

        result = create_plancoverage.invoke({
            "health_plan_id": 1,
            "coverage_id": 1,
            "premium_value": 50.0,
        })

        assert "✅" in result
        assert "criada com sucesso" in result


class TestAcceptanceRuleTools:
    """Testes para as ferramentas de regras de aceitação."""

    @patch("tools.acceptancerule_tools.api_client")
    def test_get_all_acceptancerules_success(self, mock_client):
        """Testa busca de todas as regras de aceitação com sucesso."""
        from tools.acceptancerule_tools import get_all_acceptancerules
        
        mock_client.get_acceptancerules.return_value = [
            {"id": 1, "healthPlanId": 1, "ruleType": "Age", "operator": ">="},
        ]

        result = get_all_acceptancerules.invoke({})

        assert "1 regra(s) de aceitação" in result


class TestAdhesionFeeTools:
    """Testes para as ferramentas de taxas de adesão."""

    @patch("tools.adhesionfee_tools.api_client")
    def test_get_all_adhesionfees_success(self, mock_client):
        """Testa busca de todas as taxas de adesão com sucesso."""
        from tools.adhesionfee_tools import get_all_adhesionfees
        
        mock_client.get_adhesionfees.return_value = [
            {"id": 1, "healthPlanId": 1, "value": 150.0},
        ]

        result = get_all_adhesionfees.invoke({})

        assert "1 taxa(s) de adesão" in result


class TestPromotionalDiscountTools:
    """Testes para as ferramentas de descontos promocionais."""

    @patch("tools.promotionaldiscount_tools.api_client")
    def test_get_all_promotionaldiscounts_success(self, mock_client):
        """Testa busca de todos os descontos promocionais com sucesso."""
        from tools.promotionaldiscount_tools import get_all_promotionaldiscounts
        
        mock_client.get_promotionaldiscounts.return_value = [
            {"id": 1, "healthPlanId": 1, "discountPercentage": 10.0},
        ]

        result = get_all_promotionaldiscounts.invoke({})

        assert "1 desconto(s) promocional(is)" in result


class TestProcedureCoparticipationTools:
    """Testes para as ferramentas de coparticipações de procedimentos."""

    @patch("tools.procedurecoparticipation_tools.api_client")
    def test_get_all_procedurecoparticipations_success(self, mock_client):
        """Testa busca de todas as coparticipações de procedimentos com sucesso."""
        from tools.procedurecoparticipation_tools import get_all_procedurecoparticipations
        
        mock_client.get_procedurecoparticipations.return_value = [
            {"id": 1, "healthPlanId": 1, "procedure": "Consulta", "value": 30.0},
        ]

        result = get_all_procedurecoparticipations.invoke({})

        assert "1 coparticipação(ões) de procedimento" in result


class TestPlanPriceRangeTools:
    """Testes para as ferramentas de faixas de preços de planos."""

    @patch("tools.planpricerange_tools.api_client")
    def test_get_all_planpriceranges_success(self, mock_client):
        """Testa busca de todas as faixas de preços de planos com sucesso."""
        from tools.planpricerange_tools import get_all_planpriceranges
        
        mock_client.get_planpriceranges.return_value = [
            {"id": 1, "healthPlanId": 1, "ageRangeId": 1, "originalValue": 500.0},
        ]

        result = get_all_planpriceranges.invoke({})

        assert "1 faixa(s) de preços de plano" in result
