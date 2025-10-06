using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Services.Interface
{
    /// <summary>
    /// Service interface for PlanPriceRange business operations.
    /// Provides business logic layer for PlanPriceRange management.
    /// </summary>
    public interface IPlanPriceRangeService
    {
        IEnumerable<PlanPriceRange> GetAllActivePrecoPlanoFaixa();
        PlanPriceRange? GetById(int id);
        void AddPrecoPlanoFaixa(PlanPriceRange precoPlanoFaixa);
        void UpdatePrecoPlanoFaixa(PlanPriceRange precoPlanoFaixa);
        void DeletePrecoPlanoFaixa(int id);
    }
}