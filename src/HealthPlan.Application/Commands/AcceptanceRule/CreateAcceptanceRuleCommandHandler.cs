using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class CreateAcceptanceRuleCommandHandler : IRequestHandler<CreateAcceptanceRuleCommand, AcceptanceRuleResponseDTO>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public CreateAcceptanceRuleCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<AcceptanceRuleResponseDTO> Handle(CreateAcceptanceRuleCommand request, CancellationToken cancellationToken)
        {
            var acceptanceRule = new AcceptanceRule
            {
                IdHealthPlan = request.HealthPlanId,
                RuleType = request.RuleType,
                Operator = request.Operator,
                MinValue = request.MinValue,
                MaxValue = request.MaxValue,
                ValuesList = request.ValuesList,
                Description = request.Description,
                RejectionMessage = request.RejectionMessage,
                IsMandatory = request.IsMandatory,
                CreatedBy = request.CreatedBy,
                DtCreated = DateTime.UtcNow,
            };

            unitOfWork.AcceptanceRuleRepository.Add(acceptanceRule);

            return Task.FromResult(CleanTemplateApplicationMapperInitializer.Mapper.Map<AcceptanceRuleResponseDTO>(acceptanceRule));
        }
    }
}
