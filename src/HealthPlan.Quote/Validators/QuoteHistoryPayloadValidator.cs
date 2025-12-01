using FluentValidation;
using HealthPlan.Quote.DTO;

namespace HealthPlan.Quote.Validators
{
    /// <summary>
    /// Validator for QuoteHistoryPayLoadDTO.
    /// Defines validation rules for quote history payload data.
    /// </summary>
    public class QuoteHistoryPayloadValidator : AbstractValidator<QuoteHistoryPayLoadDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteHistoryPayloadValidator"/> class.
        /// </summary>
        public QuoteHistoryPayloadValidator()
        {
            RuleFor(x => x.QuoteId)
                .GreaterThan(0)
                .WithMessage("Quote ID must be greater than 0");

            RuleFor(x => x.NewStatus)
                .NotEmpty()
                .WithMessage("New Status is required")
                .MaximumLength(50)
                .WithMessage("New Status must not exceed 50 characters");

            RuleFor(x => x.ResponsibleUser)
                .NotEmpty()
                .WithMessage("Responsible User is required")
                .MaximumLength(100)
                .WithMessage("Responsible User must not exceed 100 characters");

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithMessage("CreatedBy is required")
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters");

            RuleFor(x => x.PreviousStatus)
                .MaximumLength(50)
                .When(x => !string.IsNullOrEmpty(x.PreviousStatus))
                .WithMessage("Previous Status must not exceed 50 characters");

            RuleFor(x => x.Reason)
                .MaximumLength(500)
                .When(x => !string.IsNullOrEmpty(x.Reason))
                .WithMessage("Reason must not exceed 500 characters");

            RuleFor(x => x.Observations)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrEmpty(x.Observations))
                .WithMessage("Observations must not exceed 1000 characters");

            RuleFor(x => x.UpdatedBy)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.UpdatedBy))
                .WithMessage("UpdatedBy must not exceed 100 characters");
        }
    }
}
