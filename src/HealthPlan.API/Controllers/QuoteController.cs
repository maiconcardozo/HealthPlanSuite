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
    /// ResourceAPI.QuoteControllerDescription
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the QuoteController.
        /// </summary>
        /// <param name="mediator">MediatR mediator for command/query handling.</param>
        public QuoteController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// ResourceAPI.DocumentationGetQuotes
        /// </summary>
        /// <returns>
        /// ResourceAPI.ReturnsListOfQuoteObjectsWithTheirDetailsAndStatusOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError
        /// </returns>
        /// <response code="200">ResourceAPI.QuotesRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorized</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet(QuoteRoutes.GetQuotes)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<QuoteResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetQuotes()
        {
            try
            {
                var query = new GetAllQuotesQuery();
                var quotes = await mediator.Send(query);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quotes, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationGetQuoteById
        /// </summary>
        /// <param name="id">Quote ID to search for</param>
        /// <returns>ResourceAPI.ReturnsQuoteMatchingTheSpecifiedID</returns>
        /// <response code="200">ResourceAPI.QuoteRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.QuoteNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet(QuoteRoutes.GetQuoteById)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(QuoteResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetQuote(int id)
        {
            try
            {
                var query = new GetQuoteByIdQuery { Id = id };
                var quote = await mediator.Send(query);
                if (quote == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Quote not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var successResponse = SuccessResponseExampleFactory.ForSuccess(quote, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationGetQuotesByBeneficiary
        /// </summary>
        /// <param name="beneficiaryId">Beneficiary ID to search for</param>
        /// <returns>ResourceAPI.ReturnsQuotesForSpecifiedBeneficiary</returns>
        /// <response code="200">ResourceAPI.QuotesRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet(QuoteRoutes.GetQuotesByBeneficiary)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<QuoteResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetQuotesByBeneficiary(int beneficiaryId)
        {
            try
            {
                var query = new GetQuotesByBeneficiaryQuery { BeneficiaryId = beneficiaryId };
                var quotes = await mediator.Send(query);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quotes, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationAddQuote
        /// </summary>
        /// <param name="quotePayLoad">Quote data to create</param>
        /// <returns>ResourceAPI.ReturnsCreatedQuoteOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="201">ResourceAPI.QuoteCreatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="409">ResourceAPI.QuoteAlreadyExists</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpPost(QuoteRoutes.AddQuote)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(QuoteResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> CreateQuote([FromBody] QuotePayLoadDTO quotePayLoad)
        {
            try
            {
                var command = new CreateQuoteCommand
                {
                    IdCompany = quotePayLoad.IdCompany,
                    IdBeneficiary = quotePayLoad.IdBeneficiary,
                    IdHealthPlan = quotePayLoad.IdHealthPlan,
                    IdAgeRange = quotePayLoad.IdAgeRange,
                    MonthlyPremium = quotePayLoad.MonthlyPremium,
                    ValidUntil = quotePayLoad.ValidUntil,
                    CreatedBy = quotePayLoad.CreatedBy,
                    Notes = quotePayLoad.Notes,
                };
                var quote = await mediator.Send(command);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quote, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationUpdateQuote
        /// </summary>
        /// <param name="quotePayLoad">Updated quote data including the ID</param>
        /// <returns>ResourceAPI.ReturnsUpdatedQuoteOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.QuoteUpdatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.QuoteNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpPut(QuoteRoutes.UpdateQuote)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(QuoteResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> UpdateQuote([FromBody] QuotePayLoadDTO quotePayLoad)
        {
            try
            {
                var command = new UpdateQuoteCommand
                {
                    Id = quotePayLoad.Id,
                    IdCompany = quotePayLoad.IdCompany,
                    IdBeneficiary = quotePayLoad.IdBeneficiary,
                    IdHealthPlan = quotePayLoad.IdHealthPlan,
                    IdAgeRange = quotePayLoad.IdAgeRange,
                    MonthlyPremium = quotePayLoad.MonthlyPremium,
                    ValidUntil = quotePayLoad.ValidUntil,
                    Notes = quotePayLoad.Notes,
                    UpdatedBy = quotePayLoad.UpdatedBy,
                };
                var quote = await mediator.Send(command);
                if (quote == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Quote not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var successResponse = SuccessResponseExampleFactory.ForSuccess(quote, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationDeleteQuote
        /// </summary>
        /// <param name="id">Quote ID to delete</param>
        /// <returns>ResourceAPI.ReturnsConfirmationMessageOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.QuoteDeletedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.QuoteNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpDelete(QuoteRoutes.DeleteQuote)]
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
        public async Task<IActionResult> DeleteQuote(int id)
        {
            try
            {
                var command = new DeleteQuoteCommand { Id = id };
                var result = await mediator.Send(command);
                if (!result)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Quote not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var successResponse = SuccessResponseExampleFactory.ForSuccess("Quote deleted successfully", "Quote deleted successfully", HttpContext.Request.Path);
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
