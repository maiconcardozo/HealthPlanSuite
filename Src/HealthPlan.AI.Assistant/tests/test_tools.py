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
