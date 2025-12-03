using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for deleting age ranges.
    /// </summary>
    public class DeleteAgeRangeCommandHandler : IRequestHandler<DeleteAgeRangeCommand, bool>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public DeleteAgeRangeCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteAgeRangeCommand request, CancellationToken cancellationToken)
        {
            var ageRange = unitOfWork.AgeRangeRepository.GetById(request.Id);

            if (ageRange == null)
            {
                return Task.FromResult(false);
            }

            unitOfWork.AgeRangeRepository.Remove(ageRange);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult(true);
        }
    }
}
