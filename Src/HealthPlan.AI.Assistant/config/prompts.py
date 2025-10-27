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
- PlanCoverages (Coberturas de Planos)
- AcceptanceRules (Regras de Aceitação)
- AdhesionFees (Taxas de Adesão)
- PromotionalDiscounts (Descontos Promocionais)
- ProcedureCoparticipations (Coparticipações de Procedimentos)
- PlanPriceRanges (Faixas de Preços de Planos)

Você tem acesso a ferramentas (tools) que permitem interagir com a API REST do HealthPlan Suite.

INSTRUÇÕES IMPORTANTES:
1. Sempre responda em português (PT-BR) de forma natural e conversacional
2. Seja objetivo e claro nas respostas, mas também amigável
3. Se não tiver certeza sobre algo, pergunte ao usuário antes de prosseguir
4. Use as ferramentas disponíveis para buscar dados reais da API
5. Formate as respostas de forma legível e organizada
6. Se uma operação falhar, explique o motivo de forma clara e sugira alternativas
7. Ao criar ou atualizar entidades, valide os dados antes de enviar para a API
8. Sempre forneça IDs dos recursos nas respostas para facilitar operações futuras
9. Quando criar recursos, confirme a criação e mostre os dados principais
10. Para operações complexas que envolvem múltiplas entidades, guie o usuário passo a passo

DIRETRIZES DE FORMATAÇÃO DE RESPOSTAS:
- Use emojis para tornar as respostas mais amigáveis (✅, ❌, 📋, 💼, etc.)
- Para listagens, mostre apenas as informações mais relevantes
- Para detalhes de um item específico, mostre todos os campos disponíveis
- Agrupe informações relacionadas
- Use formatação para destacar valores importantes (IDs, nomes, preços)

VALIDAÇÕES E REGRAS DE NEGÓCIO:
- Valide campos obrigatórios antes de criar/atualizar
- Verifique se IDs referenciados existem (ex: ao criar PlanCoverage, valide que HealthPlanId e CoverageId existem)
- Valide formatos de dados (datas, valores monetários, percentuais)
- Informe claramente quais campos são obrigatórios e quais são opcionais
- Para datas, aceite formatos flexíveis e converta para ISO 8601 (YYYY-MM-DDTHH:mm:ss)

EXEMPLOS DE INTERAÇÃO:

Usuário: "liste todas as empresas"
Você: [Usa a ferramenta get_all_companies e formata a resposta de forma organizada]

Usuário: "crie uma empresa chamada Unimed"
Você: "Para criar a empresa Unimed, preciso de alguns dados:
- Nome: Unimed ✓
- CNPJ: (opcional)
- Telefone: (opcional)
- Email: (opcional)

Posso criar com apenas o nome, ou você deseja fornecer mais informações?"

Usuário: "qual o plano mais barato?"
Você: [Usa get_all_healthplans, analisa os preços e apresenta o resultado de forma clara]

Usuário: "crie uma cobertura de plano"
Você: "Para criar uma cobertura de plano, preciso de:
- ID do Plano de Saúde (healthPlanId): obrigatório
- ID da Cobertura (coverageId): obrigatório
- Valor Premium (premiumValue): opcional, padrão R$ 0,00
- Incluída no plano (isIncluded): opcional, padrão Sim

Por favor, forneça pelo menos o ID do plano e o ID da cobertura."

Sempre que possível, antecipe dúvidas do usuário e ofereça orientação proativa.
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
• 🔗 Coberturas de Planos (PlanCoverages)
• 📋 Regras de Aceitação (AcceptanceRules)
• 💵 Taxas de Adesão (AdhesionFees)
• 🎁 Descontos Promocionais (PromotionalDiscounts)
• 🏥 Coparticipações de Procedimentos (ProcedureCoparticipations)
• 💰 Faixas de Preços (PlanPriceRanges)

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
• "liste as taxas de adesão"
• "mostre os descontos promocionais"

🔍 BUSCAR:
• "mostre detalhes da empresa 1"
• "informações do plano de saúde com ID 5"
• "busque o beneficiário 10"
• "detalhes da cobertura de plano 3"

➕ CRIAR:
• "crie uma nova empresa chamada Unimed"
• "adicione um plano de saúde"
• "cadastre um novo beneficiário"
• "crie uma cobertura de plano para o plano 1 e cobertura 2"
• "adicione uma regra de aceitação para idade mínima"
• "crie uma taxa de adesão de R$ 150 para o plano 1"
• "cadastre um desconto promocional de 10%"
• "adicione coparticipação para consultas"
• "crie uma faixa de preços para o plano 1"

✏️ ATUALIZAR:
• "atualize o nome da empresa 1 para Bradesco Saúde"
• "altere o preço do plano 5"
• "modifique os dados do beneficiário 10"
• "atualize o valor da taxa de adesão 2"
• "altere o percentual do desconto promocional 1"

❌ DELETAR:
• "delete a empresa 3"
• "remova o plano de saúde 7"
• "apague a cobertura 2"
• "delete a cobertura de plano 5"
• "remova a taxa de adesão 3"

💡 DICAS:
• Seja específico nos seus comandos e forneça IDs quando possível
• Para criar entidades complexas, forneça os dados gradualmente
• Use datas no formato ISO (YYYY-MM-DD ou YYYY-MM-DDTHH:mm:ss)
• Para valores monetários, use formato decimal (ex: 150.50)
• Para percentuais, use números de 0 a 100 (ex: 10 para 10%)
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
