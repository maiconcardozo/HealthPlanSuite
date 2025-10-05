using FluentValidation;
using HealthPlan.Quote.DTO;

namespace HealthPlan.Quote.Validators
{
    /// <summary>
    /// Validator for CoparticipacaoProcedimentoPayLoadDTO.
    /// Defines validation rules for procedure co-participation payload data.
    /// </summary>
    public class CoparticipacaoProcedimentoPayloadValidator : AbstractValidator<CoparticipacaoProcedimentoPayLoadDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoparticipacaoProcedimentoPayloadValidator"/> class.
        /// </summary>
        public CoparticipacaoProcedimentoPayloadValidator()
        {
            RuleFor(x => x.HealthPlanId)
                .GreaterThan(0)
                .WithMessage("Health Plan ID must be greater than 0");

            RuleFor(x => x.TipoCoparticipacao)
                .NotEmpty()
                .WithMessage("Tipo Coparticipacao is required")
                .MaximumLength(50)
                .WithMessage("Tipo Coparticipacao must not exceed 50 characters");

            RuleFor(x => x.Procedimento)
                .NotEmpty()
                .WithMessage("Procedimento is required")
                .MaximumLength(200)
                .WithMessage("Procedimento must not exceed 200 characters");

            RuleFor(x => x.Valor)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Valor must be greater than or equal to 0");

            RuleFor(x => x.Limite)
                .GreaterThanOrEqualTo(0)
                .When(x => x.Limite.HasValue)
                .WithMessage("Limite must be greater than or equal to 0");

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
