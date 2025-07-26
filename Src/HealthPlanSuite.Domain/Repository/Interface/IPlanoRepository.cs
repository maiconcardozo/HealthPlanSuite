using Foundation.Base.Repository.Interface;
using HealthPlanSuite.Domain.Implementation;

namespace HealthPlanSuite.Repository.Interface
{
    public interface IPlanoRepository : IEntityRepository<Plano>
    {
        Task<IEnumerable<Plano>> GetByOperadoraIdAsync(int operadoraId);
        Task<IEnumerable<Plano>> GetByTipoPlanoIdAsync(int tipoPlanoId);
        Task<IEnumerable<Plano>> GetByNomeAsync(string nome);
        Task<IEnumerable<Plano>> GetByFilterAsync(string? filter, int? operadoraId = null, int? tipoPlanoId = null);
        Task<IEnumerable<Plano>> GetWithTabelasPrecosAsync();
        Task<IEnumerable<Plano>> GetWithAbrangenciaAsync();
        Task<Plano?> GetWithAllRelationsAsync(int id);
    }
}