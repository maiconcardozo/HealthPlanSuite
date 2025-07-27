using Authentication.API.Resource;
using Authentication.API.Swagger;
using Authentication.Login.Domain.Implementation;
using Authentication.Login.DTO;
using Authentication.Login.Mapping;
using Authentication.Login.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Authentication.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActionController : ControllerBase
    {
        private readonly IActionService _actionService;

        public ActionController(IActionService actionService)
        {
            _actionService = actionService;
        }

        [HttpGet(ActionRoutes.GetActions)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ActionResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetActions()
        {
            try
            {
                var actions = _actionService.GetAll();
                var actionsResponse = actions.Select(a => AuthenticationLoginProfileMapperInitializer.Mapper.Map<ActionResponseDTO>(a));
                return Ok(actionsResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredActionsCouldNotBeRetrieved);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpGet(ActionRoutes.GetActionById)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ActionResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetActionById(int id)
        {
            try
            {
                var action = _actionService.GetById(id);
                if (action == null)
                {
                    var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.ActionNotFound);
                    return NotFound(notFoundDetails);
                }

                var actionResponse = AuthenticationLoginProfileMapperInitializer.Mapper.Map<ActionResponseDTO>(action);
                return Ok(actionResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredActionCouldNotBeRetrieved);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpPost(ActionRoutes.AddAction)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ActionResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> AddAction([FromBody] ActionPayLoadDTO actionDTO, [FromServices] IServiceProvider serviceProvider)
        {
            var action = AuthenticationLoginProfileMapperInitializer.Mapper.Map<Authentication.Login.Domain.Implementation.Action>(actionDTO);

            try
            {
                _actionService.AddAction(action);
                var actionResponse = AuthenticationLoginProfileMapperInitializer.Mapper.Map<ActionResponseDTO>(action);
                return Ok(actionResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ResourceAPI.AnUnexpectedErrorOccurredActionCouldNotBeInserted);
                return BadRequest(problemDetails);
            }
        }

        [HttpPut(ActionRoutes.UpdateAction)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ActionResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> UpdateAction(int id, [FromBody] ActionPayLoadDTO actionDTO, [FromServices] IServiceProvider serviceProvider)
        {
            try
            {
                var existingAction = _actionService.GetById(id);
                if (existingAction == null)
                {
                    var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.ActionNotFound);
                    return NotFound(notFoundDetails);
                }

                var action = AuthenticationLoginProfileMapperInitializer.Mapper.Map<Authentication.Login.Domain.Implementation.Action>(actionDTO);
                action.Id = id;

                _actionService.UpdateAction(action);
                var actionResponse = AuthenticationLoginProfileMapperInitializer.Mapper.Map<ActionResponseDTO>(action);
                return Ok(actionResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ResourceAPI.AnUnexpectedErrorOccurredActionCouldNotBeUpdated);
                return BadRequest(problemDetails);
            }
        }

        [HttpDelete(ActionRoutes.DeleteAction)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SuccessResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> DeleteAction(int id)
        {
            try
            {
                var existingAction = _actionService.GetById(id);
                if (existingAction == null)
                {
                    var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.ActionNotFound);
                    return NotFound(notFoundDetails);
                }

                var actionResponseDTO = AuthenticationLoginProfileMapperInitializer.Mapper.Map<ActionResponseDTO>(existingAction);
                _actionService.DeleteAction(id);
                var successResponse = new SuccessResponseDTO 
                { 
                    Message = ResourceAPI.ActionDeletedSuccessfully,
                    Data = actionResponseDTO
                };
                return Ok(successResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredActionCouldNotBeDeleted);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }
    }
}