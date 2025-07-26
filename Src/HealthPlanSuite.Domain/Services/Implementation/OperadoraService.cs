using HealthPlanSuite.Services.Interface;
using HealthPlanSuite.UnitOfWork.Interface;
using HealthPlanSuite.DTO;
using HealthPlanSuite.Mapping;

namespace HealthPlanSuite.Services.Implementation
{
    public class OperadoraService : IOperadoraService
    {
        private readonly IHealthPlanSuiteUnitOfWork _unitOfWork;

        public OperadoraService(IHealthPlanSuiteUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperadoraResponseDTO> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.OperadoraRepository.GetByIdAsync(id);
            
            if (entity == null)
                throw new KeyNotFoundException($"Operadora com ID {id} não encontrada");
                
            return entity.MapToResponseDTO();
        }

        public async Task<IEnumerable<OperadoraResponseDTO>> GetAllAsync()
        {
            var entities = await _unitOfWork.OperadoraRepository.GetAllAsync();
            return entities.Select(e => e.MapToResponseDTO());
        }

        public async Task<OperadoraResponseDTO> CreateAsync(OperadoraRequestDTO request)
        {
            // Validações de negócio
            await ValidateBusinessRulesForCreate(request);
            
            // Criar entidade
            var entity = request.MapToEntity();
            
            // Persistir
            await _unitOfWork.OperadoraRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            
            return entity.MapToResponseDTO();
        }

        public async Task<OperadoraResponseDTO> UpdateAsync(int id, OperadoraRequestDTO request)
        {
            var entity = await _unitOfWork.OperadoraRepository.GetByIdAsync(id);
            
            if (entity == null)
                throw new KeyNotFoundException($"Operadora com ID {id} não encontrada");
            
            // Validações de negócio
            await ValidateBusinessRulesForUpdate(request, entity);
            
            // Atualizar entidade
            entity.SetNome(request.Nome);
            entity.SetCNPJ(request.CNPJ);
            entity.SetSite(request.Site);
            entity.SetTelefone(request.Telefone);
            
            // Persistir
            await _unitOfWork.OperadoraRepository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            
            return entity.MapToResponseDTO();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.OperadoraRepository.GetByIdAsync(id);
            
            if (entity == null)
                return false;
            
            await _unitOfWork.OperadoraRepository.DeleteAsync(entity);
            await _unitOfWork.CommitAsync();
            
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _unitOfWork.OperadoraRepository.ExistsAsync(id);
        }

        public async Task<OperadoraResponseDTO?> GetByCNPJAsync(string cnpj)
        {
            var entity = await _unitOfWork.OperadoraRepository.GetByCNPJAsync(cnpj);
            return entity?.MapToResponseDTO();
        }

        public async Task<IEnumerable<OperadoraResponseDTO>> GetByFilterAsync(string? filter)
        {
            var entities = await _unitOfWork.OperadoraRepository.GetByFilterAsync(filter);
            return entities.Select(e => e.MapToResponseDTO());
        }

        public async Task<IEnumerable<OperadoraResponseDTO>> GetWithPlanosAsync()
        {
            var entities = await _unitOfWork.OperadoraRepository.GetWithPlanosAsync();
            return entities.Select(e => e.MapToResponseDTO());
        }

        // Métodos de validação privados
        private async Task ValidateBusinessRulesForCreate(OperadoraRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Nome))
                throw new ArgumentException("Nome é obrigatório");
                
            if (string.IsNullOrWhiteSpace(request.CNPJ))
                throw new ArgumentException("CNPJ é obrigatório");
                
            // Verificar se já existe operadora com o mesmo CNPJ
            var existingEntity = await _unitOfWork.OperadoraRepository.GetByCNPJAsync(request.CNPJ);
            if (existingEntity != null)
                throw new InvalidOperationException("Já existe uma operadora com este CNPJ");
        }

        private async Task ValidateBusinessRulesForUpdate(OperadoraRequestDTO request, Domain.Implementation.Operadora existingEntity)
        {
            if (string.IsNullOrWhiteSpace(request.Nome))
                throw new ArgumentException("Nome é obrigatório");
                
            if (string.IsNullOrWhiteSpace(request.CNPJ))
                throw new ArgumentException("CNPJ é obrigatório");
                
            // Verificar se o novo CNPJ já existe em outra operadora
            var duplicateEntity = await _unitOfWork.OperadoraRepository.GetByCNPJAsync(request.CNPJ);
            if (duplicateEntity != null && duplicateEntity.Id != existingEntity.Id)
                throw new InvalidOperationException("Já existe uma operadora com este CNPJ");
        }
    }
}