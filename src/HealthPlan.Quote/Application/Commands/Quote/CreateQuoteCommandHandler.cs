using HealthPlan.Quote.DTO;
using HealthPlan.Quote.Mapping;
using HealthPlan.Quote.UnitOfWork.Interface;
using MediatR;

namespace HealthPlan.Quote.Application.Commands.Quote
{
    /// <summary>
    /// Handler for creating new quotes.
    /// </summary>
    public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, QuoteResponseDTO>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public CreateQuoteCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<QuoteResponseDTO> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
        {
            var quote = new Domain.Implementation.Quote
            {
                IdCompany = request.IdCompany,
                IdBeneficiary = request.IdBeneficiary,
                IdHealthPlan = request.IdHealthPlan,
                IdAgeRange = request.IdAgeRange,
                MonthlyPremium = request.MonthlyPremium,
                ValidUntil = request.ValidUntil,
                CreatedBy = request.CreatedBy,
                Notes = request.Notes,
                Status = "Pending"
            };

            unitOfWork.QuoteRepository.Add(quote);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult(CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteResponseDTO>(quote));
        }
    }
}
