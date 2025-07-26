using Foundation.Base.Repository.Implementation;
using HealthPlanSuite.Repository.Interface;
using HealthPlanSuite.Domain.Implementation;
using HealthPlanSuite.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlanSuite.Repository.Implementation
{
    public class TipoPlanoRepository : EntityRepository<TipoPlano>, ITipoPlanoRepository
    {
        private readonly IHealthPlanSuiteContext _context;

        public TipoPlanoRepository(IHealthPlanSuiteContext context) : base((DbContext)context)
        {
            _context = context;
        }

        public async Task<TipoPlano?> GetByDescricaoAsync(string descricao)
        {
            return await _context.TiposPlano
                .FirstOrDefaultAsync(tp => tp.Descricao == descricao);
        }

        public async Task<IEnumerable<TipoPlano>> GetByFilterAsync(string? filter)
        {
            var query = _context.TiposPlano.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(tp => tp.Descricao.Contains(filter));
            }

            return await query
                .OrderBy(tp => tp.Descricao)
                .ToListAsync();
        }

        public async Task<bool> ExistsByDescricaoAsync(string descricao)
        {
            return await _context.TiposPlano
                .AnyAsync(tp => tp.Descricao == descricao);
        }
    }

    public class FaixaEtariaRepository : EntityRepository<FaixaEtaria>, IFaixaEtariaRepository
    {
        private readonly IHealthPlanSuiteContext _context;

        public FaixaEtariaRepository(IHealthPlanSuiteContext context) : base((DbContext)context)
        {
            _context = context;
        }

        public async Task<FaixaEtaria?> GetByDescricaoAsync(string descricao)
        {
            return await _context.FaixasEtaria
                .FirstOrDefaultAsync(fe => fe.Descricao == descricao);
        }

        public async Task<IEnumerable<FaixaEtaria>> GetByFilterAsync(string? filter)
        {
            var query = _context.FaixasEtaria.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(fe => fe.Descricao.Contains(filter));
            }

            return await query
                .OrderBy(fe => fe.IdadeMin)
                .ToListAsync();
        }

        public async Task<FaixaEtaria?> GetByIdadeAsync(int idade)
        {
            return await _context.FaixasEtaria
                .FirstOrDefaultAsync(fe => fe.IdadeMin <= idade && fe.IdadeMax >= idade);
        }

        public async Task<bool> ExistsByDescricaoAsync(string descricao)
        {
            return await _context.FaixasEtaria
                .AnyAsync(fe => fe.Descricao == descricao);
        }
    }

    public class EstabelecimentoSaudeRepository : EntityRepository<EstabelecimentoSaude>, IEstabelecimentoSaudeRepository
    {
        private readonly IHealthPlanSuiteContext _context;

        public EstabelecimentoSaudeRepository(IHealthPlanSuiteContext context) : base((DbContext)context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EstabelecimentoSaude>> GetByNomeAsync(string nome)
        {
            return await _context.EstabelecimentosSaude
                .Where(es => es.Nome.Contains(nome))
                .OrderBy(es => es.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<EstabelecimentoSaude>> GetByTipoAsync(string tipo)
        {
            return await _context.EstabelecimentosSaude
                .Where(es => es.Tipo == tipo)
                .OrderBy(es => es.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<EstabelecimentoSaude>> GetByCidadeAsync(string cidade)
        {
            return await _context.EstabelecimentosSaude
                .Where(es => es.Cidade == cidade)
                .OrderBy(es => es.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<EstabelecimentoSaude>> GetByEstadoAsync(string estado)
        {
            return await _context.EstabelecimentosSaude
                .Where(es => es.Estado == estado)
                .OrderBy(es => es.Cidade)
                .ThenBy(es => es.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<EstabelecimentoSaude>> GetByFilterAsync(string? filter, string? tipo = null, string? cidade = null, string? estado = null)
        {
            var query = _context.EstabelecimentosSaude.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(es => es.Nome.Contains(filter) || es.Endereco.Contains(filter));
            }

            if (!string.IsNullOrWhiteSpace(tipo))
            {
                query = query.Where(es => es.Tipo == tipo);
            }

            if (!string.IsNullOrWhiteSpace(cidade))
            {
                query = query.Where(es => es.Cidade == cidade);
            }

            if (!string.IsNullOrWhiteSpace(estado))
            {
                query = query.Where(es => es.Estado == estado);
            }

            return await query
                .OrderBy(es => es.Estado)
                .ThenBy(es => es.Cidade)
                .ThenBy(es => es.Nome)
                .ToListAsync();
        }
    }

    public class PlanoRepository : EntityRepository<Plano>, IPlanoRepository
    {
        private readonly IHealthPlanSuiteContext _context;

        public PlanoRepository(IHealthPlanSuiteContext context) : base((DbContext)context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Plano>> GetByOperadoraIdAsync(int operadoraId)
        {
            return await _context.Planos
                .Where(p => p.OperadoraId == operadoraId)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plano>> GetByTipoPlanoIdAsync(int tipoPlanoId)
        {
            return await _context.Planos
                .Where(p => p.TipoPlanoId == tipoPlanoId)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plano>> GetByNomeAsync(string nome)
        {
            return await _context.Planos
                .Where(p => p.Nome.Contains(nome))
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plano>> GetByFilterAsync(string? filter, int? operadoraId = null, int? tipoPlanoId = null)
        {
            var query = _context.Planos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(p => p.Nome.Contains(filter) || p.Cobertura.Contains(filter));
            }

            if (operadoraId.HasValue)
            {
                query = query.Where(p => p.OperadoraId == operadoraId.Value);
            }

            if (tipoPlanoId.HasValue)
            {
                query = query.Where(p => p.TipoPlanoId == tipoPlanoId.Value);
            }

            return await query
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plano>> GetWithTabelasPrecosAsync()
        {
            return await _context.Planos
                .Include(p => p.TabelasPreco)
                .ThenInclude(tp => tp.FaixaEtaria)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plano>> GetWithAbrangenciaAsync()
        {
            return await _context.Planos
                .Include(p => p.PlanosAbrangencia)
                .ThenInclude(pa => pa.EstabelecimentoSaude)
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<Plano?> GetWithAllRelationsAsync(int id)
        {
            return await _context.Planos
                .Include(p => p.Operadora)
                .Include(p => p.TipoPlano)
                .Include(p => p.TabelasPreco)
                .ThenInclude(tp => tp.FaixaEtaria)
                .Include(p => p.Reajustes)
                .Include(p => p.PlanosAbrangencia)
                .ThenInclude(pa => pa.EstabelecimentoSaude)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }

    public class TabelaPrecoRepository : EntityRepository<TabelaPreco>, ITabelaPrecoRepository
    {
        private readonly IHealthPlanSuiteContext _context;

        public TabelaPrecoRepository(IHealthPlanSuiteContext context) : base((DbContext)context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TabelaPreco>> GetByPlanoIdAsync(int planoId)
        {
            return await _context.TabelasPreco
                .Where(tp => tp.PlanoId == planoId)
                .Include(tp => tp.FaixaEtaria)
                .OrderBy(tp => tp.FaixaEtaria.IdadeMin)
                .ToListAsync();
        }

        public async Task<IEnumerable<TabelaPreco>> GetByFaixaEtariaIdAsync(int faixaEtariaId)
        {
            return await _context.TabelasPreco
                .Where(tp => tp.FaixaEtariaId == faixaEtariaId)
                .Include(tp => tp.Plano)
                .OrderBy(tp => tp.Plano.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<TabelaPreco>> GetVigentesAsync(DateTime? dataReferencia = null)
        {
            var data = dataReferencia ?? DateTime.UtcNow;
            return await _context.TabelasPreco
                .Where(tp => tp.DataInicioVigencia <= data && 
                           (tp.DataFimVigencia == null || tp.DataFimVigencia >= data))
                .Include(tp => tp.Plano)
                .Include(tp => tp.FaixaEtaria)
                .OrderBy(tp => tp.Plano.Nome)
                .ThenBy(tp => tp.FaixaEtaria.IdadeMin)
                .ToListAsync();
        }

        public async Task<IEnumerable<TabelaPreco>> GetByPlanoIdAndVigentesAsync(int planoId, DateTime? dataReferencia = null)
        {
            var data = dataReferencia ?? DateTime.UtcNow;
            return await _context.TabelasPreco
                .Where(tp => tp.PlanoId == planoId &&
                           tp.DataInicioVigencia <= data && 
                           (tp.DataFimVigencia == null || tp.DataFimVigencia >= data))
                .Include(tp => tp.FaixaEtaria)
                .OrderBy(tp => tp.FaixaEtaria.IdadeMin)
                .ToListAsync();
        }

        public async Task<TabelaPreco?> GetByPlanoIdAndFaixaEtariaIdAsync(int planoId, int faixaEtariaId, DateTime? dataReferencia = null)
        {
            var data = dataReferencia ?? DateTime.UtcNow;
            return await _context.TabelasPreco
                .Where(tp => tp.PlanoId == planoId && 
                           tp.FaixaEtariaId == faixaEtariaId &&
                           tp.DataInicioVigencia <= data && 
                           (tp.DataFimVigencia == null || tp.DataFimVigencia >= data))
                .Include(tp => tp.Plano)
                .Include(tp => tp.FaixaEtaria)
                .OrderByDescending(tp => tp.DataInicioVigencia)
                .FirstOrDefaultAsync();
        }
    }

    public class ReajusteRepository : EntityRepository<Reajuste>, IReajusteRepository
    {
        private readonly IHealthPlanSuiteContext _context;

        public ReajusteRepository(IHealthPlanSuiteContext context) : base((DbContext)context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reajuste>> GetByPlanoIdAsync(int planoId)
        {
            return await _context.Reajustes
                .Where(r => r.PlanoId == planoId)
                .OrderByDescending(r => r.DataReajuste)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reajuste>> GetByTipoReajusteAsync(string tipoReajuste)
        {
            return await _context.Reajustes
                .Where(r => r.TipoReajuste == tipoReajuste)
                .Include(r => r.Plano)
                .OrderByDescending(r => r.DataReajuste)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reajuste>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _context.Reajustes
                .Where(r => r.DataReajuste >= dataInicio && r.DataReajuste <= dataFim)
                .Include(r => r.Plano)
                .OrderByDescending(r => r.DataReajuste)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reajuste>> GetByPlanoIdAndPeriodoAsync(int planoId, DateTime dataInicio, DateTime dataFim)
        {
            return await _context.Reajustes
                .Where(r => r.PlanoId == planoId && 
                           r.DataReajuste >= dataInicio && 
                           r.DataReajuste <= dataFim)
                .OrderByDescending(r => r.DataReajuste)
                .ToListAsync();
        }

        public async Task<Reajuste?> GetUltimoReajusteByPlanoIdAsync(int planoId)
        {
            return await _context.Reajustes
                .Where(r => r.PlanoId == planoId)
                .OrderByDescending(r => r.DataReajuste)
                .FirstOrDefaultAsync();
        }
    }

    public class PlanoAbrangenciaRepository : EntityRepository<PlanoAbrangencia>, IPlanoAbrangenciaRepository
    {
        private readonly IHealthPlanSuiteContext _context;

        public PlanoAbrangenciaRepository(IHealthPlanSuiteContext context) : base((DbContext)context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlanoAbrangencia>> GetByPlanoIdAsync(int planoId)
        {
            return await _context.PlanosAbrangencia
                .Where(pa => pa.PlanoId == planoId)
                .Include(pa => pa.EstabelecimentoSaude)
                .OrderBy(pa => pa.EstabelecimentoSaude.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<PlanoAbrangencia>> GetByEstabelecimentoIdAsync(int estabelecimentoId)
        {
            return await _context.PlanosAbrangencia
                .Where(pa => pa.EstabelecimentoId == estabelecimentoId)
                .Include(pa => pa.Plano)
                .OrderBy(pa => pa.Plano.Nome)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int planoId, int estabelecimentoId)
        {
            return await _context.PlanosAbrangencia
                .AnyAsync(pa => pa.PlanoId == planoId && pa.EstabelecimentoId == estabelecimentoId);
        }

        public async Task<PlanoAbrangencia?> GetByPlanoIdAndEstabelecimentoIdAsync(int planoId, int estabelecimentoId)
        {
            return await _context.PlanosAbrangencia
                .Include(pa => pa.Plano)
                .Include(pa => pa.EstabelecimentoSaude)
                .FirstOrDefaultAsync(pa => pa.PlanoId == planoId && pa.EstabelecimentoId == estabelecimentoId);
        }

        public async Task<IEnumerable<EstabelecimentoSaude>> GetEstabelecimentosByPlanoIdAsync(int planoId)
        {
            return await _context.PlanosAbrangencia
                .Where(pa => pa.PlanoId == planoId)
                .Select(pa => pa.EstabelecimentoSaude)
                .OrderBy(es => es.Nome)
                .ToListAsync();
        }
    }
}