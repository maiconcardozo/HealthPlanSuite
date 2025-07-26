using Foundation.Base.Repository.Interface;
using HealthPlanSuite.Domain.Implementation;

namespace HealthPlanSuite.Repository.Interface
{
    public interface IPlanoAbrangenciaRepository : IEntityRepository<PlanoAbrangencia>
    {
        Task<IEnumerable<PlanoAbrangencia>> GetByPlanoIdAsync(int planoId);
        Task<IEnumerable<PlanoAbrangencia>> GetByEstabelecimentoIdAsync(int estabelecimentoId);
        Task<bool> ExistsAsync(int planoId, int estabelecimentoId);
        Task<PlanoAbrangencia?> GetByPlanoIdAndEstabelecimentoIdAsync(int planoId, int estabelecimentoId);
        Task<IEnumerable<EstabelecimentoSaude>> GetEstabelecimentosByPlanoIdAsync(int planoId);
    }
}