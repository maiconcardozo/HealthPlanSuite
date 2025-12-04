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
    public class ProcedureCoparticipationController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProcedureCoparticipationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProcedureCoparticipationResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetProcedureCoparticipations()
        {
            try
            {
                var query = new GetAllProcedureCoparticipationsQuery();
                var procedureCoparticipations = await mediator.Send(query);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(procedureCoparticipations, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProcedureCoparticipationResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetProcedureCoparticipation(int id)
        {
            try
            {
                var query = new GetProcedureCoparticipationByIdQuery { Id = id };
                var procedureCoparticipation = await mediator.Send(query);
                if (procedureCoparticipation == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Procedure coparticipation not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }
                var successResponse = SuccessResponseExampleFactory.ForSuccess(procedureCoparticipation, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(ProcedureCoparticipationResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> CreateProcedureCoparticipation([FromBody] ProcedureCoparticipationPayLoadDTO procedureCoparticipationPayLoad)
        {
            try
            {
                var command = new CreateProcedureCoparticipationCommand
                {
                    HealthPlanId = procedureCoparticipationPayLoad.HealthPlanId,
                    CoparticipationType = procedureCoparticipationPayLoad.CoparticipationType,
                    Procedure = procedureCoparticipationPayLoad.Procedure,
                    Value = procedureCoparticipationPayLoad.Value,
                    Limit = procedureCoparticipationPayLoad.Limit,
                    CreatedBy = procedureCoparticipationPayLoad.CreatedBy,
                };
                var procedureCoparticipation = await mediator.Send(command);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(procedureCoparticipation, "Procedure coparticipation created successfully", HttpContext.Request.Path);
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
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProcedureCoparticipationResponseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateProcedureCoparticipation([FromBody] ProcedureCoparticipationPayLoadDTO procedureCoparticipationPayLoad)
        {
            try
            {
                var command = new UpdateProcedureCoparticipationCommand
                {
                    Id = procedureCoparticipationPayLoad.Id,
                    HealthPlanId = procedureCoparticipationPayLoad.HealthPlanId,
                    CoparticipationType = procedureCoparticipationPayLoad.CoparticipationType,
                    Procedure = procedureCoparticipationPayLoad.Procedure,
                    Value = procedureCoparticipationPayLoad.Value,
                    Limit = procedureCoparticipationPayLoad.Limit,
                    UpdatedBy = procedureCoparticipationPayLoad.UpdatedBy,
                };
                var procedureCoparticipation = await mediator.Send(command);
                if (procedureCoparticipation == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Procedure coparticipation not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }
                var successResponse = SuccessResponseExampleFactory.ForSuccess(procedureCoparticipation, "Procedure coparticipation updated successfully", HttpContext.Request.Path);
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
        public async Task<IActionResult> DeleteProcedureCoparticipation(int id)
        {
            try
            {
                var command = new DeleteProcedureCoparticipationCommand { Id = id };
                var result = await mediator.Send(command);
                if (!result)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Procedure coparticipation not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Procedure coparticipation deleted successfully", "Procedure coparticipation deleted successfully", HttpContext.Request.Path);
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
