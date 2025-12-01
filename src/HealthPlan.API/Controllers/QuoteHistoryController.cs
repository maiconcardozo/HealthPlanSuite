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
    /// ResourceAPI.QuoteHistoryControllerDescription
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
        /// ResourceAPI.DocumentationGetQuoteHistorys
        /// </summary>
        /// <returns>
        /// ResourceAPI.ReturnsListOfQuoteHistoryObjectsWithTheirDetailsAndStatusOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError
        /// </returns>
        /// <response code="200">ResourceAPI.QuoteHistorysRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
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
        /// ResourceAPI.DocumentationGetQuoteHistoryById
        /// </summary>
        /// <param name="id">QuoteHistory ID to search for</param>
        /// <returns>ResourceAPI.ReturnsQuoteHistoryMatchingTheSpecifiedID</returns>
        /// <response code="200">ResourceAPI.QuoteHistorysRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.QuoteHistoryNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
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
        /// ResourceAPI.DocumentationAddQuoteHistory
        /// </summary>
        /// <param name="quoteHistoryPayLoad">Quote history data to create</param>
        /// <returns>ResourceAPI.ReturnsCreatedQuoteHistoryOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="201">ResourceAPI.QuoteHistoryCreatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="409">ResourceAPI.QuoteHistoryAlreadyExists</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
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
        /// ResourceAPI.DocumentationUpdateQuoteHistory
        /// </summary>
        /// <param name="quoteHistoryPayLoad">Updated quote history data including the ID</param>
        /// <returns>ResourceAPI.ReturnsUpdatedQuoteHistoryOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.QuoteHistoryUpdatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.QuoteHistoryNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
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
        /// <returns>ResourceAPI.ReturnsConfirmationMessageOnSuccessQuoteHistoryDeletionValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.QuoteHistoryDeletedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.QuoteHistoryNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
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