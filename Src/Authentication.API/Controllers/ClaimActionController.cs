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
    public class ClaimActionController : ControllerBase
    {
        private readonly IClaimActionService _claimActionService;

        public ClaimActionController(IClaimActionService claimActionService)
        {
            _claimActionService = claimActionService;
        }

        [HttpGet(ClaimActionRoutes.GetClaimActions)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClaimActionResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetClaimActions()
        {
            try
            {
                var claimActions = _claimActionService.GetAll();
                var claimActionsResponse = claimActions.Select(ca => AuthenticationLoginProfileMapperInitializer.Mapper.Map<ClaimActionResponseDTO>(ca));
                return Ok(claimActionsResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredClaimActionsCouldNotBeRetrieved);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpGet(ClaimActionRoutes.GetClaimActionById)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ClaimActionResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetClaimActionById(int id)
        {
            try
            {
                var claimAction = _claimActionService.GetById(id);
                if (claimAction == null)
                {
                    var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.ClaimActionNotFound);
                    return NotFound(notFoundDetails);
                }

                var claimActionResponse = AuthenticationLoginProfileMapperInitializer.Mapper.Map<ClaimActionResponseDTO>(claimAction);
                return Ok(claimActionResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredClaimActionCouldNotBeRetrieved);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpPost(ClaimActionRoutes.AddClaimAction)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ClaimActionResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> AddClaimAction([FromBody] ClaimActionPayLoadDTO claimActionDTO, [FromServices] IServiceProvider serviceProvider)
        {
            var claimAction = AuthenticationLoginProfileMapperInitializer.Mapper.Map<ClaimAction>(claimActionDTO);

            try
            {
                _claimActionService.AddClaimAction(claimAction);
                var claimActionResponse = AuthenticationLoginProfileMapperInitializer.Mapper.Map<ClaimActionResponseDTO>(claimAction);
                return Ok(claimActionResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ResourceAPI.AnUnexpectedErrorOccurredClaimActionCouldNotBeInserted);
                return BadRequest(problemDetails);
            }
        }

        [HttpPut(ClaimActionRoutes.UpdateClaimAction)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ClaimActionResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> UpdateClaimAction(int id, [FromBody] ClaimActionPayLoadDTO claimActionDTO, [FromServices] IServiceProvider serviceProvider)
        {
            try
            {
                var existingClaimAction = _claimActionService.GetById(id);
                if (existingClaimAction == null)
                {
                    var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.ClaimActionNotFound);
                    return NotFound(notFoundDetails);
                }

                var claimAction = AuthenticationLoginProfileMapperInitializer.Mapper.Map<ClaimAction>(claimActionDTO);
                claimAction.Id = id;

                _claimActionService.UpdateClaimAction(claimAction);
                var claimActionResponse = AuthenticationLoginProfileMapperInitializer.Mapper.Map<ClaimActionResponseDTO>(claimAction);
                return Ok(claimActionResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ResourceAPI.AnUnexpectedErrorOccurredClaimActionCouldNotBeUpdated);
                return BadRequest(problemDetails);
            }
        }

        [HttpDelete(ClaimActionRoutes.DeleteClaimAction)]
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
        public async Task<IActionResult> DeleteClaimAction(int id)
        {
            try
            {
                var existingClaimAction = _claimActionService.GetById(id);
                if (existingClaimAction == null)
                {
                    var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.ClaimActionNotFound);
                    return NotFound(notFoundDetails);
                }

                var claimActionResponseDTO = AuthenticationLoginProfileMapperInitializer.Mapper.Map<ClaimActionResponseDTO>(existingClaimAction);
                _claimActionService.DeleteClaimAction(id);
                var successResponse = new SuccessResponseDTO 
                { 
                    Message = ResourceAPI.ClaimActionDeletedSuccessfully,
                    Data = claimActionResponseDTO
                };
                return Ok(successResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredClaimActionCouldNotBeDeleted);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }
    }
}