using HealthPlan.API.Resource;
using HealthPlan.API.Swagger;
using HealthPlan.Domain.Entities;
using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace HealthPlan.API.Controllers
{
    /// <summary>
    /// ResourceAPI.PlanCoverageControllerDescription
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
        /// ResourceAPI.DocumentationGetPlanCoverages
        /// </summary>
        /// <returns>
        /// ResourceAPI.ReturnsListOfPlanCoverageObjectsWithTheirDetailsAndStatusOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError
        /// </returns>
        /// <response code="200">ResourceAPI.PlanCoveragesRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
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
        /// ResourceAPI.DocumentationGetPlanCoverageById
        /// </summary>
        /// <param name="id">PlanCoverage ID to search for</param>
        /// <returns>ResourceAPI.ReturnsPlanCoverageMatchingTheSpecifiedID</returns>
        /// <response code="200">ResourceAPI.PlanCoveragesRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.PlanCoverageNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
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
        /// ResourceAPI.DocumentationAddPlanCoverage
        /// </summary>
        /// <param name="planCoveragePayLoad">Plan coverage data to create</param>
        /// <returns>ResourceAPI.ReturnsCreatedPlanCoverageOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="201">ResourceAPI.PlanCoverageCreatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="409">ResourceAPI.PlanCoverageAlreadyExists</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
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
        /// ResourceAPI.DocumentationUpdatePlanCoverage
        /// </summary>
        /// <param name="planCoveragePayLoad">Updated plan coverage data including the ID</param>
        /// <returns>ResourceAPI.ReturnsUpdatedPlanCoverageOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.PlanCoverageUpdatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.PlanCoverageNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
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
        /// <returns>ResourceAPI.ReturnsConfirmationMessageOnSuccessPlanCoverageDeletionValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.PlanCoverageDeletedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.PlanCoverageNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
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