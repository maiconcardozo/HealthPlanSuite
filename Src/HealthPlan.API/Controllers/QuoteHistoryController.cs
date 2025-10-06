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
    /// Controller for managing QuoteHistory entities.
    /// Provides comprehensive CRUD operations following the established CleanEntity pattern.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class QuoteHistoryController : ControllerBase
    {
        private readonly IQuoteHistoryService _quoteHistoryService;
        private readonly IValidator<QuoteHistoryPayLoadDTO> validator;

        /// <summary>
        /// Initializes a new instance of the QuoteHistoryController.
        /// </summary>
        /// <param name="quoteHistoryService">Service for quote history management operations</param>
        /// <param name="validator">Validator for QuoteHistoryPayLoadDTO</param>
        public QuoteHistoryController(IQuoteHistoryService quoteHistoryService, IValidator<QuoteHistoryPayLoadDTO> validator)
        {
            _quoteHistoryService = quoteHistoryService;
            this.validator = validator;
        }

        /// <summary>
        /// Retrieves all quote histories from the system.
        /// </summary>
        /// <returns>
        /// Returns list of QuoteHistory objects with their details and status on success, validation errors, unauthorized access, or internal server error.
        /// </returns>
        /// <response code="200">Quote histories retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(QuoteHistoryRoutes.GetQuoteHistories)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<QuoteHistoryResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetQuoteHistories()
        {
            try
            {
                var quoteHistories = _quoteHistoryService.GetAllActiveQuoteHistories();
                var quoteHistoriesResponse = quoteHistories.Select(qh => CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteHistoryResponseDTO>(qh));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quoteHistoriesResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Retrieves a quote history by its unique identifier.
        /// </summary>
        /// <param name="id">QuoteHistory ID to search for</param>
        /// <returns>Returns QuoteHistory matching the specified ID</returns>
        /// <response code="200">Quote history retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Quote history not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(QuoteHistoryRoutes.GetQuoteHistoryById)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(QuoteHistoryResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetQuoteHistory(int id)
        {
            try
            {
                var quoteHistory = _quoteHistoryService.GetById(id);
                if (quoteHistory == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Quote history not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var quoteHistoryResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteHistoryResponseDTO>(quoteHistory);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quoteHistoryResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Creates a new quote history in the system.
        /// </summary>
        /// <param name="quoteHistoryPayLoad">Quote history data to create</param>
        /// <returns>Returns created QuoteHistory on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="201">Quote history created successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Quote history already exists</response>
        /// <response code="500">Internal server error</response>
        [HttpPost(QuoteHistoryRoutes.AddQuoteHistory)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(QuoteHistoryResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult CreateQuoteHistory([FromBody] QuoteHistoryPayLoadDTO quoteHistoryPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(quoteHistoryPayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var quoteHistory = CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteHistory>(quoteHistoryPayLoad);
                _quoteHistoryService.AddQuoteHistory(quoteHistory);

                var quoteHistoryResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteHistoryResponseDTO>(quoteHistory);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quoteHistoryResponse, "Quote history created successfully", HttpContext.Request.Path);
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
        /// Updates an existing quote history.
        /// </summary>
        /// <param name="quoteHistoryPayLoad">Updated quote history data including the ID</param>
        /// <returns>Returns updated QuoteHistory on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Quote history updated successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Quote history not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut(QuoteHistoryRoutes.UpdateQuoteHistory)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(QuoteHistoryResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult UpdateQuoteHistory([FromBody] QuoteHistoryPayLoadDTO quoteHistoryPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(quoteHistoryPayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var existingQuoteHistory = _quoteHistoryService.GetById(quoteHistoryPayLoad.Id);
                if (existingQuoteHistory == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Quote history not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var quoteHistory = CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteHistory>(quoteHistoryPayLoad);
                quoteHistory.Id = quoteHistoryPayLoad.Id;
                _quoteHistoryService.UpdateQuoteHistory(quoteHistory);

                var quoteHistoryResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteHistoryResponseDTO>(quoteHistory);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quoteHistoryResponse, "Quote history updated successfully", HttpContext.Request.Path);
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
        /// Deletes a quote history from the system.
        /// </summary>
        /// <param name="id">QuoteHistory ID to delete</param>
        /// <returns>Returns confirmation message on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Quote history deleted successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Quote history not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete(QuoteHistoryRoutes.DeleteQuoteHistory)]
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
        public IActionResult DeleteQuoteHistory(int id)
        {
            try
            {
                var existingQuoteHistory = _quoteHistoryService.GetById(id);
                if (existingQuoteHistory == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Quote history not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _quoteHistoryService.DeleteQuoteHistory(id);
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Quote history deleted successfully", "Quote history deleted successfully", HttpContext.Request.Path);
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