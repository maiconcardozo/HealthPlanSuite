using HealthPlanSuite.DTO;

namespace HealthPlanSuite.Services.Interface
{
    public interface IOperadoraService
    {
        Task<OperadoraResponseDTO> GetByIdAsync(int id);
        Task<IEnumerable<OperadoraResponseDTO>> GetAllAsync();
        Task<OperadoraResponseDTO> CreateAsync(OperadoraRequestDTO request);
        Task<OperadoraResponseDTO> UpdateAsync(int id, OperadoraRequestDTO request);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<OperadoraResponseDTO?> GetByCNPJAsync(string cnpj);
        Task<IEnumerable<OperadoraResponseDTO>> GetByFilterAsync(string? filter);
        Task<IEnumerable<OperadoraResponseDTO>> GetWithPlanosAsync();
    }
}