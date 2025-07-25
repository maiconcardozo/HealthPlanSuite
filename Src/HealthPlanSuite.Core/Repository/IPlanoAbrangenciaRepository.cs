using HealthPlanSuite.Core.Domain.Implementation;

namespace HealthPlanSuite.Core.Repository
{
    public interface IPlanoAbrangenciaRepository : IBaseRepository<PlanoAbrangencia>
    {
        Task<IEnumerable<PlanoAbrangencia>> GetAbrangenciasWithDetailsAsync();
        Task<PlanoAbrangencia?> GetAbrangenciaWithDetailsAsync(int id);
        Task<IEnumerable<PlanoAbrangencia>> GetAbrangenciasByPlanoAsync(int planoId);
        Task<IEnumerable<PlanoAbrangencia>> GetAbrangenciasByEstabelecimentoAsync(int estabelecimentoId);
        Task<bool> ExistsAsync(int planoId, int estabelecimentoId);
    }
}