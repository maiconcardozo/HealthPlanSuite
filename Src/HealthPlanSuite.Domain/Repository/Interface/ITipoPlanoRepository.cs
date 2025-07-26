using Foundation.Base.Repository.Interface;
using HealthPlanSuite.Domain.Implementation;

namespace HealthPlanSuite.Repository.Interface
{
    public interface ITipoPlanoRepository : IEntityRepository<TipoPlano>
    {
        Task<TipoPlano?> GetByDescricaoAsync(string descricao);
        Task<IEnumerable<TipoPlano>> GetByFilterAsync(string? filter);
        Task<bool> ExistsByDescricaoAsync(string descricao);
    }
}