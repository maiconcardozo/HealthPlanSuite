using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for updating companies.
    /// </summary>
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public UpdateCompanyCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<CompanyResponseDTO?> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = unitOfWork.CompanyRepository.GetById(request.Id);

            if (company == null)
            {
                return Task.FromResult<CompanyResponseDTO?>(null);
            }

            company.Name = request.Name;
            company.TradeName = request.TradeName;
            company.CNPJ = request.CNPJ;
            company.Email = request.Email;
            company.Phone = request.Phone;
            company.Address = request.Address;
            company.City = request.City;
            company.State = request.State;
            company.ZipCode = request.ZipCode;
            company.UpdatedBy = request.UpdatedBy;
            company.DtUpdated = DateTime.UtcNow;

            unitOfWork.CompanyRepository.Update(company);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult<CompanyResponseDTO?>(CleanTemplateApplicationMapperInitializer.Mapper.Map<CompanyResponseDTO>(company));
        }
    }
}
