using Foundation.Base.Repository.Interface;
using HealthPlanSuite.Domain.Implementation;

namespace HealthPlanSuite.Repository.Interface
{
    public interface IReajusteRepository : IEntityRepository<Reajuste>
    {
        Task<IEnumerable<Reajuste>> GetByPlanoIdAsync(int planoId);
        Task<IEnumerable<Reajuste>> GetByTipoReajusteAsync(string tipoReajuste);
        Task<IEnumerable<Reajuste>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<Reajuste>> GetByPlanoIdAndPeriodoAsync(int planoId, DateTime dataInicio, DateTime dataFim);
        Task<Reajuste?> GetUltimoReajusteByPlanoIdAsync(int planoId);
    }
}