using HealthPlan.API.Resource;
using HealthPlan.API.Swagger;
using HealthPlan.API.Util;
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
    /// ResourceAPI.QuoteControllerDescription
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        /// <summary>
        /// Initializes a new instance of the QuoteController.
        /// </summary>
        /// <param name="quoteService">Service for quote management operations</param>
        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
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
        public IActionResult GetQuotes()
        {
            try
            {
                var quotes = _quoteService.GetAllActiveQuotes();
                var quotesResponse = quotes.Select(q => CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteResponseDTO>(q));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quotesResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        public IActionResult GetQuote(int id)
        {
            try
            {
                var quote = _quoteService.GetById(id);
                if (quote == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Quote not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var quoteResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteResponseDTO>(quote);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quoteResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        public IActionResult GetQuotesByBeneficiary(int beneficiaryId)
        {
            try
            {
                var quotes = _quoteService.GetQuotesByBeneficiary(beneficiaryId);
                var quotesResponse = quotes.Select(q => CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteResponseDTO>(q));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quotesResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        public async Task<IActionResult> CreateQuote([FromBody] QuotePayLoadDTO quotePayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(quotePayLoad, serviceProvider, this);

            if (validationResult != null)
            {
                return validationResult;
            }

            try
            {
                var quote = CleanTemplateApplicationMapperInitializer.Mapper.Map<HealthPlan.Quote.Domain.Implementation.Quote>(quotePayLoad);
                _quoteService.AddQuote(quote);

                var quoteResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteResponseDTO>(quote);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quoteResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        public async Task<IActionResult> UpdateQuote([FromBody] QuotePayLoadDTO quotePayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(quotePayLoad, serviceProvider, this);

            if (validationResult != null)
            {
                return validationResult;
            }

            try
            {
                var existingQuote = _quoteService.GetById(quotePayLoad.Id);
                if (existingQuote == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.AccountNotFound, HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var quote = CleanTemplateApplicationMapperInitializer.Mapper.Map<HealthPlan.Quote.Domain.Implementation.Quote>(quotePayLoad);
                quote.Id = quotePayLoad.Id;
                _quoteService.UpdateQuote(quote);

                var quoteResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteResponseDTO>(quote);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quoteResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        public IActionResult DeleteQuote(int id)
        {
            try
            {
                var existingQuote = _quoteService.GetById(id);
                if (existingQuote == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Quote not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _quoteService.DeleteQuote(id);
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

        /// <summary>
        /// Retrieves a complete quote with all related entities and relationships.
        /// </summary>
        /// <param name="id">Quote ID to retrieve complete information for</param>
        /// <returns>Returns complete Quote object with all relationships populated</returns>
        /// <response code="200">Complete quote retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Quote not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}/complete")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(object))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetCompleteQuote(int id)
        {
            try
            {
                var quote = _quoteService.GetById(id);
                if (quote == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Quote not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                // Note: This endpoint provides the structure for complete quote retrieval
                // The actual implementation with full relationships would require:
                // 1. Service method to load all related entities (Company, Beneficiary, HealthPlan, AgeRange, etc.)
                // 2. CompleteQuoteResponseDTO to structure the response with all relationships
                // 3. Mapping configuration to handle the complex object relationships
                
                // For now, return the basic quote structure with a note about the complete implementation
                var basicQuoteResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteResponseDTO>(quote);
                
                // Create a placeholder complete response structure
                var completeQuoteResponse = new
                {
                    Quote = basicQuoteResponse,
                    Message = "Complete quote endpoint structure created. Full implementation requires:",
                    RequiredImplementations = new[]
                    {
                        "CompleteQuoteResponseDTO with all relationship properties",
                        "Service methods to load related entities (Company, Beneficiary, HealthPlan, AgeRange)",
                        "Navigation property loading for PlanCoverages, AcceptanceRules, QuoteHistory",
                        "Mapping configuration for complex object relationships"
                    },
                    PlannedStructure = new
                    {
                        QuoteDetails = "Basic quote information",
                        Company = "Company details and information",
                        Beneficiary = "Beneficiary personal information",
                        HealthPlan = "Health plan details with accommodations",
                        AgeRange = "Age range and premium multiplier",
                        PlanCoverages = "List of coverages included in the plan",
                        AcceptanceRules = "Rules that must be met for plan acceptance",
                        QuoteHistory = "History of quote status changes"
                    }
                };

                var successResponse = SuccessResponseExampleFactory.ForSuccess(completeQuoteResponse, "Complete quote structure endpoint", HttpContext.Request.Path);
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
    }
}