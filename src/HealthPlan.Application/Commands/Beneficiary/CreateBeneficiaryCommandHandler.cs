using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for creating new beneficiaries.
    /// </summary>
    public class CreateBeneficiaryCommandHandler : IRequestHandler<CreateBeneficiaryCommand, BeneficiaryResponseDTO>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public CreateBeneficiaryCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<BeneficiaryResponseDTO> Handle(CreateBeneficiaryCommand request, CancellationToken cancellationToken)
        {
            var beneficiary = new Beneficiary
            {
                Name = request.Name,
                CPF = request.CPF,
                Email = request.Email,
                Phone = request.Phone,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Address = request.Address,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode,
                CreatedBy = request.CreatedBy,
                DtCreated = DateTime.UtcNow,
            };

            unitOfWork.BeneficiaryRepository.Add(beneficiary);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult(CleanTemplateApplicationMapperInitializer.Mapper.Map<BeneficiaryResponseDTO>(beneficiary));
        }
    }
}
