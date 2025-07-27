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
    public class AccountClaimActionController : ControllerBase
    {
        private readonly IAccountClaimActionService _accountClaimActionService;

        public AccountClaimActionController(IAccountClaimActionService accountClaimActionService)
        {
            _accountClaimActionService = accountClaimActionService;
        }

        [HttpGet(AccountClaimActionRoutes.GetAccountClaimActions)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<AccountClaimActionResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetAccountClaimActions([FromQuery] int? idAccount = null, [FromQuery] int? idClaimAction = null)
        {
            try
            {
                IEnumerable<AccountClaimAction> accountClaimActions;
                
                if (idAccount.HasValue)
                {
                    accountClaimActions = _accountClaimActionService.GetByIdAccount(idAccount.Value);
                }
                else if (idClaimAction.HasValue)
                {
                    accountClaimActions = _accountClaimActionService.GetByIdClaimAction(idClaimAction.Value);
                }
                else
                {
                    // For now, return empty list - could implement GetAll if needed
                    accountClaimActions = new List<AccountClaimAction>();
                }

                var accountClaimActionsResponse = accountClaimActions.Select(aca => AuthenticationLoginProfileMapperInitializer.Mapper.Map<AccountClaimActionResponseDTO>(aca));
                return Ok(accountClaimActionsResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredAccountClaimActionsCouldNotBeRetrieved);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpGet(AccountClaimActionRoutes.GetAccountClaimActionById)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AccountClaimActionResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetAccountClaimActionById(int idAccount, int idClaimAction)
        {
            try
            {
                var accountClaimAction = _accountClaimActionService.GetByAccountAndClaimAction(idAccount, idClaimAction);
                if (accountClaimAction == null)
                {
                    var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.AccountClaimActionNotFound);
                    return NotFound(notFoundDetails);
                }

                var accountClaimActionResponse = AuthenticationLoginProfileMapperInitializer.Mapper.Map<AccountClaimActionResponseDTO>(accountClaimAction);
                return Ok(accountClaimActionResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredAccountClaimActionCouldNotBeRetrieved);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpPost(AccountClaimActionRoutes.AddAccountClaimAction)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AccountClaimActionResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> AddAccountClaimAction([FromBody] AccountClaimActionPayLoadDTO accountClaimActionDTO, [FromServices] IServiceProvider serviceProvider)
        {
            var accountClaimAction = AuthenticationLoginProfileMapperInitializer.Mapper.Map<AccountClaimAction>(accountClaimActionDTO);

            try
            {
                _accountClaimActionService.AddAccountClaimAction(accountClaimAction);
                var accountClaimActionResponse = AuthenticationLoginProfileMapperInitializer.Mapper.Map<AccountClaimActionResponseDTO>(accountClaimAction);
                return Ok(accountClaimActionResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ResourceAPI.AnUnexpectedErrorOccurredAccountClaimActionCouldNotBeInserted);
                return BadRequest(problemDetails);
            }
        }

        [HttpPut(AccountClaimActionRoutes.UpdateAccountClaimAction)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AccountClaimActionResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> UpdateAccountClaimAction(int id, [FromBody] AccountClaimActionPayLoadDTO accountClaimActionDTO, [FromServices] IServiceProvider serviceProvider)
        {
            try
            {
                // For simplicity, we'll use the DTO values to find the existing record
                var existingAccountClaimAction = _accountClaimActionService.GetByAccountAndClaimAction(accountClaimActionDTO.IdAccount, accountClaimActionDTO.IdClaimAction);
                if (existingAccountClaimAction == null)
                {
                    var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.AccountClaimActionNotFound);
                    return NotFound(notFoundDetails);
                }

                var accountClaimAction = AuthenticationLoginProfileMapperInitializer.Mapper.Map<AccountClaimAction>(accountClaimActionDTO);
                accountClaimAction.Id = existingAccountClaimAction.Id;

                _accountClaimActionService.UpdateAccountClaimAction(accountClaimAction);
                var accountClaimActionResponse = AuthenticationLoginProfileMapperInitializer.Mapper.Map<AccountClaimActionResponseDTO>(accountClaimAction);
                return Ok(accountClaimActionResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ResourceAPI.AnUnexpectedErrorOccurredAccountClaimActionCouldNotBeUpdated);
                return BadRequest(problemDetails);
            }
        }

        [HttpDelete(AccountClaimActionRoutes.DeleteAccountClaimAction)]
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
        public async Task<IActionResult> DeleteAccountClaimAction(int idAccount, int idClaimAction)
        {
            try
            {
                var existingAccountClaimAction = _accountClaimActionService.GetByAccountAndClaimAction(idAccount, idClaimAction);
                if (existingAccountClaimAction == null)
                {
                    var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.AccountClaimActionNotFound);
                    return NotFound(notFoundDetails);
                }

                var accountClaimActionResponseDTO = AuthenticationLoginProfileMapperInitializer.Mapper.Map<AccountClaimActionResponseDTO>(existingAccountClaimAction);
                _accountClaimActionService.DeleteAccountClaimAction(existingAccountClaimAction.Id);
                var successResponse = new SuccessResponseDTO 
                { 
                    Message = ResourceAPI.AccountClaimActionDeletedSuccessfully,
                    Data = accountClaimActionResponseDTO
                };
                return Ok(successResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredAccountClaimActionCouldNotBeDeleted);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }
    }
}