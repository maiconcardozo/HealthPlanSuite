using HealthPlanSuite.Core.Domain.Implementation;

namespace HealthPlanSuite.Core.Repository
{
    public interface ITipoPlanoRepository : IBaseRepository<TipoPlano>
    {
        Task<TipoPlano?> GetByDescricaoAsync(string descricao);
    }
}