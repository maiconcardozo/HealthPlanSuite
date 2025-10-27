"""
Templates de prompts para o agente LangChain.

Este módulo contém todos os prompts utilizados pelo agente
para processar requisições e gerar respostas.
"""

SYSTEM_PROMPT = """Você é um assistente especializado em gerenciar o sistema HealthPlan Suite.

Sua função é ajudar usuários a realizar operações CRUD (Create, Read, Update, Delete) nas seguintes entidades:
- Companies (Empresas)
- HealthPlans (Planos de Saúde)
- Beneficiaries (Beneficiários)
- Quotes (Cotações)
- Coverages (Coberturas)
- AgeRanges (Faixas Etárias)
- Accommodations (Acomodações)

Você tem acesso a ferramentas (tools) que permitem interagir com a API REST do HealthPlan Suite.

INSTRUÇÕES IMPORTANTES:
1. Sempre responda em português (PT-BR)
2. Seja objetivo e claro nas respostas
3. Se não tiver certeza sobre algo, pergunte ao usuário
4. Use as ferramentas disponíveis para buscar dados reais
5. Formate as respostas de forma legível e organizada
6. Se uma operação falhar, explique o motivo de forma clara
7. Ao criar ou atualizar entidades, valide os dados antes de enviar

EXEMPLOS DE INTERAÇÃO:

Usuário: "liste todas as empresas"
Você: [Usa a ferramenta get_all_companies e formata a resposta]

Usuário: "crie uma empresa chamada Unimed"
Você: [Pergunta os dados necessários ou usa a ferramenta create_company se tiver todos os dados]

Usuário: "qual o plano mais barato?"
Você: [Usa get_all_healthplans e analisa os preços]

Sempre que possível, forneça IDs dos recursos nas respostas para facilitar operações futuras.
"""

WELCOME_MESSAGE = """
🤖 Bem-vindo ao HealthPlan.AI.Assistant!

Eu posso ajudá-lo a gerenciar:
• 🏢 Empresas (Companies)
• 💼 Planos de Saúde (HealthPlans)
• 👥 Beneficiários (Beneficiaries)
• 📋 Cotações (Quotes)
• 🛡️ Coberturas (Coverages)
• 📊 Faixas Etárias (AgeRanges)
• 🏨 Acomodações (Accommodations)

Digite sua pergunta ou comando em linguagem natural.
Para ajuda, digite 'ajuda'. Para sair, digite 'sair'.
""".strip()

HELP_MESSAGE = """
📚 Comandos e Exemplos

COMANDOS ESPECIAIS:
• ajuda / help     - Mostra esta mensagem
• limpar / clear   - Limpa o histórico da conversa
• resetar / reset  - Reinicia o agente
• sair / exit      - Sai do programa

EXEMPLOS DE COMANDOS:

📋 LISTAR:
• "liste todas as empresas"
• "mostre os planos de saúde"
• "quais são as coberturas disponíveis?"

🔍 BUSCAR:
• "mostre detalhes da empresa 1"
• "informações do plano de saúde com ID 5"
• "busque o beneficiário 10"

➕ CRIAR:
• "crie uma nova empresa chamada Unimed"
• "adicione um plano de saúde"
• "cadastre um novo beneficiário"

✏️ ATUALIZAR:
• "atualize o nome da empresa 1 para Bradesco Saúde"
• "altere o preço do plano 5"
• "modifique os dados do beneficiário 10"

❌ DELETAR:
• "delete a empresa 3"
• "remova o plano de saúde 7"
• "apague a cobertura 2"

💡 DICA: Seja específico nos seus comandos e forneça IDs quando possível!
""".strip()

ERROR_MESSAGES = {
    "api_unavailable": "❌ Erro: A API não está acessível. Verifique se o servidor está rodando.",
    "invalid_input": "❌ Erro: Entrada inválida. Por favor, forneça os dados corretos.",
    "not_found": "❌ Erro: Recurso não encontrado.",
    "general_error": "❌ Erro inesperado. Por favor, tente novamente.",
    "authentication_error": "❌ Erro de autenticação. Verifique suas credenciais.",
}

SUCCESS_MESSAGES = {
    "created": "✅ Recurso criado com sucesso!",
    "updated": "✅ Recurso atualizado com sucesso!",
    "deleted": "✅ Recurso deletado com sucesso!",
}
