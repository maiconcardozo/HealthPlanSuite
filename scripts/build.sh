#!/bin/bash

# Authentication Build Script
# Este script facilita a compilação do projeto Authentication

set -e

echo "🏗️ Authentication Build Script"
echo "=============================="

# Função para mostrar ajuda
show_help() {
    echo "Uso: $0 [opção]"
    echo ""
    echo "Opções:"
    echo "  debug         Compila em modo Debug (padrão)"
    echo "  release       Compila em modo Release"
    echo "  clean         Limpa e reconstrói"
    echo "  restore       Apenas restaura dependências"
    echo "  verify        Verifica compilação e testes"
    echo "  help          Mostra esta ajuda"
    echo ""
    echo "Exemplos:"
    echo "  $0              # Compila em modo Debug"
    echo "  $0 release      # Compila em modo Release"
    echo "  $0 verify       # Verifica tudo funciona"
}

# Navegar para o diretório raiz do projeto
cd "$(dirname "$0")/.."

# Verificar se o arquivo de solução existe
if [ ! -f "Solution/CleanTemplate.sln" ]; then
    echo "❌ Arquivo de solução não encontrado!"
    echo "Verifique se você está na raiz do projeto CleanTemplate."
    exit 1
fi

# Verificar .NET 9.0
echo "🔍 Verificando versão do .NET..."
DOTNET_VERSION=$(dotnet --version 2>/dev/null || echo "not found")
if [[ ! "$DOTNET_VERSION" =~ ^9\. ]]; then
    echo "❌ .NET 9.0 SDK não encontrado!"
    echo "Versão atual: $DOTNET_VERSION"
    echo "Instale o .NET 9.0 SDK de: https://dotnet.microsoft.com/download/dotnet/9.0"
    exit 1
fi
echo "✅ .NET versão: $DOTNET_VERSION"

# Função para executar build
run_build() {
    local configuration="$1"
    echo "🏃 Compilando em modo $configuration..."
    echo ""
    
    if dotnet build Solution/CleanTemplate.sln --configuration "$configuration"; then
        echo ""
        echo "✅ Compilação concluída com sucesso!"
    else
        echo ""
        echo "❌ Falha na compilação!"
        exit 1
    fi
}

# Processar argumentos
case "${1:-debug}" in
    "debug")
        echo "🛠️ Restaurando dependências..."
        dotnet restore Solution/CleanTemplate.sln
        run_build "Debug"
        ;;
    
    "release")
        echo "🛠️ Restaurando dependências..."
        dotnet restore Solution/CleanTemplate.sln
        run_build "Release"
        ;;
    
    "clean")
        echo "🧹 Limpando projeto..."
        dotnet clean Solution/CleanTemplate.sln
        echo "🛠️ Restaurando dependências..."
        dotnet restore Solution/CleanTemplate.sln
        run_build "Debug"
        ;;
    
    "restore")
        echo "📦 Restaurando dependências..."
        if dotnet restore Solution/CleanTemplate.sln; then
            echo "✅ Dependências restauradas com sucesso!"
        else
            echo "❌ Falha ao restaurar dependências!"
            exit 1
        fi
        ;;
    
    "verify")
        echo "🔍 Verificação completa do projeto..."
        echo ""
        
        # Restaurar
        echo "📦 Restaurando dependências..."
        dotnet restore Solution/CleanTemplate.sln
        
        # Compilar Release
        run_build "Release"
        
        # Executar testes
        echo ""
        echo "🧪 Executando testes..."
        scripts/run-tests.sh all
        
        echo ""
        echo "🎉 Verificação completa bem-sucedida!"
        echo "✅ Projeto compila corretamente"
        echo "✅ Todos os testes passaram"
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