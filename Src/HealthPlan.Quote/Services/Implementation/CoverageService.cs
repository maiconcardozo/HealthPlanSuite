using HealthPlan.Quote.Constants;
using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Services.Interface;
using System.Linq.Expressions;

namespace HealthPlan.Quote.Services.Implementation
{
    /// <summary>
    /// Service implementation for Coverage management operations.
    /// Provides business logic and data access coordination for Coverage operations.
    /// </summary>
    public class CoverageService : ICoverageService
    {
        private readonly ICoverageRepository _coverageRepository;
        private readonly HashSet<string> _validCoverageTypes = new HashSet<string>
        {
            "Ambulatorial",
            "Hospitalar", 
            "Obstétrico",
            "Odontológico"
        };

        /// <summary>
        /// Initializes a new instance of the CoverageService.
        /// </summary>
        /// <param name="coverageRepository">Repository for coverage data operations</param>
        public CoverageService(ICoverageRepository coverageRepository)
        {
            _coverageRepository = coverageRepository;
        }

        #region Query Operations

        /// <summary>
        /// Retrieves all coverages from the system.
        /// </summary>
        /// <returns>Collection of all coverage entities</returns>
        public IEnumerable<Coverage> GetAllCoverages()
        {
            return _coverageRepository.GetAll().Where(c => c.IsActive);
        }

        /// <summary>
        /// Retrieves a coverage by its unique identifier.
        /// </summary>
        /// <param name="id">Coverage ID</param>
        /// <returns>Coverage if found, null otherwise</returns>
        public Coverage? GetById(int id)
        {
            return _coverageRepository.GetById(id);
        }

        /// <summary>
        /// Retrieves multiple coverages by their IDs.
        /// </summary>
        /// <param name="coverageIds">Collection of coverage IDs</param>
        /// <returns>Collection of matching coverage entities</returns>
        public IEnumerable<Coverage> GetCoveragesByIds(IEnumerable<int> coverageIds)
        {
            // Use the NuGet package's GetByLstId method with an entity containing the IDs
            var coverage = new Coverage { LstId = coverageIds };
            return _coverageRepository.GetByLstId(coverage);
        }

        /// <summary>
        /// Retrieves coverages by name (partial match).
        /// </summary>
        /// <param name="name">Coverage name or part of name</param>
        /// <returns>Collection of coverages matching the name criteria</returns>
        public IEnumerable<Coverage> GetCoveragesByName(string name)
        {
            return _coverageRepository.GetByName(name);
        }

        /// <summary>
        /// Retrieves coverages by type.
        /// </summary>
        /// <param name="coverageType">Coverage type</param>
        /// <returns>Collection of coverages of the specified type</returns>
        public IEnumerable<Coverage> GetCoveragesByType(string coverageType)
        {
            return _coverageRepository.GetByCoverageType(coverageType);
        }

        /// <summary>
        /// Retrieves all available coverage types.
        /// </summary>
        /// <returns>Collection of distinct coverage types</returns>
        public IEnumerable<string> GetAllCoverageTypes()
        {
            return _coverageRepository.GetAllCoverageTypes();
        }

        /// <summary>
        /// Retrieves coverages that match the specified predicate condition.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter coverages</param>
        /// <returns>Collection of matching coverage entities</returns>
        public IEnumerable<Coverage> GetCoverages(Expression<Func<Coverage, bool>> predicate)
        {
            return _coverageRepository.Find(predicate);
        }

        /// <summary>
        /// Retrieves a single coverage that matches the predicate, or null if none found.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter coverages</param>
        /// <returns>Single matching coverage or null</returns>
        /// <exception cref="InvalidOperationException">Thrown when multiple coverages match the predicate</exception>
        public Coverage? GetSingleOrDefaultCoverage(Expression<Func<Coverage, bool>> predicate)
        {
            return _coverageRepository.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Retrieves all active coverages.
        /// </summary>
        /// <returns>Collection of active coverages</returns>
        public IEnumerable<Coverage> GetAllActiveCoverages()
        {
            return _coverageRepository.GetAll().Where(c => c.IsActive);
        }

        #endregion

        #region Modification Operations

        /// <summary>
        /// Creates a new coverage in the system.
        /// Sets audit fields and validates business rules.
        /// </summary>
        /// <param name="coverage">Coverage to create</param>
        public void AddCoverage(Coverage coverage)
        {
            if (coverage == null)
                throw new ArgumentNullException(nameof(coverage));

            if (string.IsNullOrWhiteSpace(coverage.Name))
                throw new ArgumentException("Coverage name is required", nameof(coverage));

            if (string.IsNullOrWhiteSpace(coverage.CoverageType))
                throw new ArgumentException("Coverage type is required", nameof(coverage));

            if (!IsValidCoverageType(coverage.CoverageType))
                throw new ArgumentException($"Invalid coverage type: {coverage.CoverageType}. Valid types are: {string.Join(", ", _validCoverageTypes)}", nameof(coverage));

            if (!IsNameUnique(coverage.Name))
                throw new InvalidOperationException("Coverage name already exists");

            // Set audit fields
            coverage.DtCreated = DateTime.UtcNow;
            coverage.CreatedBy = string.IsNullOrEmpty(coverage.CreatedBy) 
                ? ApplicationConstants.DefaultCreatedByUser 
                : coverage.CreatedBy;

            _coverageRepository.Add(coverage);
        }

        /// <summary>
        /// Creates multiple coverages in a single transaction.
        /// </summary>
        /// <param name="coverages">Collection of coverage entities to create</param>
        public void AddCoverages(IEnumerable<Coverage> coverages)
        {
            if (coverages == null)
                throw new ArgumentNullException(nameof(coverages));

            var coverageList = coverages.ToList();
            if (!coverageList.Any())
                return;

            foreach (var coverage in coverageList)
            {
                if (string.IsNullOrWhiteSpace(coverage.Name))
                    throw new ArgumentException("Coverage name is required for all coverages");

                if (string.IsNullOrWhiteSpace(coverage.CoverageType))
                    throw new ArgumentException("Coverage type is required for all coverages");

                if (!IsValidCoverageType(coverage.CoverageType))
                    throw new ArgumentException($"Invalid coverage type: {coverage.CoverageType}");

                if (!IsNameUnique(coverage.Name))
                    throw new InvalidOperationException($"Coverage name already exists: {coverage.Name}");

                // Set audit fields
                coverage.DtCreated = DateTime.UtcNow;
                coverage.CreatedBy = string.IsNullOrEmpty(coverage.CreatedBy) 
                    ? ApplicationConstants.DefaultCreatedByUser 
                    : coverage.CreatedBy;
            }

            _coverageRepository.AddRange(coverageList);
        }

        /// <summary>
        /// Updates an existing coverage.
        /// </summary>
        /// <param name="coverage">Coverage with updated information</param>
        public void UpdateCoverage(Coverage coverage)
        {
            if (coverage == null)
                throw new ArgumentNullException(nameof(coverage));

            if (coverage.Id <= 0)
                throw new ArgumentException("Valid coverage ID is required", nameof(coverage));

            if (string.IsNullOrWhiteSpace(coverage.Name))
                throw new ArgumentException("Coverage name is required", nameof(coverage));

            if (string.IsNullOrWhiteSpace(coverage.CoverageType))
                throw new ArgumentException("Coverage type is required", nameof(coverage));

            if (!IsValidCoverageType(coverage.CoverageType))
                throw new ArgumentException($"Invalid coverage type: {coverage.CoverageType}. Valid types are: {string.Join(", ", _validCoverageTypes)}", nameof(coverage));

            if (!IsNameUniqueForUpdate(coverage.Name, coverage.Id))
                throw new InvalidOperationException("Coverage name already exists for another coverage");

            // Set audit fields
            coverage.DtUpdated = DateTime.UtcNow;
            coverage.UpdatedBy = string.IsNullOrEmpty(coverage.UpdatedBy) 
                ? ApplicationConstants.DefaultCreatedByUser 
                : coverage.UpdatedBy;

            _coverageRepository.Update(coverage);
        }

        /// <summary>
        /// Deletes a coverage.
        /// </summary>
        /// <param name="coverage">Coverage to delete</param>
        public void DeleteCoverage(Coverage coverage)
        {
            if (coverage == null)
                throw new ArgumentNullException(nameof(coverage));

            _coverageRepository.Remove(coverage);
        }

        /// <summary>
        /// Deletes a coverage by its ID.
        /// </summary>
        /// <param name="id">Coverage ID to delete</param>
        public void DeleteCoverage(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Valid coverage ID is required", nameof(id));

            var coverage = _coverageRepository.GetById(id);
            if (coverage == null)
                throw new ArgumentException("Coverage not found", nameof(id));

            _coverageRepository.Remove(coverage);
        }

        /// <summary>
        /// Deletes multiple coverage entities.
        /// </summary>
        /// <param name="coverages">Collection of coverage entities to delete</param>
        public void DeleteCoverages(IEnumerable<Coverage> coverages)
        {
            if (coverages == null)
                throw new ArgumentNullException(nameof(coverages));

            var coverageList = coverages.ToList();
            if (!coverageList.Any())
                return;

            _coverageRepository.RemoveRange(coverageList);
        }

        #endregion

        #region Business Logic

        /// <summary>
        /// Validates if a coverage name is unique.
        /// </summary>
        /// <param name="name">Coverage name to validate</param>
        /// <returns>True if name is unique, false otherwise</returns>
        public bool IsNameUnique(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !_coverageRepository.NameExists(name);
        }

        /// <summary>
        /// Validates if a coverage name is unique for updates (excludes current entity).
        /// </summary>
        /// <param name="name">Coverage name to validate</param>
        /// <param name="excludeId">Coverage ID to exclude from validation</param>
        /// <returns>True if name is unique, false otherwise</returns>
        public bool IsNameUniqueForUpdate(string name, int excludeId)
        {
            return !string.IsNullOrWhiteSpace(name) && !_coverageRepository.NameExistsForDifferentCoverage(name, excludeId);
        }

        /// <summary>
        /// Validates coverage type.
        /// </summary>
        /// <param name="coverageType">Coverage type to validate</param>
        /// <returns>True if coverage type is valid, false otherwise</returns>
        public bool IsValidCoverageType(string coverageType)
        {
            return !string.IsNullOrWhiteSpace(coverageType) && _validCoverageTypes.Contains(coverageType);
        }

        #endregion
    }
}