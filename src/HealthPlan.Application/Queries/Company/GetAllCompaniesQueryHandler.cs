using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Handler for retrieving all companies.
    /// </summary>
    public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, IEnumerable<CompanyResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAllCompaniesQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<CompanyResponseDTO>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = unitOfWork.CompanyRepository.GetAll().Where(c => c.IsActive);
            var companyDtos = companies.Select(c => CleanTemplateApplicationMapperInitializer.Mapper.Map<CompanyResponseDTO>(c));

            return Task.FromResult(companyDtos);
        }
    }
}
