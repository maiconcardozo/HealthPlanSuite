using HealthPlan.API.Resource;
using HealthPlan.API.Swagger;
using HealthPlan.Domain.Entities;
using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace HealthPlan.API.Controllers
{
    /// <summary>
    /// ResourceAPI.BeneficiaryControllerDescription
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IValidator<BeneficiaryPayLoadDTO> validator;

        /// <summary>
        /// Initializes a new instance of the BeneficiaryController.
        /// </summary>
        /// <param name="beneficiaryService">Service for beneficiary management operations</param>
        /// <param name="validator">Validator for BeneficiaryPayLoadDTO</param>
        public BeneficiaryController(IBeneficiaryService beneficiaryService, IValidator<BeneficiaryPayLoadDTO> validator)
        {
            _beneficiaryService = beneficiaryService;
            this.validator = validator;
        }

        /// <summary>
        /// ResourceAPI.DocumentationGetBeneficiarys
        /// </summary>
        /// <returns>
        /// ResourceAPI.ReturnsListOfBeneficiaryObjectsWithTheirDetailsAndStatusOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError
        /// </returns>
        /// <response code="200">ResourceAPI.BeneficiarysRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<BeneficiaryResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetBeneficiaries()
        {
            try
            {
                var beneficiaries = _beneficiaryService.GetAllActiveBeneficiaries();
                var beneficiariesResponse = beneficiaries.Select(b => CleanTemplateApplicationMapperInitializer.Mapper.Map<BeneficiaryResponseDTO>(b));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(beneficiariesResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationGetBeneficiaryById
        /// </summary>
        /// <param name="id">Beneficiary ID to search for</param>
        /// <returns>ResourceAPI.ReturnsBeneficiaryMatchingTheSpecifiedID</returns>
        /// <response code="200">ResourceAPI.BeneficiarysRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.BeneficiaryNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BeneficiaryResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetBeneficiary(int id)
        {
            try
            {
                var beneficiary = _beneficiaryService.GetById(id);
                if (beneficiary == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Beneficiary not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var beneficiaryResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<BeneficiaryResponseDTO>(beneficiary);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(beneficiaryResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationAddBeneficiary
        /// </summary>
        /// <param name="beneficiaryPayLoad">Beneficiary data to create</param>
        /// <returns>ResourceAPI.ReturnsCreatedBeneficiaryOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="201">ResourceAPI.BeneficiaryCreatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="409">ResourceAPI.BeneficiaryAlreadyExists</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(BeneficiaryResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult CreateBeneficiary([FromBody] BeneficiaryPayLoadDTO beneficiaryPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(beneficiaryPayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var beneficiary = CleanTemplateApplicationMapperInitializer.Mapper.Map<Beneficiary>(beneficiaryPayLoad);
                _beneficiaryService.AddBeneficiary(beneficiary);

                var beneficiaryResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<BeneficiaryResponseDTO>(beneficiary);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(beneficiaryResponse, "Beneficiary created successfully", HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationUpdateBeneficiary
        /// </summary>
        /// <param name="beneficiaryPayLoad">Updated beneficiary data including the ID</param>
        /// <returns>ResourceAPI.ReturnsUpdatedBeneficiaryOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.BeneficiaryUpdatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.BeneficiaryNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BeneficiaryResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult UpdateBeneficiary([FromBody] BeneficiaryPayLoadDTO beneficiaryPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(beneficiaryPayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var existingBeneficiary = _beneficiaryService.GetById(beneficiaryPayLoad.Id);
                if (existingBeneficiary == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Beneficiary not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var beneficiary = CleanTemplateApplicationMapperInitializer.Mapper.Map<Beneficiary>(beneficiaryPayLoad);
                beneficiary.Id = beneficiaryPayLoad.Id;
                _beneficiaryService.UpdateBeneficiary(beneficiary);

                var beneficiaryResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<BeneficiaryResponseDTO>(beneficiary);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(beneficiaryResponse, "Beneficiary updated successfully", HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationDeleteBeneficiary
        /// </summary>
        /// <param name="id">Beneficiary ID to delete</param>
        /// <returns>ResourceAPI.ReturnsConfirmationMessageOnSuccessBeneficiaryDeletionValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.BeneficiaryDeletedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.BeneficiaryNotFound</response>
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
        public IActionResult DeleteBeneficiary(int id)
        {
            try
            {
                var existingBeneficiary = _beneficiaryService.GetById(id);
                if (existingBeneficiary == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Beneficiary not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _beneficiaryService.DeleteBeneficiary(id);
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Beneficiary deleted successfully", "Beneficiary deleted successfully", HttpContext.Request.Path);
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
        /// Searches beneficiaries by CPF.
        /// Note: This functionality is not implemented in the current service layer.
        /// </summary>
        /// <param name="cpf">CPF to search for</param>
        /// <returns>Returns message indicating feature not available</returns>
        /// <response code="501">Feature not implemented</response>
        [HttpGet("cpf/{cpf}")]
        [SwaggerResponse(StatusCodes.Status501NotImplemented, Type = typeof(string))]
        public IActionResult GetBeneficiaryByCPF(string cpf)
        {
            // This would require implementing GetByCPF method in IBeneficiaryService
            var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError("GetByCPF feature not yet implemented in service layer", HttpContext.Request.Path);
            return StatusCode(StatusCodes.Status501NotImplemented, problemDetails);
        }
    }
}