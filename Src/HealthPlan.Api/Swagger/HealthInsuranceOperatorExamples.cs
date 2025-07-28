using HealthPlan.Quote.DTO.HealthPlan;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace HealthPlan.API.Swagger
{
    public class HealthInsuranceOperatorResponseExample : IExamplesProvider<HealthInsuranceOperatorResponseDTO>
    {
        public HealthInsuranceOperatorResponseDTO GetExamples()
        {
            return new HealthInsuranceOperatorResponseDTO
            {
                Id = 1,
                Name = "Unimed Nacional",
                CNPJ = "12.345.678/0001-90",
                Website = "https://www.unimed.com.br",
                Phone = "(11) 1234-5678",
                CreatedAt = DateTime.UtcNow.AddDays(-30),
                UpdatedAt = DateTime.UtcNow.AddDays(-5)
            };
        }
    }

    public class HealthInsuranceOperatorPayLoadExample : IExamplesProvider<HealthInsuranceOperatorPayLoadDTO>
    {
        public HealthInsuranceOperatorPayLoadDTO GetExamples()
        {
            return new HealthInsuranceOperatorPayLoadDTO
            {
                Name = "Unimed Nacional",
                CNPJ = "12.345.678/0001-90",
                Website = "https://www.unimed.com.br",
                Phone = "(11) 1234-5678"
            };
        }
    }

    public class HealthInsuranceOperatorListResponseExample : IExamplesProvider<List<HealthInsuranceOperatorResponseDTO>>
    {
        public List<HealthInsuranceOperatorResponseDTO> GetExamples()
        {
            return new List<HealthInsuranceOperatorResponseDTO>
            {
                new HealthInsuranceOperatorResponseDTO
                {
                    Id = 1,
                    Name = "Unimed Nacional",
                    CNPJ = "12.345.678/0001-90",
                    Website = "https://www.unimed.com.br",
                    Phone = "(11) 1234-5678",
                    CreatedAt = DateTime.UtcNow.AddDays(-30),
                    UpdatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new HealthInsuranceOperatorResponseDTO
                {
                    Id = 2,
                    Name = "Bradesco Saúde",
                    CNPJ = "98.765.432/0001-01",
                    Website = "https://www.bradescosaude.com.br",
                    Phone = "(11) 9876-5432",
                    CreatedAt = DateTime.UtcNow.AddDays(-25),
                    UpdatedAt = DateTime.UtcNow.AddDays(-3)
                }
            };
        }
    }

    public class ProblemDetailsBadRequestExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples()
        {
            return new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Bad Request",
                Status = 400,
                Detail = "The request is invalid."
            };
        }
    }

    public class ProblemDetailsNotFoundExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples()
        {
            return new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",  
                Title = "Not Found",
                Status = 404,
                Detail = "The requested resource was not found."
            };
        }
    }

    public class ProblemDetailsUnauthorizedExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples()
        {
            return new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Unauthorized",
                Status = 401,
                Detail = "Authentication is required to access this resource."
            };
        }
    }

    public class ProblemDetailsInternalServerErrorExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples()
        {
            return new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "Internal Server Error", 
                Status = 500,
                Detail = "An error occurred while processing your request."
            };
        }
    }

    public class SuccessDetailsExample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new { success = true, message = "Operation completed successfully" };
        }
    }
}