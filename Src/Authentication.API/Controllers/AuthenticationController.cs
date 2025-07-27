using Authentication.API.Resource;
using Authentication.API.Swagger;
using Authentication.Login.Domain.Implementation;
using Authentication.Login.Domain.Interface;
using Authentication.Login.DTO;
using Authentication.Login.Mapping;
using Authentication.Login.Services.Interface;
using Foundation.Base.Util;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Authentication.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJwtSettings _jwtSettings;

        public AuthenticationController(
            IAccountService accountService, IJwtSettings jwtSettings)
        {
            _accountService = accountService;
            _jwtSettings = jwtSettings;
        }

        [HttpPost(AuthenticationRoutes.GenerateToken)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TokenResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GenerateToken([FromBody] AccountPayLoadDTO authenticationDTO, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(authenticationDTO, serviceProvider, this);
            if (validationResult != null)
                return validationResult;

            var account = AuthenticationLoginProfileMapperInitializer.Mapper.Map<Account>(authenticationDTO);

            try
            {
                var token = _accountService.GenerateToken(account, _jwtSettings);
                var tokenDTO = AuthenticationLoginProfileMapperInitializer.Mapper.Map<TokenResponseDTO>(token);
                return Ok(tokenDTO);
            }
            catch (InvalidOperationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message);
                return BadRequest(problemDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message);
                return Unauthorized(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredTokenCouldGenerated);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpPost(AuthenticationRoutes.AddAccount)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AccountResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> AddAccount([FromBody] AccountPayLoadDTO authenticationDTO, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(authenticationDTO, serviceProvider, this);
            if (validationResult != null)
                return validationResult;

            var account = AuthenticationLoginProfileMapperInitializer.Mapper.Map<Account>(authenticationDTO);

            try
            {
                _accountService.AddAccount(account);
                var accountResponse = AuthenticationLoginProfileMapperInitializer.Mapper.Map<AccountResponseDTO>(account);
                return Ok(accountResponse);
            }
            catch (InvalidOperationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message);
                return BadRequest(problemDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message);
                return Unauthorized(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredAccountCouldInserted);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }
    }
}