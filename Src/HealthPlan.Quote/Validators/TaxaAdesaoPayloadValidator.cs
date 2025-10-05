using FluentValidation;
using HealthPlan.Quote.DTO;

namespace HealthPlan.Quote.Validators
{
    /// <summary>
    /// Validator for TaxaAdesaoPayLoadDTO.
    /// Defines validation rules for adhesion fee payload data.
    /// </summary>
    public class TaxaAdesaoPayloadValidator : AbstractValidator<TaxaAdesaoPayLoadDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxaAdesaoPayloadValidator"/> class.
        /// </summary>
        public TaxaAdesaoPayloadValidator()
        {
            RuleFor(x => x.HealthPlanId)
                .GreaterThan(0)
                .WithMessage("Health Plan ID must be greater than 0");

            RuleFor(x => x.Valor)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Valor must be greater than or equal to 0");

            RuleFor(x => x.ValidadeInicio)
                .NotEmpty()
                .WithMessage("Validade Inicio is required");

            RuleFor(x => x.ValidadeFim)
                .NotEmpty()
                .WithMessage("Validade Fim is required")
                .GreaterThan(x => x.ValidadeInicio)
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
