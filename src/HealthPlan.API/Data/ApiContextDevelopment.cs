using Microsoft.Extensions.Configuration;

namespace HealthPlan.API.Data
{
    public class ApiContextDevelopment : BaseApiContext
    {
        public ApiContextDevelopment(IConfiguration configuration) : base(configuration)
        {
        }
    }
}