using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAcceptanceRuleByIdQueryHandler : IRequestHandler<GetAcceptanceRuleByIdQuery, AcceptanceRuleResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAcceptanceRuleByIdQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<AcceptanceRuleResponseDTO?> Handle(GetAcceptanceRuleByIdQuery request, CancellationToken cancellationToken)
        {
            var acceptanceRule = unitOfWork.AcceptanceRuleRepository.GetById(request.Id);

            if (acceptanceRule == null)
            {
                return Task.FromResult<AcceptanceRuleResponseDTO?>(null);
            }

            var acceptanceRuleDto = CleanTemplateApplicationMapperInitializer.Mapper.Map<AcceptanceRuleResponseDTO>(acceptanceRule);
            return Task.FromResult<AcceptanceRuleResponseDTO?>(acceptanceRuleDto);
        }
    }
}
