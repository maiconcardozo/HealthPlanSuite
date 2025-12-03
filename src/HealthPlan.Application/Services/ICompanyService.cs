using System.Linq.Expressions;
using HealthPlan.Domain.Entities;

namespace HealthPlan.Application.Services
{
    /// <summary>
    /// Service interface for Company management operations.
    /// Provides comprehensive Company CRUD operations following service layer patterns.
    /// </summary>
    public interface ICompanyService
    {
        #region Query Operations
        
        /// <summary>
        /// Retrieves all companies from the system.
        /// </summary>
        /// <returns>Collection of all company entities</returns>
        IEnumerable<Company> GetAllCompanies();
        
        /// <summary>
        /// Finds a company by CNPJ.
        /// </summary>
        /// <param name="cnpj">The CNPJ to search for</param>
        /// <returns>Company if found, null otherwise</returns>
        Company? GetCompanyByCNPJ(string cnpj);
        
        /// <summary>
        /// Retrieves a company by its unique identifier.
        /// </summary>
        /// <param name="id">Company ID</param>
        /// <returns>Company if found, null otherwise</returns>
        Company? GetById(int id);
        
        /// <summary>
        /// Retrieves multiple companies by their IDs.
        /// </summary>
        /// <param name="companyIds">Collection of company IDs</param>
        /// <returns>Collection of matching company entities</returns>
        IEnumerable<Company> GetCompaniesByIds(IEnumerable<int> companyIds);
        
        /// <summary>
        /// Retrieves companies by name (partial match).
        /// </summary>
        /// <param name="name">Company name or part of name</param>
        /// <returns>Collection of companies matching the name criteria</returns>
        IEnumerable<Company> GetCompaniesByName(string name);
        
        /// <summary>
        /// Retrieves companies by city.
        /// </summary>
        /// <param name="city">City name</param>
        /// <returns>Collection of companies in the specified city</returns>
        IEnumerable<Company> GetCompaniesByCity(string city);
        
        /// <summary>
        /// Retrieves companies by state.
        /// </summary>
        /// <param name="state">State name</param>
        /// <returns>Collection of companies in the specified state</returns>
        IEnumerable<Company> GetCompaniesByState(string state);
        
        /// <summary>
        /// Retrieves companies that match the specified predicate condition.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter companies</param>
        /// <returns>Collection of matching company entities</returns>
        IEnumerable<Company> GetCompanies(Expression<Func<Company, bool>> predicate);
        
        /// <summary>
        /// Retrieves a single company that matches the predicate, or null if none found.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter companies</param>
        /// <returns>Single matching company or null</returns>
        /// <exception cref="InvalidOperationException">Thrown when multiple companies match the predicate</exception>
        Company? GetSingleOrDefaultCompany(Expression<Func<Company, bool>> predicate);
        
        /// <summary>
        /// Retrieves all active companies.
        /// </summary>
        /// <returns>Collection of active companies</returns>
        IEnumerable<Company> GetAllActiveCompanies();
        
        #endregion
        
        #region Modification Operations
        
        /// <summary>
        /// Creates a new company in the system.
        /// Sets audit fields and validates business rules.
        /// </summary>
        /// <param name="company">Company to create</param>
        void AddCompany(Company company);
        
        /// <summary>
        /// Creates multiple companies in a single transaction.
        /// </summary>
        /// <param name="companies">Collection of company entities to create</param>
        void AddCompanies(IEnumerable<Company> companies);
        
        /// <summary>
        /// Updates an existing company.
        /// </summary>
        /// <param name="company">Company with updated information</param>
        void UpdateCompany(Company company);
        
        /// <summary>
        /// Deletes a company.
        /// </summary>
        /// <param name="company">Company to delete</param>
        void DeleteCompany(Company company);
        
        /// <summary>
        /// Deletes a company by its ID.
        /// </summary>
        /// <param name="id">Company ID to delete</param>
        void DeleteCompany(int id);
        
        /// <summary>
        /// Deletes multiple company entities.
        /// </summary>
        /// <param name="companies">Collection of company entities to delete</param>
        void DeleteCompanies(IEnumerable<Company> companies);
        
        #endregion
        
        #region Business Logic
        
        /// <summary>
        /// Validates if a CNPJ is unique.
        /// </summary>
        /// <param name="cnpj">CNPJ to validate</param>
        /// <returns>True if CNPJ is unique, false otherwise</returns>
        bool IsCNPJUnique(string cnpj);
        
        /// <summary>
        /// Validates if a CNPJ is unique for updates (excludes current entity).
        /// </summary>
        /// <param name="cnpj">CNPJ to validate</param>
        /// <param name="excludeId">Company ID to exclude from validation</param>
        /// <returns>True if CNPJ is unique, false otherwise</returns>
        bool IsCNPJUniqueForUpdate(string cnpj, int excludeId);
        
        /// <summary>
        /// Validates CNPJ format.
        /// </summary>
        /// <param name="cnpj">CNPJ to validate</param>
        /// <returns>True if CNPJ format is valid, false otherwise</returns>
        bool IsValidCNPJ(string cnpj);
        
        #endregion
    }
}