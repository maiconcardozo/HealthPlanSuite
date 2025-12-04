using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class DeleteAdhesionFeeCommandHandler : IRequestHandler<DeleteAdhesionFeeCommand, bool>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public DeleteAdhesionFeeCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteAdhesionFeeCommand request, CancellationToken cancellationToken)
        {
            var adhesionFee = unitOfWork.AdhesionFeeRepository.GetById(request.Id);

            if (adhesionFee == null)
            {
                return Task.FromResult(false);
            }

            unitOfWork.AdhesionFeeRepository.Remove(adhesionFee);

            return Task.FromResult(true);
        }
    }
}
