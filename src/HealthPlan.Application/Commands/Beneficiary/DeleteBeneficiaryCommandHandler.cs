using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for deleting beneficiaries.
    /// </summary>
    public class DeleteBeneficiaryCommandHandler : IRequestHandler<DeleteBeneficiaryCommand, bool>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public DeleteBeneficiaryCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteBeneficiaryCommand request, CancellationToken cancellationToken)
        {
            var beneficiary = unitOfWork.BeneficiaryRepository.GetById(request.Id);

            if (beneficiary == null)
            {
                return Task.FromResult(false);
            }

            unitOfWork.BeneficiaryRepository.Remove(beneficiary);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult(true);
        }
    }
}
