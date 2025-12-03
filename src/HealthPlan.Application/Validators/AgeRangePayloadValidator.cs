using FluentValidation;
using HealthPlan.Application.DTOs;

namespace HealthPlan.Application.Validators
{
    /// <summary>
    /// Validator for AgeRangePayLoadDTO.
    /// Defines validation rules for age range payload data.
    /// </summary>
    public class AgeRangePayloadValidator : AbstractValidator<AgeRangePayLoadDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgeRangePayloadValidator"/> class.
        /// </summary>
        public AgeRangePayloadValidator()
        {
            RuleFor(x => x.MinAge)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MinAge must be greater than or equal to 0");

            RuleFor(x => x.MaxAge)
                .GreaterThan(x => x.MinAge)
                .WithMessage("MaxAge must be greater than MinAge");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MaximumLength(200)
                .WithMessage("Description must not exceed 200 characters");

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
