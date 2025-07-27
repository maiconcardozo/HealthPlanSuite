#!/bin/bash

# Authentication Tests Runner
# Este script facilita a execução dos testes do projeto Authentication

set -e

echo "🧪 Authentication Tests Runner"
echo "================================"

# Função para mostrar ajuda
show_help() {
    echo "Uso: $0 [opção]"
    echo ""
    echo "Opções:"
    echo "  all           Executa todos os testes (padrão)"
    echo "  integration   Executa apenas testes de integração"
    echo "  unit          Executa apenas testes unitários"
    echo "  coverage      Executa testes com cobertura de código"
    echo "  watch         Executa testes em modo watch"
    echo "  verbose       Executa testes com saída detalhada"
    echo "  clean         Limpa e reconstrói antes de executar"
    echo "  help          Mostra esta ajuda"
    echo ""
    echo "Exemplos:"
    echo "  $0                # Executa todos os testes"
    echo "  $0 integration    # Executa apenas testes de integração"
    echo "  $0 coverage       # Executa com cobertura de código"
}

# Navegar para o diretório do projeto
cd "$(dirname "$0")"

# Verificar se o projeto de testes existe
if [ ! -f "Src/Authentication.Tests/Authentication.Tests.csproj" ]; then
    echo "❌ Projeto de testes não encontrado!"
    echo "Verifique se você está na raiz do projeto Authentication."
    exit 1
fi

# Restaurar dependências se necessário
if [ ! -d "Src/Authentication.Tests/bin" ]; then
    echo "📦 Restaurando dependências..."
    dotnet restore Solution/Authentication.sln
fi

# Função para executar testes
run_tests() {
    local test_command="$1"
    echo "🏃 Executando: $test_command"
    echo ""
    
    if eval "$test_command"; then
        echo ""
        echo "✅ Testes executados com sucesso!"
    else
        echo ""
        echo "❌ Alguns testes falharam!"
        exit 1
    fi
}

# Processar argumentos
case "${1:-all}" in
    "all")
        echo "🎯 Executando todos os testes..."
        run_tests "dotnet test Src/Authentication.Tests/Authentication.Tests.csproj"
        ;;
    
    "integration")
        echo "🔗 Executando testes de integração..."
        run_tests "dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --filter \"FullyQualifiedName~Integration\""
        ;;
    
    "unit")
        echo "🧩 Executando testes unitários..."
        run_tests "dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --filter \"FullyQualifiedName~Unit\""
        ;;
    
    "coverage")
        echo "📊 Executando testes com cobertura de código..."
        run_tests "dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --collect:\"XPlat Code Coverage\""
        echo ""
        echo "📈 Relatório de cobertura gerado em: TestResults/"
        ;;
    
    "watch")
        echo "👀 Executando testes em modo watch..."
        echo "Pressione Ctrl+C para parar"
        dotnet watch test Src/Authentication.Tests/Authentication.Tests.csproj
        ;;
    
    "verbose")
        echo "📝 Executando testes com saída detalhada..."
        run_tests "dotnet test Src/Authentication.Tests/Authentication.Tests.csproj --verbosity normal"
        ;;
    
    "clean")
        echo "🧹 Limpando e reconstruindo..."
        dotnet clean Solution/Authentication.sln
        dotnet build Solution/Authentication.sln
        echo "🎯 Executando todos os testes..."
        run_tests "dotnet test Src/Authentication.Tests/Authentication.Tests.csproj"
        ;;
    
    "help"|"-h"|"--help")
        show_help
        ;;
    
    *)
        echo "❌ Opção inválida: $1"
        echo ""
        show_help
        exit 1
        ;;
esac

echo ""
echo "🎉 Script executado com sucesso!"