using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.HealthPlan.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Implementation
{
    public class PlanType : Entity, IPlanType
    {
        public string Description { get; set; } = string.Empty;
        public string ANSRegulation { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<HealthPlan> HealthPlans { get; set; } = new List<HealthPlan>();
    }
}