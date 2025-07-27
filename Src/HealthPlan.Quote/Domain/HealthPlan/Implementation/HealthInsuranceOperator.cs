using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.HealthPlan.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Implementation
{
    public class HealthInsuranceOperator : Entity, IHealthInsuranceOperator
    {
        public string Name { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        // Navigation properties
        public virtual ICollection<HealthPlan> HealthPlans { get; set; } = new List<HealthPlan>();
    }
}