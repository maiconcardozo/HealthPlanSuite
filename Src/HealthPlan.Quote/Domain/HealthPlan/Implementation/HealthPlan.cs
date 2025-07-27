using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.HealthPlan.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Implementation
{
    public class HealthPlan : Entity, IHealthPlan
    {
        public int HealthInsuranceOperatorId { get; set; }
        public int PlanTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Coverage { get; set; } = string.Empty;
        public bool HasCoparticipation { get; set; }

        // Navigation properties
        public virtual HealthInsuranceOperator HealthInsuranceOperator { get; set; } = null!;
        public virtual PlanType PlanType { get; set; } = null!;
        public virtual ICollection<PriceTable> PriceTables { get; set; } = new List<PriceTable>();
        public virtual ICollection<PlanAdjustment> PlanAdjustments { get; set; } = new List<PlanAdjustment>();
        public virtual ICollection<PlanCoverage> PlanCoverages { get; set; } = new List<PlanCoverage>();
    }
}