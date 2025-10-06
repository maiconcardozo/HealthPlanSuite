namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for PlanPriceRange payload operations.
    /// Used for creating and updating PlanPriceRange instances.
    /// </summary>
    public class PlanPriceRangePayLoadDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the plan price range.
        /// Used for update operations.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the health plan ID that this price range applies to.
        /// References the HealthPlan entity.
        /// </summary>
        public int HealthPlanId { get; set; }
        
        /// <summary>
        /// Gets or sets the age range ID that this price applies to.
        /// References the AgeRange entity.
        /// </summary>
        public int AgeRangeId { get; set; }
        
        /// <summary>
        /// Gets or sets the type of contract.
        /// Possible values: "Individual", "Coletivo por Adesão", "Empresarial".
        /// </summary>
        public string ContractType { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the type of co-participation.
        /// Possible values: "Parcial", "Total", "Sem Coparticipação".
        /// </summary>
        public string CoparticipationType { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the original price value.
        /// The base price before any discounts are applied.
        /// </summary>
        public decimal OriginalValue { get; set; }
        
        /// <summary>
        /// Gets or sets the discount value.
        /// The amount of discount applied to the original price.
        /// </summary>
        public decimal DiscountValue { get; set; }
        
        /// <summary>
        /// Gets or sets the start date of validity for this price.
        /// The date from which this price becomes effective.
        /// </summary>
        public DateTime ValidityStart { get; set; }
        
        /// <summary>
        /// Gets or sets the end date of validity for this price.
        /// The date until which this price remains effective.
        /// </summary>
        public DateTime ValidityEnd { get; set; }
        
        /// <summary>
        /// Gets or sets the user who created this entity.
        /// Used for audit trail purposes.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the user who last updated this entity.
        /// Used for audit trail purposes during updates.
        /// </summary>
        public string? UpdatedBy { get; set; }
    }
}