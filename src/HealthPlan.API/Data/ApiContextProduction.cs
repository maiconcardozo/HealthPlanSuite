using Microsoft.Extensions.Configuration;

namespace HealthPlan.API.Data
{
    public class ApiContextProduction : BaseApiContext
    {
        public ApiContextProduction(IConfiguration configuration) : base(configuration)
        {
        }
    }
}