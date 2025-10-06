using HealthPlan.API.Resource;
using HealthPlan.API.Swagger;
using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.DTO;
using HealthPlan.Quote.Mapping;
using HealthPlan.Quote.Services.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace HealthPlan.API.Controllers
{
    /// <summary>
    /// Controller for managing Company entities.
    /// Provides comprehensive CRUD operations following the established CleanEntity pattern.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IValidator<CompanyPayLoadDTO> validator;

        /// <summary>
        /// Initializes a new instance of the CompanyController.
        /// </summary>
        /// <param name="companyService">Service for company management operations</param>
        /// <param name="validator">Validator for CompanyPayLoadDTO</param>
        public CompanyController(ICompanyService companyService, IValidator<CompanyPayLoadDTO> validator)
        {
            _companyService = companyService;
            this.validator = validator;
        }

        /// <summary>
        /// Retrieves all companies from the system.
        /// </summary>
        /// <returns>
        /// Returns list of Company objects with their details and status on success, validation errors, unauthorized access, or internal server error.
        /// </returns>
        /// <response code="200">Companies retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(CompanyRoutes.GetCompanies)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CompanyResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _companyService.GetAllActiveCompanies();
                var companiesResponse = companies.Select(c => CleanTemplateApplicationMapperInitializer.Mapper.Map<CompanyResponseDTO>(c));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(companiesResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Retrieves a company by its unique identifier.
        /// </summary>
        /// <param name="id">Company ID to search for</param>
        /// <returns>Returns Company matching the specified ID</returns>
        /// <response code="200">Company retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Company not found</response>
        /// <response code="500">Internal server error</response>
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
        public IActionResult GetCompany(int id)
        {
            try
            {
                var company = _companyService.GetById(id);
                if (company == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Company not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var companyResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<CompanyResponseDTO>(company);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(companyResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// <response code="200">Company retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Company not found</response>
        /// <response code="500">Internal server error</response>
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
        public IActionResult GetCompanyByCNPJ(string cnpj)
        {
            try
            {
                var company = _companyService.GetCompanyByCNPJ(cnpj);
                if (company == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Company not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var companyResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<CompanyResponseDTO>(company);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(companyResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Creates a new company in the system.
        /// </summary>
        /// <param name="companyPayLoad">Company data to create</param>
        /// <returns>Returns created Company on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="201">Company created successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Company already exists</response>
        /// <response code="500">Internal server error</response>
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
        public IActionResult CreateCompany([FromBody] CompanyPayLoadDTO companyPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(companyPayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var company = CleanTemplateApplicationMapperInitializer.Mapper.Map<Company>(companyPayLoad);
                _companyService.AddCompany(company);

                var companyResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<CompanyResponseDTO>(company);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(companyResponse, "Company created successfully", HttpContext.Request.Path);
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
        /// Updates an existing company.
        /// </summary>
        /// <param name="id">Company ID to update</param>
        /// <param name="companyPayLoad">Updated company data</param>
        /// <returns>Returns updated Company on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Company updated successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Company not found</response>
        /// <response code="500">Internal server error</response>
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
        public IActionResult UpdateCompany(int id, [FromBody] CompanyPayLoadDTO companyPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(companyPayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var existingCompany = _companyService.GetById(id);
                if (existingCompany == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Company not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var company = CleanTemplateApplicationMapperInitializer.Mapper.Map<Company>(companyPayLoad);
                company.Id = id;
                _companyService.UpdateCompany(company);

                var companyResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<CompanyResponseDTO>(company);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(companyResponse, "Company updated successfully", HttpContext.Request.Path);
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
        /// Deletes a company from the system.
        /// </summary>
        /// <param name="id">Company ID to delete</param>
        /// <returns>Returns confirmation message on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Company deleted successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Company not found</response>
        /// <response code="500">Internal server error</response>
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
        public IActionResult DeleteCompany(int id)
        {
            try
            {
                var existingCompany = _companyService.GetById(id);
                if (existingCompany == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Company not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _companyService.DeleteCompany(id);
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