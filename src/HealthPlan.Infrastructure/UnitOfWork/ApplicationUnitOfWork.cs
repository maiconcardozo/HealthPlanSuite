using HealthPlan.Infrastructure.Persistence;
using HealthPlan.Domain.Interfaces;

namespace HealthPlan.Infrastructure.UnitOfWork
{
    /// <summary>
    /// Unit of Work implementation for managing repository transactions
    /// </summary>
    public class ApplicationUnitOfWork : IApplicationUnitOfWork
    {
        private readonly IApplicationContext _context;
        private bool _disposed = false;

        public IAgeRangeRepository AgeRangeRepository { get; }
        public IBeneficiaryRepository BeneficiaryRepository { get; }
        public ICompanyRepository CompanyRepository { get; }
        public ICoverageRepository CoverageRepository { get; }
        public IHealthPlanRepository HealthPlanRepository { get; }
        public IQuoteRepository QuoteRepository { get; }

        public ApplicationUnitOfWork(
            IApplicationContext context,
            IAgeRangeRepository ageRangeRepository,
            IBeneficiaryRepository beneficiaryRepository,
            ICompanyRepository companyRepository,
            ICoverageRepository coverageRepository,
            IHealthPlanRepository healthPlanRepository,
            IQuoteRepository quoteRepository
        )
        {
            _context = context;
            AgeRangeRepository = ageRangeRepository;
            BeneficiaryRepository = beneficiaryRepository;
            CompanyRepository = companyRepository;
            CoverageRepository = coverageRepository;
            HealthPlanRepository = healthPlanRepository;
            QuoteRepository = quoteRepository;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}