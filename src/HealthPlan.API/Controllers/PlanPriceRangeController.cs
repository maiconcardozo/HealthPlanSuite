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
    /// ResourceAPI.PlanPriceRangeControllerDescription
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PlanPriceRangeController : ControllerBase
    {
        private readonly IMediator mediator;

        public PlanPriceRangeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<PlanPriceRangeResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetPlanPriceRanges()
        {
            try
            {
                var query = new GetAllPlanPriceRangesQuery();
                var planPriceRanges = await mediator.Send(query);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(planPriceRanges, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PlanPriceRangeResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetPlanPriceRange(int id)
        {
            try
            {
                var query = new GetPlanPriceRangeByIdQuery { Id = id };
                var planPriceRange = await mediator.Send(query);
                if (planPriceRange == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Plan price range not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }
                var successResponse = SuccessResponseExampleFactory.ForSuccess(planPriceRange, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(PlanPriceRangeResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> CreatePlanPriceRange([FromBody] PlanPriceRangePayLoadDTO planPriceRangePayLoad)
        {
            try
            {
                var command = new CreatePlanPriceRangeCommand
                {
                    HealthPlanId = planPriceRangePayLoad.HealthPlanId,
                    AgeRangeId = planPriceRangePayLoad.AgeRangeId,
                    ContractType = planPriceRangePayLoad.ContractType,
                    CoparticipationType = planPriceRangePayLoad.CoparticipationType,
                    OriginalValue = planPriceRangePayLoad.OriginalValue,
                    DiscountValue = planPriceRangePayLoad.DiscountValue,
                    ValidityStart = planPriceRangePayLoad.ValidityStart,
                    ValidityEnd = planPriceRangePayLoad.ValidityEnd,
                    CreatedBy = planPriceRangePayLoad.CreatedBy,
                };
                var planPriceRange = await mediator.Send(command);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(planPriceRange, "Plan price range created successfully", HttpContext.Request.Path);
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

        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PlanPriceRangeResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> UpdatePlanPriceRange([FromBody] PlanPriceRangePayLoadDTO planPriceRangePayLoad)
        {
            try
            {
                var command = new UpdatePlanPriceRangeCommand
                {
                    Id = planPriceRangePayLoad.Id,
                    HealthPlanId = planPriceRangePayLoad.HealthPlanId,
                    AgeRangeId = planPriceRangePayLoad.AgeRangeId,
                    ContractType = planPriceRangePayLoad.ContractType,
                    CoparticipationType = planPriceRangePayLoad.CoparticipationType,
                    OriginalValue = planPriceRangePayLoad.OriginalValue,
                    DiscountValue = planPriceRangePayLoad.DiscountValue,
                    ValidityStart = planPriceRangePayLoad.ValidityStart,
                    ValidityEnd = planPriceRangePayLoad.ValidityEnd,
                    UpdatedBy = planPriceRangePayLoad.UpdatedBy,
                };
                var planPriceRange = await mediator.Send(command);
                if (planPriceRange == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Plan price range not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }
                var successResponse = SuccessResponseExampleFactory.ForSuccess(planPriceRange, "Plan price range updated successfully", HttpContext.Request.Path);
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
        public async Task<IActionResult> DeletePlanPriceRange(int id)
        {
            try
            {
                var command = new DeletePlanPriceRangeCommand { Id = id };
                var result = await mediator.Send(command);
                if (!result)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Plan price range not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Plan price range deleted successfully", "Plan price range deleted successfully", HttpContext.Request.Path);
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
