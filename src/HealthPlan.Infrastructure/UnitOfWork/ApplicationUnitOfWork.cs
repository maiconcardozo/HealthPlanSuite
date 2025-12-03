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

        public IAcceptanceRuleRepository AcceptanceRuleRepository { get; }
        public IAccommodationRepository AccommodationRepository { get; }
        public IAdhesionFeeRepository AdhesionFeeRepository { get; }
        public IAgeRangeRepository AgeRangeRepository { get; }
        public IBeneficiaryRepository BeneficiaryRepository { get; }
        public ICompanyRepository CompanyRepository { get; }
        public ICoverageRepository CoverageRepository { get; }
        public IHealthPlanRepository HealthPlanRepository { get; }
        public IPlanCoverageRepository PlanCoverageRepository { get; }
        public IPlanPriceRangeRepository PlanPriceRangeRepository { get; }
        public IProcedureCoparticipationRepository ProcedureCoparticipationRepository { get; }
        public IPromotionalDiscountRepository PromotionalDiscountRepository { get; }
        public IQuoteRepository QuoteRepository { get; }
        public IQuoteHistoryRepository QuoteHistoryRepository { get; }

        public ApplicationUnitOfWork(
            IApplicationContext context,
            IAcceptanceRuleRepository acceptanceRuleRepository,
            IAccommodationRepository accommodationRepository,
            IAdhesionFeeRepository adhesionFeeRepository,
            IAgeRangeRepository ageRangeRepository,
            IBeneficiaryRepository beneficiaryRepository,
            ICompanyRepository companyRepository,
            ICoverageRepository coverageRepository,
            IHealthPlanRepository healthPlanRepository,
            IPlanCoverageRepository planCoverageRepository,
            IPlanPriceRangeRepository planPriceRangeRepository,
            IProcedureCoparticipationRepository procedureCoparticipationRepository,
            IPromotionalDiscountRepository promotionalDiscountRepository,
            IQuoteRepository quoteRepository,
            IQuoteHistoryRepository quoteHistoryRepository
        )
        {
            _context = context;
            AcceptanceRuleRepository = acceptanceRuleRepository;
            AccommodationRepository = accommodationRepository;
            AdhesionFeeRepository = adhesionFeeRepository;
            AgeRangeRepository = ageRangeRepository;
            BeneficiaryRepository = beneficiaryRepository;
            CompanyRepository = companyRepository;
            CoverageRepository = coverageRepository;
            HealthPlanRepository = healthPlanRepository;
            PlanCoverageRepository = planCoverageRepository;
            PlanPriceRangeRepository = planPriceRangeRepository;
            ProcedureCoparticipationRepository = procedureCoparticipationRepository;
            PromotionalDiscountRepository = promotionalDiscountRepository;
            QuoteRepository = quoteRepository;
            QuoteHistoryRepository = quoteHistoryRepository;
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