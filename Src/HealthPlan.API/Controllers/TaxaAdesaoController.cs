using HealthPlan.API.Resource;
using HealthPlan.API.Swagger;
using HealthPlan.API.Util;
using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.DTO;
using HealthPlan.Quote.Mapping;
using HealthPlan.Quote.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace HealthPlan.API.Controllers
{
    /// <summary>
    /// Controller for managing TaxaAdesao entities (Taxas de Adesão).
    /// Provides comprehensive CRUD operations for adhesion fees of health plans.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TaxaAdesaoController : ControllerBase
    {
        private readonly ITaxaAdesaoService _taxaAdesaoService;

        /// <summary>
        /// Initializes a new instance of the TaxaAdesaoController.
        /// </summary>
        /// <param name="taxaAdesaoService">Service for adhesion fee management operations</param>
        public TaxaAdesaoController(ITaxaAdesaoService taxaAdesaoService)
        {
            _taxaAdesaoService = taxaAdesaoService;
        }

        /// <summary>
        /// Retrieves all adhesion fees from the system.
        /// </summary>
        /// <returns>
        /// Returns list of TaxaAdesao objects with their details and status on success, validation errors, unauthorized access, or internal server error.
        /// </returns>
        /// <response code="200">Adhesion fees retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<TaxaAdesaoResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetTaxaAdesao()
        {
            try
            {
                var taxaAdesao = _taxaAdesaoService.GetAllActiveTaxaAdesao();
                var taxaAdesaoResponse = taxaAdesao.Select(ta => CleanTemplateApplicationMapperInitializer.Mapper.Map<TaxaAdesaoResponseDTO>(ta));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(taxaAdesaoResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (InvalidOperationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
                return Unauthorized(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        /// <summary>
        /// Retrieves a specific adhesion fee by ID.
        /// </summary>
        /// <param name="id">Adhesion fee ID to search for</param>
        /// <returns>Returns TaxaAdesao matching the specified ID</returns>
        /// <response code="200">Adhesion fee retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Adhesion fee not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TaxaAdesaoResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetTaxaAdesao(int id)
        {
            try
            {
                var taxaAdesao = _taxaAdesaoService.GetById(id);
                if (taxaAdesao == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Adhesion fee not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var taxaAdesaoResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<TaxaAdesaoResponseDTO>(taxaAdesao);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(taxaAdesaoResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (InvalidOperationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
                return Unauthorized(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        /// <summary>
        /// ResourceAPI.DocumentationCreateAdhesionFee.
        /// </summary>
        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(TaxaAdesaoResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> CreateTaxaAdesao([FromBody] TaxaAdesaoPayLoadDTO taxaAdesaoPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(taxaAdesaoPayLoad, serviceProvider, this);

            if (validationResult != null)
            {
                return validationResult;
            }

            try
            {
                var taxaAdesao = CleanTemplateApplicationMapperInitializer.Mapper.Map<TaxaAdesao>(taxaAdesaoPayLoad);
                _taxaAdesaoService.AddTaxaAdesao(taxaAdesao);

                var taxaAdesaoResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<TaxaAdesaoResponseDTO>(taxaAdesao);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(taxaAdesaoResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status201Created, successResponse);
            }
            catch (InvalidOperationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForConflict(ex.Message, HttpContext.Request.Path);
                return Conflict(problemDetails);
            }
            catch (ArgumentException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
                return Unauthorized(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        /// <summary>
        /// ResourceAPI.DocumentationUpdateAdhesionFee.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TaxaAdesaoResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> UpdateTaxaAdesao(int id, [FromBody] TaxaAdesaoPayLoadDTO taxaAdesaoPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(taxaAdesaoPayLoad, serviceProvider, this);

            if (validationResult != null)
            {
                return validationResult;
            }

            try
            {
                var existingTaxaAdesao = _taxaAdesaoService.GetById(id);
                if (existingTaxaAdesao == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.AccountNotFound, HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var taxaAdesao = CleanTemplateApplicationMapperInitializer.Mapper.Map<TaxaAdesao>(taxaAdesaoPayLoad);
                taxaAdesao.Id = id;
                _taxaAdesaoService.UpdateTaxaAdesao(taxaAdesao);

                var taxaAdesaoResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<TaxaAdesaoResponseDTO>(taxaAdesao);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(taxaAdesaoResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (ArgumentException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
                return Unauthorized(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        /// <summary>
        /// Deletes an existing adhesion fee.
        /// </summary>
        /// <param name="id">Adhesion fee ID to delete</param>
        /// <returns>Returns confirmation message on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Adhesion fee deleted successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Adhesion fee not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult DeleteTaxaAdesao(int id)
        {
            try
            {
                var existingTaxaAdesao = _taxaAdesaoService.GetById(id);
                if (existingTaxaAdesao == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Adhesion fee not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _taxaAdesaoService.DeleteTaxaAdesao(id);
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Adhesion fee deleted successfully", "Adhesion fee deleted successfully", HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (ArgumentException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
                return Unauthorized(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }
    }
}