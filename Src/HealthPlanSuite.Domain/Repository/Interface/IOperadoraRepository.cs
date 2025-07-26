using Foundation.Base.Repository.Interface;
using HealthPlanSuite.Domain.Implementation;

namespace HealthPlanSuite.Repository.Interface
{
    public interface IOperadoraRepository : IEntityRepository<Operadora>
    {
        Task<Operadora?> GetByCNPJAsync(string cnpj);
        Task<IEnumerable<Operadora>> GetByFilterAsync(string? filter);
        Task<bool> ExistsByCNPJAsync(string cnpj);
        Task<IEnumerable<Operadora>> GetWithPlanosAsync();
    }
}