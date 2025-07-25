using HealthPlanSuite.Core.Domain.Implementation;
using HealthPlanSuite.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HealthPlanSuite.Core.Repository
{
    public class PlanoAbrangenciaRepository : BaseRepository<PlanoAbrangencia>, IPlanoAbrangenciaRepository
    {
        public PlanoAbrangenciaRepository(HealthPlanSuiteDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PlanoAbrangencia>> GetAbrangenciasWithDetailsAsync()
        {
            return await _dbSet
                .Include(pa => pa.Plano)
                .Include(pa => pa.EstabelecimentoSaude)
                .ToListAsync();
        }

        public async Task<PlanoAbrangencia?> GetAbrangenciaWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(pa => pa.Plano)
                .Include(pa => pa.EstabelecimentoSaude)
                .FirstOrDefaultAsync(pa => pa.Id == id);
        }

        public async Task<IEnumerable<PlanoAbrangencia>> GetAbrangenciasByPlanoAsync(int planoId)
        {
            return await _dbSet
                .Include(pa => pa.EstabelecimentoSaude)
                .Where(pa => pa.PlanoId == planoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PlanoAbrangencia>> GetAbrangenciasByEstabelecimentoAsync(int estabelecimentoId)
        {
            return await _dbSet
                .Include(pa => pa.Plano)
                .Where(pa => pa.EstabelecimentoId == estabelecimentoId)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int planoId, int estabelecimentoId)
        {
            return await _dbSet
                .AnyAsync(pa => pa.PlanoId == planoId && pa.EstabelecimentoId == estabelecimentoId);
        }
    }
}