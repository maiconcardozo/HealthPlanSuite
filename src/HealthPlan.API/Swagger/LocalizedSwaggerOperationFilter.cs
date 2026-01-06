using System.Globalization;
using HealthPlan.API.Resource;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HealthPlan.API.Swagger
{
    public class LocalizedSwaggerOperationFilter : IOperationFilter
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalizedSwaggerOperationFilter(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            switch (context.MethodInfo.Name)
            {
                // CompanyController
                case "GetCompanies":
                    operation.Summary = ResourceAPI.GetCompanies;
                    operation.Description = ResourceAPI.DocumentationGetCompanys;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.CompanysRetrievedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "GetCompany":
                    operation.Summary = ResourceAPI.GetCompanyById;
                    operation.Description = ResourceAPI.DocumentationGetCompanyById;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.CompanysRetrievedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, ResourceAPI.CompanyNotFound);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "GetCompanyByCNPJ":
                    operation.Summary = ResourceAPI.GetCompanyByCNPJ;
                    operation.Description = ResourceAPI.DocumentationGetCompanyByCNPJ;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.CompanysRetrievedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, ResourceAPI.CompanyNotFound);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "CreateCompany":
                    operation.Summary = ResourceAPI.AddCompany;
                    operation.Description = ResourceAPI.DocumentationAddCompany;
                    SetResponseDescription(operation, StatusCodes.Status201Created, ResourceAPI.CompanyCreatedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status409Conflict, ResourceAPI.CompanyAlreadyExists);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "UpdateCompany":
                    operation.Summary = ResourceAPI.UpdateCompany;
                    operation.Description = ResourceAPI.DocumentationUpdateCompany;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.CompanyUpdatedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, ResourceAPI.CompanyNotFound);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "DeleteCompany":
                    operation.Summary = ResourceAPI.DeleteCompany;
                    operation.Description = ResourceAPI.DocumentationDeleteCompany;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.CompanyDeletedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, ResourceAPI.CompanyNotFound);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;

                // CoverageController
                case "GetCoverages":
                    operation.Summary = ResourceAPI.GetCoverages;
                    operation.Description = ResourceAPI.DocumentationGetCoverages;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.CoveragesRetrievedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "GetCoverage":
                    operation.Summary = ResourceAPI.GetCoverageById;
                    operation.Description = ResourceAPI.DocumentationGetCoverageById;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.CoveragesRetrievedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, ResourceAPI.CoverageNotFound);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "GetCoveragesByType":
                    operation.Summary = ResourceAPI.GetCoveragesByType;
                    operation.Description = ResourceAPI.DocumentationGetCoveragesByType;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.CoveragesRetrievedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "CreateCoverage":
                    operation.Summary = ResourceAPI.AddCoverage;
                    operation.Description = ResourceAPI.DocumentationAddCoverage;
                    SetResponseDescription(operation, StatusCodes.Status201Created, ResourceAPI.CoverageCreatedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status409Conflict, ResourceAPI.CoverageAlreadyExists);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "UpdateCoverage":
                    operation.Summary = ResourceAPI.UpdateCoverage;
                    operation.Description = ResourceAPI.DocumentationUpdateCoverage;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.CoverageUpdatedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, ResourceAPI.CoverageNotFound);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "DeleteCoverage":
                    operation.Summary = ResourceAPI.DeleteCoverage;
                    operation.Description = ResourceAPI.DocumentationDeleteCoverage;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.CoverageDeletedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, ResourceAPI.CoverageNotFound);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;

                // HealthPlanController
                case "GetHealthPlans":
                    operation.Summary = ResourceAPI.GetHealthPlans;
                    operation.Description = ResourceAPI.DocumentationGetHealthPlans;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.HealthPlansRetrievedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "GetHealthPlan":
                    operation.Summary = ResourceAPI.GetHealthPlanById;
                    operation.Description = ResourceAPI.DocumentationGetHealthPlanById;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.HealthPlansRetrievedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, ResourceAPI.HealthPlanNotFound);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "CreateHealthPlan":
                    operation.Summary = ResourceAPI.AddHealthPlan;
                    operation.Description = ResourceAPI.DocumentationAddHealthPlan;
                    SetResponseDescription(operation, StatusCodes.Status201Created, ResourceAPI.HealthPlanCreatedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status409Conflict, ResourceAPI.HealthPlanAlreadyExists);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "UpdateHealthPlan":
                    operation.Summary = ResourceAPI.UpdateHealthPlan;
                    operation.Description = ResourceAPI.DocumentationUpdateHealthPlan;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.HealthPlanUpdatedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, ResourceAPI.HealthPlanNotFound);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "DeleteHealthPlan":
                    operation.Summary = ResourceAPI.DeleteHealthPlan;
                    operation.Description = ResourceAPI.DocumentationDeleteHealthPlan;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.HealthPlanDeletedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, ResourceAPI.HealthPlanNotFound);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;

                // QuoteController
                case "GetQuotes":
                    operation.Summary = ResourceAPI.GetQuotes;
                    operation.Description = ResourceAPI.DocumentationGetQuotes;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.QuotesRetrievedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorized);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "GetQuote":
                    operation.Summary = ResourceAPI.GetQuoteById;
                    operation.Description = ResourceAPI.DocumentationGetQuoteById;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.QuoteRetrievedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, ResourceAPI.QuoteNotFound);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "GetQuotesByBeneficiary":
                    operation.Summary = ResourceAPI.GetQuotesByBeneficiary;
                    operation.Description = ResourceAPI.DocumentationGetQuotesByBeneficiary;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.QuotesRetrievedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "CreateQuote":
                    operation.Summary = ResourceAPI.AddQuote;
                    operation.Description = ResourceAPI.DocumentationAddQuote;
                    SetResponseDescription(operation, StatusCodes.Status201Created, ResourceAPI.QuoteCreatedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status409Conflict, ResourceAPI.QuoteAlreadyExists);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "UpdateQuote":
                    operation.Summary = ResourceAPI.UpdateQuote;
                    operation.Description = ResourceAPI.DocumentationUpdateQuote;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.QuoteUpdatedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, ResourceAPI.QuoteNotFound);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
                case "DeleteQuote":
                    operation.Summary = ResourceAPI.DeleteQuote;
                    operation.Description = ResourceAPI.DocumentationDeleteQuote;
                    SetResponseDescription(operation, StatusCodes.Status200OK, ResourceAPI.QuoteDeletedSuccessfully);
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, ResourceAPI.ResponseInvalidRequestParameters);
                    SetResponseDescription(operation, StatusCodes.Status401Unauthorized, ResourceAPI.ResponseUnauthorizedAccess);
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, ResourceAPI.QuoteNotFound);
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, ResourceAPI.InternalServerError);
                    break;
            }
        }

        private void SetResponseDescription(OpenApiOperation operation, int statusCode, string description)
        {
            var key = statusCode.ToString();
            if (operation.Responses.ContainsKey(key))
            {
                operation.Responses[key].Description = description;
            }
        }
    }
}
