using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class UpdateAcceptanceRuleCommandHandler : IRequestHandler<UpdateAcceptanceRuleCommand, AcceptanceRuleResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public UpdateAcceptanceRuleCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<AcceptanceRuleResponseDTO?> Handle(UpdateAcceptanceRuleCommand request, CancellationToken cancellationToken)
        {
            var acceptanceRule = unitOfWork.AcceptanceRuleRepository.GetById(request.Id);

            if (acceptanceRule == null)
            {
                return Task.FromResult<AcceptanceRuleResponseDTO?>(null);
            }

            acceptanceRule.IdHealthPlan = request.HealthPlanId;
            acceptanceRule.RuleType = request.RuleType;
            acceptanceRule.Operator = request.Operator;
            acceptanceRule.MinValue = request.MinValue;
            acceptanceRule.MaxValue = request.MaxValue;
            acceptanceRule.ValuesList = request.ValuesList;
            acceptanceRule.Description = request.Description;
            acceptanceRule.RejectionMessage = request.RejectionMessage;
            acceptanceRule.IsMandatory = request.IsMandatory;
            acceptanceRule.UpdatedBy = request.UpdatedBy;
            acceptanceRule.DtUpdated = DateTime.UtcNow;

            unitOfWork.AcceptanceRuleRepository.Update(acceptanceRule);

            return Task.FromResult<AcceptanceRuleResponseDTO?>(CleanTemplateApplicationMapperInitializer.Mapper.Map<AcceptanceRuleResponseDTO>(acceptanceRule));
        }
    }
}
