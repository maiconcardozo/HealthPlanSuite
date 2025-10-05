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
    /// Controller for managing AcceptanceRule entities.
    /// Provides comprehensive CRUD operations following the established CleanEntity pattern.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AcceptanceRuleController : ControllerBase
    {
        private readonly IAcceptanceRuleService _acceptanceRuleService;
        private readonly IValidator<AcceptanceRulePayLoadDTO> validator;

        /// <summary>
        /// Initializes a new instance of the AcceptanceRuleController.
        /// </summary>
        /// <param name="acceptanceRuleService">Service for acceptance rule management operations</param>
        /// <param name="validator">Validator for AcceptanceRulePayLoadDTO</param>
        public AcceptanceRuleController(IAcceptanceRuleService acceptanceRuleService, IValidator<AcceptanceRulePayLoadDTO> validator)
        {
            _acceptanceRuleService = acceptanceRuleService;
            this.validator = validator;
        }

        /// <summary>
        /// Retrieves all acceptance rules from the system.
        /// </summary>
        /// <returns>
        /// Returns list of AcceptanceRule objects with their details and status on success, validation errors, unauthorized access, or internal server error.
        /// </returns>
        /// <response code="200">Acceptance rules retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(AcceptanceRuleRoutes.GetAcceptanceRules)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<AcceptanceRuleResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetAcceptanceRules()
        {
            try
            {
                var acceptanceRules = _acceptanceRuleService.GetAllActiveAcceptanceRules();
                var acceptanceRulesResponse = acceptanceRules.Select(ar => CleanTemplateApplicationMapperInitializer.Mapper.Map<AcceptanceRuleResponseDTO>(ar));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(acceptanceRulesResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Retrieves an acceptance rule by its unique identifier.
        /// </summary>
        /// <param name="id">AcceptanceRule ID to search for</param>
        /// <returns>Returns AcceptanceRule matching the specified ID</returns>
        /// <response code="200">Acceptance rule retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Acceptance rule not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(AcceptanceRuleRoutes.GetAcceptanceRuleById)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AcceptanceRuleResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetAcceptanceRule(int id)
        {
            try
            {
                var acceptanceRule = _acceptanceRuleService.GetById(id);
                if (acceptanceRule == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Acceptance rule not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var acceptanceRuleResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<AcceptanceRuleResponseDTO>(acceptanceRule);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(acceptanceRuleResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Creates a new acceptance rule in the system.
        /// </summary>
        /// <param name="acceptanceRulePayLoad">Acceptance rule data to create</param>
        /// <returns>Returns created AcceptanceRule on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="201">Acceptance rule created successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Acceptance rule already exists</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(AcceptanceRuleRoutes.AddAcceptanceRule)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(AcceptanceRuleResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult CreateAcceptanceRule([FromBody] AcceptanceRulePayLoadDTO acceptanceRulePayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(acceptanceRulePayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var acceptanceRule = CleanTemplateApplicationMapperInitializer.Mapper.Map<AcceptanceRule>(acceptanceRulePayLoad);
                _acceptanceRuleService.AddAcceptanceRule(acceptanceRule);

                var acceptanceRuleResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<AcceptanceRuleResponseDTO>(acceptanceRule);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(acceptanceRuleResponse, "Acceptance rule created successfully", HttpContext.Request.Path);
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
        /// Updates an existing acceptance rule.
        /// </summary>
        /// <param name="id">AcceptanceRule ID to update</param>
        /// <param name="acceptanceRulePayLoad">Updated acceptance rule data</param>
        /// <returns>Returns updated AcceptanceRule on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Acceptance rule updated successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Acceptance rule not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut(AcceptanceRuleRoutes.UpdateAcceptanceRule)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AcceptanceRuleResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult UpdateAcceptanceRule(int id, [FromBody] AcceptanceRulePayLoadDTO acceptanceRulePayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(acceptanceRulePayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var existingAcceptanceRule = _acceptanceRuleService.GetById(id);
                if (existingAcceptanceRule == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Acceptance rule not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var acceptanceRule = CleanTemplateApplicationMapperInitializer.Mapper.Map<AcceptanceRule>(acceptanceRulePayLoad);
                acceptanceRule.Id = id;
                _acceptanceRuleService.UpdateAcceptanceRule(acceptanceRule);

                var acceptanceRuleResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<AcceptanceRuleResponseDTO>(acceptanceRule);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(acceptanceRuleResponse, "Acceptance rule updated successfully", HttpContext.Request.Path);
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
        /// Deletes an acceptance rule from the system.
        /// </summary>
        /// <param name="id">AcceptanceRule ID to delete</param>
        /// <returns>Returns confirmation message on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Acceptance rule deleted successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Acceptance rule not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete(AcceptanceRuleRoutes.DeleteAcceptanceRule)]
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
        public IActionResult DeleteAcceptanceRule(int id)
        {
            try
            {
                var existingAcceptanceRule = _acceptanceRuleService.GetById(id);
                if (existingAcceptanceRule == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Acceptance rule not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _acceptanceRuleService.DeleteAcceptanceRule(id);
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Acceptance rule deleted successfully", "Acceptance rule deleted successfully", HttpContext.Request.Path);
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