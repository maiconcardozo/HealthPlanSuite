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
    public class AcceptanceRuleController : ControllerBase
    {
        private readonly IMediator mediator;

        public AcceptanceRuleController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<AcceptanceRuleResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetAcceptanceRules()
        {
            try
            {
                var query = new GetAllAcceptanceRulesQuery();
                var acceptanceRules = await mediator.Send(query);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(acceptanceRules, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AcceptanceRuleResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetAcceptanceRule(int id)
        {
            try
            {
                var query = new GetAcceptanceRuleByIdQuery { Id = id };
                var acceptanceRule = await mediator.Send(query);
                if (acceptanceRule == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Acceptance rule not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }
                var successResponse = SuccessResponseExampleFactory.ForSuccess(acceptanceRule, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(AcceptanceRuleResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> CreateAcceptanceRule([FromBody] AcceptanceRulePayLoadDTO acceptanceRulePayLoad)
        {
            try
            {
                var command = new CreateAcceptanceRuleCommand
                {
                    HealthPlanId = acceptanceRulePayLoad.HealthPlanId,
                    RuleType = acceptanceRulePayLoad.RuleType,
                    Operator = acceptanceRulePayLoad.Operator,
                    MinValue = acceptanceRulePayLoad.MinValue,
                    MaxValue = acceptanceRulePayLoad.MaxValue,
                    ValuesList = acceptanceRulePayLoad.ValuesList,
                    Description = acceptanceRulePayLoad.Description,
                    RejectionMessage = acceptanceRulePayLoad.RejectionMessage,
                    IsMandatory = acceptanceRulePayLoad.IsMandatory,
                    CreatedBy = acceptanceRulePayLoad.CreatedBy,
                };
                var acceptanceRule = await mediator.Send(command);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(acceptanceRule, "Acceptance rule created successfully", HttpContext.Request.Path);
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AcceptanceRuleResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateAcceptanceRule([FromBody] AcceptanceRulePayLoadDTO acceptanceRulePayLoad)
        {
            try
            {
                var command = new UpdateAcceptanceRuleCommand
                {
                    Id = acceptanceRulePayLoad.Id,
                    HealthPlanId = acceptanceRulePayLoad.HealthPlanId,
                    RuleType = acceptanceRulePayLoad.RuleType,
                    Operator = acceptanceRulePayLoad.Operator,
                    MinValue = acceptanceRulePayLoad.MinValue,
                    MaxValue = acceptanceRulePayLoad.MaxValue,
                    ValuesList = acceptanceRulePayLoad.ValuesList,
                    Description = acceptanceRulePayLoad.Description,
                    RejectionMessage = acceptanceRulePayLoad.RejectionMessage,
                    IsMandatory = acceptanceRulePayLoad.IsMandatory,
                    UpdatedBy = acceptanceRulePayLoad.UpdatedBy,
                };
                var acceptanceRule = await mediator.Send(command);
                if (acceptanceRule == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Acceptance rule not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }
                var successResponse = SuccessResponseExampleFactory.ForSuccess(acceptanceRule, "Acceptance rule updated successfully", HttpContext.Request.Path);
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
        public async Task<IActionResult> DeleteAcceptanceRule(int id)
        {
            try
            {
                var command = new DeleteAcceptanceRuleCommand { Id = id };
                var result = await mediator.Send(command);
                if (!result)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Acceptance rule not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Acceptance rule deleted successfully", "Acceptance rule deleted successfully", HttpContext.Request.Path);
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
