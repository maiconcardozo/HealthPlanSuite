using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.HealthPlan.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Implementation
{
    public class AgeRange : Entity, IAgeRange
    {
        public string Description { get; set; } = string.Empty;
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        // Navigation properties
        public virtual ICollection<PriceTable> PriceTables { get; set; } = new List<PriceTable>();
    }
}