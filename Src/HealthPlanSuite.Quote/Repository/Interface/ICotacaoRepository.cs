using HealthPlanSuite.Quote.Domain.Implementation;

namespace HealthPlanSuite.Quote.Repository.Interface
{
    /// <summary>
    /// Interface do repositório de Cotações
    /// </summary>
    public interface ICotacaoRepository
    {
        Task<IEnumerable<Cotacao>> GetAllAsync();
        Task<Cotacao?> GetByIdAsync(int id);
        Task<Cotacao?> GetByProtocoloAsync(string protocolo);
        Task<IEnumerable<Cotacao>> GetByBeneficiarioAsync(int beneficiarioId);
        Task<IEnumerable<Cotacao>> GetByStatusAsync(StatusCotacao status);
        Task<IEnumerable<Cotacao>> GetByDataCotacaoAsync(DateTime dataInicio, DateTime dataFim);
        Task<Cotacao> CreateAsync(Cotacao cotacao);
        Task UpdateAsync(Cotacao cotacao);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByProtocoloAsync(string protocolo);
        Task<IEnumerable<Cotacao>> GetExpirandoAsync(DateTime dataLimite);
    }
}