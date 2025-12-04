using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class DeleteAcceptanceRuleCommandHandler : IRequestHandler<DeleteAcceptanceRuleCommand, bool>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public DeleteAcceptanceRuleCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteAcceptanceRuleCommand request, CancellationToken cancellationToken)
        {
            var acceptanceRule = unitOfWork.AcceptanceRuleRepository.GetById(request.Id);

            if (acceptanceRule == null)
            {
                return Task.FromResult(false);
            }

            unitOfWork.AcceptanceRuleRepository.Remove(acceptanceRule);

            return Task.FromResult(true);
        }
    }
}
