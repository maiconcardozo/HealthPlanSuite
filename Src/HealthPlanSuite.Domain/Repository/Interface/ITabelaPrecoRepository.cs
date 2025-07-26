using Foundation.Base.Repository.Interface;
using HealthPlanSuite.Domain.Implementation;

namespace HealthPlanSuite.Repository.Interface
{
    public interface ITabelaPrecoRepository : IEntityRepository<TabelaPreco>
    {
        Task<IEnumerable<TabelaPreco>> GetByPlanoIdAsync(int planoId);
        Task<IEnumerable<TabelaPreco>> GetByFaixaEtariaIdAsync(int faixaEtariaId);
        Task<IEnumerable<TabelaPreco>> GetVigentesAsync(DateTime? dataReferencia = null);
        Task<IEnumerable<TabelaPreco>> GetByPlanoIdAndVigentesAsync(int planoId, DateTime? dataReferencia = null);
        Task<TabelaPreco?> GetByPlanoIdAndFaixaEtariaIdAsync(int planoId, int faixaEtariaId, DateTime? dataReferencia = null);
    }
}