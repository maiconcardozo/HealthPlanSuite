using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for deleting accommodations.
    /// </summary>
    public class DeleteAccommodationCommandHandler : IRequestHandler<DeleteAccommodationCommand, bool>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public DeleteAccommodationCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteAccommodationCommand request, CancellationToken cancellationToken)
        {
            var accommodation = unitOfWork.AccommodationRepository.GetById(request.Id);

            if (accommodation == null)
            {
                return Task.FromResult(false);
            }

            unitOfWork.AccommodationRepository.Remove(accommodation);

            return Task.FromResult(true);
        }
    }
}
