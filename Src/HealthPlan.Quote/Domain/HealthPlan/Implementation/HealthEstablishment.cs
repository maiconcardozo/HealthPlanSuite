using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.HealthPlan.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Implementation
{
    public class HealthEstablishment : Entity, IHealthEstablishment
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Clinic, Hospital, Laboratory, etc.
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<PlanCoverage> PlanCoverages { get; set; } = new List<PlanCoverage>();
    }
}