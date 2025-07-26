using HealthPlanSuite.DTO;

namespace HealthPlanSuite.Services.Interface
{
    public interface IPlanoService
    {
        Task<PlanoResponseDTO> GetByIdAsync(int id);
        Task<IEnumerable<PlanoResponseDTO>> GetAllAsync();
        Task<PlanoResponseDTO> CreateAsync(PlanoRequestDTO request);
        Task<PlanoResponseDTO> UpdateAsync(int id, PlanoRequestDTO request);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<PlanoResponseDTO>> GetByOperadoraIdAsync(int operadoraId);
        Task<IEnumerable<PlanoResponseDTO>> GetByTipoPlanoIdAsync(int tipoPlanoId);
        Task<IEnumerable<PlanoResponseDTO>> GetByFilterAsync(string? filter, int? operadoraId = null, int? tipoPlanoId = null);
        Task<PlanoResponseDTO?> GetWithAllRelationsAsync(int id);
    }
}