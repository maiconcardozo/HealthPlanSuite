using HealthPlanSuite.Repository.Interface;

namespace HealthPlanSuite.UnitOfWork.Interface
{
    public interface IHealthPlanSuiteUnitOfWork : IDisposable
    {
        // Repositories
        IOperadoraRepository OperadoraRepository { get; }
        ITipoPlanoRepository TipoPlanoRepository { get; }
        IFaixaEtariaRepository FaixaEtariaRepository { get; }
        IEstabelecimentoSaudeRepository EstabelecimentoSaudeRepository { get; }
        IPlanoRepository PlanoRepository { get; }
        ITabelaPrecoRepository TabelaPrecoRepository { get; }
        IReajusteRepository ReajusteRepository { get; }
        IPlanoAbrangenciaRepository PlanoAbrangenciaRepository { get; }
        
        // Transaction methods
        Task<int> CommitAsync();
        Task RollbackAsync();
        Task BeginTransactionAsync();
    }
}