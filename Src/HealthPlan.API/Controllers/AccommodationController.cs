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
    /// Controller for managing Accommodation entities.
    /// Provides comprehensive CRUD operations following the established CleanEntity pattern.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AccommodationController : ControllerBase
    {
        private readonly IAccommodationService _accommodationService;
        private readonly IValidator<AccommodationPayLoadDTO> validator;

        /// <summary>
        /// Initializes a new instance of the AccommodationController.
        /// </summary>
        /// <param name="accommodationService">Service for accommodation management operations</param>
        /// <param name="validator">Validator for AccommodationPayLoadDTO</param>
        public AccommodationController(IAccommodationService accommodationService, IValidator<AccommodationPayLoadDTO> validator)
        {
            _accommodationService = accommodationService;
            this.validator = validator;
        }

        /// <summary>
        /// Retrieves all accommodations from the system.
        /// </summary>
        /// <returns>
        /// Returns list of Accommodation objects with their details and status on success, validation errors, unauthorized access, or internal server error.
        /// </returns>
        /// <response code="200">Accommodations retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(AccommodationRoutes.GetAccommodations)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<AccommodationResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetAccommodations()
        {
            try
            {
                var accommodations = _accommodationService.GetAllActiveAccommodations();
                var accommodationsResponse = accommodations.Select(a => CleanTemplateApplicationMapperInitializer.Mapper.Map<AccommodationResponseDTO>(a));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(accommodationsResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Retrieves an accommodation by its unique identifier.
        /// </summary>
        /// <param name="id">Accommodation ID to search for</param>
        /// <returns>Returns Accommodation matching the specified ID</returns>
        /// <response code="200">Accommodation retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Accommodation not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(AccommodationRoutes.GetAccommodationById)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AccommodationResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetAccommodation(int id)
        {
            try
            {
                var accommodation = _accommodationService.GetById(id);
                if (accommodation == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Accommodation not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var accommodationResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<AccommodationResponseDTO>(accommodation);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(accommodationResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Creates a new accommodation in the system.
        /// </summary>
        /// <param name="accommodationPayLoad">Accommodation data to create</param>
        /// <returns>Returns created Accommodation on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="201">Accommodation created successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Accommodation already exists</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(AccommodationRoutes.AddAccommodation)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(AccommodationResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult CreateAccommodation([FromBody] AccommodationPayLoadDTO accommodationPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(accommodationPayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var accommodation = CleanTemplateApplicationMapperInitializer.Mapper.Map<Accommodation>(accommodationPayLoad);
                _accommodationService.AddAccommodation(accommodation);

                var accommodationResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<AccommodationResponseDTO>(accommodation);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(accommodationResponse, "Accommodation created successfully", HttpContext.Request.Path);
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
        /// Updates an existing accommodation.
        /// </summary>
        /// <param name="accommodationPayLoad">Updated accommodation data including the ID</param>
        /// <returns>Returns updated Accommodation on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Accommodation updated successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Accommodation not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut(AccommodationRoutes.UpdateAccommodation)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AccommodationResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult UpdateAccommodation([FromBody] AccommodationPayLoadDTO accommodationPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(accommodationPayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var existingAccommodation = _accommodationService.GetById(accommodationPayLoad.Id);
                if (existingAccommodation == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Accommodation not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var accommodation = CleanTemplateApplicationMapperInitializer.Mapper.Map<Accommodation>(accommodationPayLoad);
                accommodation.Id = accommodationPayLoad.Id;
                _accommodationService.UpdateAccommodation(accommodation);

                var accommodationResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<AccommodationResponseDTO>(accommodation);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(accommodationResponse, "Accommodation updated successfully", HttpContext.Request.Path);
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
        /// Deletes an accommodation from the system.
        /// </summary>
        /// <param name="id">Accommodation ID to delete</param>
        /// <returns>Returns confirmation message on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Accommodation deleted successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Accommodation not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete(AccommodationRoutes.DeleteAccommodation)]
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
        public IActionResult DeleteAccommodation(int id)
        {
            try
            {
                var existingAccommodation = _accommodationService.GetById(id);
                if (existingAccommodation == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Accommodation not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _accommodationService.DeleteAccommodation(id);
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Accommodation deleted successfully", "Accommodation deleted successfully", HttpContext.Request.Path);
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