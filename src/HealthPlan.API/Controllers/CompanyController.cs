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
    /// ResourceAPI.CompanyControllerDescription
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the CompanyController.
        /// </summary>
        /// <param name="mediator">MediatR mediator for command/query handling.</param>
        public CompanyController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// ResourceAPI.DocumentationGetCompanys
        /// </summary>
        /// <returns>
        /// ResourceAPI.ReturnsListOfCompanyObjectsWithTheirDetailsAndStatusOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError
        /// </returns>
        /// <response code="200">ResourceAPI.CompanysRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet(CompanyRoutes.GetCompanies)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CompanyResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var query = new GetAllCompaniesQuery();
                var companies = await mediator.Send(query);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(companies, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationGetCompanyById
        /// </summary>
        /// <param name="id">Company ID to search for</param>
        /// <returns>ResourceAPI.ReturnsCompanyMatchingTheSpecifiedID</returns>
        /// <response code="200">ResourceAPI.CompanysRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.CompanyNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet(CompanyRoutes.GetCompanyById)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CompanyResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var query = new GetCompanyByIdQuery { Id = id };
                var company = await mediator.Send(query);
                if (company == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Company not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var successResponse = SuccessResponseExampleFactory.ForSuccess(company, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Retrieves a company by its CNPJ.
        /// </summary>
        /// <param name="cnpj">CNPJ to search for</param>
        /// <returns>Returns Company matching the specified CNPJ</returns>
        /// <response code="200">ResourceAPI.CompanysRetrievedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.CompanyNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpGet(CompanyRoutes.GetCompanyByCNPJ)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CompanyResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> GetCompanyByCNPJ(string cnpj)
        {
            try
            {
                var query = new GetCompanyByCNPJQuery { CNPJ = cnpj };
                var company = await mediator.Send(query);
                if (company == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Company not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var successResponse = SuccessResponseExampleFactory.ForSuccess(company, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationAddCompany
        /// </summary>
        /// <param name="companyPayLoad">Company data to create</param>
        /// <returns>ResourceAPI.ReturnsCreatedCompanyOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="201">ResourceAPI.CompanyCreatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="409">ResourceAPI.CompanyAlreadyExists</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpPost(CompanyRoutes.AddCompany)]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(CompanyResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyPayLoadDTO companyPayLoad)
        {
            try
            {
                var command = new CreateCompanyCommand
                {
                    Name = companyPayLoad.Name,
                    TradeName = companyPayLoad.TradeName,
                    CNPJ = companyPayLoad.CNPJ,
                    Email = companyPayLoad.Email,
                    Phone = companyPayLoad.Phone,
                    Address = companyPayLoad.Address,
                    City = companyPayLoad.City,
                    State = companyPayLoad.State,
                    ZipCode = companyPayLoad.ZipCode,
                    CreatedBy = companyPayLoad.CreatedBy,
                };
                var company = await mediator.Send(command);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(company, "Company created successfully", HttpContext.Request.Path);
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
        /// ResourceAPI.DocumentationUpdateCompany
        /// </summary>
        /// <param name="companyPayLoad">Updated company data including the ID</param>
        /// <returns>ResourceAPI.ReturnsUpdatedCompanyOnSuccessValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.CompanyUpdatedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.CompanyNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpPut(CompanyRoutes.UpdateCompany)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(CompanyResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyPayLoadDTO companyPayLoad)
        {
            try
            {
                var command = new UpdateCompanyCommand
                {
                    Id = companyPayLoad.Id,
                    Name = companyPayLoad.Name,
                    TradeName = companyPayLoad.TradeName,
                    CNPJ = companyPayLoad.CNPJ,
                    Email = companyPayLoad.Email,
                    Phone = companyPayLoad.Phone,
                    Address = companyPayLoad.Address,
                    City = companyPayLoad.City,
                    State = companyPayLoad.State,
                    ZipCode = companyPayLoad.ZipCode,
                    UpdatedBy = companyPayLoad.UpdatedBy,
                };
                var company = await mediator.Send(command);
                if (company == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Company not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var successResponse = SuccessResponseExampleFactory.ForSuccess(company, "Company updated successfully", HttpContext.Request.Path);
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
        /// Deletes a company from the system.
        /// </summary>
        /// <param name="id">Company ID to delete</param>
        /// <returns>ResourceAPI.ReturnsConfirmationMessageOnSuccessCompanyDeletionValidationErrorsUnauthorizedAccessOrInternalServerError</returns>
        /// <response code="200">ResourceAPI.CompanyDeletedSuccessfully</response>
        /// <response code="400">ResourceAPI.ResponseInvalidRequestParameters</response>
        /// <response code="401">ResourceAPI.ResponseUnauthorizedAccess</response>
        /// <response code="404">ResourceAPI.CompanyNotFound</response>
        /// <response code="500">ResourceAPI.InternalServerError</response>
        [HttpDelete(CompanyRoutes.DeleteCompany)]
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
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var command = new DeleteCompanyCommand { Id = id };
                var result = await mediator.Send(command);
                if (!result)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Company not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var successResponse = SuccessResponseExampleFactory.ForSuccess("Company deleted successfully", "Company deleted successfully", HttpContext.Request.Path);
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