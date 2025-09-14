using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.Interface;

namespace HealthPlan.Quote.Domain.Implementation
{
    /// <summary>
    /// Represents acceptance rules for health plans.
    /// These rules define criteria that beneficiaries must meet to be eligible for a health plan.
    /// Inherits from Entity base class providing audit fields and implements IAcceptanceRule interface.
    /// </summary>
    public class AcceptanceRule : Entity, IAcceptanceRule
    {
        /// <summary>
        /// Gets or sets the health plan ID this rule applies to.
        /// References the HealthPlan entity.
        /// Maps to SQL column: PlanoSaudeId
        /// </summary>
        public int HealthPlanId { get; set; }

        /// <summary>
        /// Gets or sets the health plan this rule applies to.
        /// Navigation property for HealthPlanId foreign key.
        /// </summary>
        public HealthPlan? HealthPlan { get; set; }

        /// <summary>
        /// Gets or sets the type of rule.
        /// Examples: "Idade", "Renda", "Profissão", "Estado Civil".
        /// Maps to SQL column: TipoRegra
        /// </summary>
        public string RuleType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the operator for the rule.
        /// Possible values: "=", ">", "<", ">=", "<=", "BETWEEN", "IN".
        /// Maps to SQL column: Operador
        /// </summary>
        public string Operator { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the minimum value for the rule.
        /// Maps to SQL column: ValorMinimo
        /// </summary>
        public string? MinValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value for the rule.
        /// Maps to SQL column: ValorMaximo
        /// </summary>
        public string? MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the list of accepted values (JSON format).
        /// Used for "IN" operator rules.
        /// Maps to SQL column: ListaValores
        /// </summary>
        public string? ValuesList { get; set; }

        /// <summary>
        /// Gets or sets the description of the rule.
        /// Maps to SQL column: Descricao
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the rejection message displayed when rule is not met.
        /// Maps to SQL column: MensagemRejeicao
        /// </summary>
        public string? RejectionMessage { get; set; }

        /// <summary>
        /// Gets or sets whether this rule is mandatory.
        /// Maps to SQL column: IsObrigatoria
        /// </summary>
        public bool IsMandatory { get; set; } = true;
    }
}