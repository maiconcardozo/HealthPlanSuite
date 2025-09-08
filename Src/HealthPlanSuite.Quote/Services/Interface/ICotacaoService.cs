using HealthPlanSuite.Quote.DTO;
using HealthPlanSuite.Quote.Domain.Implementation;

namespace HealthPlanSuite.Quote.Services.Interface
{
    /// <summary>
    /// Interface do serviço de Cotações
    /// </summary>
    public interface ICotacaoService
    {
        Task<IEnumerable<CotacaoResumoDto>> GetAllAsync();
        Task<CotacaoDto?> GetByIdAsync(int id);
        Task<CotacaoDto?> GetByProtocoloAsync(string protocolo);
        Task<IEnumerable<CotacaoResumoDto>> GetByBeneficiarioAsync(int beneficiarioId);
        Task<IEnumerable<CotacaoResumoDto>> GetByStatusAsync(StatusCotacao status);
        Task<CotacaoDto> CreateAsync(CotacaoCreateDto cotacaoCreateDto);
        Task<CotacaoDto> UpdateAsync(int id, CotacaoCreateDto cotacaoUpdateDto);
        Task<CotacaoDto> UpdateStatusAsync(int id, StatusCotacao novoStatus);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<string> GerarProtocoloAsync();
        Task<decimal> CalcularValorTotalAsync(int cotacaoId);
        Task ProcessarExpiracaoAutomaticaAsync();
    }
}