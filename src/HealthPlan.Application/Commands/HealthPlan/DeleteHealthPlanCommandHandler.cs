using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for deleting health plans.
    /// </summary>
    public class DeleteHealthPlanCommandHandler : IRequestHandler<DeleteHealthPlanCommand, bool>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public DeleteHealthPlanCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteHealthPlanCommand request, CancellationToken cancellationToken)
        {
            var healthPlan = unitOfWork.HealthPlanRepository.GetById(request.Id);

            if (healthPlan == null)
            {
                return Task.FromResult(false);
            }

            unitOfWork.HealthPlanRepository.Remove(healthPlan);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult(true);
        }
    }
}
