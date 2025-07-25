using HealthPlanSuite.Core.Domain.Implementation;
using HealthPlanSuite.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HealthPlanSuite.Core.Repository
{
    public class TabelaPrecoRepository : BaseRepository<TabelaPreco>, ITabelaPrecoRepository
    {
        public TabelaPrecoRepository(HealthPlanSuiteDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TabelaPreco>> GetTabelasWithDetailsAsync()
        {
            return await _dbSet
                .Include(tp => tp.Plano)
                .Include(tp => tp.FaixaEtaria)
                .ToListAsync();
        }

        public async Task<TabelaPreco?> GetTabelaWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(tp => tp.Plano)
                .Include(tp => tp.FaixaEtaria)
                .FirstOrDefaultAsync(tp => tp.Id == id);
        }

        public async Task<IEnumerable<TabelaPreco>> GetTabelasByPlanoAsync(int planoId)
        {
            return await _dbSet
                .Include(tp => tp.FaixaEtaria)
                .Where(tp => tp.PlanoId == planoId)
                .ToListAsync();
        }

        public async Task<TabelaPreco?> GetTabelaVigenteAsync(int planoId, int faixaEtariaId, DateTime data)
        {
            return await _dbSet
                .Include(tp => tp.Plano)
                .Include(tp => tp.FaixaEtaria)
                .Where(tp => tp.PlanoId == planoId && 
                           tp.FaixaEtariaId == faixaEtariaId &&
                           tp.DataInicioVigencia <= data &&
                           (tp.DataFimVigencia == null || tp.DataFimVigencia >= data))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TabelaPreco>> GetTabelasVigentesAsync(DateTime data)
        {
            return await _dbSet
                .Include(tp => tp.Plano)
                .Include(tp => tp.FaixaEtaria)
                .Where(tp => tp.DataInicioVigencia <= data &&
                           (tp.DataFimVigencia == null || tp.DataFimVigencia >= data))
                .ToListAsync();
        }
    }
}