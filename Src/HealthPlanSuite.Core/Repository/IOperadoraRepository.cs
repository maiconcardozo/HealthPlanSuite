using HealthPlanSuite.Core.Domain.Implementation;

namespace HealthPlanSuite.Core.Repository
{
    public interface IOperadoraRepository : IBaseRepository<Operadora>
    {
        Task<Operadora?> GetByCNPJAsync(string cnpj);
        Task<bool> CNPJExistsAsync(string cnpj, int? excludeId = null);
    }
}