using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Infrastructure.Interface;
using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Repository.Interface;

namespace HealthPlan.Quote.Repository.Implementation
{
    /// <summary>
    /// Repository implementation for Company management operations.
    /// Provides concrete data access methods for Company following the repository pattern.
    /// </summary>
    public class CompanyRepository : EntityRepository<Company>, ICompanyRepository
    {
        /// <summary>
        /// Initializes a new instance of the CompanyRepository.
        /// </summary>
        /// <param name="context">Database context for data operations</param>
        public CompanyRepository(IApplicationContext context) : base(context)
        {
        }

        /// <summary>
        /// Finds a company by its CNPJ.
        /// </summary>
        /// <param name="cnpj">CNPJ to search for</param>
        /// <returns>Company if found, null otherwise</returns>
        public Company? GetByCNPJ(string cnpj)
        {
            return _context.Set<Company>().FirstOrDefault(c => c.CNPJ == cnpj);
        }

        /// <summary>
        /// Retrieves companies by name (partial match).
        /// </summary>
        /// <param name="name">Company name or part of name</param>
        /// <returns>Collection of companies matching the name criteria</returns>
        public IEnumerable<Company> GetByName(string name)
        {
            return _context.Set<Company>()
                .Where(c => c.Name.Contains(name) || (c.TradeName != null && c.TradeName.Contains(name)))
                .OrderBy(c => c.Name)
                .ToList();
        }

        /// <summary>
        /// Retrieves companies by city.
        /// </summary>
        /// <param name="city">City name</param>
        /// <returns>Collection of companies in the specified city</returns>
        public IEnumerable<Company> GetByCity(string city)
        {
            return _context.Set<Company>()
                .Where(c => c.City == city)
                .OrderBy(c => c.Name)
                .ToList();
        }

        /// <summary>
        /// Retrieves companies by state.
        /// </summary>
        /// <param name="state">State name</param>
        /// <returns>Collection of companies in the specified state</returns>
        public IEnumerable<Company> GetByState(string state)
        {
            return _context.Set<Company>()
                .Where(c => c.State == state)
                .OrderBy(c => c.Name)
                .ToList();
        }

        /// <summary>
        /// Checks if a CNPJ already exists.
        /// </summary>
        /// <param name="cnpj">CNPJ to check</param>
        /// <returns>True if the CNPJ exists, false otherwise</returns>
        public bool CNPJExists(string cnpj)
        {
            return _context.Set<Company>().Any(c => c.CNPJ == cnpj);
        }

        /// <summary>
        /// Checks if a CNPJ exists for a different company (used during updates).
        /// </summary>
        /// <param name="cnpj">CNPJ to check</param>
        /// <param name="excludeId">Company ID to exclude from the check</param>
        /// <returns>True if the CNPJ exists for another company, false otherwise</returns>
        public bool CNPJExistsForDifferentCompany(string cnpj, int excludeId)
        {
            return _context.Set<Company>().Any(c => c.CNPJ == cnpj && c.Id != excludeId);
        }

        /// <summary>
        /// Gets companies by a list of IDs.
        /// </summary>
        /// <param name="company">Company entity containing list of IDs</param>
        /// <returns>Collection of companies matching the provided IDs</returns>
        public IEnumerable<Company> GetByLstId(Company company)
        {
            return company.LstId != null && company.LstId.Any()
                ? _context.Set<Company>()
                .Where(c => company.LstId.Contains(c.Id))
                .OrderBy(c => c.Name)
                .ToList()
                : new List<Company>();
        }
    }
}
