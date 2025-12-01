using Microsoft.AspNetCore.Mvc;

namespace HealthPlan.API.Swagger
{
    public class SucessDetails : ProblemDetails
    {
        public object? Data { get; set; }
    }
}
