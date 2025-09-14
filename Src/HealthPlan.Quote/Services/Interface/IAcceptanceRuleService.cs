using System.Linq.Expressions;
using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Services.Interface
{
    /// <summary>
    /// Service interface for AcceptanceRule management operations.
    /// Provides comprehensive AcceptanceRule CRUD operations following service layer patterns.
    /// </summary>
    public interface IAcceptanceRuleService
    {
        #region Query Operations
        
        /// <summary>
        /// Retrieves all acceptance rules from the system.
        /// </summary>
        /// <returns>Collection of all acceptance rule entities</returns>
        IEnumerable<AcceptanceRule> GetAllAcceptanceRules();
        
        /// <summary>
        /// Retrieves an acceptance rule by its unique identifier.
        /// </summary>
        /// <param name="id">AcceptanceRule ID</param>
        /// <returns>AcceptanceRule if found, null otherwise</returns>
        AcceptanceRule? GetById(int id);
        
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
        /// Retrieves acceptance rules that match the specified predicate condition.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter acceptance rules</param>
        /// <returns>Collection of matching acceptance rule entities</returns>
        IEnumerable<AcceptanceRule> GetAcceptanceRules(Expression<Func<AcceptanceRule, bool>> predicate);
        
        /// <summary>
        /// Retrieves a single acceptance rule that matches the predicate, or null if none found.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter acceptance rules</param>
        /// <returns>Single matching acceptance rule or null</returns>
        /// <exception cref="InvalidOperationException">Thrown when multiple acceptance rules match the predicate</exception>
        AcceptanceRule? GetSingleOrDefaultAcceptanceRule(Expression<Func<AcceptanceRule, bool>> predicate);
        
        /// <summary>
        /// Retrieves all active acceptance rules.
        /// </summary>
        /// <returns>Collection of active acceptance rules</returns>
        IEnumerable<AcceptanceRule> GetAllActiveAcceptanceRules();
        
        #endregion
        
        #region Modification Operations
        
        /// <summary>
        /// Creates a new acceptance rule in the system.
        /// Sets audit fields and validates business rules.
        /// </summary>
        /// <param name="acceptanceRule">AcceptanceRule to create</param>
        void AddAcceptanceRule(AcceptanceRule acceptanceRule);
        
        /// <summary>
        /// Creates multiple acceptance rules in a single transaction.
        /// </summary>
        /// <param name="acceptanceRules">Collection of acceptance rule entities to create</param>
        void AddAcceptanceRules(IEnumerable<AcceptanceRule> acceptanceRules);
        
        /// <summary>
        /// Updates an existing acceptance rule.
        /// </summary>
        /// <param name="acceptanceRule">AcceptanceRule with updated information</param>
        void UpdateAcceptanceRule(AcceptanceRule acceptanceRule);
        
        /// <summary>
        /// Deletes an acceptance rule.
        /// </summary>
        /// <param name="acceptanceRule">AcceptanceRule to delete</param>
        void DeleteAcceptanceRule(AcceptanceRule acceptanceRule);
        
        /// <summary>
        /// Deletes an acceptance rule by its ID.
        /// </summary>
        /// <param name="id">AcceptanceRule ID to delete</param>
        void DeleteAcceptanceRule(int id);
        
        /// <summary>
        /// Deletes multiple acceptance rule entities.
        /// </summary>
        /// <param name="acceptanceRules">Collection of acceptance rule entities to delete</param>
        void DeleteAcceptanceRules(IEnumerable<AcceptanceRule> acceptanceRules);
        
        #endregion
        
        #region Business Logic
        
        /// <summary>
        /// Validates if a rule type is valid.
        /// </summary>
        /// <param name="ruleType">Rule type to validate</param>
        /// <returns>True if rule type is valid, false otherwise</returns>
        bool IsValidRuleType(string ruleType);
        
        /// <summary>
        /// Validates if an operator is valid.
        /// </summary>
        /// <param name="operatorValue">Operator to validate</param>
        /// <returns>True if operator is valid, false otherwise</returns>
        bool IsValidOperator(string operatorValue);
        
        #endregion
    }
}