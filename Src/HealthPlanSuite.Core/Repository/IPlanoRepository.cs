using HealthPlanSuite.Core.Domain.Implementation;

namespace HealthPlanSuite.Core.Repository
{
    public interface IPlanoRepository : IBaseRepository<Plano>
    {
        Task<IEnumerable<Plano>> GetPlanosWithDetailsAsync();
        Task<Plano?> GetPlanoWithDetailsAsync(int id);
        Task<IEnumerable<Plano>> GetPlanosByOperadoraAsync(int operadoraId);
        Task<IEnumerable<Plano>> GetPlanosByTipoPlanoAsync(int tipoPlanoId);
    }
}