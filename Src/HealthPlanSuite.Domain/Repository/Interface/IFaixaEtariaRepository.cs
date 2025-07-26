using Foundation.Base.Repository.Interface;
using HealthPlanSuite.Domain.Implementation;

namespace HealthPlanSuite.Repository.Interface
{
    public interface IFaixaEtariaRepository : IEntityRepository<FaixaEtaria>
    {
        Task<FaixaEtaria?> GetByDescricaoAsync(string descricao);
        Task<IEnumerable<FaixaEtaria>> GetByFilterAsync(string? filter);
        Task<FaixaEtaria?> GetByIdadeAsync(int idade);
        Task<bool> ExistsByDescricaoAsync(string descricao);
    }
}