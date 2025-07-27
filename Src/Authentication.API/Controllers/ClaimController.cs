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
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpGet(ClaimRoutes.GetClaims)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClaimResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetClaims()
        {
            try
            {
                var claims = _claimService.GetAll();
                var claimsResponse = claims.Select(c => AuthenticationLoginProfileMapperInitializer.Mapper.Map<ClaimResponseDTO>(c));
                return Ok(claimsResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredClaimsCouldNotBeRetrieved);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpGet(ClaimRoutes.GetClaimById)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ClaimResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetClaimById(int id)
        {
            try
            {
                var claim = _claimService.GetById(id);
                if (claim == null)
                {
                    var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.ClaimNotFound);
                    return NotFound(notFoundDetails);
                }

                var claimResponse = AuthenticationLoginProfileMapperInitializer.Mapper.Map<ClaimResponseDTO>(claim);
                return Ok(claimResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredClaimCouldNotBeRetrieved);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        [HttpPost(ClaimRoutes.AddClaim)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ClaimResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> AddClaim([FromBody] ClaimPayLoadDTO claimDTO, [FromServices] IServiceProvider serviceProvider)
        {
            // TODO: Validation would go here if needed
            
            var claim = AuthenticationLoginProfileMapperInitializer.Mapper.Map<Claim>(claimDTO);

            try
            {
                _claimService.AddClaim(claim);
                var claimResponse = AuthenticationLoginProfileMapperInitializer.Mapper.Map<ClaimResponseDTO>(claim);
                return Ok(claimResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ResourceAPI.AnUnexpectedErrorOccurredClaimCouldNotBeInserted);
                return BadRequest(problemDetails);
            }
        }

        [HttpPut(ClaimRoutes.UpdateClaim)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ClaimResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SuccessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> UpdateClaim(int id, [FromBody] ClaimPayLoadDTO claimDTO, [FromServices] IServiceProvider serviceProvider)
        {
            try
            {
                var existingClaim = _claimService.GetById(id);
                if (existingClaim == null)
                {
                    var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.ClaimNotFound);
                    return NotFound(notFoundDetails);
                }

                var claim = AuthenticationLoginProfileMapperInitializer.Mapper.Map<Claim>(claimDTO);
                claim.Id = id;

                _claimService.UpdateClaim(claim);
                var claimResponse = AuthenticationLoginProfileMapperInitializer.Mapper.Map<ClaimResponseDTO>(claim);
                return Ok(claimResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ResourceAPI.AnUnexpectedErrorOccurredClaimCouldNotBeUpdated);
                return BadRequest(problemDetails);
            }
        }

        [HttpDelete(ClaimRoutes.DeleteClaim)]
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
        public async Task<IActionResult> DeleteClaim(int id)
        {
            try
            {
                var existingClaim = _claimService.GetById(id);
                if (existingClaim == null)
                {
                    var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.ClaimNotFound);
                    return NotFound(notFoundDetails);
                }

                var claimResponseDTO = AuthenticationLoginProfileMapperInitializer.Mapper.Map<ClaimResponseDTO>(existingClaim);
                _claimService.DeleteClaim(id);
                var successResponse = new SuccessResponseDTO 
                { 
                    Message = ResourceAPI.ClaimDeletedSuccessfully,
                    Data = claimResponseDTO
                };
                return Ok(successResponse);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.AnUnexpectedErrorOccurredClaimCouldNotBeDeleted);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }
    }
}