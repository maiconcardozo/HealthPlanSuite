using HealthPlanSuite.Core.Domain.Implementation;

namespace HealthPlanSuite.Core.Repository
{
    public interface IEstabelecimentoSaudeRepository : IBaseRepository<EstabelecimentoSaude>
    {
        Task<IEnumerable<EstabelecimentoSaude>> GetEstabelecimentosByTipoAsync(string tipo);
        Task<IEnumerable<EstabelecimentoSaude>> GetEstabelecimentosByCidadeAsync(string cidade);
        Task<IEnumerable<EstabelecimentoSaude>> GetEstabelecimentosByEstadoAsync(string estado);
    }
}