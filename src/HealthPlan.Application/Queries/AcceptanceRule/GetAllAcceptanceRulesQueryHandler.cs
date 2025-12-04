using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAllAcceptanceRulesQueryHandler : IRequestHandler<GetAllAcceptanceRulesQuery, IEnumerable<AcceptanceRuleResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAllAcceptanceRulesQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<AcceptanceRuleResponseDTO>> Handle(GetAllAcceptanceRulesQuery request, CancellationToken cancellationToken)
        {
            var acceptanceRules = unitOfWork.AcceptanceRuleRepository.GetAll().Where(ar => ar.IsActive);
            var acceptanceRuleDtos = acceptanceRules.Select(ar => CleanTemplateApplicationMapperInitializer.Mapper.Map<AcceptanceRuleResponseDTO>(ar));

            return Task.FromResult(acceptanceRuleDtos);
        }
    }
}
