using HealthPlanSuite.Core.Domain.Implementation;
using HealthPlanSuite.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HealthPlanSuite.Core.Repository
{
    public class ReajusteRepository : BaseRepository<Reajuste>, IReajusteRepository
    {
        public ReajusteRepository(HealthPlanSuiteDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Reajuste>> GetReajustesWithDetailsAsync()
        {
            return await _dbSet
                .Include(r => r.Plano)
                .ToListAsync();
        }

        public async Task<Reajuste?> GetReajusteWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(r => r.Plano)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Reajuste>> GetReajustesByPlanoAsync(int planoId)
        {
            return await _dbSet
                .Where(r => r.PlanoId == planoId)
                .OrderByDescending(r => r.DataReajuste)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reajuste>> GetReajustesByPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _dbSet
                .Include(r => r.Plano)
                .Where(r => r.DataReajuste >= dataInicio && r.DataReajuste <= dataFim)
                .OrderByDescending(r => r.DataReajuste)
                .ToListAsync();
        }
    }
}