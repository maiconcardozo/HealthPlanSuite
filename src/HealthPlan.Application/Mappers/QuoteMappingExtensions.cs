using HealthPlan.Application.DTOs;
using HealthPlan.Domain.Entities;

namespace HealthPlan.Application.Mappers
{
    /// <summary>
    /// Extension methods for Quote entity and DTO mapping.
    /// </summary>
    public static class QuoteMappingExtensions
    {
        /// <summary>
        /// Converts a QuotePayLoadDTO to a Quote entity.
        /// </summary>
        /// <param name="dto">The QuotePayLoadDTO to convert</param>
        /// <returns>Quote entity</returns>
        public static Domain.Entities.Quote ToEntity(this QuotePayLoadDTO dto)
        {
            return new Domain.Entities.Quote
            {
                IdCompany = dto.IdCompany,
                IdBeneficiary = dto.IdBeneficiary,
                IdHealthPlan = dto.IdHealthPlan,
                ValidUntil = dto.ValidUntil,
                MonthlyPremium = dto.MonthlyPremium,
                IdAgeRange = dto.IdAgeRange,
                Notes = dto.Notes,
                CreatedBy = dto.CreatedBy,
                UpdatedBy = dto.UpdatedBy,
                QuoteDate = DateTime.UtcNow,
                Status = "Pending",
                IsActive = true,
                DtCreated = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Converts a Quote entity to a QuoteResponseDTO.
        /// </summary>
        /// <param name="entity">The Quote entity to convert</param>
        /// <returns>QuoteResponseDTO</returns>
        public static QuoteResponseDTO ToResponseDTO(this Domain.Entities.Quote entity)
        {
            return new QuoteResponseDTO
            {
                IdQuote = entity.Id,
                IdCompany = entity.IdCompany,
                IdBeneficiary = entity.IdBeneficiary,
                IdHealthPlan = entity.IdHealthPlan,
                IdAgeRange = entity.IdAgeRange,
                QuoteNumber = entity.QuoteNumber,
                QuoteDate = entity.QuoteDate,
                ValidUntil = entity.ValidUntil,
                MonthlyPremium = entity.MonthlyPremium,
                Status = entity.Status,
                Notes = entity.Notes,
                DtCreated = entity.DtCreated,
                DtDeleted = entity.DtDeleted,
                DtUpdated = entity.DtUpdated,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                DeletedBy = entity.DeletedBy
            };
        }

        /// <summary>
        /// Converts a Quote entity to a QuotePayLoadDTO.
        /// </summary>
        /// <param name="entity">The Quote entity to convert</param>
        /// <returns>QuotePayLoadDTO</returns>
        public static QuotePayLoadDTO ToPayLoadDTO(this Domain.Entities.Quote entity)
        {
            return new QuotePayLoadDTO
            {
                CompanyId = entity.CompanyId,
                BeneficiaryId = entity.BeneficiaryId,
                HealthPlanId = entity.HealthPlanId,
                ValidUntil = entity.ValidUntil,
                MonthlyPremium = entity.MonthlyPremium,
                AgeRangeId = entity.AgeRangeId,
                Notes = entity.Notes,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy
            };
        }
    }
}