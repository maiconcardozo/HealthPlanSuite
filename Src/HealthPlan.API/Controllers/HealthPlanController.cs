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
    /// Controller for managing Health Plan entities.
    /// Provides comprehensive CRUD operations for health insurance plans offered by companies.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HealthPlanController : ControllerBase
    {
        private readonly IHealthPlanService _healthPlanService;
        private readonly IValidator<HealthPlanPayLoadDTO> validator;

        /// <summary>
        /// Initializes a new instance of the HealthPlanController.
        /// </summary>
        /// <param name="healthPlanService">Service for health plan management operations</param>
        /// <param name="validator">Validator for HealthPlanPayLoadDTO</param>
        public HealthPlanController(IHealthPlanService healthPlanService, IValidator<HealthPlanPayLoadDTO> validator)
        {
            _healthPlanService = healthPlanService;
            this.validator = validator;
        }

        /// <summary>
        /// Retrieves all health plans from the system.
        /// </summary>
        /// <returns>
        /// Returns list of HealthPlan objects with their details and status on success, validation errors, unauthorized access, or internal server error.
        /// </returns>
        /// <response code="200">Health plans retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<HealthPlanResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetHealthPlans()
        {
            try
            {
                var healthPlans = _healthPlanService.GetAllActiveHealthPlans();
                var healthPlansResponse = healthPlans.Select(hp => CleanTemplateApplicationMapperInitializer.Mapper.Map<HealthPlanResponseDTO>(hp));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(healthPlansResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Retrieves a specific health plan by ID.
        /// </summary>
        /// <param name="id">Health plan ID to search for</param>
        /// <returns>Returns HealthPlan matching the specified ID</returns>
        /// <response code="200">Health plan retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Health plan not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(HealthPlanResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetHealthPlan(int id)
        {
            try
            {
                var healthPlan = _healthPlanService.GetById(id);
                if (healthPlan == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Health plan not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var healthPlanResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<HealthPlanResponseDTO>(healthPlan);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(healthPlanResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Creates a new health plan.
        /// </summary>
        /// <param name="healthPlanPayLoad">Health plan data to create</param>
        /// <returns>Returns created HealthPlan on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="201">Health plan created successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Health plan already exists</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(HealthPlanResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult CreateHealthPlan([FromBody] HealthPlanPayLoadDTO healthPlanPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(healthPlanPayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var healthPlan = CleanTemplateApplicationMapperInitializer.Mapper.Map<HealthPlan.Quote.Domain.Implementation.HealthPlan>(healthPlanPayLoad);
                _healthPlanService.AddHealthPlan(healthPlan);

                var healthPlanResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<HealthPlanResponseDTO>(healthPlan);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(healthPlanResponse, "Health plan created successfully", HttpContext.Request.Path);
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
        /// Updates an existing health plan.
        /// </summary>
        /// <param name="healthPlanPayLoad">Updated health plan data including the ID</param>
        /// <returns>Returns updated HealthPlan on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Health plan updated successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Health plan not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(HealthPlanResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult UpdateHealthPlan([FromBody] HealthPlanPayLoadDTO healthPlanPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(healthPlanPayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var existingHealthPlan = _healthPlanService.GetById(healthPlanPayLoad.Id);
                if (existingHealthPlan == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Health plan not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var healthPlan = CleanTemplateApplicationMapperInitializer.Mapper.Map<HealthPlan.Quote.Domain.Implementation.HealthPlan>(healthPlanPayLoad);
                healthPlan.Id = healthPlanPayLoad.Id;
                _healthPlanService.UpdateHealthPlan(healthPlan);

                var healthPlanResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<HealthPlanResponseDTO>(healthPlan);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(healthPlanResponse, "Health plan updated successfully", HttpContext.Request.Path);
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
        /// Deletes an existing health plan.
        /// </summary>
        /// <param name="id">Health plan ID to delete</param>
        /// <returns>Returns confirmation message on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Health plan deleted successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Health plan not found</response>
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
        public IActionResult DeleteHealthPlan(int id)
        {
            try
            {
                var existingHealthPlan = _healthPlanService.GetById(id);
                if (existingHealthPlan == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Health plan not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _healthPlanService.DeleteHealthPlan(id);
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Health plan deleted successfully", "Health plan deleted successfully", HttpContext.Request.Path);
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
        /// Retrieves health plans by company.
        /// Note: This functionality is not implemented in the current service layer.
        /// </summary>
        /// <param name="companyId">Company ID to search health plans for</param>
        /// <returns>Returns message indicating feature not available</returns>
        /// <response code="501">Feature not implemented</response>
        [HttpGet("company/{companyId}")]
        [SwaggerResponse(StatusCodes.Status501NotImplemented, Type = typeof(string))]
        public IActionResult GetHealthPlansByCompany(int companyId)
        {
            // This would require implementing GetHealthPlansByCompany method in IHealthPlanService
            var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError("GetHealthPlansByCompany feature not yet implemented in service layer", HttpContext.Request.Path);
            return StatusCode(StatusCodes.Status501NotImplemented, problemDetails);
        }

        /// <summary>
        /// Searches health plans by code.
        /// Note: This functionality is not implemented in the current service layer.
        /// </summary>
        /// <param name="code">Health plan code to search for</param>
        /// <returns>Returns message indicating feature not available</returns>
        /// <response code="501">Feature not implemented</response>
        [HttpGet("code/{code}")]
        [SwaggerResponse(StatusCodes.Status501NotImplemented, Type = typeof(string))]
        public IActionResult GetHealthPlanByCode(string code)
        {
            // This would require implementing GetByCode method in IHealthPlanService
            var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError("GetByCode feature not yet implemented in service layer", HttpContext.Request.Path);
            return StatusCode(StatusCodes.Status501NotImplemented, problemDetails);
        }
    }
}