using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class DeletePromotionalDiscountCommandHandler : IRequestHandler<DeletePromotionalDiscountCommand, bool>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public DeletePromotionalDiscountCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeletePromotionalDiscountCommand request, CancellationToken cancellationToken)
        {
            var promotionalDiscount = unitOfWork.PromotionalDiscountRepository.GetById(request.Id);

            if (promotionalDiscount == null)
            {
                return Task.FromResult(false);
            }

            unitOfWork.PromotionalDiscountRepository.Remove(promotionalDiscount);

            return Task.FromResult(true);
        }
    }
}
