using HealthPlan.API.Resource;

namespace HealthPlan.API.Swagger
{
    /// <summary>
    /// Route constants for Beneficiary API endpoints.
    /// Uses resource files for localization and consistency.
    /// </summary>
    public static class BeneficiaryRoutes
    {
        /// <summary>
        /// Route for getting all beneficiaries.
        /// </summary>
        public const string GetBeneficiaries = "beneficiaries";
        
        /// <summary>
        /// Route for getting a beneficiary by ID.
        /// </summary>
        public const string GetBeneficiaryById = "{id}";
        
        /// <summary>
        /// Route for getting a beneficiary by CPF.
        /// </summary>
        public const string GetBeneficiaryByCPF = "cpf/{cpf}";
        
        /// <summary>
        /// Route for adding a new beneficiary.
        /// </summary>
        public const string AddBeneficiary = "";
        
        /// <summary>
        /// Route for updating an existing beneficiary.
        /// </summary>
        public const string UpdateBeneficiary = "";
        
        /// <summary>
        /// Route for deleting a beneficiary.
        /// </summary>
        public const string DeleteBeneficiary = "{id}";
    }
}