using HealthPlan.Quote.DTO.HealthPlan;
using HealthPlan.Quote.Services.HealthPlan.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HealthPlan.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HealthInsuranceOperatorController : ControllerBase
    {
        private readonly IHealthInsuranceOperatorService _service;

        public HealthInsuranceOperatorController(IHealthInsuranceOperatorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var operators = _service.GetAll();
                var response = operators.Select(o => new HealthInsuranceOperatorResponseDTO
                {
                    Id = o.Id,
                    Name = o.Name,
                    CNPJ = o.CNPJ,
                    Website = o.Website,
                    Phone = o.Phone,
                    CreatedAt = o.CreatedAt,
                    UpdatedAt = o.UpdatedAt
                }).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving health insurance operators.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var @operator = _service.GetById(id);
                if (@operator == null)
                {
                    return NotFound(new { message = "Health insurance operator not found." });
                }

                var response = new HealthInsuranceOperatorResponseDTO
                {
                    Id = @operator.Id,
                    Name = @operator.Name,
                    CNPJ = @operator.CNPJ,
                    Website = @operator.Website,
                    Phone = @operator.Phone,
                    CreatedAt = @operator.CreatedAt,
                    UpdatedAt = @operator.UpdatedAt
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while retrieving the health insurance operator.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HealthInsuranceOperatorPayLoadDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var @operator = new HealthPlan.Quote.Domain.HealthPlan.Implementation.HealthInsuranceOperator
                {
                    Name = dto.Name,
                    CNPJ = dto.CNPJ,
                    Website = dto.Website,
                    Phone = dto.Phone
                };

                var created = _service.Add(@operator);

                var response = new HealthInsuranceOperatorResponseDTO
                {
                    Id = created.Id,
                    Name = created.Name,
                    CNPJ = created.CNPJ,
                    Website = created.Website,
                    Phone = created.Phone,
                    CreatedAt = created.CreatedAt,
                    UpdatedAt = created.UpdatedAt
                };

                return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while creating the health insurance operator.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HealthInsuranceOperatorPayLoadDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existing = _service.GetById(id);
                if (existing == null)
                {
                    return NotFound(new { message = "Health insurance operator not found." });
                }

                existing.Name = dto.Name;
                existing.CNPJ = dto.CNPJ;
                existing.Website = dto.Website;
                existing.Phone = dto.Phone;

                _service.Update(existing);

                var response = new HealthInsuranceOperatorResponseDTO
                {
                    Id = existing.Id,
                    Name = existing.Name,
                    CNPJ = existing.CNPJ,
                    Website = existing.Website,
                    Phone = existing.Phone,
                    CreatedAt = existing.CreatedAt,
                    UpdatedAt = existing.UpdatedAt
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while updating the health insurance operator.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existing = _service.GetById(id);
                if (existing == null)
                {
                    return NotFound(new { message = "Health insurance operator not found." });
                }

                _service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "An error occurred while deleting the health insurance operator.", error = ex.Message });
            }
        }
    }
}