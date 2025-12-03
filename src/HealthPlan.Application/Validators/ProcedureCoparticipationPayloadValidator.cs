using FluentValidation;
using HealthPlan.Application.DTOs;

namespace HealthPlan.Application.Validators
{
    /// <summary>
    /// Validator for ProcedureCoparticipationPayLoadDTO.
    /// Defines validation rules for procedure co-participation payload data.
    /// </summary>
    public class ProcedureCoparticipationPayloadValidator : AbstractValidator<ProcedureCoparticipationPayLoadDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcedureCoparticipationPayloadValidator"/> class.
        /// </summary>
        public ProcedureCoparticipationPayloadValidator()
        {
            RuleFor(x => x.HealthPlanId)
                .GreaterThan(0)
                .WithMessage("Health Plan ID must be greater than 0");

            RuleFor(x => x.CoparticipationType)
                .NotEmpty()
                .WithMessage("Tipo Coparticipacao is required")
                .MaximumLength(50)
                .WithMessage("Tipo Coparticipacao must not exceed 50 characters");

            RuleFor(x => x.Procedure)
                .NotEmpty()
                .WithMessage("Procedimento is required")
                .MaximumLength(200)
                .WithMessage("Procedimento must not exceed 200 characters");

            RuleFor(x => x.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Valor must be greater than or equal to 0");

            RuleFor(x => x.Limit)
                .GreaterThanOrEqualTo(0)
                .When(x => x.Limit.HasValue)
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
