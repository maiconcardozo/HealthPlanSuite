using HealthPlan.Domain.Interfaces;
using HealthPlan.Domain.Entities;

namespace HealthPlan.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for AcceptanceRule data access operations.
    /// Extends base repository functionality with AcceptanceRule-specific methods.
    /// </summary>
    public interface IAcceptanceRuleRepository : IEntityRepository<AcceptanceRule>
    {
        /// <summary>
        /// Retrieves acceptance rules by health plan ID.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of acceptance rules for the specified health plan</returns>
        IEnumerable<AcceptanceRule> GetByHealthPlanId(int healthPlanId);
        
        /// <summary>
        /// Retrieves acceptance rules by rule type.
        /// </summary>
        /// <param name="ruleType">Rule type to filter by</param>
        /// <returns>Collection of acceptance rules of the specified type</returns>
        IEnumerable<AcceptanceRule> GetByRuleType(string ruleType);
        
        /// <summary>
        /// Retrieves mandatory acceptance rules.
        /// </summary>
        /// <returns>Collection of mandatory acceptance rules</returns>
        IEnumerable<AcceptanceRule> GetMandatoryRules();
        
        /// <summary>
        /// Checks if a rule type is valid.
        /// </summary>
        /// <param name="ruleType">Rule type to validate</param>
        /// <returns>True if rule type is valid, false otherwise</returns>
        bool IsValidRuleType(string ruleType);
        
        /// <summary>
        /// Checks if an operator is valid.
        /// </summary>
        /// <param name="operatorValue">Operator to validate</param>
        /// <returns>True if operator is valid, false otherwise</returns>
        bool IsValidOperator(string operatorValue);
    }
}