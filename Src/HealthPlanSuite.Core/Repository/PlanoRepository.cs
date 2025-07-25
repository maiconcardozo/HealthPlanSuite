using HealthPlanSuite.Core.Domain.Implementation;
using HealthPlanSuite.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HealthPlanSuite.Core.Repository
{
    public class PlanoRepository : BaseRepository<Plano>, IPlanoRepository
    {
        public PlanoRepository(HealthPlanSuiteDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Plano>> GetPlanosWithDetailsAsync()
        {
            return await _dbSet
                .Include(p => p.Operadora)
                .Include(p => p.TipoPlano)
                .ToListAsync();
        }

        public async Task<Plano?> GetPlanoWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Operadora)
                .Include(p => p.TipoPlano)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Plano>> GetPlanosByOperadoraAsync(int operadoraId)
        {
            return await _dbSet
                .Include(p => p.TipoPlano)
                .Where(p => p.OperadoraId == operadoraId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plano>> GetPlanosByTipoPlanoAsync(int tipoPlanoId)
        {
            return await _dbSet
                .Include(p => p.Operadora)
                .Where(p => p.TipoPlanoId == tipoPlanoId)
                .ToListAsync();
        }
    }
}