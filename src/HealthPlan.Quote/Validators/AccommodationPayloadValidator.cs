using FluentValidation;
using HealthPlan.Quote.DTO;

namespace HealthPlan.Quote.Validators
{
    /// <summary>
    /// Validator for AccommodationPayLoadDTO.
    /// Defines validation rules for accommodation payload data.
    /// </summary>
    public class AccommodationPayloadValidator : AbstractValidator<AccommodationPayLoadDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccommodationPayloadValidator"/> class.
        /// </summary>
        public AccommodationPayloadValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Type is required")
                .MaximumLength(100)
                .WithMessage("Type must not exceed 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MaximumLength(500)
                .WithMessage("Description must not exceed 500 characters");

            RuleFor(x => x.AdditionalValue)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Additional Value must be greater than or equal to 0");

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
