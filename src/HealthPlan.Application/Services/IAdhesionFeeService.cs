using HealthPlan.Domain.Entities;

namespace HealthPlan.Application.Services
{
    /// <summary>
    /// Service interface for AdhesionFee business operations.
    /// Provides business logic layer for AdhesionFee management.
    /// </summary>
    public interface IAdhesionFeeService
    {
        IEnumerable<AdhesionFee> GetAllActiveAdhesionFees();
        AdhesionFee? GetById(int id);
        void AddAdhesionFee(AdhesionFee adhesionFee);
        void UpdateAdhesionFee(AdhesionFee adhesionFee);
        void DeleteAdhesionFee(int id);
    }
}