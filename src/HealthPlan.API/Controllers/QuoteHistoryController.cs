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
    [ApiController]
    [Route("[controller]")]
    public class QuoteHistoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public QuoteHistoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<QuoteHistoryResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetQuoteHistories()
        {
            try
            {
                var query = new GetAllQuoteHistoriesQuery();
                var quoteHistories = await mediator.Send(query);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quoteHistories, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(QuoteHistoryResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetQuoteHistory(int id)
        {
            try
            {
                var query = new GetQuoteHistoryByIdQuery { Id = id };
                var quoteHistory = await mediator.Send(query);
                if (quoteHistory == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Quote history not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quoteHistory, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(QuoteHistoryResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> CreateQuoteHistory([FromBody] QuoteHistoryPayLoadDTO quoteHistoryPayLoad)
        {
            try
            {
                var command = new CreateQuoteHistoryCommand
                {
                    QuoteId = quoteHistoryPayLoad.QuoteId,
                    PreviousStatus = quoteHistoryPayLoad.PreviousStatus,
                    NewStatus = quoteHistoryPayLoad.NewStatus,
                    Reason = quoteHistoryPayLoad.Reason,
                    Observations = quoteHistoryPayLoad.Observations,
                    ChangeDate = quoteHistoryPayLoad.ChangeDate,
                    ResponsibleUser = quoteHistoryPayLoad.ResponsibleUser,
                    CreatedBy = quoteHistoryPayLoad.CreatedBy,
                };
                var quoteHistory = await mediator.Send(command);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quoteHistory, "Quote history created successfully", HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status201Created, successResponse);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", ex.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpPut("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(QuoteHistoryResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateQuoteHistory([FromBody] QuoteHistoryPayLoadDTO quoteHistoryPayLoad)
        {
            try
            {
                var command = new UpdateQuoteHistoryCommand
                {
                    Id = quoteHistoryPayLoad.Id,
                    QuoteId = quoteHistoryPayLoad.QuoteId,
                    PreviousStatus = quoteHistoryPayLoad.PreviousStatus,
                    NewStatus = quoteHistoryPayLoad.NewStatus,
                    Reason = quoteHistoryPayLoad.Reason,
                    Observations = quoteHistoryPayLoad.Observations,
                    ChangeDate = quoteHistoryPayLoad.ChangeDate,
                    ResponsibleUser = quoteHistoryPayLoad.ResponsibleUser,
                    UpdatedBy = quoteHistoryPayLoad.UpdatedBy,
                };
                var quoteHistory = await mediator.Send(command);
                if (quoteHistory == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Quote history not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }
                var successResponse = SuccessResponseExampleFactory.ForSuccess(quoteHistory, "Quote history updated successfully", HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", ex.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteQuoteHistory(int id)
        {
            try
            {
                var command = new DeleteQuoteHistoryCommand { Id = id };
                var result = await mediator.Send(command);
                if (!result)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Quote history not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Quote history deleted successfully", "Quote history deleted successfully", HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }
    }
}
