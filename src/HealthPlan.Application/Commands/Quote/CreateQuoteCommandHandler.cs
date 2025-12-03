using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
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
            var quote = new Domain.Entities.Quote
            {
                IdCompany = request.IdCompany,
                IdBeneficiary = request.IdBeneficiary,
                IdHealthPlan = request.IdHealthPlan,
                IdAgeRange = request.IdAgeRange,
                MonthlyPremium = request.MonthlyPremium,
                ValidUntil = request.ValidUntil,
                CreatedBy = request.CreatedBy,
                Notes = request.Notes,
                Status = "Pending",
                DtCreated = DateTime.UtcNow,
            };

            unitOfWork.QuoteRepository.Add(quote);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult(CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteResponseDTO>(quote));
        }
    }
}
