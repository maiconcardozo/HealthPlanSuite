using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Infrastructure.Persistence;

namespace HealthPlan.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for AcceptanceRule management operations.
    /// </summary>
    public class AcceptanceRuleRepository : EntityRepository<AcceptanceRule>, IAcceptanceRuleRepository
    {
        private static readonly string[] ValidRuleTypes = { "age", "gender", "pre_existing_condition", "coverage", "waiting_period" };
        private static readonly string[] ValidOperators = { "equals", "not_equals", "greater_than", "less_than", "greater_or_equal", "less_or_equal", "contains", "not_contains" };

        public AcceptanceRuleRepository(IApplicationContext context) : base(context)
        {
        }

        public IEnumerable<AcceptanceRule> GetByHealthPlanId(int healthPlanId)
        {
            return _context.Set<AcceptanceRule>()
                .Where(ar => ar.HealthPlanId == healthPlanId && ar.IsActive)
                .ToList();
        }

        public IEnumerable<AcceptanceRule> GetByRuleType(string ruleType)
        {
            return _context.Set<AcceptanceRule>()
                .Where(ar => ar.RuleType == ruleType && ar.IsActive)
                .ToList();
        }

        public IEnumerable<AcceptanceRule> GetMandatoryRules()
        {
            return _context.Set<AcceptanceRule>()
                .Where(ar => ar.IsMandatory && ar.IsActive)
                .ToList();
        }

        public bool IsValidRuleType(string ruleType)
        {
            return ValidRuleTypes.Contains(ruleType.ToLowerInvariant());
        }

        public bool IsValidOperator(string operatorValue)
        {
            return ValidOperators.Contains(operatorValue.ToLowerInvariant());
        }
    }
}
