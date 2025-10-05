using FluentValidation;
using HealthPlan.Quote.DTO;

namespace HealthPlan.Quote.Validators
{
    /// <summary>
    /// Validator for PrecoPlanoFaixaPayLoadDTO.
    /// Defines validation rules for plan price range payload data.
    /// </summary>
    public class PrecoPlanoFaixaPayloadValidator : AbstractValidator<PrecoPlanoFaixaPayLoadDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrecoPlanoFaixaPayloadValidator"/> class.
        /// </summary>
        public PrecoPlanoFaixaPayloadValidator()
        {
            RuleFor(x => x.HealthPlanId)
                .GreaterThan(0)
                .WithMessage("Health Plan ID must be greater than 0");

            RuleFor(x => x.AgeRangeId)
                .GreaterThan(0)
                .WithMessage("Age Range ID must be greater than 0");

            RuleFor(x => x.TipoContratacao)
                .NotEmpty()
                .WithMessage("Tipo Contratacao is required")
                .MaximumLength(50)
                .WithMessage("Tipo Contratacao must not exceed 50 characters");

            RuleFor(x => x.TipoCoparticipacao)
                .NotEmpty()
                .WithMessage("Tipo Coparticipacao is required")
                .MaximumLength(50)
                .WithMessage("Tipo Coparticipacao must not exceed 50 characters");

            RuleFor(x => x.ValorOriginal)
                .GreaterThan(0)
                .WithMessage("Valor Original must be greater than 0");

            RuleFor(x => x.ValorDesconto)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Valor Desconto must be greater than or equal to 0");

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
