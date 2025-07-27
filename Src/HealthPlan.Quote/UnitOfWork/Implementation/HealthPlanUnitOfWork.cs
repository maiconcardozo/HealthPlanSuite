using Foundation.Base.UnitOfWork.Implementation;
using HealthPlan.Quote.Repository.HealthPlan.Interface;
using HealthPlan.Quote.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using UnitOfWorkBase = Foundation.Base.UnitOfWork.Implementation.UnitOfWork;

namespace HealthPlan.Quote.UnitOfWork.Implementation
{
    public class HealthPlanUnitOfWork : UnitOfWorkBase, IHealthPlanUnitOfWork
    {
        public IHealthInsuranceOperatorRepository HealthInsuranceOperatorRepository { get; }
        public IHealthPlanRepository HealthPlanRepository { get; }
        public IPlanTypeRepository PlanTypeRepository { get; }
        public IAgeRangeRepository AgeRangeRepository { get; }
        public IPriceTableRepository PriceTableRepository { get; }
        public IPlanAdjustmentRepository PlanAdjustmentRepository { get; }
        public IHealthEstablishmentRepository HealthEstablishmentRepository { get; }
        public IPlanCoverageRepository PlanCoverageRepository { get; }

        public HealthPlanUnitOfWork(
            DbContext context,
            IHealthInsuranceOperatorRepository healthInsuranceOperatorRepository,
            IHealthPlanRepository healthPlanRepository,
            IPlanTypeRepository planTypeRepository,
            IAgeRangeRepository ageRangeRepository,
            IPriceTableRepository priceTableRepository,
            IPlanAdjustmentRepository planAdjustmentRepository,
            IHealthEstablishmentRepository healthEstablishmentRepository,
            IPlanCoverageRepository planCoverageRepository
        ) : base(context)
        {
            HealthInsuranceOperatorRepository = healthInsuranceOperatorRepository;
            HealthPlanRepository = healthPlanRepository;
            PlanTypeRepository = planTypeRepository;
            AgeRangeRepository = ageRangeRepository;
            PriceTableRepository = priceTableRepository;
            PlanAdjustmentRepository = planAdjustmentRepository;
            HealthEstablishmentRepository = healthEstablishmentRepository;
            PlanCoverageRepository = planCoverageRepository;
        }
    }
}