using HealthPlanSuite.Core.Domain.Implementation;
using HealthPlanSuite.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HealthPlanSuite.Core.Repository
{
    public class OperadoraRepository : BaseRepository<Operadora>, IOperadoraRepository
    {
        public OperadoraRepository(HealthPlanSuiteDbContext context) : base(context)
        {
        }

        public async Task<Operadora?> GetByCNPJAsync(string cnpj)
        {
            return await _dbSet.FirstOrDefaultAsync(o => o.CNPJ == cnpj);
        }

        public async Task<bool> CNPJExistsAsync(string cnpj, int? excludeId = null)
        {
            var query = _dbSet.Where(o => o.CNPJ == cnpj);
            
            if (excludeId.HasValue)
                query = query.Where(o => o.Id != excludeId.Value);

            return await query.AnyAsync();
        }
    }
}