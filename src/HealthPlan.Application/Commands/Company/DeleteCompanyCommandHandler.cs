using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for deleting companies.
    /// </summary>
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, bool>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public DeleteCompanyCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = unitOfWork.CompanyRepository.GetById(request.Id);

            if (company == null)
            {
                return Task.FromResult(false);
            }

            unitOfWork.CompanyRepository.Remove(company);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult(true);
        }
    }
}
