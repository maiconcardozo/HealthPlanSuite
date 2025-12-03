using HealthPlan.Application.Constants;
using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Application.Services;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace HealthPlan.Application.Services
{
    /// <summary>
    /// Service implementation for Company management operations.
    /// Provides business logic and data access coordination for Company operations.
    /// </summary>
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        /// <summary>
        /// Initializes a new instance of the CompanyService.
        /// </summary>
        /// <param name="companyRepository">Repository for company data operations</param>
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        #region Query Operations

        /// <summary>
        /// Retrieves all companies from the system.
        /// </summary>
        /// <returns>Collection of all company entities</returns>
        public IEnumerable<Company> GetAllCompanies()
        {
            return _companyRepository.GetAll().Where(c => c.IsActive);
        }

        /// <summary>
        /// Finds a company by CNPJ.
        /// </summary>
        /// <param name="cnpj">The CNPJ to search for</param>
        /// <returns>Company if found, null otherwise</returns>
        public Company? GetCompanyByCNPJ(string cnpj)
        {
            return _companyRepository.GetByCNPJ(cnpj);
        }

        /// <summary>
        /// Retrieves a company by its unique identifier.
        /// </summary>
        /// <param name="id">Company ID</param>
        /// <returns>Company if found, null otherwise</returns>
        public Company? GetById(int id)
        {
            return _companyRepository.GetById(id);
        }

        /// <summary>
        /// Retrieves multiple companies by their IDs.
        /// </summary>
        /// <param name="companyIds">Collection of company IDs</param>
        /// <returns>Collection of matching company entities</returns>
        public IEnumerable<Company> GetCompaniesByIds(IEnumerable<int> companyIds)
        {
            // Use the NuGet package's GetByLstId method with an entity containing the IDs
            var company = new Company { LstId = companyIds };
            return _companyRepository.GetByLstId(company);
        }

        /// <summary>
        /// Retrieves companies by name (partial match).
        /// </summary>
        /// <param name="name">Company name or part of name</param>
        /// <returns>Collection of companies matching the name criteria</returns>
        public IEnumerable<Company> GetCompaniesByName(string name)
        {
            return _companyRepository.GetByName(name);
        }

        /// <summary>
        /// Retrieves companies by city.
        /// </summary>
        /// <param name="city">City name</param>
        /// <returns>Collection of companies in the specified city</returns>
        public IEnumerable<Company> GetCompaniesByCity(string city)
        {
            return _companyRepository.GetByCity(city);
        }

        /// <summary>
        /// Retrieves companies by state.
        /// </summary>
        /// <param name="state">State name</param>
        /// <returns>Collection of companies in the specified state</returns>
        public IEnumerable<Company> GetCompaniesByState(string state)
        {
            return _companyRepository.GetByState(state);
        }

        /// <summary>
        /// Retrieves companies that match the specified predicate condition.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter companies</param>
        /// <returns>Collection of matching company entities</returns>
        public IEnumerable<Company> GetCompanies(Expression<Func<Company, bool>> predicate)
        {
            return _companyRepository.Find(predicate);
        }

        /// <summary>
        /// Retrieves a single company that matches the predicate, or null if none found.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter companies</param>
        /// <returns>Single matching company or null</returns>
        /// <exception cref="InvalidOperationException">Thrown when multiple companies match the predicate</exception>
        public Company? GetSingleOrDefaultCompany(Expression<Func<Company, bool>> predicate)
        {
            return _companyRepository.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Retrieves all active companies.
        /// </summary>
        /// <returns>Collection of active companies</returns>
        public IEnumerable<Company> GetAllActiveCompanies()
        {
            return _companyRepository.GetAll().Where(c => c.IsActive);
        }

        #endregion

        #region Modification Operations

        /// <summary>
        /// Creates a new company in the system.
        /// Sets audit fields and validates business rules.
        /// </summary>
        /// <param name="company">Company to create</param>
        public void AddCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));

            if (string.IsNullOrWhiteSpace(company.Name))
                throw new ArgumentException("Company name is required", nameof(company));

            if (string.IsNullOrWhiteSpace(company.CNPJ))
                throw new ArgumentException("CNPJ is required", nameof(company));

            if (!IsValidCNPJ(company.CNPJ))
                throw new ArgumentException("Invalid CNPJ format", nameof(company));

            if (!IsCNPJUnique(company.CNPJ))
                throw new InvalidOperationException("CNPJ already exists");

            // Set audit fields
            company.DtCreated = DateTime.UtcNow;
            company.CreatedBy = string.IsNullOrEmpty(company.CreatedBy) 
                ? ApplicationConstants.DefaultCreatedByUser 
                : company.CreatedBy;

            _companyRepository.Add(company);
        }

        /// <summary>
        /// Creates multiple companies in a single transaction.
        /// </summary>
        /// <param name="companies">Collection of company entities to create</param>
        public void AddCompanies(IEnumerable<Company> companies)
        {
            if (companies == null)
                throw new ArgumentNullException(nameof(companies));

            var companyList = companies.ToList();
            if (!companyList.Any())
                return;

            foreach (var company in companyList)
            {
                if (string.IsNullOrWhiteSpace(company.Name))
                    throw new ArgumentException("Company name is required for all companies");

                if (string.IsNullOrWhiteSpace(company.CNPJ))
                    throw new ArgumentException("CNPJ is required for all companies");

                if (!IsValidCNPJ(company.CNPJ))
                    throw new ArgumentException($"Invalid CNPJ format: {company.CNPJ}");

                if (!IsCNPJUnique(company.CNPJ))
                    throw new InvalidOperationException($"CNPJ already exists: {company.CNPJ}");

                // Set audit fields
                company.DtCreated = DateTime.UtcNow;
                company.CreatedBy = string.IsNullOrEmpty(company.CreatedBy) 
                    ? ApplicationConstants.DefaultCreatedByUser 
                    : company.CreatedBy;
            }

            _companyRepository.AddRange(companyList);
        }

        /// <summary>
        /// Updates an existing company.
        /// </summary>
        /// <param name="company">Company with updated information</param>
        public void UpdateCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));

            if (company.Id <= 0)
                throw new ArgumentException("Valid company ID is required", nameof(company));

            if (string.IsNullOrWhiteSpace(company.Name))
                throw new ArgumentException("Company name is required", nameof(company));

            if (string.IsNullOrWhiteSpace(company.CNPJ))
                throw new ArgumentException("CNPJ is required", nameof(company));

            if (!IsValidCNPJ(company.CNPJ))
                throw new ArgumentException("Invalid CNPJ format", nameof(company));

            if (!IsCNPJUniqueForUpdate(company.CNPJ, company.Id))
                throw new InvalidOperationException("CNPJ already exists for another company");

            // Set audit fields
            company.DtUpdated = DateTime.UtcNow;
            company.UpdatedBy = string.IsNullOrEmpty(company.UpdatedBy) 
                ? ApplicationConstants.DefaultCreatedByUser 
                : company.UpdatedBy;

            _companyRepository.Update(company);
        }

        /// <summary>
        /// Deletes a company.
        /// </summary>
        /// <param name="company">Company to delete</param>
        public void DeleteCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));

            _companyRepository.Remove(company);
        }

        /// <summary>
        /// Deletes a company by its ID.
        /// </summary>
        /// <param name="id">Company ID to delete</param>
        public void DeleteCompany(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Valid company ID is required", nameof(id));

            var company = _companyRepository.GetById(id);
            if (company == null)
                throw new ArgumentException("Company not found", nameof(id));

            _companyRepository.Remove(company);
        }

        /// <summary>
        /// Deletes multiple company entities.
        /// </summary>
        /// <param name="companies">Collection of company entities to delete</param>
        public void DeleteCompanies(IEnumerable<Company> companies)
        {
            if (companies == null)
                throw new ArgumentNullException(nameof(companies));

            var companyList = companies.ToList();
            if (!companyList.Any())
                return;

            _companyRepository.RemoveRange(companyList);
        }

        #endregion

        #region Business Logic

        /// <summary>
        /// Validates if a CNPJ is unique.
        /// </summary>
        /// <param name="cnpj">CNPJ to validate</param>
        /// <returns>True if CNPJ is unique, false otherwise</returns>
        public bool IsCNPJUnique(string cnpj)
        {
            return !string.IsNullOrWhiteSpace(cnpj) && !_companyRepository.CNPJExists(cnpj);
        }

        /// <summary>
        /// Validates if a CNPJ is unique for updates (excludes current entity).
        /// </summary>
        /// <param name="cnpj">CNPJ to validate</param>
        /// <param name="excludeId">Company ID to exclude from validation</param>
        /// <returns>True if CNPJ is unique, false otherwise</returns>
        public bool IsCNPJUniqueForUpdate(string cnpj, int excludeId)
        {
            return !string.IsNullOrWhiteSpace(cnpj) && !_companyRepository.CNPJExistsForDifferentCompany(cnpj, excludeId);
        }

        /// <summary>
        /// Validates CNPJ format.
        /// Basic CNPJ validation - checks if it's 14 digits and follows basic format rules.
        /// </summary>
        /// <param name="cnpj">CNPJ to validate</param>
        /// <returns>True if CNPJ format is valid, false otherwise</returns>
        public bool IsValidCNPJ(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            // Remove formatting
            cnpj = Regex.Replace(cnpj, @"\D", "");

            // Must have exactly 14 digits
            if (cnpj.Length != 14)
                return false;

            // Check if all digits are the same (invalid CNPJ)
            if (cnpj.All(c => c == cnpj[0]))
                return false;

            // CNPJ check digit validation
            int[] multiplier1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int sum = 0;

            for (int i = 0; i < 12; i++)
            {
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];
            }

            int remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;

            string digit = remainder.ToString();
            tempCnpj = tempCnpj + digit;
            sum = 0;

            for (int i = 0; i < 13; i++)
            {
                sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];
            }

            remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;
            digit = digit + remainder.ToString();

            return cnpj.EndsWith(digit);
        }

        #endregion
    }
}