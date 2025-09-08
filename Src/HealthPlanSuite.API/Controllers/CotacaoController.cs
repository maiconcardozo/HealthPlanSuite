using HealthPlanSuite.Quote.DTO;
using HealthPlanSuite.Quote.Domain.Implementation;
using HealthPlanSuite.Quote.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HealthPlanSuite.API.Controllers
{
    /// <summary>
    /// Controller para gerenciamento de Cotações de Planos de Saúde
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CotacaoController : ControllerBase
    {
        private readonly ICotacaoService _cotacaoService;

        public CotacaoController(ICotacaoService cotacaoService)
        {
            _cotacaoService = cotacaoService ?? throw new ArgumentNullException(nameof(cotacaoService));
        }

        /// <summary>
        /// Obtém todas as cotações (resumo)
        /// </summary>
        /// <returns>Lista de cotações resumidas</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cotacoes = await _cotacaoService.GetAllAsync();
            return Ok(cotacoes);
        }

        /// <summary>
        /// Obtém cotação completa por ID
        /// </summary>
        /// <param name="id">ID da cotação</param>
        /// <returns>Cotação completa</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cotacao = await _cotacaoService.GetByIdAsync(id);
            if (cotacao == null)
                return NotFound($"Cotação com ID {id} não encontrada.");

            return Ok(cotacao);
        }

        /// <summary>
        /// Obtém cotação por protocolo
        /// </summary>
        /// <param name="protocolo">Protocolo da cotação</param>
        /// <returns>Cotação completa</returns>
        [HttpGet("protocolo/{protocolo}")]
        public async Task<IActionResult> GetByProtocolo(string protocolo)
        {
            var cotacao = await _cotacaoService.GetByProtocoloAsync(protocolo);
            if (cotacao == null)
                return NotFound($"Cotação com protocolo {protocolo} não encontrada.");

            return Ok(cotacao);
        }

        /// <summary>
        /// Obtém cotações por beneficiário
        /// </summary>
        /// <param name="beneficiarioId">ID do beneficiário</param>
        /// <returns>Lista de cotações do beneficiário</returns>
        [HttpGet("beneficiario/{beneficiarioId}")]
        public async Task<IActionResult> GetByBeneficiario(int beneficiarioId)
        {
            var cotacoes = await _cotacaoService.GetByBeneficiarioAsync(beneficiarioId);
            return Ok(cotacoes);
        }

        /// <summary>
        /// Obtém cotações por status
        /// </summary>
        /// <param name="status">Status da cotação</param>
        /// <returns>Lista de cotações com o status especificado</returns>
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(StatusCotacao status)
        {
            var cotacoes = await _cotacaoService.GetByStatusAsync(status);
            return Ok(cotacoes);
        }

        /// <summary>
        /// Cria uma nova cotação
        /// </summary>
        /// <param name="cotacaoCreateDto">Dados da cotação</param>
        /// <returns>Cotação criada</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CotacaoCreateDto cotacaoCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cotacao = await _cotacaoService.CreateAsync(cotacaoCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = cotacao.Id }, cotacao);
        }

        /// <summary>
        /// Atualiza uma cotação existente
        /// </summary>
        /// <param name="id">ID da cotação</param>
        /// <param name="cotacaoUpdateDto">Dados atualizados</param>
        /// <returns>Cotação atualizada</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CotacaoCreateDto cotacaoUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _cotacaoService.ExistsAsync(id))
                return NotFound($"Cotação com ID {id} não encontrada.");

            var cotacao = await _cotacaoService.UpdateAsync(id, cotacaoUpdateDto);
            return Ok(cotacao);
        }

        /// <summary>
        /// Atualiza o status de uma cotação
        /// </summary>
        /// <param name="id">ID da cotação</param>
        /// <param name="novoStatus">Novo status</param>
        /// <returns>Cotação atualizada</returns>
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusCotacao novoStatus)
        {
            if (!await _cotacaoService.ExistsAsync(id))
                return NotFound($"Cotação com ID {id} não encontrada.");

            var cotacao = await _cotacaoService.UpdateStatusAsync(id, novoStatus);
            return Ok(cotacao);
        }

        /// <summary>
        /// Remove uma cotação
        /// </summary>
        /// <param name="id">ID da cotação</param>
        /// <returns>Resultado da operação</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _cotacaoService.ExistsAsync(id))
                return NotFound($"Cotação com ID {id} não encontrada.");

            await _cotacaoService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Gera um novo protocolo para cotação
        /// </summary>
        /// <returns>Protocolo gerado</returns>
        [HttpGet("gerar-protocolo")]
        public async Task<IActionResult> GerarProtocolo()
        {
            var protocolo = await _cotacaoService.GerarProtocoloAsync();
            return Ok(new { protocolo });
        }

        /// <summary>
        /// Calcula o valor total de uma cotação
        /// </summary>
        /// <param name="id">ID da cotação</param>
        /// <returns>Valor total calculado</returns>
        [HttpGet("{id}/valor-total")]
        public async Task<IActionResult> CalcularValorTotal(int id)
        {
            if (!await _cotacaoService.ExistsAsync(id))
                return NotFound($"Cotação com ID {id} não encontrada.");

            var valorTotal = await _cotacaoService.CalcularValorTotalAsync(id);
            return Ok(new { cotacaoId = id, valorTotalMensal = valorTotal });
        }

        /// <summary>
        /// Processa expiração automática de cotações
        /// </summary>
        /// <returns>Resultado da operação</returns>
        [HttpPost("processar-expiracao")]
        public async Task<IActionResult> ProcessarExpiracao()
        {
            await _cotacaoService.ProcessarExpiracaoAutomaticaAsync();
            return Ok(new { mensagem = "Processamento de expiração executado com sucesso." });
        }
    }
}