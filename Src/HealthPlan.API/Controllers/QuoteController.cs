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
    /// Controller for health plan quote management operations.
    /// Provides comprehensive CRUD operations for health plan quotes.
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
        /// Retrieves all active quotes from the system.
        /// </summary>
        /// <returns>
        /// List of quote objects with their details, pricing, and status information on success.
        /// Returns validation errors, unauthorized access, or internal server error on failure.
        /// </returns>
        /// <response code="200">Quotes retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("quotes")]
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
                var quoteDtos = quotes.Select(q => q.ToResponseDTO()).ToList();
                return Ok(quoteDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Retrieves a specific quote by its ID.
        /// </summary>
        /// <param name="id">Quote ID</param>
        /// <returns>Quote object with its details</returns>
        /// <response code="200">Quote retrieved successfully</response>
        /// <response code="404">Quote not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(QuoteResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public IActionResult GetQuote(int id)
        {
            try
            {
                var quote = _quoteService.GetById(id);
                if (quote == null)
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = 404,
                        Title = "Quote not found",
                        Detail = $"Quote with ID {id} was not found."
                    });
                }

                var quoteDto = quote.ToResponseDTO();
                return Ok(quoteDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Retrieves quotes for a specific beneficiary.
        /// </summary>
        /// <param name="beneficiaryId">Beneficiary ID</param>
        /// <returns>List of quotes for the beneficiary</returns>
        /// <response code="200">Quotes retrieved successfully</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("beneficiary/{beneficiaryId}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<QuoteResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public IActionResult GetQuotesByBeneficiary(int beneficiaryId)
        {
            try
            {
                var quotes = _quoteService.GetQuotesByBeneficiary(beneficiaryId);
                var quoteDtos = quotes.Select(q => q.ToResponseDTO()).ToList();
                return Ok(quoteDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Creates a new health plan quote.
        /// </summary>
        /// <param name="quotePayload">Quote data for creation</param>
        /// <returns>Created quote object</returns>
        /// <response code="201">Quote created successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(QuoteResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public IActionResult CreateQuote([FromBody] QuotePayLoadDTO quotePayload)
        {
            try
            {
                if (quotePayload == null)
                {
                    return BadRequest(new ProblemDetails
                    {
                        Status = 400,
                        Title = "Invalid request",
                        Detail = "Quote data is required."
                    });
                }

                var quote = quotePayload.ToEntity();
                _quoteService.AddQuote(quote);
                
                var responseDto = quote.ToResponseDTO();
                return CreatedAtAction(nameof(GetQuote), new { id = quote.Id }, responseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Updates an existing quote.
        /// </summary>
        /// <param name="id">Quote ID</param>
        /// <param name="quotePayload">Updated quote data</param>
        /// <returns>Updated quote object</returns>
        /// <response code="200">Quote updated successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="404">Quote not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(QuoteResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public IActionResult UpdateQuote(int id, [FromBody] QuotePayLoadDTO quotePayload)
        {
            try
            {
                var existingQuote = _quoteService.GetById(id);
                if (existingQuote == null)
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = 404,
                        Title = "Quote not found",
                        Detail = $"Quote with ID {id} was not found."
                    });
                }

                // Update the existing quote with new data
                existingQuote.CompanyId = quotePayload.CompanyId;
                existingQuote.BeneficiaryId = quotePayload.BeneficiaryId;
                existingQuote.HealthPlanId = quotePayload.HealthPlanId;
                existingQuote.ValidUntil = quotePayload.ValidUntil;
                existingQuote.MonthlyPremium = quotePayload.MonthlyPremium;
                existingQuote.AgeRangeId = quotePayload.AgeRangeId;
                existingQuote.Notes = quotePayload.Notes;
                existingQuote.UpdatedBy = quotePayload.UpdatedBy;

                _quoteService.UpdateQuote(existingQuote);
                
                var responseDto = existingQuote.ToResponseDTO();
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = ex.Message
                });
            }
        }

        /// <summary>
        /// Deletes a quote by its ID.
        /// </summary>
        /// <param name="id">Quote ID</param>
        /// <returns>Success status</returns>
        /// <response code="204">Quote deleted successfully</response>
        /// <response code="404">Quote not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public IActionResult DeleteQuote(int id)
        {
            try
            {
                var quote = _quoteService.GetById(id);
                if (quote == null)
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = 404,
                        Title = "Quote not found",
                        Detail = $"Quote with ID {id} was not found."
                    });
                }

                _quoteService.DeleteQuote(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ProblemDetails
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Detail = ex.Message
                });
            }
        }
    }
}