using HealthPlan.API.Resource;
using HealthPlan.API.Swagger;
using HealthPlan.API.Util;
using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.DTO;
using HealthPlan.Quote.Mapping;
using HealthPlan.Quote.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace HealthPlan.API.Controllers
{
    /// <summary>
    /// ResourceAPI.AdhesionFeeControllerDescription
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AdhesionFeeController : ControllerBase
    {
        private readonly IAdhesionFeeService _adhesionFeeService;

        /// <summary>
        /// Initializes a new instance of the AdhesionFeeController.
        /// </summary>
        /// <param name="adhesionFeeService">Service for adhesion fee management operations</param>
        public AdhesionFeeController(IAdhesionFeeService adhesionFeeService)
        {
            _adhesionFeeService = adhesionFeeService;
        }

        /// <summary>
        /// ResourceAPI.DocumentationGetAdhesionFees
        /// </summary>
        /// <returns>
        /// ResourceAPI.ReturnsListOfAdhesionFeeObjectsWithTheirDetailsAndStatusOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError
        /// </returns>
        /// <response code="200">ResourceAPI.AdhesionFeesRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<AdhesionFeeResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetAdhesionFees()
        {
            try
            {
                var adhesionFee = _adhesionFeeService.GetAllActiveAdhesionFees();
                var adhesionFeeResponse = adhesionFee.Select(ta => CleanTemplateApplicationMapperInitializer.Mapper.Map<AdhesionFeeResponseDTO>(ta));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(adhesionFeeResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (InvalidOperationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
                return Unauthorized(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        /// <summary>
        /// ResourceAPI.DocumentationGetAdhesionFeeById
        /// </summary>
        /// <param name="id">Adhesion fee ID to search for</param>
        /// <returns>ResourceAPI.ReturnsAdhesionFeeMatchingTheSpecifiedID</returns>
        /// <response code="200">ResourceAPI.AdhesionFeeRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.AdhesionFeeNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AdhesionFeeResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetAdhesionFees(int id)
        {
            try
            {
                var adhesionFee = _adhesionFeeService.GetById(id);
                if (adhesionFee == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Adhesion fee not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var adhesionFeeResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<AdhesionFeeResponseDTO>(adhesionFee);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(adhesionFeeResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (InvalidOperationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
                return Unauthorized(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        /// <summary>
        /// ResourceAPI.DocumentationAddAdhesionFee
        /// </summary>
        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(AdhesionFeeResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> CreateAdhesionFee([FromBody] AdhesionFeePayLoadDTO adhesionFeePayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(adhesionFeePayLoad, serviceProvider, this);

            if (validationResult != null)
            {
                return validationResult;
            }

            try
            {
                var adhesionFee = CleanTemplateApplicationMapperInitializer.Mapper.Map<AdhesionFee>(adhesionFeePayLoad);
                _adhesionFeeService.AddAdhesionFee(adhesionFee);

                var adhesionFeeResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<AdhesionFeeResponseDTO>(adhesionFee);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(adhesionFeeResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status201Created, successResponse);
            }
            catch (InvalidOperationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForConflict(ex.Message, HttpContext.Request.Path);
                return Conflict(problemDetails);
            }
            catch (ArgumentException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
                return Unauthorized(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        /// <summary>
        /// ResourceAPI.DocumentationUpdateAdhesionFee
        /// </summary>
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(AdhesionFeeResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> UpdateAdhesionFee([FromBody] AdhesionFeePayLoadDTO adhesionFeePayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = await ValidationHelper.ValidateEntityAsync(adhesionFeePayLoad, serviceProvider, this);

            if (validationResult != null)
            {
                return validationResult;
            }

            try
            {
                var existingAdhesionFee = _adhesionFeeService.GetById(adhesionFeePayLoad.Id);
                if (existingAdhesionFee == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound(ResourceAPI.AccountNotFound, HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var adhesionFee = CleanTemplateApplicationMapperInitializer.Mapper.Map<AdhesionFee>(adhesionFeePayLoad);
                adhesionFee.Id = adhesionFeePayLoad.Id;
                _adhesionFeeService.UpdateAdhesionFee(adhesionFee);

                var adhesionFeeResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<AdhesionFeeResponseDTO>(adhesionFee);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(adhesionFeeResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (ArgumentException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
                return Unauthorized(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }

        /// <summary>
        /// ResourceAPI.DocumentationDeleteAdhesionFee
        /// </summary>
        /// <param name="id">Adhesion fee ID to delete</param>
        /// <returns>ResourceAPI.ReturnsConfirmationMessageOnSuccessAdhesionFeeDeletionValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.AdhesionFeeDeletedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.AdhesionFeeNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult DeleteAdhesionFee(int id)
        {
            try
            {
                var existingAdhesionFee = _adhesionFeeService.GetById(id);
                if (existingAdhesionFee == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Adhesion fee not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _adhesionFeeService.DeleteAdhesionFee(id);
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Adhesion fee deleted successfully", "Adhesion fee deleted successfully", HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (ArgumentException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }
            catch (UnauthorizedAccessException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
                return Unauthorized(problemDetails);
            }
            catch (Exception)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError(ResourceAPI.InternalServerError, HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
            }
        }
    }
}