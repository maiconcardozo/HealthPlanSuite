using AutoMapper;
using HealthPlanSuite.Quote.Domain.Implementation;
using HealthPlanSuite.Quote.DTO;
using HealthPlanSuite.Quote.Repository.Interface;
using HealthPlanSuite.Quote.Services.Interface;

namespace HealthPlanSuite.Quote.Services.Implementation
{
    /// <summary>
    /// Implementação do serviço de Operadoras
    /// </summary>
    public class OperadoraService : IOperadoraService
    {
        private readonly IOperadoraRepository _operadoraRepository;
        private readonly IMapper _mapper;

        public OperadoraService(IOperadoraRepository operadoraRepository, IMapper mapper)
        {
            _operadoraRepository = operadoraRepository ?? throw new ArgumentNullException(nameof(operadoraRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<OperadoraDto>> GetAllAsync()
        {
            var operadoras = await _operadoraRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OperadoraDto>>(operadoras);
        }

        public async Task<OperadoraDto?> GetByIdAsync(int id)
        {
            var operadora = await _operadoraRepository.GetByIdAsync(id);
            return operadora != null ? _mapper.Map<OperadoraDto>(operadora) : null;
        }

        public async Task<OperadoraDto?> GetByRegistroANSAsync(string registroANS)
        {
            var operadora = await _operadoraRepository.GetByRegistroANSAsync(registroANS);
            return operadora != null ? _mapper.Map<OperadoraDto>(operadora) : null;
        }

        public async Task<OperadoraDto> CreateAsync(OperadoraCreateDto operadoraCreateDto)
        {
            var operadora = _mapper.Map<Operadora>(operadoraCreateDto);
            operadora.DataCriacao = DateTime.UtcNow;
            operadora.DataAtualizacao = DateTime.UtcNow;

            var createdOperadora = await _operadoraRepository.CreateAsync(operadora);
            return _mapper.Map<OperadoraDto>(createdOperadora);
        }

        public async Task<OperadoraDto> UpdateAsync(int id, OperadoraCreateDto operadoraUpdateDto)
        {
            var existingOperadora = await _operadoraRepository.GetByIdAsync(id);
            if (existingOperadora == null)
                throw new ArgumentException($"Operadora com ID {id} não encontrada.");

            _mapper.Map(operadoraUpdateDto, existingOperadora);
            existingOperadora.DataAtualizacao = DateTime.UtcNow;

            await _operadoraRepository.UpdateAsync(existingOperadora);
            return _mapper.Map<OperadoraDto>(existingOperadora);
        }

        public async Task DeleteAsync(int id)
        {
            await _operadoraRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _operadoraRepository.ExistsAsync(id);
        }

        public async Task<bool> ExistsByRegistroANSAsync(string registroANS)
        {
            return await _operadoraRepository.ExistsByRegistroANSAsync(registroANS);
        }

        public async Task<bool> ExistsByCNPJAsync(string cnpj)
        {
            return await _operadoraRepository.ExistsByCNPJAsync(cnpj);
        }
    }
}