using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class DeletePlanCoverageCommandHandler : IRequestHandler<DeletePlanCoverageCommand, bool>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public DeletePlanCoverageCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeletePlanCoverageCommand request, CancellationToken cancellationToken)
        {
            var planCoverage = unitOfWork.PlanCoverageRepository.GetById(request.Id);

            if (planCoverage == null)
            {
                return Task.FromResult(false);
            }

            unitOfWork.PlanCoverageRepository.Remove(planCoverage);

            return Task.FromResult(true);
        }
    }
}
