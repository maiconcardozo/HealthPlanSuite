using HealthPlanSuite.Core.Infrastructure;
using HealthPlanSuite.Core.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace HealthPlanSuite.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HealthPlanSuiteDbContext _context;
        private IDbContextTransaction? _transaction;

        private IOperadoraRepository? _operadoraRepository;
        private ITipoPlanoRepository? _tipoPlanoRepository;
        private IPlanoRepository? _planoRepository;
        private IFaixaEtariaRepository? _faixaEtariaRepository;
        private ITabelaPrecoRepository? _tabelaPrecoRepository;
        private IReajusteRepository? _reajusteRepository;
        private IEstabelecimentoSaudeRepository? _estabelecimentoSaudeRepository;
        private IPlanoAbrangenciaRepository? _planoAbrangenciaRepository;

        public UnitOfWork(HealthPlanSuiteDbContext context)
        {
            _context = context;
        }

        public IOperadoraRepository OperadoraRepository =>
            _operadoraRepository ??= new OperadoraRepository(_context);

        public ITipoPlanoRepository TipoPlanoRepository =>
            _tipoPlanoRepository ??= new TipoPlanoRepository(_context);

        public IPlanoRepository PlanoRepository =>
            _planoRepository ??= new PlanoRepository(_context);

        public IFaixaEtariaRepository FaixaEtariaRepository =>
            _faixaEtariaRepository ??= new FaixaEtariaRepository(_context);

        public ITabelaPrecoRepository TabelaPrecoRepository =>
            _tabelaPrecoRepository ??= new TabelaPrecoRepository(_context);

        public IReajusteRepository ReajusteRepository =>
            _reajusteRepository ??= new ReajusteRepository(_context);

        public IEstabelecimentoSaudeRepository EstabelecimentoSaudeRepository =>
            _estabelecimentoSaudeRepository ??= new EstabelecimentoSaudeRepository(_context);

        public IPlanoAbrangenciaRepository PlanoAbrangenciaRepository =>
            _planoAbrangenciaRepository ??= new PlanoAbrangenciaRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}