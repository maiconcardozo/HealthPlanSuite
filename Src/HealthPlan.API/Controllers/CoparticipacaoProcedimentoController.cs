using HealthPlan.API.Resource;
using HealthPlan.API.Swagger;
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
    /// Controller for managing CoparticipacaoProcedimento entities (Coparticipação de Procedimentos).
    /// Provides comprehensive CRUD operations for co-participation procedures of health plans.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CoparticipacaoProcedimentoController : ControllerBase
    {
        private readonly ICoparticipacaoProcedimentoService _coparticipacaoProcedimentoService;

        /// <summary>
        /// Initializes a new instance of the CoparticipacaoProcedimentoController.
        /// </summary>
        /// <param name="coparticipacaoProcedimentoService">Service for co-participation procedure management operations</param>
        public CoparticipacaoProcedimentoController(ICoparticipacaoProcedimentoService coparticipacaoProcedimentoService)
        {
            _coparticipacaoProcedimentoService = coparticipacaoProcedimentoService;
        }

        /// <summary>
        /// Retrieves all co-participation procedures from the system.
        /// </summary>
        /// <returns>
        /// Returns list of CoparticipacaoProcedimento objects with their details and status on success, validation errors, unauthorized access, or internal server error.
        /// </returns>
        /// <response code="200">Co-participation procedures retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CoparticipacaoProcedimentoResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetCoparticipacaoProcedimento()
        {
            try
            {
                var coparticipacaoProcedimento = _coparticipacaoProcedimentoService.GetAllActiveCoparticipacaoProcedimento();
                var coparticipacaoProcedimentoResponse = coparticipacaoProcedimento.Select(cp => CleanTemplateApplicationMapperInitializer.Mapper.Map<CoparticipacaoProcedimentoResponseDTO>(cp));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(coparticipacaoProcedimentoResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Retrieves a specific co-participation procedure by ID.
        /// </summary>
        /// <param name="id">Co-participation procedure ID to search for</param>
        /// <returns>Returns CoparticipacaoProcedimento matching the specified ID</returns>
        /// <response code="200">Co-participation procedure retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Co-participation procedure not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CoparticipacaoProcedimentoResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetCoparticipacaoProcedimento(int id)
        {
            try
            {
                var coparticipacaoProcedimento = _coparticipacaoProcedimentoService.GetById(id);
                if (coparticipacaoProcedimento == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Co-participation procedure not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var coparticipacaoProcedimentoResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<CoparticipacaoProcedimentoResponseDTO>(coparticipacaoProcedimento);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(coparticipacaoProcedimentoResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Creates a new co-participation procedure.
        /// </summary>
        /// <param name="coparticipacaoProcedimentoPayLoad">Co-participation procedure data to create</param>
        /// <returns>Returns created CoparticipacaoProcedimento on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="201">Co-participation procedure created successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Co-participation procedure already exists</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(CoparticipacaoProcedimentoResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult CreateCoparticipacaoProcedimento([FromBody] CoparticipacaoProcedimentoPayLoadDTO coparticipacaoProcedimentoPayLoad)
        {
            try
            {
                var coparticipacaoProcedimento = CleanTemplateApplicationMapperInitializer.Mapper.Map<CoparticipacaoProcedimento>(coparticipacaoProcedimentoPayLoad);
                _coparticipacaoProcedimentoService.AddCoparticipacaoProcedimento(coparticipacaoProcedimento);

                var coparticipacaoProcedimentoResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<CoparticipacaoProcedimentoResponseDTO>(coparticipacaoProcedimento);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(coparticipacaoProcedimentoResponse, "Co-participation procedure created successfully", HttpContext.Request.Path);
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
        /// Updates an existing co-participation procedure.
        /// </summary>
        /// <param name="id">Co-participation procedure ID to update</param>
        /// <param name="coparticipacaoProcedimentoPayLoad">Updated co-participation procedure data</param>
        /// <returns>Returns updated CoparticipacaoProcedimento on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Co-participation procedure updated successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Co-participation procedure not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CoparticipacaoProcedimentoResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult UpdateCoparticipacaoProcedimento(int id, [FromBody] CoparticipacaoProcedimentoPayLoadDTO coparticipacaoProcedimentoPayLoad)
        {
            try
            {
                var existingCoparticipacaoProcedimento = _coparticipacaoProcedimentoService.GetById(id);
                if (existingCoparticipacaoProcedimento == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Co-participation procedure not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var coparticipacaoProcedimento = CleanTemplateApplicationMapperInitializer.Mapper.Map<CoparticipacaoProcedimento>(coparticipacaoProcedimentoPayLoad);
                coparticipacaoProcedimento.Id = id;
                _coparticipacaoProcedimentoService.UpdateCoparticipacaoProcedimento(coparticipacaoProcedimento);

                var coparticipacaoProcedimentoResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<CoparticipacaoProcedimentoResponseDTO>(coparticipacaoProcedimento);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(coparticipacaoProcedimentoResponse, "Co-participation procedure updated successfully", HttpContext.Request.Path);
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
        /// Deletes an existing co-participation procedure.
        /// </summary>
        /// <param name="id">Co-participation procedure ID to delete</param>
        /// <returns>Returns confirmation message on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Co-participation procedure deleted successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Co-participation procedure not found</response>
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
        public IActionResult DeleteCoparticipacaoProcedimento(int id)
        {
            try
            {
                var existingCoparticipacaoProcedimento = _coparticipacaoProcedimentoService.GetById(id);
                if (existingCoparticipacaoProcedimento == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Co-participation procedure not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _coparticipacaoProcedimentoService.DeleteCoparticipacaoProcedimento(id);
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Co-participation procedure deleted successfully", "Co-participation procedure deleted successfully", HttpContext.Request.Path);
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