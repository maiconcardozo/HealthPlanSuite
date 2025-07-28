using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using HealthPlan.API.Resource;

namespace HealthPlan.API.Swagger
{
    public class LocalizedSwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            switch (context.MethodInfo.Name)
            {
                case "GetHealthInsuranceOperators":
                    operation.Summary = "Get all health insurance operators";
                    operation.Description = "Retrieve a list of all health insurance operators";
                    SetResponseDescription(operation, StatusCodes.Status200OK, "Success");
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, "Bad Request");
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, "Internal Server Error");
                    break;

                case "GetHealthInsuranceOperatorById":
                    operation.Summary = "Get health insurance operator by ID";
                    operation.Description = "Retrieve a specific health insurance operator by ID";
                    SetResponseDescription(operation, StatusCodes.Status200OK, "Success");
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, "Bad Request");
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, "Not Found");
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, "Internal Server Error");
                    break;

                case "AddHealthInsuranceOperator":
                    operation.Summary = "Add new health insurance operator";
                    operation.Description = "Create a new health insurance operator";
                    SetResponseDescription(operation, StatusCodes.Status201Created, "Created");
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, "Bad Request");
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, "Internal Server Error");
                    break;

                case "UpdateHealthInsuranceOperator":
                    operation.Summary = "Update health insurance operator";
                    operation.Description = "Update an existing health insurance operator";
                    SetResponseDescription(operation, StatusCodes.Status200OK, "Success");
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, "Bad Request");
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, "Not Found");
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, "Internal Server Error");
                    break;

                case "DeleteHealthInsuranceOperator":
                    operation.Summary = "Delete health insurance operator";
                    operation.Description = "Delete an existing health insurance operator";
                    SetResponseDescription(operation, StatusCodes.Status200OK, "Success");
                    SetResponseDescription(operation, StatusCodes.Status400BadRequest, "Bad Request");
                    SetResponseDescription(operation, StatusCodes.Status404NotFound, "Not Found");
                    SetResponseDescription(operation, StatusCodes.Status500InternalServerError, "Internal Server Error");
                    break;

                default:
                    // Default handling for other operations
                    break;
            }
        }

        private static void SetResponseDescription(OpenApiOperation operation, int statusCode, string description)
        {
            var statusCodeStr = statusCode.ToString();
            if (operation.Responses.ContainsKey(statusCodeStr))
            {
                operation.Responses[statusCodeStr].Description = description;
            }
        }
    }
}