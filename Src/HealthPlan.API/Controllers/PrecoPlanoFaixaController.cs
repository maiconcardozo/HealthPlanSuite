using HealthPlan.API.Resource;
using HealthPlan.API.Swagger;
using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.DTO;
using HealthPlan.Quote.Mapping;
using HealthPlan.Quote.Services.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace HealthPlan.API.Controllers
{
    /// <summary>
    /// Controller for managing PrecoPlanoFaixa entities (Preços por Plano e Faixa).
    /// Provides comprehensive CRUD operations for plan price ranges of health plans.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PrecoPlanoFaixaController : ControllerBase
    {
        private readonly IPrecoPlanoFaixaService _precoPlanoFaixaService;
        private readonly IValidator<PrecoPlanoFaixaPayLoadDTO> validator;

        /// <summary>
        /// Initializes a new instance of the PrecoPlanoFaixaController.
        /// </summary>
        /// <param name="precoPlanoFaixaService">Service for plan price range management operations</param>
        /// <param name="validator">Validator for PrecoPlanoFaixaPayLoadDTO</param>
        public PrecoPlanoFaixaController(IPrecoPlanoFaixaService precoPlanoFaixaService, IValidator<PrecoPlanoFaixaPayLoadDTO> validator)
        {
            _precoPlanoFaixaService = precoPlanoFaixaService;
            this.validator = validator;
        }

        /// <summary>
        /// Retrieves all plan price ranges from the system.
        /// </summary>
        /// <returns>
        /// Returns list of PrecoPlanoFaixa objects with their details and status on success, validation errors, unauthorized access, or internal server error.
        /// </returns>
        /// <response code="200">Plan price ranges retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<PrecoPlanoFaixaResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetPrecoPlanoFaixa()
        {
            try
            {
                var precoPlanoFaixa = _precoPlanoFaixaService.GetAllActivePrecoPlanoFaixa();
                var precoPlanoFaixaResponse = precoPlanoFaixa.Select(ppf => CleanTemplateApplicationMapperInitializer.Mapper.Map<PrecoPlanoFaixaResponseDTO>(ppf));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(precoPlanoFaixaResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Retrieves a specific plan price range by ID.
        /// </summary>
        /// <param name="id">Plan price range ID to search for</param>
        /// <returns>Returns PrecoPlanoFaixa matching the specified ID</returns>
        /// <response code="200">Plan price range retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Plan price range not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PrecoPlanoFaixaResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetPrecoPlanoFaixa(int id)
        {
            try
            {
                var precoPlanoFaixa = _precoPlanoFaixaService.GetById(id);
                if (precoPlanoFaixa == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Plan price range not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var precoPlanoFaixaResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<PrecoPlanoFaixaResponseDTO>(precoPlanoFaixa);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(precoPlanoFaixaResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Creates a new plan price range.
        /// </summary>
        /// <param name="precoPlanoFaixaPayLoad">Plan price range data to create</param>
        /// <returns>Returns created PrecoPlanoFaixa on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="201">Plan price range created successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Plan price range already exists</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(PrecoPlanoFaixaResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult CreatePrecoPlanoFaixa([FromBody] PrecoPlanoFaixaPayLoadDTO precoPlanoFaixaPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(precoPlanoFaixaPayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var precoPlanoFaixa = CleanTemplateApplicationMapperInitializer.Mapper.Map<PrecoPlanoFaixa>(precoPlanoFaixaPayLoad);
                _precoPlanoFaixaService.AddPrecoPlanoFaixa(precoPlanoFaixa);

                var precoPlanoFaixaResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<PrecoPlanoFaixaResponseDTO>(precoPlanoFaixa);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(precoPlanoFaixaResponse, "Plan price range created successfully", HttpContext.Request.Path);
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
        /// Updates an existing plan price range.
        /// </summary>
        /// <param name="id">Plan price range ID to update</param>
        /// <param name="precoPlanoFaixaPayLoad">Updated plan price range data</param>
        /// <returns>Returns updated PrecoPlanoFaixa on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Plan price range updated successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Plan price range not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PrecoPlanoFaixaResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult UpdatePrecoPlanoFaixa(int id, [FromBody] PrecoPlanoFaixaPayLoadDTO precoPlanoFaixaPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(precoPlanoFaixaPayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var existingPrecoPlanoFaixa = _precoPlanoFaixaService.GetById(id);
                if (existingPrecoPlanoFaixa == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Plan price range not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var precoPlanoFaixa = CleanTemplateApplicationMapperInitializer.Mapper.Map<PrecoPlanoFaixa>(precoPlanoFaixaPayLoad);
                precoPlanoFaixa.Id = id;
                _precoPlanoFaixaService.UpdatePrecoPlanoFaixa(precoPlanoFaixa);

                var precoPlanoFaixaResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<PrecoPlanoFaixaResponseDTO>(precoPlanoFaixa);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(precoPlanoFaixaResponse, "Plan price range updated successfully", HttpContext.Request.Path);
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
        /// Deletes an existing plan price range.
        /// </summary>
        /// <param name="id">Plan price range ID to delete</param>
        /// <returns>Returns confirmation message on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Plan price range deleted successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Plan price range not found</response>
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
        public IActionResult DeletePrecoPlanoFaixa(int id)
        {
            try
            {
                var existingPrecoPlanoFaixa = _precoPlanoFaixaService.GetById(id);
                if (existingPrecoPlanoFaixa == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Plan price range not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _precoPlanoFaixaService.DeletePrecoPlanoFaixa(id);
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Plan price range deleted successfully", "Plan price range deleted successfully", HttpContext.Request.Path);
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