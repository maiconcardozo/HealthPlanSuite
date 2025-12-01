using FluentValidation;
using HealthPlan.Quote.DTO;

namespace HealthPlan.Quote.Validators
{
    /// <summary>
    /// Validator for HealthPlanPayLoadDTO.
    /// Defines validation rules for health plan payload data.
    /// </summary>
    public class HealthPlanPayloadValidator : AbstractValidator<HealthPlanPayLoadDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthPlanPayloadValidator"/> class.
        /// </summary>
        public HealthPlanPayloadValidator()
        {
            RuleFor(x => x.CompanyId)
                .GreaterThan(0)
                .WithMessage("Company ID must be greater than 0");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(200)
                .WithMessage("Name must not exceed 200 characters");

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Code is required")
                .MaximumLength(50)
                .WithMessage("Code must not exceed 50 characters");

            RuleFor(x => x.PlanType)
                .NotEmpty()
                .WithMessage("Plan type is required")
                .MaximumLength(50)
                .WithMessage("Plan type must not exceed 50 characters");

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithMessage("CreatedBy is required")
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters");

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrEmpty(x.Description))
                .WithMessage("Description must not exceed 1000 characters");

            RuleFor(x => x.UpdatedBy)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.UpdatedBy))
                .WithMessage("UpdatedBy must not exceed 100 characters");
        }
    }
}
