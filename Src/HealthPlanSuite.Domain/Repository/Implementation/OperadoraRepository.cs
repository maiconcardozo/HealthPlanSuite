using Foundation.Base.Repository.Implementation;
using HealthPlanSuite.Repository.Interface;
using HealthPlanSuite.Domain.Implementation;
using HealthPlanSuite.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlanSuite.Repository.Implementation
{
    public class OperadoraRepository : EntityRepository<Operadora>, IOperadoraRepository
    {
        private readonly IHealthPlanSuiteContext _context;

        public OperadoraRepository(IHealthPlanSuiteContext context) : base((DbContext)context)
        {
            _context = context;
        }

        public async Task<Operadora?> GetByCNPJAsync(string cnpj)
        {
            return await _context.Operadoras
                .FirstOrDefaultAsync(o => o.CNPJ == cnpj);
        }

        public async Task<IEnumerable<Operadora>> GetByFilterAsync(string? filter)
        {
            var query = _context.Operadoras.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(o => o.Nome.Contains(filter) || 
                                        o.CNPJ.Contains(filter));
            }

            return await query
                .OrderBy(o => o.Nome)
                .ToListAsync();
        }

        public async Task<bool> ExistsByCNPJAsync(string cnpj)
        {
            return await _context.Operadoras
                .AnyAsync(o => o.CNPJ == cnpj);
        }

        public async Task<IEnumerable<Operadora>> GetWithPlanosAsync()
        {
            return await _context.Operadoras
                .Include(o => o.Planos)
                .OrderBy(o => o.Nome)
                .ToListAsync();
        }
    }
}