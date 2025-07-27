using Authentication.API.Resource;

namespace Authentication.API.Swagger
{
    public static class HealthInsuranceOperatorRoutes
    {
        public const string GetHealthInsuranceOperators = nameof(ResourceRoutesAPI.GetHealthInsuranceOperators);
        public const string GetHealthInsuranceOperatorById = nameof(ResourceRoutesAPI.GetHealthInsuranceOperatorById);
        public const string AddHealthInsuranceOperator = nameof(ResourceRoutesAPI.AddHealthInsuranceOperator);
        public const string UpdateHealthInsuranceOperator = nameof(ResourceRoutesAPI.UpdateHealthInsuranceOperator);
        public const string DeleteHealthInsuranceOperator = nameof(ResourceRoutesAPI.DeleteHealthInsuranceOperator);
    }
}