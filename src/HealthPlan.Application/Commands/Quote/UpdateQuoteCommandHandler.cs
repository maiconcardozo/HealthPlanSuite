using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for updating quotes.
    /// </summary>
    public class UpdateQuoteCommandHandler : IRequestHandler<UpdateQuoteCommand, QuoteResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public UpdateQuoteCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<QuoteResponseDTO?> Handle(UpdateQuoteCommand request, CancellationToken cancellationToken)
        {
            var quote = unitOfWork.QuoteRepository.GetById(request.Id);

            if (quote == null)
            {
                return Task.FromResult<QuoteResponseDTO?>(null);
            }

            quote.IdCompany = request.IdCompany;
            quote.IdBeneficiary = request.IdBeneficiary;
            quote.IdHealthPlan = request.IdHealthPlan;
            quote.IdAgeRange = request.IdAgeRange;
            quote.MonthlyPremium = request.MonthlyPremium;
            quote.ValidUntil = request.ValidUntil;
            quote.Status = request.Status ?? quote.Status;
            quote.Notes = request.Notes;
            quote.UpdatedBy = request.UpdatedBy;
            quote.DtUpdated = DateTime.UtcNow;

            unitOfWork.QuoteRepository.Update(quote);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult<QuoteResponseDTO?>(CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteResponseDTO>(quote));
        }
    }
}
