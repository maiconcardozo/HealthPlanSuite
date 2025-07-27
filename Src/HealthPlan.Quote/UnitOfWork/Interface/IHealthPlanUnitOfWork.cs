using Foundation.Base.UnitOfWork.Interface;
using HealthPlan.Quote.Repository.HealthPlan.Interface;

namespace HealthPlan.Quote.UnitOfWork.Interface
{
    public interface IHealthPlanUnitOfWork : IUnitOfWork
    {
        IHealthInsuranceOperatorRepository HealthInsuranceOperatorRepository { get; }
        IHealthPlanRepository HealthPlanRepository { get; }
        IPlanTypeRepository PlanTypeRepository { get; }
        IAgeRangeRepository AgeRangeRepository { get; }
        IPriceTableRepository PriceTableRepository { get; }
        IPlanAdjustmentRepository PlanAdjustmentRepository { get; }
        IHealthEstablishmentRepository HealthEstablishmentRepository { get; }
        IPlanCoverageRepository PlanCoverageRepository { get; }
    }
}