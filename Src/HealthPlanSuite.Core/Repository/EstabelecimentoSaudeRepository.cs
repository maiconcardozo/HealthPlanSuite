using HealthPlanSuite.Core.Domain.Implementation;
using HealthPlanSuite.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HealthPlanSuite.Core.Repository
{
    public class EstabelecimentoSaudeRepository : BaseRepository<EstabelecimentoSaude>, IEstabelecimentoSaudeRepository
    {
        public EstabelecimentoSaudeRepository(HealthPlanSuiteDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<EstabelecimentoSaude>> GetEstabelecimentosByTipoAsync(string tipo)
        {
            return await _dbSet
                .Where(es => es.Tipo == tipo)
                .ToListAsync();
        }

        public async Task<IEnumerable<EstabelecimentoSaude>> GetEstabelecimentosByCidadeAsync(string cidade)
        {
            return await _dbSet
                .Where(es => es.Cidade == cidade)
                .ToListAsync();
        }

        public async Task<IEnumerable<EstabelecimentoSaude>> GetEstabelecimentosByEstadoAsync(string estado)
        {
            return await _dbSet
                .Where(es => es.Estado == estado)
                .ToListAsync();
        }
    }
}