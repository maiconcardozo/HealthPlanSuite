using HealthPlan.Quote.DTO.HealthPlan;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Authentication.API.Swagger
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
}