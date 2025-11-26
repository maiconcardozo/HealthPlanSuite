using FluentValidation;

namespace HealthPlan.Quote.Application.Commands.Quote
{
    /// <summary>
    /// Validator for CreateQuoteCommand.
    /// Ensures all required fields are provided and meet business rules.
    /// </summary>
    public class CreateQuoteCommandValidator : AbstractValidator<CreateQuoteCommand>
    {
        public CreateQuoteCommandValidator()
        {
            RuleFor(x => x.IdCompany)
                .GreaterThan(0).WithMessage("Company ID must be greater than 0");

            RuleFor(x => x.IdBeneficiary)
                .GreaterThan(0).WithMessage("Beneficiary ID must be greater than 0");

            RuleFor(x => x.IdHealthPlan)
                .GreaterThan(0).WithMessage("Health Plan ID must be greater than 0");

            RuleFor(x => x.IdAgeRange)
                .GreaterThan(0).WithMessage("Age Range ID must be greater than 0");

            RuleFor(x => x.ValidUntil)
                .GreaterThan(DateTime.UtcNow).WithMessage("Valid Until date must be in the future");

            RuleFor(x => x.MonthlyPremium)
                .GreaterThan(0).WithMessage("Monthly Premium must be greater than 0");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("CreatedBy is required")
                .MaximumLength(100).WithMessage("CreatedBy must not exceed 100 characters");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).When(x => !string.IsNullOrEmpty(x.Notes))
                .WithMessage("Notes must not exceed 1000 characters");
        }
    }
}
