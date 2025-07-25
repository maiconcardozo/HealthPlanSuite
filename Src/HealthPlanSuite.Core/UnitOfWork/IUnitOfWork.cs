using HealthPlanSuite.Core.Repository;

namespace HealthPlanSuite.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IOperadoraRepository OperadoraRepository { get; }
        ITipoPlanoRepository TipoPlanoRepository { get; }
        IPlanoRepository PlanoRepository { get; }
        IFaixaEtariaRepository FaixaEtariaRepository { get; }
        ITabelaPrecoRepository TabelaPrecoRepository { get; }
        IReajusteRepository ReajusteRepository { get; }
        IEstabelecimentoSaudeRepository EstabelecimentoSaudeRepository { get; }
        IPlanoAbrangenciaRepository PlanoAbrangenciaRepository { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}