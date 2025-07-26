using Foundation.Base.Repository.Interface;
using HealthPlanSuite.Domain.Implementation;

namespace HealthPlanSuite.Repository.Interface
{
    public interface IEstabelecimentoSaudeRepository : IEntityRepository<EstabelecimentoSaude>
    {
        Task<IEnumerable<EstabelecimentoSaude>> GetByNomeAsync(string nome);
        Task<IEnumerable<EstabelecimentoSaude>> GetByTipoAsync(string tipo);
        Task<IEnumerable<EstabelecimentoSaude>> GetByCidadeAsync(string cidade);
        Task<IEnumerable<EstabelecimentoSaude>> GetByEstadoAsync(string estado);
        Task<IEnumerable<EstabelecimentoSaude>> GetByFilterAsync(string? filter, string? tipo = null, string? cidade = null, string? estado = null);
    }
}