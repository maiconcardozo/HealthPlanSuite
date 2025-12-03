using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for updating coverages.
    /// </summary>
    public class UpdateCoverageCommandHandler : IRequestHandler<UpdateCoverageCommand, CoverageResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public UpdateCoverageCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<CoverageResponseDTO?> Handle(UpdateCoverageCommand request, CancellationToken cancellationToken)
        {
            var coverage = unitOfWork.CoverageRepository.GetById(request.Id);

            if (coverage == null)
            {
                return Task.FromResult<CoverageResponseDTO?>(null);
            }

            coverage.Name = request.Name;
            coverage.Description = request.Description;
            coverage.CoverageType = request.CoverageType;
            coverage.UpdatedBy = request.UpdatedBy;
            coverage.DtUpdated = DateTime.UtcNow;

            unitOfWork.CoverageRepository.Update(coverage);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult<CoverageResponseDTO?>(CleanTemplateApplicationMapperInitializer.Mapper.Map<CoverageResponseDTO>(coverage));
        }
    }
}
