using HealthPlanSuite.Core.Domain.Implementation;

namespace HealthPlanSuite.Core.Repository
{
    public interface ITabelaPrecoRepository : IBaseRepository<TabelaPreco>
    {
        Task<IEnumerable<TabelaPreco>> GetTabelasWithDetailsAsync();
        Task<TabelaPreco?> GetTabelaWithDetailsAsync(int id);
        Task<IEnumerable<TabelaPreco>> GetTabelasByPlanoAsync(int planoId);
        Task<TabelaPreco?> GetTabelaVigenteAsync(int planoId, int faixaEtariaId, DateTime data);
        Task<IEnumerable<TabelaPreco>> GetTabelasVigentesAsync(DateTime data);
    }
}