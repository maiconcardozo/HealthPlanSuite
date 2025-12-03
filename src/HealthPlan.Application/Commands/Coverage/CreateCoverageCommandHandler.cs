using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for creating new coverages.
    /// </summary>
    public class CreateCoverageCommandHandler : IRequestHandler<CreateCoverageCommand, CoverageResponseDTO>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public CreateCoverageCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<CoverageResponseDTO> Handle(CreateCoverageCommand request, CancellationToken cancellationToken)
        {
            var coverage = new Coverage
            {
                Name = request.Name,
                Description = request.Description,
                CoverageType = request.CoverageType,
                CreatedBy = request.CreatedBy,
                DtCreated = DateTime.UtcNow,
            };

            unitOfWork.CoverageRepository.Add(coverage);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult(CleanTemplateApplicationMapperInitializer.Mapper.Map<CoverageResponseDTO>(coverage));
        }
    }
}
