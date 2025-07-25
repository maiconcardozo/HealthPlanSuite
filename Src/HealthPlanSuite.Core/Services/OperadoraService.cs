using AutoMapper;
using HealthPlanSuite.Core.Domain.Implementation;
using HealthPlanSuite.Core.DTO;
using HealthPlanSuite.Core.UnitOfWork;

namespace HealthPlanSuite.Core.Services
{
    public class OperadoraService : IOperadoraService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OperadoraService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OperadoraResponseDTO>> GetAllAsync()
        {
            var operadoras = await _unitOfWork.OperadoraRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OperadoraResponseDTO>>(operadoras);
        }

        public async Task<OperadoraResponseDTO?> GetByIdAsync(int id)
        {
            var operadora = await _unitOfWork.OperadoraRepository.GetByIdAsync(id);
            return operadora != null ? _mapper.Map<OperadoraResponseDTO>(operadora) : null;
        }

        public async Task<OperadoraResponseDTO> CreateAsync(OperadoraPayLoadDTO payloadDto)
        {
            var operadora = _mapper.Map<Operadora>(payloadDto);
            operadora.DataCriacao = DateTime.UtcNow;
            operadora.DataAtualizacao = DateTime.UtcNow;

            await _unitOfWork.OperadoraRepository.AddAsync(operadora);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OperadoraResponseDTO>(operadora);
        }

        public async Task<OperadoraResponseDTO?> UpdateAsync(int id, OperadoraPayLoadDTO payloadDto)
        {
            var existingOperadora = await _unitOfWork.OperadoraRepository.GetByIdAsync(id);
            if (existingOperadora == null)
                return null;

            _mapper.Map(payloadDto, existingOperadora);
            existingOperadora.DataAtualizacao = DateTime.UtcNow;

            await _unitOfWork.OperadoraRepository.UpdateAsync(existingOperadora);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OperadoraResponseDTO>(existingOperadora);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _unitOfWork.OperadoraRepository.DeleteAsync(id);
            if (deleted)
                await _unitOfWork.SaveChangesAsync();

            return deleted;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _unitOfWork.OperadoraRepository.ExistsAsync(id);
        }

        public async Task<bool> CNPJExistsAsync(string cnpj, int? excludeId = null)
        {
            return await _unitOfWork.OperadoraRepository.CNPJExistsAsync(cnpj, excludeId);
        }
    }
}