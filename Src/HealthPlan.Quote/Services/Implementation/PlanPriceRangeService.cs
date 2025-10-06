using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Services.Interface;

namespace HealthPlan.Quote.Services.Implementation
{
    /// <summary>
    /// Service implementation for PlanPriceRange business operations.
    /// </summary>
    public class PlanPriceRangeService : IPlanPriceRangeService
    {
        private readonly IPlanPriceRangeRepository _planPriceRangeRepository;

        public PlanPriceRangeService(IPlanPriceRangeRepository planPriceRangeRepository)
        {
            _planPriceRangeRepository = planPriceRangeRepository;
        }

        public IEnumerable<PlanPriceRange> GetAllActivePlanPriceRanges()
        {
            return _planPriceRangeRepository.Find(ppf => ppf.IsActive);
        }

        public PlanPriceRange? GetById(int id)
        {
            return _planPriceRangeRepository.GetById(id);
        }

        public void AddPlanPriceRange(PlanPriceRange planPriceRange)
        {
            _planPriceRangeRepository.Add(planPriceRange);
        }

        public void UpdatePlanPriceRange(PlanPriceRange planPriceRange)
        {
            _planPriceRangeRepository.Update(planPriceRange);
        }

        public void DeletePlanPriceRange(int id)
        {
            _planPriceRangeRepository.Remove(id);
        }
    }
}