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
    /// Controller for managing Age Range entities.
    /// Provides comprehensive CRUD operations for age ranges used in premium calculations.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AgeRangeController : ControllerBase
    {
        private readonly IAgeRangeService _ageRangeService;

        /// <summary>
        /// Initializes a new instance of the AgeRangeController.
        /// </summary>
        /// <param name="ageRangeService">Service for age range management operations</param>
        public AgeRangeController(IAgeRangeService ageRangeService)
        {
            _ageRangeService = ageRangeService;
        }

        /// <summary>
        /// Retrieves all age ranges from the system.
        /// </summary>
        /// <returns>
        /// Returns list of AgeRange objects with their details and status on success, validation errors, unauthorized access, or internal server error.
        /// </returns>
        /// <response code="200">Age ranges retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<AgeRangeResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetAgeRanges()
        {
            try
            {
                var ageRanges = _ageRangeService.GetAllActiveAgeRanges();
                var ageRangesResponse = ageRanges.Select(ar => CleanTemplateApplicationMapperInitializer.Mapper.Map<AgeRangeResponseDTO>(ar));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(ageRangesResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Retrieves a specific age range by ID.
        /// </summary>
        /// <param name="id">Age range ID to search for</param>
        /// <returns>Returns AgeRange matching the specified ID</returns>
        /// <response code="200">Age range retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Age range not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AgeRangeResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetAgeRange(int id)
        {
            try
            {
                var ageRange = _ageRangeService.GetById(id);
                if (ageRange == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Age range not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var ageRangeResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<AgeRangeResponseDTO>(ageRange);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(ageRangeResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Creates a new age range.
        /// </summary>
        /// <param name="ageRangePayLoad">Age range data to create</param>
        /// <returns>Returns created AgeRange on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="201">Age range created successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Age range already exists</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(AgeRangeResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult CreateAgeRange([FromBody] AgeRangePayLoadDTO ageRangePayLoad)
        {
            try
            {
                var ageRange = CleanTemplateApplicationMapperInitializer.Mapper.Map<AgeRange>(ageRangePayLoad);
                _ageRangeService.AddAgeRange(ageRange);

                var ageRangeResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<AgeRangeResponseDTO>(ageRange);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(ageRangeResponse, "Age range created successfully", HttpContext.Request.Path);
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
        /// Updates an existing age range.
        /// </summary>
        /// <param name="id">Age range ID to update</param>
        /// <param name="ageRangePayLoad">Updated age range data</param>
        /// <returns>Returns updated AgeRange on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Age range updated successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Age range not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AgeRangeResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult UpdateAgeRange(int id, [FromBody] AgeRangePayLoadDTO ageRangePayLoad)
        {
            try
            {
                var existingAgeRange = _ageRangeService.GetById(id);
                if (existingAgeRange == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Age range not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var ageRange = CleanTemplateApplicationMapperInitializer.Mapper.Map<AgeRange>(ageRangePayLoad);
                ageRange.Id = id;
                _ageRangeService.UpdateAgeRange(ageRange);

                var ageRangeResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<AgeRangeResponseDTO>(ageRange);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(ageRangeResponse, "Age range updated successfully", HttpContext.Request.Path);
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
        /// Deletes an existing age range.
        /// </summary>
        /// <param name="id">Age range ID to delete</param>
        /// <returns>Returns confirmation message on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Age range deleted successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Age range not found</response>
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
        public IActionResult DeleteAgeRange(int id)
        {
            try
            {
                var existingAgeRange = _ageRangeService.GetById(id);
                if (existingAgeRange == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Age range not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _ageRangeService.DeleteAgeRange(id);
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Age range deleted successfully", "Age range deleted successfully", HttpContext.Request.Path);
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
        /// Gets the appropriate age range for a specific age.
        /// Note: This functionality is not implemented in the current service layer.
        /// </summary>
        /// <param name="age">Age to find the range for</param>
        /// <returns>Returns message indicating feature not available</returns>
        /// <response code="501">Feature not implemented</response>
        [HttpGet("age/{age}")]
        [SwaggerResponse(StatusCodes.Status501NotImplemented, Type = typeof(string))]
        public IActionResult GetAgeRangeByAge(int age)
        {
            // This would require implementing GetByAge method in IAgeRangeService
            var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError("GetByAge feature not yet implemented in service layer", HttpContext.Request.Path);
            return StatusCode(StatusCodes.Status501NotImplemented, problemDetails);
        }
    }
}