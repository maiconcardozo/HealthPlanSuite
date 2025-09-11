using Foundation.Base.Repository.Interface;
using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Repository.Interface
{
    /// <summary>
    /// Repository interface for Company data access operations.
    /// Extends base repository functionality with Company-specific methods.
    /// </summary>
    public interface ICompanyRepository : IEntityRepository<Company>
    {
        /// <summary>
        /// Finds a company by its CNPJ.
        /// </summary>
        /// <param name="cnpj">CNPJ to search for</param>
        /// <returns>Company if found, null otherwise</returns>
        Company? GetByCNPJ(string cnpj);
        
        /// <summary>
        /// Retrieves companies by name (partial match).
        /// </summary>
        /// <param name="name">Company name or part of name</param>
        /// <returns>Collection of companies matching the name criteria</returns>
        IEnumerable<Company> GetByName(string name);
        
        /// <summary>
        /// Retrieves companies by city.
        /// </summary>
        /// <param name="city">City name</param>
        /// <returns>Collection of companies in the specified city</returns>
        IEnumerable<Company> GetByCity(string city);
        
        /// <summary>
        /// Retrieves companies by state.
        /// </summary>
        /// <param name="state">State name</param>
        /// <returns>Collection of companies in the specified state</returns>
        IEnumerable<Company> GetByState(string state);
        
        /// <summary>
        /// Checks if a CNPJ already exists.
        /// </summary>
        /// <param name="cnpj">CNPJ to check</param>
        /// <returns>True if the CNPJ exists, false otherwise</returns>
        bool CNPJExists(string cnpj);
        
        /// <summary>
        /// Checks if a CNPJ exists for a different company (used during updates).
        /// </summary>
        /// <param name="cnpj">CNPJ to check</param>
        /// <param name="excludeId">Company ID to exclude from the check</param>
        /// <returns>True if the CNPJ exists for another company, false otherwise</returns>
        bool CNPJExistsForDifferentCompany(string cnpj, int excludeId);
    }
}