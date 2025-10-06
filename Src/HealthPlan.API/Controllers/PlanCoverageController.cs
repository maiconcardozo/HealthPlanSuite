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
    /// Controller for managing PlanCoverage entities.
    /// Provides comprehensive CRUD operations following the established CleanEntity pattern.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PlanCoverageController : ControllerBase
    {
        private readonly IPlanCoverageService _planCoverageService;
        private readonly IValidator<PlanCoveragePayLoadDTO> validator;

        /// <summary>
        /// Initializes a new instance of the PlanCoverageController.
        /// </summary>
        /// <param name="planCoverageService">Service for plan coverage management operations</param>
        /// <param name="validator">Validator for PlanCoveragePayLoadDTO</param>
        public PlanCoverageController(IPlanCoverageService planCoverageService, IValidator<PlanCoveragePayLoadDTO> validator)
        {
            _planCoverageService = planCoverageService;
            this.validator = validator;
        }

        /// <summary>
        /// Retrieves all plan coverages from the system.
        /// </summary>
        /// <returns>
        /// Returns list of PlanCoverage objects with their details and status on success, validation errors, unauthorized access, or internal server error.
        /// </returns>
        /// <response code="200">Plan coverages retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(PlanCoverageRoutes.GetPlanCoverages)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<PlanCoverageResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetPlanCoverages()
        {
            try
            {
                var planCoverages = _planCoverageService.GetAllActivePlanCoverages();
                var planCoveragesResponse = planCoverages.Select(pc => CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanCoverageResponseDTO>(pc));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(planCoveragesResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Retrieves a plan coverage by its unique identifier.
        /// </summary>
        /// <param name="id">PlanCoverage ID to search for</param>
        /// <returns>Returns PlanCoverage matching the specified ID</returns>
        /// <response code="200">Plan coverage retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Plan coverage not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(PlanCoverageRoutes.GetPlanCoverageById)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PlanCoverageResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetPlanCoverage(int id)
        {
            try
            {
                var planCoverage = _planCoverageService.GetById(id);
                if (planCoverage == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Plan coverage not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var planCoverageResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanCoverageResponseDTO>(planCoverage);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(planCoverageResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Creates a new plan coverage in the system.
        /// </summary>
        /// <param name="planCoveragePayLoad">Plan coverage data to create</param>
        /// <returns>Returns created PlanCoverage on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="201">Plan coverage created successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Plan coverage already exists</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(PlanCoverageRoutes.AddPlanCoverage)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(PlanCoverageResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult CreatePlanCoverage([FromBody] PlanCoveragePayLoadDTO planCoveragePayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(planCoveragePayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var planCoverage = CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanCoverage>(planCoveragePayLoad);
                _planCoverageService.AddPlanCoverage(planCoverage);

                var planCoverageResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanCoverageResponseDTO>(planCoverage);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(planCoverageResponse, "Plan coverage created successfully", HttpContext.Request.Path);
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
        /// Updates an existing plan coverage.
        /// </summary>
        /// <param name="planCoveragePayLoad">Updated plan coverage data including the ID</param>
        /// <returns>Returns updated PlanCoverage on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Plan coverage updated successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Plan coverage not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut(PlanCoverageRoutes.UpdatePlanCoverage)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PlanCoverageResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult UpdatePlanCoverage([FromBody] PlanCoveragePayLoadDTO planCoveragePayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(planCoveragePayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var existingPlanCoverage = _planCoverageService.GetById(planCoveragePayLoad.Id);
                if (existingPlanCoverage == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Plan coverage not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var planCoverage = CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanCoverage>(planCoveragePayLoad);
                planCoverage.Id = planCoveragePayLoad.Id;
                _planCoverageService.UpdatePlanCoverage(planCoverage);

                var planCoverageResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanCoverageResponseDTO>(planCoverage);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(planCoverageResponse, "Plan coverage updated successfully", HttpContext.Request.Path);
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
        /// Deletes a plan coverage from the system.
        /// </summary>
        /// <param name="id">PlanCoverage ID to delete</param>
        /// <returns>Returns confirmation message on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Plan coverage deleted successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Plan coverage not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete(PlanCoverageRoutes.DeletePlanCoverage)]
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
        public IActionResult DeletePlanCoverage(int id)
        {
            try
            {
                var existingPlanCoverage = _planCoverageService.GetById(id);
                if (existingPlanCoverage == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Plan coverage not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _planCoverageService.DeletePlanCoverage(id);
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Plan coverage deleted successfully", "Plan coverage deleted successfully", HttpContext.Request.Path);
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