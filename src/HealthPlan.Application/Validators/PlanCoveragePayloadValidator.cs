using FluentValidation;
using HealthPlan.Application.DTOs;

namespace HealthPlan.Application.Validators
{
    /// <summary>
    /// Validator for PlanCoveragePayLoadDTO.
    /// Defines validation rules for plan coverage payload data.
    /// </summary>
    public class PlanCoveragePayloadValidator : AbstractValidator<PlanCoveragePayLoadDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlanCoveragePayloadValidator"/> class.
        /// </summary>
        public PlanCoveragePayloadValidator()
        {
            RuleFor(x => x.HealthPlanId)
                .GreaterThan(0)
                .WithMessage("Health Plan ID must be greater than 0");

            RuleFor(x => x.CoverageId)
                .GreaterThan(0)
                .WithMessage("Coverage ID must be greater than 0");

            RuleFor(x => x.PremiumValue)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Premium Value must be greater than or equal to 0");

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithMessage("CreatedBy is required")
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters");

            RuleFor(x => x.UpdatedBy)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.UpdatedBy))
                .WithMessage("UpdatedBy must not exceed 100 characters");
        }
    }
}
