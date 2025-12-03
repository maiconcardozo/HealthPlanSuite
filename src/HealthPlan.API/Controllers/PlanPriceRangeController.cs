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
    /// ResourceAPI.PlanPriceRangeControllerDescription
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PlanPriceRangeController : ControllerBase
    {
        private readonly IPlanPriceRangeService _planPriceRangeService;
        private readonly IValidator<PlanPriceRangePayLoadDTO> validator;

        /// <summary>
        /// Initializes a new instance of the PlanPriceRangeController.
        /// </summary>
        /// <param name="planPriceRangeService">Service for plan price range management operations</param>
        /// <param name="validator">Validator for PlanPriceRangePayLoadDTO</param>
        public PlanPriceRangeController(IPlanPriceRangeService planPriceRangeService, IValidator<PlanPriceRangePayLoadDTO> validator)
        {
            _planPriceRangeService = planPriceRangeService;
            this.validator = validator;
        }

        /// <summary>
        /// ResourceAPI.DocumentationGetPlanPriceRanges
        /// </summary>
        /// <returns>
        /// ResourceAPI.ReturnsListOfPlanPriceRangeObjectsWithTheirDetailsAndStatusOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError
        /// </returns>
        /// <response code="200">ResourceAPI.PlanPriceRangesRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<PlanPriceRangeResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetPlanPriceRanges()
        {
            try
            {
                var planPriceRange = _planPriceRangeService.GetAllActivePlanPriceRanges();
                var planPriceRangeResponse = planPriceRange.Select(ppf => CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanPriceRangeResponseDTO>(ppf));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(planPriceRangeResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationGetPlanPriceRangeById
        /// </summary>
        /// <param name="id">Plan price range ID to search for</param>
        /// <returns>ResourceAPI.ReturnsPlanPriceRangeMatchingTheSpecifiedID</returns>
        /// <response code="200">ResourceAPI.PlanPriceRangesRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.PlanPriceRangeNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PlanPriceRangeResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetPlanPriceRanges(int id)
        {
            try
            {
                var planPriceRange = _planPriceRangeService.GetById(id);
                if (planPriceRange == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Plan price range not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var planPriceRangeResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanPriceRangeResponseDTO>(planPriceRange);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(planPriceRangeResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationAddPlanPriceRange
        /// </summary>
        /// <param name="planPriceRangePayLoad">Plan price range data to create</param>
        /// <returns>ResourceAPI.ReturnsCreatedPlanPriceRangeOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="201">ResourceAPI.PlanPriceRangeCreatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="409">ResourceAPI.PlanPriceRangeAlreadyExists</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(PlanPriceRangeResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult CreatePlanPriceRange([FromBody] PlanPriceRangePayLoadDTO planPriceRangePayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(planPriceRangePayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var planPriceRange = CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanPriceRange>(planPriceRangePayLoad);
                _planPriceRangeService.AddPlanPriceRange(planPriceRange);

                var planPriceRangeResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanPriceRangeResponseDTO>(planPriceRange);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(planPriceRangeResponse, "Plan price range created successfully", HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationUpdatePlanPriceRange
        /// </summary>
        /// <param name="planPriceRangePayLoad">Updated plan price range data including the ID</param>
        /// <returns>ResourceAPI.ReturnsUpdatedPlanPriceRangeOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.PlanPriceRangeUpdatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.PlanPriceRangeNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PlanPriceRangeResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult UpdatePlanPriceRange([FromBody] PlanPriceRangePayLoadDTO planPriceRangePayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(planPriceRangePayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var existingPlanPriceRange = _planPriceRangeService.GetById(planPriceRangePayLoad.Id);
                if (existingPlanPriceRange == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Plan price range not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var planPriceRange = CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanPriceRange>(planPriceRangePayLoad);
                planPriceRange.Id = planPriceRangePayLoad.Id;
                _planPriceRangeService.UpdatePlanPriceRange(planPriceRange);

                var planPriceRangeResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanPriceRangeResponseDTO>(planPriceRange);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(planPriceRangeResponse, "Plan price range updated successfully", HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationDeletePlanPriceRange
        /// </summary>
        /// <param name="id">Plan price range ID to delete</param>
        /// <returns>ResourceAPI.ReturnsConfirmationMessageOnSuccessPlanPriceRangeDeletionValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.PlanPriceRangeDeletedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.PlanPriceRangeNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
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
        public IActionResult DeletePlanPriceRange(int id)
        {
            try
            {
                var existingPlanPriceRange = _planPriceRangeService.GetById(id);
                if (existingPlanPriceRange == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Plan price range not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _planPriceRangeService.DeletePlanPriceRange(id);
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