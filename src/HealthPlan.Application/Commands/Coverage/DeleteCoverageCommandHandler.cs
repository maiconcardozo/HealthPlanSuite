using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for deleting coverages.
    /// </summary>
    public class DeleteCoverageCommandHandler : IRequestHandler<DeleteCoverageCommand, bool>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public DeleteCoverageCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteCoverageCommand request, CancellationToken cancellationToken)
        {
            var coverage = unitOfWork.CoverageRepository.GetById(request.Id);

            if (coverage == null)
            {
                return Task.FromResult(false);
            }

            unitOfWork.CoverageRepository.Remove(coverage);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult(true);
        }
    }
}
