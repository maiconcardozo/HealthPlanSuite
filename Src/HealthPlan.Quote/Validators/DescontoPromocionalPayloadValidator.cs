using FluentValidation;
using HealthPlan.Quote.DTO;

namespace HealthPlan.Quote.Validators
{
    /// <summary>
    /// Validator for DescontoPromocionalPayLoadDTO.
    /// Defines validation rules for promotional discount payload data.
    /// </summary>
    public class DescontoPromocionalPayloadValidator : AbstractValidator<DescontoPromocionalPayLoadDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DescontoPromocionalPayloadValidator"/> class.
        /// </summary>
        public DescontoPromocionalPayloadValidator()
        {
            RuleFor(x => x.HealthPlanId)
                .GreaterThan(0)
                .WithMessage("Health Plan ID must be greater than 0");

            RuleFor(x => x.PercentualDesconto)
                .GreaterThan(0)
                .WithMessage("Percentual Desconto must be greater than 0")
                .LessThanOrEqualTo(100)
                .WithMessage("Percentual Desconto must not exceed 100");

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

            RuleFor(x => x.Observacao)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrEmpty(x.Observacao))
                .WithMessage("Observacao must not exceed 1000 characters");

            RuleFor(x => x.UpdatedBy)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.UpdatedBy))
                .WithMessage("UpdatedBy must not exceed 100 characters");
        }
    }
}
