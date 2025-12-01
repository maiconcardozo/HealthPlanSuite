using HealthPlan.Quote.Repository.Interface;

namespace HealthPlan.Quote.UnitOfWork.Interface
{
    /// <summary>
    /// Unit of Work interface for managing repository transactions
    /// </summary>
    public interface IApplicationUnitOfWork : IDisposable
    {
        IAgeRangeRepository AgeRangeRepository { get; }
        IBeneficiaryRepository BeneficiaryRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        ICoverageRepository CoverageRepository { get; }
        IHealthPlanRepository HealthPlanRepository { get; }
        IQuoteRepository QuoteRepository { get; }
        
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