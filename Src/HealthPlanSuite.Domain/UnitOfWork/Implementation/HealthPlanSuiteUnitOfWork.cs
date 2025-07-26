using HealthPlanSuite.UnitOfWork.Interface;
using HealthPlanSuite.Repository.Interface;
using HealthPlanSuite.Repository.Implementation;
using HealthPlanSuite.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace HealthPlanSuite.UnitOfWork.Implementation
{
    public class HealthPlanSuiteUnitOfWork : IHealthPlanSuiteUnitOfWork
    {
        private readonly IHealthPlanSuiteContext _context;
        private IDbContextTransaction? _transaction;

        // Repository instances
        private IOperadoraRepository? _operadoraRepository;
        private ITipoPlanoRepository? _tipoPlanoRepository;
        private IFaixaEtariaRepository? _faixaEtariaRepository;
        private IEstabelecimentoSaudeRepository? _estabelecimentoSaudeRepository;
        private IPlanoRepository? _planoRepository;
        private ITabelaPrecoRepository? _tabelaPrecoRepository;
        private IReajusteRepository? _reajusteRepository;
        private IPlanoAbrangenciaRepository? _planoAbrangenciaRepository;

        public HealthPlanSuiteUnitOfWork(IHealthPlanSuiteContext context)
        {
            _context = context;
        }

        // Repository properties with lazy initialization
        public IOperadoraRepository OperadoraRepository
        {
            get { return _operadoraRepository ??= new OperadoraRepository(_context); }
        }

        public ITipoPlanoRepository TipoPlanoRepository
        {
            get { return _tipoPlanoRepository ??= new TipoPlanoRepository(_context); }
        }

        public IFaixaEtariaRepository FaixaEtariaRepository
        {
            get { return _faixaEtariaRepository ??= new FaixaEtariaRepository(_context); }
        }

        public IEstabelecimentoSaudeRepository EstabelecimentoSaudeRepository
        {
            get { return _estabelecimentoSaudeRepository ??= new EstabelecimentoSaudeRepository(_context); }
        }

        public IPlanoRepository PlanoRepository
        {
            get { return _planoRepository ??= new PlanoRepository(_context); }
        }

        public ITabelaPrecoRepository TabelaPrecoRepository
        {
            get { return _tabelaPrecoRepository ??= new TabelaPrecoRepository(_context); }
        }

        public IReajusteRepository ReajusteRepository
        {
            get { return _reajusteRepository ??= new ReajusteRepository(_context); }
        }

        public IPlanoAbrangenciaRepository PlanoAbrangenciaRepository
        {
            get { return _planoAbrangenciaRepository ??= new PlanoAbrangenciaRepository(_context); }
        }

        // Transaction methods
        public async Task<int> CommitAsync()
        {
            var result = await _context.SaveChangesAsync();
            
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
            
            return result;
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null && _context is Microsoft.EntityFrameworkCore.DbContext dbContext)
            {
                _transaction = await dbContext.Database.BeginTransactionAsync();
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }
    }
}