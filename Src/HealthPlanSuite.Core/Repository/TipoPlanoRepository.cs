using HealthPlanSuite.Core.Domain.Implementation;
using HealthPlanSuite.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HealthPlanSuite.Core.Repository
{
    public class TipoPlanoRepository : BaseRepository<TipoPlano>, ITipoPlanoRepository
    {
        public TipoPlanoRepository(HealthPlanSuiteDbContext context) : base(context)
        {
        }

        public async Task<TipoPlano?> GetByDescricaoAsync(string descricao)
        {
            return await _dbSet.FirstOrDefaultAsync(tp => tp.Descricao == descricao);
        }
    }
}