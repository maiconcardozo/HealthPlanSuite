using FluentValidation;
using HealthPlan.Quote.DTO;

namespace HealthPlan.Quote.Validators
{
    /// <summary>
    /// Validator for PromotionalDiscountPayLoadDTO.
    /// Defines validation rules for promotional discount payload data.
    /// </summary>
    public class PromotionalDiscountPayloadValidator : AbstractValidator<PromotionalDiscountPayLoadDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PromotionalDiscountPayloadValidator"/> class.
        /// </summary>
        public PromotionalDiscountPayloadValidator()
        {
            RuleFor(x => x.HealthPlanId)
                .GreaterThan(0)
                .WithMessage("Health Plan ID must be greater than 0");

            RuleFor(x => x.DiscountPercentage)
                .GreaterThan(0)
                .WithMessage("Percentual Desconto must be greater than 0")
                .LessThanOrEqualTo(100)
                .WithMessage("Percentual Desconto must not exceed 100");

            RuleFor(x => x.ValidityStart)
                .NotEmpty()
                .WithMessage("Validade Inicio is required");

            RuleFor(x => x.ValidityEnd)
                .NotEmpty()
                .WithMessage("Validade Fim is required")
                .GreaterThan(x => x.ValidityStart)
                .WithMessage("Validade Fim must be greater than Validade Inicio");

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithMessage("CreatedBy is required")
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters");

            RuleFor(x => x.Observation)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrEmpty(x.Observation))
                .WithMessage("Observacao must not exceed 1000 characters");

            RuleFor(x => x.UpdatedBy)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.UpdatedBy))
                .WithMessage("UpdatedBy must not exceed 100 characters");
        }
    }
}
