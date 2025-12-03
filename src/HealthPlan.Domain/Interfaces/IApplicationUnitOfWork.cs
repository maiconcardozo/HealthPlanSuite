using HealthPlan.Domain.Interfaces;

namespace HealthPlan.Domain.Interfaces
{
    /// <summary>
    /// Unit of Work interface for managing repository transactions
    /// </summary>
    public interface IApplicationUnitOfWork : IDisposable
    {
        IAcceptanceRuleRepository AcceptanceRuleRepository { get; }
        IAccommodationRepository AccommodationRepository { get; }
        IAdhesionFeeRepository AdhesionFeeRepository { get; }
        IAgeRangeRepository AgeRangeRepository { get; }
        IBeneficiaryRepository BeneficiaryRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        ICoverageRepository CoverageRepository { get; }
        IHealthPlanRepository HealthPlanRepository { get; }
        IPlanCoverageRepository PlanCoverageRepository { get; }
        IPlanPriceRangeRepository PlanPriceRangeRepository { get; }
        IProcedureCoparticipationRepository ProcedureCoparticipationRepository { get; }
        IPromotionalDiscountRepository PromotionalDiscountRepository { get; }
        IQuoteRepository QuoteRepository { get; }
        IQuoteHistoryRepository QuoteHistoryRepository { get; }
        
        /// <summary>
        /// Saves all changes made in this unit of work to the database
        /// </summary>
        /// <returns>Number of state entries written to the database</returns>
        int Complete();
        
        /// <summary>
        /// Asynchronously saves all changes made in this unit of work to the database
        /// </summary>
        /// <returns>Task representing the asynchronous operation with number of state entries written</returns>
        Task<int> CompleteAsync();
    }
}