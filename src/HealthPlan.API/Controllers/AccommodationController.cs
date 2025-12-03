using HealthPlan.API.Resource;
using HealthPlan.API.Swagger;
using HealthPlan.Application.Commands;
using HealthPlan.Application.DTOs;
using HealthPlan.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace HealthPlan.API.Controllers
{
    /// <summary>
    /// ResourceAPI.AccommodationControllerDescription
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AccommodationController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the AccommodationController.
        /// </summary>
        /// <param name="mediator">MediatR mediator for command/query handling.</param>
        public AccommodationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// ResourceAPI.DocumentationGetAccommodations
        /// </summary>
        /// <returns>
        /// ResourceAPI.ReturnsListOfAccommodationObjectsWithTheirDetailsAndStatusOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError
        /// </returns>
        /// <response code="200">ResourceAPI.AccommodationsRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<AccommodationResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetAccommodations()
        {
            try
            {
                var query = new GetAllAccommodationsQuery();
                var accommodations = await mediator.Send(query);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(accommodations, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationGetAccommodationById
        /// </summary>
        /// <param name="id">Accommodation ID to search for</param>
        /// <returns>ResourceAPI.ReturnsAccommodationMatchingTheSpecifiedID</returns>
        /// <response code="200">ResourceAPI.AccommodationsRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.AccommodationNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet("{id}")]
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
        public async Task<IActionResult> GetAccommodation(int id)
        {
            try
            {
                var query = new GetAccommodationByIdQuery { Id = id };
                var accommodation = await mediator.Send(query);
                if (accommodation == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Accommodation not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var successResponse = SuccessResponseExampleFactory.ForSuccess(accommodation, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationAddAccommodation
        /// </summary>
        /// <param name="accommodationPayLoad">Accommodation data to create</param>
        /// <returns>ResourceAPI.ReturnsCreatedAccommodationOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="201">ResourceAPI.AccommodationCreatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="409">ResourceAPI.AccommodationAlreadyExists</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpPost("")]
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
        public async Task<IActionResult> CreateAccommodation([FromBody] AccommodationPayLoadDTO accommodationPayLoad)
        {
            try
            {
                var command = new CreateAccommodationCommand
                {
                    Type = accommodationPayLoad.Type,
                    Description = accommodationPayLoad.Description,
                    AdditionalValue = accommodationPayLoad.AdditionalValue,
                    CreatedBy = accommodationPayLoad.CreatedBy,
                };
                var accommodation = await mediator.Send(command);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(accommodation, "Accommodation created successfully", HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status201Created, successResponse);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", ex.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
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
        /// ResourceAPI.DocumentationUpdateAccommodation
        /// </summary>
        /// <param name="accommodationPayLoad">Updated accommodation data including the ID</param>
        /// <returns>ResourceAPI.ReturnsUpdatedAccommodationOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.AccommodationUpdatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.AccommodationNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpPut]
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
        public async Task<IActionResult> UpdateAccommodation([FromBody] AccommodationPayLoadDTO accommodationPayLoad)
        {
            try
            {
                var command = new UpdateAccommodationCommand
                {
                    Id = accommodationPayLoad.Id,
                    Type = accommodationPayLoad.Type,
                    Description = accommodationPayLoad.Description,
                    AdditionalValue = accommodationPayLoad.AdditionalValue,
                    UpdatedBy = accommodationPayLoad.UpdatedBy,
                };
                var accommodation = await mediator.Send(command);
                if (accommodation == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Accommodation not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var successResponse = SuccessResponseExampleFactory.ForSuccess(accommodation, "Accommodation updated successfully", HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", ex.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
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
        /// <returns>ResourceAPI.ReturnsConfirmationMessageOnSuccessAccommodationDeletionValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.AccommodationDeletedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.AccommodationNotFound</response>
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
        public async Task<IActionResult> DeleteAccommodation(int id)
        {
            try
            {
                var command = new DeleteAccommodationCommand { Id = id };
                var result = await mediator.Send(command);
                if (!result)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Accommodation not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

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