using HealthPlanSuite.Core.Domain.Implementation;
using HealthPlanSuite.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HealthPlanSuite.Core.Repository
{
    public class FaixaEtariaRepository : BaseRepository<FaixaEtaria>, IFaixaEtariaRepository
    {
        public FaixaEtariaRepository(HealthPlanSuiteDbContext context) : base(context)
        {
        }

        public async Task<FaixaEtaria?> GetByIdadeAsync(int idade)
        {
            return await _dbSet.FirstOrDefaultAsync(fe => idade >= fe.IdadeMin && idade <= fe.IdadeMax);
        }

        public async Task<IEnumerable<FaixaEtaria>> GetFaixasOverlappingAsync(int idadeMin, int idadeMax)
        {
            return await _dbSet
                .Where(fe => fe.IdadeMin <= idadeMax && fe.IdadeMax >= idadeMin)
                .ToListAsync();
        }
    }
}