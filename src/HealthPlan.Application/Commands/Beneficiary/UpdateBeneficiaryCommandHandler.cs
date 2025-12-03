using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for updating beneficiaries.
    /// </summary>
    public class UpdateBeneficiaryCommandHandler : IRequestHandler<UpdateBeneficiaryCommand, BeneficiaryResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public UpdateBeneficiaryCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<BeneficiaryResponseDTO?> Handle(UpdateBeneficiaryCommand request, CancellationToken cancellationToken)
        {
            var beneficiary = unitOfWork.BeneficiaryRepository.GetById(request.Id);

            if (beneficiary == null)
            {
                return Task.FromResult<BeneficiaryResponseDTO?>(null);
            }

            beneficiary.Name = request.Name;
            beneficiary.CPF = request.CPF;
            beneficiary.Email = request.Email;
            beneficiary.Phone = request.Phone;
            beneficiary.DateOfBirth = request.DateOfBirth;
            beneficiary.Gender = request.Gender;
            beneficiary.Address = request.Address;
            beneficiary.City = request.City;
            beneficiary.State = request.State;
            beneficiary.ZipCode = request.ZipCode;
            beneficiary.UpdatedBy = request.UpdatedBy;
            beneficiary.DtUpdated = DateTime.UtcNow;

            unitOfWork.BeneficiaryRepository.Update(beneficiary);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult<BeneficiaryResponseDTO?>(CleanTemplateApplicationMapperInitializer.Mapper.Map<BeneficiaryResponseDTO>(beneficiary));
        }
    }
}
