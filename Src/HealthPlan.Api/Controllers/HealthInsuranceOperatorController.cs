using HealthPlan.API.Resource;
using HealthPlan.API.Swagger;
using HealthPlan.Quote.DTO.HealthPlan;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Services.HealthPlan.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace HealthPlan.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthInsuranceOperatorController : ControllerBase
    {
        private readonly IHealthInsuranceOperatorService _service;

        public HealthInsuranceOperatorController(IHealthInsuranceOperatorService service)
        {
            _service = service;
        }

        [HttpGet(HealthInsuranceOperatorRoutes.GetHealthInsuranceOperators)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(List<HealthInsuranceOperatorResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(HealthInsuranceOperatorListResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetAll()
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
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Internal Server Error",
                    Status = 500,
                    Detail = "An error occurred while processing your request."
                };
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpGet(HealthInsuranceOperatorRoutes.GetHealthInsuranceOperatorById)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(HealthInsuranceOperatorResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(HealthInsuranceOperatorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetById(int id)
        {
            try
            {
                var @operator = _service.GetById(id);
                if (@operator == null)
                {
                    var notFoundProblemDetails = new ProblemDetails
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                        Title = "Not Found",
                        Status = 404,
                        Detail = "Health insurance operator not found."
                    };
                    return NotFound(notFoundProblemDetails);
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
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Internal Server Error",
                    Status = 500,
                    Detail = "An error occurred while processing your request."
                };
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpPost(HealthInsuranceOperatorRoutes.AddHealthInsuranceOperator)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(HealthInsuranceOperatorResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(HealthInsuranceOperatorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult Create([FromBody] HealthInsuranceOperatorPayLoadDTO dto)
        {
            if (dto == null)
            {
                var badRequestProblemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "Bad Request",
                    Status = 400,
                    Detail = "The request body is invalid."
                };
                return BadRequest(badRequestProblemDetails);
            }

            var @operator = new HealthInsuranceOperator
            {
                Name = dto.Name,
                CNPJ = dto.CNPJ,
                Website = dto.Website,
                Phone = dto.Phone,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            try
            {
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
            catch (InvalidOperationException ex)
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "Bad Request",
                    Status = 400,
                    Detail = ex.Message
                };
                return BadRequest(problemDetails);
            }
            catch (Exception ex)
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Internal Server Error",
                    Status = 500,
                    Detail = "An error occurred while processing your request."
                };
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpPut(HealthInsuranceOperatorRoutes.UpdateHealthInsuranceOperator)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(HealthInsuranceOperatorResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(HealthInsuranceOperatorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult Update(int id, [FromBody] HealthInsuranceOperatorPayLoadDTO dto)
        {
            if (dto == null)
            {
                var badRequestProblemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "Bad Request",
                    Status = 400,
                    Detail = "The request body is invalid."
                };
                return BadRequest(badRequestProblemDetails);
            }

            try
            {
                var existing = _service.GetById(id);
                if (existing == null)
                {
                    var notFoundProblemDetails = new ProblemDetails
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                        Title = "Not Found",
                        Status = 404,
                        Detail = "Health insurance operator not found."
                    };
                    return NotFound(notFoundProblemDetails);
                }

                // Update the existing entity with new values
                existing.Name = dto.Name;
                existing.CNPJ = dto.CNPJ;
                existing.Website = dto.Website;
                existing.Phone = dto.Phone;
                existing.UpdatedAt = DateTime.UtcNow;

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
            catch (InvalidOperationException ex)
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "Bad Request",
                    Status = 400,
                    Detail = ex.Message
                };
                return BadRequest(problemDetails);
            }
            catch (Exception ex)
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Internal Server Error", 
                    Status = 500,
                    Detail = "An error occurred while processing your request."
                };
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpDelete(HealthInsuranceOperatorRoutes.DeleteHealthInsuranceOperator)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult Delete(int id)
        {
            try
            {
                var existing = _service.GetById(id);
                if (existing == null)
                {
                    var notFoundProblemDetails = new ProblemDetails
                    {
                        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                        Title = "Not Found",
                        Status = 404,
                        Detail = "Health insurance operator not found."
                    };
                    return NotFound(notFoundProblemDetails);
                }

                _service.Delete(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "Bad Request",
                    Status = 400,
                    Detail = ex.Message
                };
                return BadRequest(problemDetails);
            }
            catch (Exception ex)
            {
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Internal Server Error",
                    Status = 500,
                    Detail = "An error occurred while processing your request."
                };
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }
    }
}