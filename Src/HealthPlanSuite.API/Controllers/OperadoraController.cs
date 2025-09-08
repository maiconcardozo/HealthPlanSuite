using HealthPlanSuite.Quote.DTO;
using HealthPlanSuite.Quote.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HealthPlanSuite.API.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de Operadoras de Planos de Saúde
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OperadoraController : ControllerBase
    {
        private readonly IOperadoraService _operadoraService;

        public OperadoraController(IOperadoraService operadoraService)
        {
            _operadoraService = operadoraService ?? throw new ArgumentNullException(nameof(operadoraService));
        }

        /// <summary>
        /// Obtém todas as operadoras
        /// </summary>
        /// <returns>Lista de operadoras</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var operadoras = await _operadoraService.GetAllAsync();
            return Ok(operadoras);
        }

        /// <summary>
        /// Obtém operadora por ID
        /// </summary>
        /// <param name="id">ID da operadora</param>
        /// <returns>Operadora encontrada</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var operadora = await _operadoraService.GetByIdAsync(id);
            if (operadora == null)
                return NotFound($"Operadora com ID {id} não encontrada.");

            return Ok(operadora);
        }

        /// <summary>
        /// Obtém operadora por registro ANS
        /// </summary>
        /// <param name="registroANS">Registro ANS da operadora</param>
        /// <returns>Operadora encontrada</returns>
        [HttpGet("registro-ans/{registroANS}")]
        public async Task<IActionResult> GetByRegistroANS(string registroANS)
        {
            var operadora = await _operadoraService.GetByRegistroANSAsync(registroANS);
            if (operadora == null)
                return NotFound($"Operadora com registro ANS {registroANS} não encontrada.");

            return Ok(operadora);
        }

        /// <summary>
        /// Cria uma nova operadora
        /// </summary>
        /// <param name="operadoraCreateDto">Dados da operadora</param>
        /// <returns>Operadora criada</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OperadoraCreateDto operadoraCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar se já existe operadora com o mesmo registro ANS ou CNPJ
            if (await _operadoraService.ExistsByRegistroANSAsync(operadoraCreateDto.RegistroANS))
                return Conflict($"Já existe operadora com o registro ANS {operadoraCreateDto.RegistroANS}.");

            if (await _operadoraService.ExistsByCNPJAsync(operadoraCreateDto.CNPJ))
                return Conflict($"Já existe operadora com o CNPJ {operadoraCreateDto.CNPJ}.");

            var operadora = await _operadoraService.CreateAsync(operadoraCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = operadora.Id }, operadora);
        }

        /// <summary>
        /// Atualiza uma operadora existente
        /// </summary>
        /// <param name="id">ID da operadora</param>
        /// <param name="operadoraUpdateDto">Dados atualizados</param>
        /// <returns>Operadora atualizada</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OperadoraCreateDto operadoraUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _operadoraService.ExistsAsync(id))
                return NotFound($"Operadora com ID {id} não encontrada.");

            var operadora = await _operadoraService.UpdateAsync(id, operadoraUpdateDto);
            return Ok(operadora);
        }

        /// <summary>
        /// Remove uma operadora
        /// </summary>
        /// <param name="id">ID da operadora</param>
        /// <returns>Resultado da operação</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _operadoraService.ExistsAsync(id))
                return NotFound($"Operadora com ID {id} não encontrada.");

            await _operadoraService.DeleteAsync(id);
            return NoContent();
        }
    }
}