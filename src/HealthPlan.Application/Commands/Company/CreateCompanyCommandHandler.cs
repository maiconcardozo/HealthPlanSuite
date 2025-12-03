using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for creating new companies.
    /// </summary>
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyResponseDTO>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public CreateCompanyCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<CompanyResponseDTO> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new Company
            {
                Name = request.Name,
                TradeName = request.TradeName,
                CNPJ = request.CNPJ,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode,
                CreatedBy = request.CreatedBy,
                DtCreated = DateTime.UtcNow,
            };

            unitOfWork.CompanyRepository.Add(company);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult(CleanTemplateApplicationMapperInitializer.Mapper.Map<CompanyResponseDTO>(company));
        }
    }
}
