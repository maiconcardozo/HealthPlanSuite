using HealthPlan.API.Resource;

namespace HealthPlan.API.Swagger
{
    /// <summary>
    /// Route constants for Company API endpoints.
    /// Uses resource files for localization and consistency.
    /// </summary>
    public static class CompanyRoutes
    {
        /// <summary>
        /// Route for getting all companies.
        /// </summary>
        public const string GetCompanies = "companies";
        
        /// <summary>
        /// Route for getting a company by ID.
        /// </summary>
        public const string GetCompanyById = "{id}";
        
        /// <summary>
        /// Route for getting a company by CNPJ.
        /// </summary>
        public const string GetCompanyByCNPJ = "cnpj/{cnpj}";
        
        /// <summary>
        /// Route for adding a new company.
        /// </summary>
        public const string AddCompany = "";
        
        /// <summary>
        /// Route for updating an existing company.
        /// </summary>
        public const string UpdateCompany = "";
        
        /// <summary>
        /// Route for deleting a company.
        /// </summary>
        public const string DeleteCompany = "{id}";
    }
}