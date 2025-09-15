using Foundation.Base.Domain.Implementation;

namespace HealthPlan.Quote.Domain.Implementation
{
    /// <summary>
    /// Represents a co-participation procedure implementation for a health plan.
    /// Co-participation procedures define the patient's financial responsibility for specific medical procedures.
    /// Inherits from Entity base class providing audit fields.
    /// </summary>
    public class CoparticipacaoProcedimento : Entity
    {
        /// <summary>
        /// Gets or sets the health plan ID that this co-participation applies to.
        /// References the HealthPlan entity.
        /// Maps to SQL column: HealthPlanId
        /// </summary>
        public int HealthPlanId { get; set; }

        /// <summary>
        /// Gets or sets the health plan that this co-participation applies to.
        /// Navigation property for HealthPlanId foreign key.
        /// </summary>
        public HealthPlan? HealthPlan { get; set; }

        /// <summary>
        /// Gets or sets the type of co-participation.
        /// Possible values: "Parcial" or "Total".
        /// Maps to SQL column: TipoCoparticipacao
        /// </summary>
        public string TipoCoparticipacao { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the procedure name or description.
        /// The medical procedure this co-participation applies to.
        /// Maps to SQL column: Procedimento
        /// </summary>
        public string Procedimento { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the co-participation value.
        /// The monetary amount or percentage of co-participation.
        /// Maps to SQL column: Valor
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Gets or sets the limit for this co-participation.
        /// Maximum amount or frequency limit for this co-participation.
        /// Maps to SQL column: Limite
        /// </summary>
        public decimal? Limite { get; set; }
    }
}