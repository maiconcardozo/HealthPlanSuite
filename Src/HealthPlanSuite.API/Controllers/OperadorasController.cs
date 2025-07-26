using Microsoft.AspNetCore.Mvc;
using HealthPlanSuite.Services.Interface;
using HealthPlanSuite.DTO;

namespace HealthPlanSuite.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class OperadorasController : ControllerBase
    {
        private readonly IOperadoraService _operadoraService;

        public OperadorasController(IOperadoraService operadoraService)
        {
            _operadoraService = operadoraService;
        }

        /// <summary>
        /// Obtém todas as operadoras
        /// </summary>
        /// <param name="filter">Filtro opcional para busca por nome ou CNPJ</param>
        /// <returns>Lista de operadoras</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OperadoraResponseDTO>), 200)]
        public async Task<ActionResult<IEnumerable<OperadoraResponseDTO>>> GetAll([FromQuery] string? filter = null)
        {
            try
            {
                var operadoras = string.IsNullOrWhiteSpace(filter) 
                    ? await _operadoraService.GetAllAsync()
                    : await _operadoraService.GetByFilterAsync(filter);
                    
                return Ok(operadoras);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
            }
        }

        /// <summary>
        /// Obtém uma operadora por ID
        /// </summary>
        /// <param name="id">ID da operadora</param>
        /// <returns>Dados da operadora</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OperadoraResponseDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OperadoraResponseDTO>> GetById(int id)
        {
            try
            {
                var operadora = await _operadoraService.GetByIdAsync(id);
                return Ok(operadora);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Operadora com ID {id} não encontrada" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
            }
        }

        /// <summary>
        /// Obtém uma operadora por CNPJ
        /// </summary>
        /// <param name="cnpj">CNPJ da operadora</param>
        /// <returns>Dados da operadora</returns>
        [HttpGet("cnpj/{cnpj}")]
        [ProducesResponseType(typeof(OperadoraResponseDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OperadoraResponseDTO>> GetByCNPJ(string cnpj)
        {
            try
            {
                var operadora = await _operadoraService.GetByCNPJAsync(cnpj);
                
                if (operadora == null)
                    return NotFound(new { message = $"Operadora com CNPJ {cnpj} não encontrada" });
                    
                return Ok(operadora);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
            }
        }

        /// <summary>
        /// Obtém operadoras com seus planos
        /// </summary>
        /// <returns>Lista de operadoras com planos</returns>
        [HttpGet("with-planos")]
        [ProducesResponseType(typeof(IEnumerable<OperadoraResponseDTO>), 200)]
        public async Task<ActionResult<IEnumerable<OperadoraResponseDTO>>> GetWithPlanos()
        {
            try
            {
                var operadoras = await _operadoraService.GetWithPlanosAsync();
                return Ok(operadoras);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
            }
        }

        /// <summary>
        /// Cria uma nova operadora
        /// </summary>
        /// <param name="request">Dados da operadora</param>
        /// <returns>Operadora criada</returns>
        [HttpPost]
        [ProducesResponseType(typeof(OperadoraResponseDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<OperadoraResponseDTO>> Create([FromBody] OperadoraRequestDTO request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { message = "Dados da operadora são obrigatórios" });

                var operadora = await _operadoraService.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = operadora.Id }, operadora);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza uma operadora existente
        /// </summary>
        /// <param name="id">ID da operadora</param>
        /// <param name="request">Dados atualizados da operadora</param>
        /// <returns>Operadora atualizada</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OperadoraResponseDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OperadoraResponseDTO>> Update(int id, [FromBody] OperadoraRequestDTO request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { message = "Dados da operadora são obrigatórios" });

                var operadora = await _operadoraService.UpdateAsync(id, request);
                return Ok(operadora);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Operadora com ID {id} não encontrada" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
            }
        }

        /// <summary>
        /// Remove uma operadora
        /// </summary>
        /// <param name="id">ID da operadora</param>
        /// <returns>Resultado da operação</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var success = await _operadoraService.DeleteAsync(id);
                
                if (!success)
                    return NotFound(new { message = $"Operadora com ID {id} não encontrada" });
                    
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
            }
        }
    }
}