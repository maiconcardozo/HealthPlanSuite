@echo off
setlocal enabledelayedexpansion

REM HealthPlanSuite Tests Runner para Windows
REM Este script facilita a execução dos testes do projeto HealthPlanSuite

echo 🧪 HealthPlanSuite Tests Runner
echo ================================

REM Função para mostrar ajuda
if "%1"=="help" goto :show_help
if "%1"=="-h" goto :show_help
if "%1"=="--help" goto :show_help

REM Verificar se o projeto de testes existe
if not exist "Src\HealthPlan.Test\HealthPlan.Test.csproj" (
    echo ❌ Projeto de testes não encontrado!
    echo Verifique se você está na raiz do projeto HealthPlanSuite.
    exit /b 1
)

REM Restaurar dependências se necessário
if not exist "Src\HealthPlan.Test\bin" (
    echo 📦 Restaurando dependências...
    dotnet restore Solution\HealthPlan.sln
)

REM Processar argumentos
set "command=%1"
if "%command%"=="" set "command=all"

if "%command%"=="all" goto :run_all
if "%command%"=="integration" goto :run_integration
if "%command%"=="unit" goto :run_unit
if "%command%"=="coverage" goto :run_coverage
if "%command%"=="watch" goto :run_watch
if "%command%"=="verbose" goto :run_verbose
if "%command%"=="clean" goto :run_clean

echo ❌ Opção inválida: %1
echo.
goto :show_help

:run_all
echo 🎯 Executando todos os testes...
echo 🏃 Executando: dotnet test Src\HealthPlan.Test\HealthPlan.Test.csproj
echo.
dotnet test Src\HealthPlan.Test\HealthPlan.Test.csproj
if %errorlevel% equ 0 (
    echo.
    echo ✅ Testes executados com sucesso!
) else (
    echo.
    echo ❌ Alguns testes falharam!
    exit /b 1
)
goto :end

:run_integration
echo 🔗 Executando testes de integração...
echo 🏃 Executando: dotnet test com filtro Integration
echo.
dotnet test Src\HealthPlan.Test\HealthPlan.Test.csproj --filter "FullyQualifiedName~Integration"
if %errorlevel% equ 0 (
    echo.
    echo ✅ Testes executados com sucesso!
) else (
    echo.
    echo ❌ Alguns testes falharam!
    exit /b 1
)
goto :end

:run_unit
echo 🧩 Executando testes unitários...
echo 🏃 Executando: dotnet test com filtro Unit
echo.
dotnet test Src\HealthPlan.Test\HealthPlan.Test.csproj --filter "FullyQualifiedName~Unit"
if %errorlevel% equ 0 (
    echo.
    echo ✅ Testes executados com sucesso!
) else (
    echo.
    echo ❌ Alguns testes falharam!
    exit /b 1
)
goto :end

:run_coverage
echo 📊 Executando testes com cobertura de código...
echo 🏃 Executando: dotnet test com cobertura
echo.
dotnet test Src\HealthPlan.Test\HealthPlan.Test.csproj --collect:"XPlat Code Coverage"
if %errorlevel% equ 0 (
    echo.
    echo ✅ Testes executados com sucesso!
    echo 📈 Relatório de cobertura gerado em: TestResults\
) else (
    echo.
    echo ❌ Alguns testes falharam!
    exit /b 1
)
goto :end

:run_watch
echo 👀 Executando testes em modo watch...
echo Pressione Ctrl+C para parar
dotnet watch test Src\HealthPlan.Test\HealthPlan.Test.csproj
goto :end

:run_verbose
echo 📝 Executando testes com saída detalhada...
echo 🏃 Executando: dotnet test com verbosidade normal
echo.
dotnet test Src\HealthPlan.Test\HealthPlan.Test.csproj --verbosity normal
if %errorlevel% equ 0 (
    echo.
    echo ✅ Testes executados com sucesso!
) else (
    echo.
    echo ❌ Alguns testes falharam!
    exit /b 1
)
goto :end

:run_clean
echo 🧹 Limpando e reconstruindo...
dotnet clean Solution\HealthPlan.sln
dotnet build Solution\HealthPlan.sln
echo 🎯 Executando todos os testes...
echo 🏃 Executando: dotnet test
echo.
dotnet test Src\HealthPlan.Test\HealthPlan.Test.csproj
if %errorlevel% equ 0 (
    echo.
    echo ✅ Testes executados com sucesso!
) else (
    echo.
    echo ❌ Alguns testes falharam!
    exit /b 1
)
goto :end

:show_help
echo Uso: %0 [opção]
echo.
echo Opções:
echo   all           Executa todos os testes (padrão)
echo   integration   Executa apenas testes de integração
echo   unit          Executa apenas testes unitários
echo   coverage      Executa testes com cobertura de código
echo   watch         Executa testes em modo watch
echo   verbose       Executa testes com saída detalhada
echo   clean         Limpa e reconstrói antes de executar
echo   help          Mostra esta ajuda
echo.
echo Exemplos:
echo   %0                # Executa todos os testes
echo   %0 integration    # Executa apenas testes de integração
echo   %0 coverage       # Executa com cobertura de código
goto :end

:end
echo.
echo 🎉 Script executado com sucesso!