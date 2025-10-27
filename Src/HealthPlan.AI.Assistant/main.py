"""
Ponto de entrada principal do HealthPlan.AI.Assistant.

Este módulo inicia a interface CLI do assistente.
"""

import sys
from interfaces.cli import main

if __name__ == "__main__":
    try:
        main()
    except KeyboardInterrupt:
        print("\n\n👋 Programa interrompido pelo usuário.")
        sys.exit(0)
    except Exception as e:
        print(f"\n❌ Erro fatal: {e}")
        sys.exit(1)
