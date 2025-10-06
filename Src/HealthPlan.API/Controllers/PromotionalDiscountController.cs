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
    /// Controller for managing PromotionalDiscount entities (Descontos Promocionais).
    /// Provides comprehensive CRUD operations for promotional discounts of health plans.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PromotionalDiscountController : ControllerBase
    {
        private readonly IPromotionalDiscountService _descontoPromocionalService;
        private readonly IValidator<PromotionalDiscountPayLoadDTO> validator;

        /// <summary>
        /// Initializes a new instance of the PromotionalDiscountController.
        /// </summary>
        /// <param name="descontoPromocionalService">Service for promotional discount management operations</param>
        /// <param name="validator">Validator for PromotionalDiscountPayLoadDTO</param>
        public PromotionalDiscountController(IPromotionalDiscountService descontoPromocionalService, IValidator<PromotionalDiscountPayLoadDTO> validator)
        {
            _descontoPromocionalService = descontoPromocionalService;
            this.validator = validator;
        }

        /// <summary>
        /// Retrieves all promotional discounts from the system.
        /// </summary>
        /// <returns>
        /// Returns list of PromotionalDiscount objects with their details and status on success, validation errors, unauthorized access, or internal server error.
        /// </returns>
        /// <response code="200">Promotional discounts retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<PromotionalDiscountResponseDTO>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetDescontoPromocional()
        {
            try
            {
                var descontoPromocional = _descontoPromocionalService.GetAllActiveDescontoPromocional();
                var descontoPromocionalResponse = descontoPromocional.Select(dp => CleanTemplateApplicationMapperInitializer.Mapper.Map<PromotionalDiscountResponseDTO>(dp));
                var successResponse = SuccessResponseExampleFactory.ForSuccess(descontoPromocionalResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Retrieves a specific promotional discount by ID.
        /// </summary>
        /// <param name="id">Promotional discount ID to search for</param>
        /// <returns>Returns PromotionalDiscount matching the specified ID</returns>
        /// <response code="200">Promotional discount retrieved successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Promotional discount not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PromotionalDiscountResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult GetDescontoPromocional(int id)
        {
            try
            {
                var descontoPromocional = _descontoPromocionalService.GetById(id);
                if (descontoPromocional == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Promotional discount not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var descontoPromocionalResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<PromotionalDiscountResponseDTO>(descontoPromocional);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(descontoPromocionalResponse, ResourceAPI.RequestWasSuccessful, HttpContext.Request.Path);
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
        /// Creates a new promotional discount.
        /// </summary>
        /// <param name="descontoPromocionalPayLoad">Promotional discount data to create</param>
        /// <returns>Returns created PromotionalDiscount on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="201">Promotional discount created successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="409">Promotional discount already exists</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(PromotionalDiscountResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status409Conflict, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(ProblemDetailsConflictExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult CreateDescontoPromocional([FromBody] PromotionalDiscountPayLoadDTO descontoPromocionalPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(descontoPromocionalPayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var descontoPromocional = CleanTemplateApplicationMapperInitializer.Mapper.Map<PromotionalDiscount>(descontoPromocionalPayLoad);
                _descontoPromocionalService.AddDescontoPromocional(descontoPromocional);

                var descontoPromocionalResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<PromotionalDiscountResponseDTO>(descontoPromocional);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(descontoPromocionalResponse, "Promotional discount created successfully", HttpContext.Request.Path);
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
        /// Updates an existing promotional discount.
        /// </summary>
        /// <param name="id">Promotional discount ID to update</param>
        /// <param name="descontoPromocionalPayLoad">Updated promotional discount data</param>
        /// <returns>Returns updated PromotionalDiscount on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Promotional discount updated successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Promotional discount not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(PromotionalDiscountResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
        public IActionResult UpdateDescontoPromocional(int id, [FromBody] PromotionalDiscountPayLoadDTO descontoPromocionalPayLoad, [FromServices] IServiceProvider serviceProvider)
        {
            var validationResult = validator.Validate(descontoPromocionalPayLoad);
            if (!validationResult.IsValid)
            {
                var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(
                    string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    HttpContext.Request.Path);
                return BadRequest(problemDetails);
            }

            try
            {
                var existingDescontoPromocional = _descontoPromocionalService.GetById(id);
                if (existingDescontoPromocional == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Promotional discount not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                var descontoPromocional = CleanTemplateApplicationMapperInitializer.Mapper.Map<PromotionalDiscount>(descontoPromocionalPayLoad);
                descontoPromocional.Id = id;
                _descontoPromocionalService.UpdateDescontoPromocional(descontoPromocional);

                var descontoPromocionalResponse = CleanTemplateApplicationMapperInitializer.Mapper.Map<PromotionalDiscountResponseDTO>(descontoPromocional);
                var successResponse = SuccessResponseExampleFactory.ForSuccess(descontoPromocionalResponse, "Promotional discount updated successfully", HttpContext.Request.Path);
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
        /// Deletes an existing promotional discount.
        /// </summary>
        /// <param name="id">Promotional discount ID to delete</param>
        /// <returns>Returns confirmation message on success, validation errors, unauthorized access, or internal server error</returns>
        /// <response code="200">Promotional discount deleted successfully</response>
        /// <response code="400">Invalid request parameters</response>
        /// <response code="401">Unauthorized access</response>
        /// <response code="404">Promotional discount not found</response>
        /// <response code="500">Internal server error</response>
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
        public IActionResult DeleteDescontoPromocional(int id)
        {
            try
            {
                var existingDescontoPromocional = _descontoPromocionalService.GetById(id);
                if (existingDescontoPromocional == null)
                {
                    var problemDetails = ProblemDetailsExampleFactory.ForNotFound("Promotional discount not found", HttpContext.Request.Path);
                    return NotFound(problemDetails);
                }

                _descontoPromocionalService.DeleteDescontoPromocional(id);
                var successResponse = SuccessResponseExampleFactory.ForSuccess("Promotional discount deleted successfully", "Promotional discount deleted successfully", HttpContext.Request.Path);
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