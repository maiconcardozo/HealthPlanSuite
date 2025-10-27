"""
Interface de linha de comando (CLI) para o HealthPlan.AI.Assistant.

Este módulo implementa a interface interativa do usuário via terminal.
"""

import sys
from colorama import init, Fore, Style

from config.settings import settings
from config.prompts import WELCOME_MESSAGE, HELP_MESSAGE
from agents import HealthPlanAgent
from utils.api_client import HealthPlanAPIClient

# Inicializar colorama para cores no terminal
init(autoreset=True)


class HealthPlanCLI:
    """Interface de linha de comando para o assistente."""

    def __init__(self):
        """Inicializa a CLI."""
        self.agent = None
        self.api_client = HealthPlanAPIClient()
        self.running = False

    def print_banner(self) -> None:
        """Exibe o banner de boas-vindas."""
        print(Fore.CYAN + "=" * 60)
        print(Fore.CYAN + WELCOME_MESSAGE)
        print(Fore.CYAN + "=" * 60)
        print()

    def check_api_health(self) -> bool:
        """
        Verifica se a API está acessível.

        Returns:
            bool: True se a API está OK, False caso contrário.
        """
        print(Fore.YELLOW + "🔍 Verificando conexão com a API...")
        if self.api_client.health_check():
            print(Fore.GREEN + "✅ API está respondendo!")
            return True
        else:
            print(Fore.RED + "❌ Erro: API não está acessível.")
            print(Fore.YELLOW + f"Verifique se a API está rodando em: {settings.API_BASE_URL}")
            return False

    def initialize_agent(self) -> bool:
        """
        Inicializa o agente LangChain.

        Returns:
            bool: True se inicializado com sucesso, False caso contrário.
        """
        print(Fore.YELLOW + "🤖 Inicializando agente...")
        try:
            self.agent = HealthPlanAgent()
            print(Fore.GREEN + "✅ Agente inicializado com sucesso!")
            print()
            print(Fore.CYAN + settings.display())
            print()
            return True
        except Exception as e:
            print(Fore.RED + f"❌ Erro ao inicializar agente: {e}")
            return False

    def process_special_command(self, user_input: str) -> bool:
        """
        Processa comandos especiais da CLI.

        Args:
            user_input: Entrada do usuário.

        Returns:
            bool: True se foi um comando especial, False caso contrário.
        """
        command = user_input.lower().strip()

        if command in ["sair", "exit", "quit"]:
            print(Fore.CYAN + "\n👋 Até logo!")
            self.running = False
            return True

        elif command in ["ajuda", "help"]:
            print(Fore.CYAN + "\n" + HELP_MESSAGE + "\n")
            return True

        elif command in ["limpar", "clear"]:
            if self.agent:
                self.agent.clear_memory()
                print(Fore.GREEN + "✅ Histórico da conversa limpo!\n")
            return True

        elif command in ["resetar", "reset"]:
            if self.agent:
                self.agent.reset()
                print(Fore.GREEN + "✅ Agente reiniciado!\n")
            return True

        return False

    def run(self) -> None:
        """Executa o loop principal da CLI."""
        self.print_banner()

        # Verificar saúde da API
        if not self.check_api_health():
            sys.exit(1)

        # Inicializar agente
        if not self.initialize_agent():
            sys.exit(1)

        # Loop principal
        self.running = True
        while self.running:
            try:
                # Ler entrada do usuário
                user_input = input(Fore.GREEN + "\n💬 Você: " + Style.RESET_ALL).strip()

                if not user_input:
                    continue

                # Processar comandos especiais
                if self.process_special_command(user_input):
                    continue

                # Processar com o agente
                print(Fore.YELLOW + "🤔 Pensando...")
                response = self.agent.run(user_input)

                # Exibir resposta
                print(Fore.BLUE + f"\n🤖 Assistente: {response}")

            except KeyboardInterrupt:
                print(Fore.CYAN + "\n\n👋 Até logo!")
                break

            except Exception as e:
                print(Fore.RED + f"\n❌ Erro inesperado: {e}")
                continue


def main() -> None:
    """Função principal para executar a CLI."""
    cli = HealthPlanCLI()
    cli.run()


if __name__ == "__main__":
    main()
