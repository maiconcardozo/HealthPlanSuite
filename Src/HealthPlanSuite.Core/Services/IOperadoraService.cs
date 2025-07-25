using HealthPlanSuite.Core.DTO;

namespace HealthPlanSuite.Core.Services
{
    public interface IOperadoraService
    {
        Task<IEnumerable<OperadoraResponseDTO>> GetAllAsync();
        Task<OperadoraResponseDTO?> GetByIdAsync(int id);
        Task<OperadoraResponseDTO> CreateAsync(OperadoraPayLoadDTO payloadDto);
        Task<OperadoraResponseDTO?> UpdateAsync(int id, OperadoraPayLoadDTO payloadDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> CNPJExistsAsync(string cnpj, int? excludeId = null);
    }
}