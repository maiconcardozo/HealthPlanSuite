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
    /// Controller for managing Coverage entities.
    /// Provides comprehensive CRUD operations following the established CleanEntity pattern.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CoverageController : ControllerBase
    {
        private readonly ICoverageService _coverageService;
        private readonly IValidator<CoveragePayLoadDTO> validator;

        /// <summary>
        /// Initializes a new instance of the CoverageController.
        /// </summary>
        /// <param name="coverageService">Service for coverage management operations</param>
        /// <param name="validator">Validator for CoveragePayLoadDTO</param>
        public CoverageController(ICoverageService coverageService, IValidator<CoveragePayLoadDTO> validator)
        {
            _coverageService = coverageService;
            this.validator = validator;
        }

        /// <summary>
        /// Retrieves all coverages from the system.
        /// </summary>
        /// <returns>
        /// Returns list of Coverage objects with their details and status on success, validation errors, unauthorized access, or internal server error.
        /// </returns>
        /// <response code="200">Coverages retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(CoverageRoutes.GetCoverages)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CoverageResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetCoverages()
        {
            try
            {
                var coverages = _coverageService.GetAllActiveCoverages();
                var coveragesResponse = coverages.Select(c => CleanTemplateApplicationMapperInitializer.Mapper.Map<CoverageResponseDTO>(c));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(coveragesResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Retrieves a coverage by its unique identifier.
        /// </summary>
        /// <param name="id">Coverage ID to search for</param>
        /// <returns>Returns Coverage matching the specified ID</returns>
        /// <response code="200">Coverage retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Coverage not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(CoverageRoutes.GetCoverageById)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CoverageResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetCoverage(int id)
        {
            try
            {
                var coverage = _coverageService.GetById(id);
                if (coverage == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Coverage not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var coverageResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<CoverageResponseDTO>(coverage);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(coverageResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Retrieves coverages by type.
        /// </summary>
        /// <param name="coverageType">Coverage type to search for</param>
        /// <returns>Returns Coverages matching the specified type</returns>
        /// <response code="200">Coverages retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(CoverageRoutes.GetCoveragesByType)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CoverageResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetCoveragesByType(string coverageType)
        {
            try
            {
                var coverages = _coverageService.GetCoveragesByType(coverageType);
                var coveragesResponse = coverages.Select(c => CleanTemplateApplicationMapperInitializer.Mapper.Map<CoverageResponseDTO>(c));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(coveragesResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Creates a new coverage in the system.
        /// </summary>
        /// <param name="coveragePayLoad">Coverage data to create</param>
        /// <returns>Returns created Coverage on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="201">Coverage created successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Coverage already exists</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(CoverageRoutes.AddCoverage)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(CoverageResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult CreateCoverage([FromBody] CoveragePayLoadDTO coveragePayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(coveragePayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var coverage = CleanTemplateApplicationMapperInitializer.Mapper.Map<Coverage>(coveragePayLoad);
                _coverageService.AddCoverage(coverage);

                var coverageResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<CoverageResponseDTO>(coverage);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(coverageResponse, "Coverage created successfully", HttpContext.Request.Path);
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
        /// Updates an existing coverage.
        /// </summary>
        /// <param name="id">Coverage ID to update</param>
        /// <param name="coveragePayLoad">Updated coverage data</param>
        /// <returns>Returns updated Coverage on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Coverage updated successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Coverage not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut(CoverageRoutes.UpdateCoverage)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CoverageResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult UpdateCoverage(int id, [FromBody] CoveragePayLoadDTO coveragePayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(coveragePayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var existingCoverage = _coverageService.GetById(id);
                if (existingCoverage == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Coverage not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var coverage = CleanTemplateApplicationMapperInitializer.Mapper.Map<Coverage>(coveragePayLoad);
                coverage.Id = id;
                _coverageService.UpdateCoverage(coverage);

                var coverageResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<CoverageResponseDTO>(coverage);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(coverageResponse, "Coverage updated successfully", HttpContext.Request.Path);
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
        /// Deletes a coverage from the system.
        /// </summary>
        /// <param name="id">Coverage ID to delete</param>
        /// <returns>Returns confirmation message on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Coverage deleted successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Coverage not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete(CoverageRoutes.DeleteCoverage)]
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
        public IActionResult DeleteCoverage(int id)
        {
            try
            {
                var existingCoverage = _coverageService.GetById(id);
                if (existingCoverage == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Coverage not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _coverageService.DeleteCoverage(id);
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Coverage deleted successfully", "Coverage deleted successfully", HttpContext.Request.Path);
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