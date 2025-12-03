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
    /// ResourceAPI.CoverageControllerDescription
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CoverageController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the CoverageController.
        /// </summary>
        /// <param name="mediator">MediatR mediator for command/query handling.</param>
        public CoverageController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// ResourceAPI.DocumentationGetCoverages
        /// </summary>
        /// <returns>
        /// ResourceAPI.ReturnsListOfCoverageObjectsWithTheirDetailsAndStatusOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError
        /// </returns>
        /// <response code="200">ResourceAPI.CoveragesRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet(CoverageRoutes.GetCoverages)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CoverageResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetCoverages()
        {
            try
            {
                var query = new GetAllCoveragesQuery();
                var coverages = await mediator.Send(query);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(coverages, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationGetCoverageById
        /// </summary>
        /// <param name="id">Coverage ID to search for</param>
        /// <returns>ResourceAPI.ReturnsCoverageMatchingTheSpecifiedID</returns>
        /// <response code="200">ResourceAPI.CoveragesRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.CoverageNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet(CoverageRoutes.GetCoverageById)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CoverageResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetCoverage(int id)
        {
            try
            {
                var query = new GetCoverageByIdQuery { Id = id };
                var coverage = await mediator.Send(query);
                if (coverage == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Coverage not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var successResponse = SuccessResponseExampleFactory.ForSuccess(coverage, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Retrieves coverages by type.
        /// </summary>
        /// <param name="coverageType">Coverage type to search for</param>
        /// <returns>Returns Coverages matching the specified type</returns>
        /// <response code="200">ResourceAPI.CoveragesRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet(CoverageRoutes.GetCoveragesByType)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CoverageResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetCoveragesByType(string coverageType)
        {
            try
            {
                var query = new GetCoveragesByTypeQuery { CoverageType = coverageType };
                var coverages = await mediator.Send(query);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(coverages, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationAddCoverage
        /// </summary>
        /// <param name="coveragePayLoad">Coverage data to create</param>
        /// <returns>ResourceAPI.ReturnsCreatedCoverageOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="201">ResourceAPI.CoverageCreatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="409">ResourceAPI.CoverageAlreadyExists</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpPost(CoverageRoutes.AddCoverage)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(CoverageResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> CreateCoverage([FromBody] CoveragePayLoadDTO coveragePayLoad)
        {
            try
            {
                var command = new CreateCoverageCommand
                {
                    Name = coveragePayLoad.Name,
                    Description = coveragePayLoad.Description,
                    CoverageType = coveragePayLoad.CoverageType,
                    CreatedBy = coveragePayLoad.CreatedBy,
                };
                var coverage = await mediator.Send(command);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(coverage, "Coverage created successfully", HttpContext.Request.Path);
                return StatusCode(StatusCodes.Status201Created, successResponse);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", ex.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
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
        /// ResourceAPI.DocumentationUpdateCoverage
        /// </summary>
        /// <param name="coveragePayLoad">Updated coverage data including the ID</param>
        /// <returns>ResourceAPI.ReturnsUpdatedCoverageOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.CoverageUpdatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.CoverageNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpPut(CoverageRoutes.UpdateCoverage)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CoverageResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> UpdateCoverage([FromBody] CoveragePayLoadDTO coveragePayLoad)
        {
            try
            {
                var command = new UpdateCoverageCommand
                {
                    Id = coveragePayLoad.Id,
                    Name = coveragePayLoad.Name,
                    Description = coveragePayLoad.Description,
                    CoverageType = coveragePayLoad.CoverageType,
                    UpdatedBy = coveragePayLoad.UpdatedBy,
                };
                var coverage = await mediator.Send(command);
                if (coverage == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Coverage not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var successResponse = SuccessResponseExampleFactory.ForSuccess(coverage, "Coverage updated successfully", HttpContext.Request.Path);
                return Ok(successResponse);
            }
            catch (FluentValidation.ValidationException ex)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", ex.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
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
        /// Deletes a coverage from the system.
        /// </summary>
        /// <param name="id">Coverage ID to delete</param>
        /// <returns>ResourceAPI.ReturnsConfirmationMessageOnSuccessCoverageDeletionValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.CoverageDeletedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.CoverageNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpDelete(CoverageRoutes.DeleteCoverage)]
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
        public async Task<IActionResult> DeleteCoverage(int id)
        {
            try
            {
                var command = new DeleteCoverageCommand { Id = id };
                var result = await mediator.Send(command);
                if (!result)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Coverage not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var successResponse = SuccessResponseExampleFactory.ForSuccess("Coverage deleted successfully", "Coverage deleted successfully", HttpContext.Request.Path);
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