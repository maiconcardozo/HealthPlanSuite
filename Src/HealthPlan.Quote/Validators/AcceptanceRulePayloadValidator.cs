using FluentValidation;
using HealthPlan.Quote.DTO;

namespace HealthPlan.Quote.Validators
{
    /// <summary>
    /// Validator for AcceptanceRulePayLoadDTO.
    /// Defines validation rules for acceptance rule payload data.
    /// </summary>
    public class AcceptanceRulePayloadValidator : AbstractValidator<AcceptanceRulePayLoadDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptanceRulePayloadValidator"/> class.
        /// </summary>
        public AcceptanceRulePayloadValidator()
        {
            RuleFor(x => x.HealthPlanId)
                .GreaterThan(0)
                .WithMessage("Health Plan ID must be greater than 0");

            RuleFor(x => x.RuleType)
                .NotEmpty()
                .WithMessage("Rule Type is required")
                .MaximumLength(50)
                .WithMessage("Rule Type must not exceed 50 characters");

            RuleFor(x => x.Operator)
                .NotEmpty()
                .WithMessage("Operator is required")
                .MaximumLength(20)
                .WithMessage("Operator must not exceed 20 characters");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MaximumLength(500)
                .WithMessage("Description must not exceed 500 characters");

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithMessage("CreatedBy is required")
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters");

            RuleFor(x => x.MinValue)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.MinValue))
                .WithMessage("MinValue must not exceed 100 characters");

            RuleFor(x => x.MaxValue)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.MaxValue))
                .WithMessage("MaxValue must not exceed 100 characters");

            RuleFor(x => x.ValuesList)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrEmpty(x.ValuesList))
                .WithMessage("ValuesList must not exceed 1000 characters");

            RuleFor(x => x.RejectionMessage)
                .MaximumLength(500)
                .When(x => !string.IsNullOrEmpty(x.RejectionMessage))
                .WithMessage("RejectionMessage must not exceed 500 characters");

            RuleFor(x => x.UpdatedBy)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.UpdatedBy))
                .WithMessage("UpdatedBy must not exceed 100 characters");
        }
    }
}
