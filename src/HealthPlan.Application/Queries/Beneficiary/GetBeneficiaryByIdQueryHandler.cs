using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Handler for retrieving a beneficiary by ID.
    /// </summary>
    public class GetBeneficiaryByIdQueryHandler : IRequestHandler<GetBeneficiaryByIdQuery, BeneficiaryResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetBeneficiaryByIdQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<BeneficiaryResponseDTO?> Handle(GetBeneficiaryByIdQuery request, CancellationToken cancellationToken)
        {
            var beneficiary = unitOfWork.BeneficiaryRepository.GetById(request.Id);

            if (beneficiary == null)
            {
                return Task.FromResult<BeneficiaryResponseDTO?>(null);
            }

            var beneficiaryDto = CleanTemplateApplicationMapperInitializer.Mapper.Map<BeneficiaryResponseDTO>(beneficiary);
            return Task.FromResult<BeneficiaryResponseDTO?>(beneficiaryDto);
        }
    }
}
