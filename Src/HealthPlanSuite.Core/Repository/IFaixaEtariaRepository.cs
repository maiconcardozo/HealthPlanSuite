using HealthPlanSuite.Core.Domain.Implementation;

namespace HealthPlanSuite.Core.Repository
{
    public interface IFaixaEtariaRepository : IBaseRepository<FaixaEtaria>
    {
        Task<FaixaEtaria?> GetByIdadeAsync(int idade);
        Task<IEnumerable<FaixaEtaria>> GetFaixasOverlappingAsync(int idadeMin, int idadeMax);
    }
}