using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Handler for retrieving a company by ID.
    /// </summary>
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetCompanyByIdQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<CompanyResponseDTO?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = unitOfWork.CompanyRepository.GetById(request.Id);

            if (company == null)
            {
                return Task.FromResult<CompanyResponseDTO?>(null);
            }

            var companyDto = CleanTemplateApplicationMapperInitializer.Mapper.Map<CompanyResponseDTO>(company);
            return Task.FromResult<CompanyResponseDTO?>(companyDto);
        }
    }
}
