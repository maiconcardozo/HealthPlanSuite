using HealthPlanSuite.Quote.DTO;

namespace HealthPlanSuite.Quote.Services.Interface
{
    /// <summary>
    /// Interface do serviço de Operadoras
    /// </summary>
    public interface IOperadoraService
    {
        Task<IEnumerable<OperadoraDto>> GetAllAsync();
        Task<OperadoraDto?> GetByIdAsync(int id);
        Task<OperadoraDto?> GetByRegistroANSAsync(string registroANS);
        Task<OperadoraDto> CreateAsync(OperadoraCreateDto operadoraCreateDto);
        Task<OperadoraDto> UpdateAsync(int id, OperadoraCreateDto operadoraUpdateDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByRegistroANSAsync(string registroANS);
        Task<bool> ExistsByCNPJAsync(string cnpj);
    }
}