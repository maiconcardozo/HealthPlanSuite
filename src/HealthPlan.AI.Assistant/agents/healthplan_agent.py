"""
Agente LangChain para o HealthPlan.AI.Assistant.

Este módulo define o agente conversacional que processa
comandos em linguagem natural e executa ações via ferramentas.
"""

from typing import Optional
from langchain.agents import AgentExecutor, create_react_agent
from langchain.memory import ConversationBufferMemory
from langchain_community.llms import Ollama
from langchain_openai import ChatOpenAI
from langchain.prompts import PromptTemplate

from config.settings import settings
from config.prompts import SYSTEM_PROMPT
from tools import ALL_TOOLS


class HealthPlanAgent:
    """Agente conversacional para gerenciamento do HealthPlan Suite."""

    def __init__(self):
        """Inicializa o agente com LLM e ferramentas."""
        self.llm = self._initialize_llm()
        self.memory = ConversationBufferMemory(
            memory_key="chat_history",
            return_messages=True,
        )
        self.tools = ALL_TOOLS
        self.agent_executor = self._create_agent()

    def _initialize_llm(self):
        """
        Inicializa o modelo de linguagem baseado na configuração.

        Returns:
            Instância do LLM (Ollama ou OpenAI).

        Raises:
            ValueError: Se o provider configurado for inválido.
        """
        if settings.LLM_PROVIDER == "ollama":
            return Ollama(
                base_url=settings.OLLAMA_BASE_URL,
                model=settings.OLLAMA_MODEL,
            )
        elif settings.LLM_PROVIDER == "openai":
            return ChatOpenAI(
                api_key=settings.OPENAI_API_KEY,
                model=settings.OPENAI_MODEL,
                temperature=settings.OPENAI_TEMPERATURE,
            )
        else:
            raise ValueError(f"LLM Provider inválido: {settings.LLM_PROVIDER}")

    def _create_agent(self) -> AgentExecutor:
        """
        Cria o executor do agente com prompt e ferramentas.

        Returns:
            AgentExecutor configurado.
        """
        # Template do prompt para o agente
        prompt_template = f"""{SYSTEM_PROMPT}

Você tem acesso às seguintes ferramentas:
{{tools}}

Nomes das ferramentas: {{tool_names}}

Use o seguinte formato:

Pergunta: a pergunta de entrada do usuário
Pensamento: você deve sempre pensar sobre o que fazer
Ação: a ação a tomar, deve ser uma de [{{tool_names}}]
Entrada da Ação: a entrada para a ação
Observação: o resultado da ação
... (este Pensamento/Ação/Entrada da Ação/Observação pode se repetir N vezes)
Pensamento: Agora eu sei a resposta final
Resposta Final: a resposta final para a pergunta original do usuário

Comece!

Histórico da conversa:
{{chat_history}}

Pergunta: {{input}}
Pensamento: {{agent_scratchpad}}
"""

        prompt = PromptTemplate(
            template=prompt_template,
            input_variables=["input", "chat_history", "agent_scratchpad"],
            partial_variables={
                "tools": "\n".join(
                    [f"{tool.name}: {tool.description}" for tool in self.tools]
                ),
                "tool_names": ", ".join([tool.name for tool in self.tools]),
            },
        )

        # Criar agente
        agent = create_react_agent(
            llm=self.llm,
            tools=self.tools,
            prompt=prompt,
        )

        # Criar executor
        agent_executor = AgentExecutor(
            agent=agent,
            tools=self.tools,
            memory=self.memory,
            verbose=settings.AGENT_VERBOSE,
            max_iterations=settings.MAX_ITERATIONS,
            handle_parsing_errors=True,
        )

        return agent_executor

    def run(self, query: str) -> str:
        """
        Executa uma query no agente.

        Args:
            query: Pergunta ou comando do usuário.

        Returns:
            Resposta do agente.
        """
        try:
            result = self.agent_executor.invoke({"input": query})
            return result.get("output", "Desculpe, não consegui processar sua solicitação.")
        except Exception as e:
            return f"❌ Erro ao processar comando: {str(e)}"

    def clear_memory(self) -> None:
        """Limpa o histórico da conversa."""
        self.memory.clear()

    def reset(self) -> None:
        """Reinicia o agente (limpa memória e recria executor)."""
        self.memory.clear()
        self.agent_executor = self._create_agent()
