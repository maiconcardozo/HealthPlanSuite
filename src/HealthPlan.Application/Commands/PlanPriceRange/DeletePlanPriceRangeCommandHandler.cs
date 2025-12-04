using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class DeletePlanPriceRangeCommandHandler : IRequestHandler<DeletePlanPriceRangeCommand, bool>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public DeletePlanPriceRangeCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeletePlanPriceRangeCommand request, CancellationToken cancellationToken)
        {
            var planPriceRange = unitOfWork.PlanPriceRangeRepository.GetById(request.Id);

            if (planPriceRange == null)
            {
                return Task.FromResult(false);
            }

            unitOfWork.PlanPriceRangeRepository.Remove(planPriceRange);

            return Task.FromResult(true);
        }
    }
}
