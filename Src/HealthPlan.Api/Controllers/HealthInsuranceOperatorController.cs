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
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(HealthInsuranceOperatorListResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var operators = _service.GetAll();
                var response = operators.Select(o => AuthenticationLoginProfileMapperInitializer.Mapper.Map<HealthInsuranceOperatorResponseDTO>(o)).ToList();
                return Ok(response);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredHealthInsuranceOperatorsCouldNotBeRetrieved);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpGet(HealthInsuranceOperatorRoutes.GetHealthInsuranceOperatorById)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(HealthInsuranceOperatorResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(HealthInsuranceOperatorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var @operator = _service.GetById(id);
                if (@operator == null)
                {
                    var notFoundProblemDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.HealthInsuranceOperatorNotFound);
                    return NotFound(notFoundProblemDetails);
                }

                var response = AuthenticationLoginProfileMapperInitializer.Mapper.Map<HealthInsuranceOperatorResponseDTO>(@operator);
                return Ok(response);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredHealthInsuranceOperatorCouldNotBeRetrieved);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpPost(HealthInsuranceOperatorRoutes.AddHealthInsuranceOperator)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(HealthInsuranceOperatorResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(HealthInsuranceOperatorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> Create([FromBody] HealthInsuranceOperatorPayLoadDTO dto, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(dto, serviceProvider, this);
            if (validationResult != null)
                return validationResult;

            var @operator = AuthenticationLoginProfileMapperInitializer.Mapper.Map<HealthInsuranceOperator>(dto);

            try
            {
                var created = _service.Add(@operator);
                var response = AuthenticationLoginProfileMapperInitializer.Mapper.Map<HealthInsuranceOperatorResponseDTO>(created);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message);
                return BadRequest(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredHealthInsuranceOperatorCouldNotBeInserted);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpPut(HealthInsuranceOperatorRoutes.UpdateHealthInsuranceOperator)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(HealthInsuranceOperatorResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(HealthInsuranceOperatorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> Update(int id, [FromBody] HealthInsuranceOperatorPayLoadDTO dto, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(dto, serviceProvider, this);
            if (validationResult != null)
                return validationResult;

            try
            {
                var existing = _service.GetById(id);
                if (existing == null)
                {
                    var notFoundProblemDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.HealthInsuranceOperatorNotFound);
                    return NotFound(notFoundProblemDetails);
                }

                // Update the existing entity with new values
                existing.Name = dto.Name;
                existing.CNPJ = dto.CNPJ;
                existing.Website = dto.Website;
                existing.Phone = dto.Phone;

                _service.Update(existing);

                var response = AuthenticationLoginProfileMapperInitializer.Mapper.Map<HealthInsuranceOperatorResponseDTO>(existing);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message);
                return BadRequest(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredHealthInsuranceOperatorCouldNotBeUpdated);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpDelete(HealthInsuranceOperatorRoutes.DeleteHealthInsuranceOperator)]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existing = _service.GetById(id);
                if (existing == null)
                {
                    var notFoundProblemDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.HealthInsuranceOperatorNotFound);
                    return NotFound(notFoundProblemDetails);
                }

                _service.Delete(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message);
                return BadRequest(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredHealthInsuranceOperatorCouldNotBeDeleted);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }
    }
}