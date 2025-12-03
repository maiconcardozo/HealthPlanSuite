using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Handler for retrieving a company by CNPJ.
    /// </summary>
    public class GetCompanyByCNPJQueryHandler : IRequestHandler<GetCompanyByCNPJQuery, CompanyResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetCompanyByCNPJQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<CompanyResponseDTO?> Handle(GetCompanyByCNPJQuery request, CancellationToken cancellationToken)
        {
            var company = unitOfWork.CompanyRepository.GetByCNPJ(request.CNPJ);

            if (company == null)
            {
                return Task.FromResult<CompanyResponseDTO?>(null);
            }

            var companyDto = CleanTemplateApplicationMapperInitializer.Mapper.Map<CompanyResponseDTO>(company);
            return Task.FromResult<CompanyResponseDTO?>(companyDto);
        }
    }
}
