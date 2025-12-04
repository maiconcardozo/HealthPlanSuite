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
    public class PromotionalDiscountController : ControllerBase
    {
        private readonly IMediator mediator;

        public PromotionalDiscountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<PromotionalDiscountResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetPromotionalDiscounts()
        {
            try
            {
                var query = new GetAllPromotionalDiscountsQuery();
                var promotionalDiscounts = await mediator.Send(query);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(promotionalDiscounts, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PromotionalDiscountResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetPromotionalDiscount(int id)
        {
            try
            {
                var query = new GetPromotionalDiscountByIdQuery { Id = id };
                var promotionalDiscount = await mediator.Send(query);
                if (promotionalDiscount == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Promotional discount not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }
                var successResponse = SuccessResponseExampleFactory.ForSuccess(promotionalDiscount, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(PromotionalDiscountResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> CreatePromotionalDiscount([FromBody] PromotionalDiscountPayLoadDTO promotionalDiscountPayLoad)
        {
            try
            {
                var command = new CreatePromotionalDiscountCommand
                {
                    HealthPlanId = promotionalDiscountPayLoad.HealthPlanId,
                    DiscountPercentage = promotionalDiscountPayLoad.DiscountPercentage,
                    ValidityStart = promotionalDiscountPayLoad.ValidityStart,
                    ValidityEnd = promotionalDiscountPayLoad.ValidityEnd,
                    Observation = promotionalDiscountPayLoad.Observation,
                    CreatedBy = promotionalDiscountPayLoad.CreatedBy,
                };
                var promotionalDiscount = await mediator.Send(command);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(promotionalDiscount, "Promotional discount created successfully", HttpContext.Request.Path);
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PromotionalDiscountResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> UpdatePromotionalDiscount([FromBody] PromotionalDiscountPayLoadDTO promotionalDiscountPayLoad)
        {
            try
            {
                var command = new UpdatePromotionalDiscountCommand
                {
                    Id = promotionalDiscountPayLoad.Id,
                    HealthPlanId = promotionalDiscountPayLoad.HealthPlanId,
                    DiscountPercentage = promotionalDiscountPayLoad.DiscountPercentage,
                    ValidityStart = promotionalDiscountPayLoad.ValidityStart,
                    ValidityEnd = promotionalDiscountPayLoad.ValidityEnd,
                    Observation = promotionalDiscountPayLoad.Observation,
                    UpdatedBy = promotionalDiscountPayLoad.UpdatedBy,
                };
                var promotionalDiscount = await mediator.Send(command);
                if (promotionalDiscount == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Promotional discount not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }
                var successResponse = SuccessResponseExampleFactory.ForSuccess(promotionalDiscount, "Promotional discount updated successfully", HttpContext.Request.Path);
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
        public async Task<IActionResult> DeletePromotionalDiscount(int id)
        {
            try
            {
                var command = new DeletePromotionalDiscountCommand { Id = id };
                var result = await mediator.Send(command);
                if (!result)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Promotional discount not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Promotional discount deleted successfully", "Promotional discount deleted successfully", HttpContext.Request.Path);
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
