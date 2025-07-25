using HealthPlanSuite.Core.DTO;

namespace HealthPlanSuite.Core.Services
{
    public interface IPlanoService
    {
        Task<IEnumerable<PlanoResponseDTO>> GetAllAsync();
        Task<PlanoResponseDTO?> GetByIdAsync(int id);
        Task<PlanoResponseDTO> CreateAsync(PlanoPayLoadDTO payloadDto);
        Task<PlanoResponseDTO?> UpdateAsync(int id, PlanoPayLoadDTO payloadDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<PlanoResponseDTO>> GetPlanosByOperadoraAsync(int operadoraId);
        Task<IEnumerable<PlanoResponseDTO>> GetPlanosByTipoPlanoAsync(int tipoPlanoId);
    }
}