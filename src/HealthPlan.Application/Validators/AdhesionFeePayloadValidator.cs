using FluentValidation;
using HealthPlan.Application.DTOs;

namespace HealthPlan.Application.Validators
{
    /// <summary>
    /// Validator for AdhesionFeePayLoadDTO.
    /// Defines validation rules for adhesion fee payload data.
    /// </summary>
    public class AdhesionFeePayloadValidator : AbstractValidator<AdhesionFeePayLoadDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdhesionFeePayloadValidator"/> class.
        /// </summary>
        public AdhesionFeePayloadValidator()
        {
            RuleFor(x => x.HealthPlanId)
                .GreaterThan(0)
                .WithMessage("Health Plan ID must be greater than 0");

            RuleFor(x => x.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Valor must be greater than or equal to 0");

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

            RuleFor(x => x.UpdatedBy)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.UpdatedBy))
                .WithMessage("UpdatedBy must not exceed 100 characters");
        }
    }
}
