using HealthPlanSuite.Core.Domain.Implementation;

namespace HealthPlanSuite.Core.Repository
{
    public interface IReajusteRepository : IBaseRepository<Reajuste>
    {
        Task<IEnumerable<Reajuste>> GetReajustesWithDetailsAsync();
        Task<Reajuste?> GetReajusteWithDetailsAsync(int id);
        Task<IEnumerable<Reajuste>> GetReajustesByPlanoAsync(int planoId);
        Task<IEnumerable<Reajuste>> GetReajustesByPeriodoAsync(DateTime dataInicio, DateTime dataFim);
    }
}