using FluentValidation;
using HealthPlan.Application.DTOs;

namespace HealthPlan.Application.Validators
{
    /// <summary>
    /// Validator for CoveragePayLoadDTO.
    /// Defines validation rules for coverage payload data.
    /// </summary>
    public class CoveragePayloadValidator : AbstractValidator<CoveragePayLoadDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoveragePayloadValidator"/> class.
        /// </summary>
        public CoveragePayloadValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(200)
                .WithMessage("Name must not exceed 200 characters");

            RuleFor(x => x.CoverageType)
                .NotEmpty()
                .WithMessage("Coverage Type is required")
                .MaximumLength(50)
                .WithMessage("Coverage Type must not exceed 50 characters");

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
