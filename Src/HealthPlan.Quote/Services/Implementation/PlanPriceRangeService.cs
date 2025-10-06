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
        private readonly IPlanPriceRangeRepository _precoPlanoFaixaRepository;

        public PlanPriceRangeService(IPlanPriceRangeRepository precoPlanoFaixaRepository)
        {
            _precoPlanoFaixaRepository = precoPlanoFaixaRepository;
        }

        public IEnumerable<PlanPriceRange> GetAllActivePrecoPlanoFaixa()
        {
            return _precoPlanoFaixaRepository.Find(ppf => ppf.IsActive);
        }

        public PlanPriceRange? GetById(int id)
        {
            return _precoPlanoFaixaRepository.GetById(id);
        }

        public void AddPrecoPlanoFaixa(PlanPriceRange precoPlanoFaixa)
        {
            _precoPlanoFaixaRepository.Add(precoPlanoFaixa);
        }

        public void UpdatePrecoPlanoFaixa(PlanPriceRange precoPlanoFaixa)
        {
            _precoPlanoFaixaRepository.Update(precoPlanoFaixa);
        }

        public void DeletePrecoPlanoFaixa(int id)
        {
            _precoPlanoFaixaRepository.Remove(id);
        }
    }
}