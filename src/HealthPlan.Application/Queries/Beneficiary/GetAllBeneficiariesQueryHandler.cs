using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Handler for retrieving all beneficiaries.
    /// </summary>
    public class GetAllBeneficiariesQueryHandler : IRequestHandler<GetAllBeneficiariesQuery, IEnumerable<BeneficiaryResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAllBeneficiariesQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<BeneficiaryResponseDTO>> Handle(GetAllBeneficiariesQuery request, CancellationToken cancellationToken)
        {
            var beneficiaries = unitOfWork.BeneficiaryRepository.GetAll().Where(b => b.IsActive);
            var beneficiaryDtos = beneficiaries.Select(b => CleanTemplateApplicationMapperInitializer.Mapper.Map<BeneficiaryResponseDTO>(b));

            return Task.FromResult(beneficiaryDtos);
        }
    }
}
