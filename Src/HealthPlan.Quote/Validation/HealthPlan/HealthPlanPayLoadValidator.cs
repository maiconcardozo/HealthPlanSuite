using FluentValidation;
using HealthPlan.Quote.DTO.HealthPlan;

namespace HealthPlan.Quote.Validation.HealthPlan
{
    public class HealthPlanPayLoadValidator : AbstractValidator<HealthPlanPayLoadDTO>
    {
        public HealthPlanPayLoadValidator()
        {
            RuleFor(x => x.HealthInsuranceOperatorId)
                .GreaterThan(0).WithMessage("ID da Operadora é obrigatório");

            RuleFor(x => x.PlanTypeId)
                .GreaterThan(0).WithMessage("ID do Tipo de Plano é obrigatório");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome do plano é obrigatório")
                .Length(2, 200).WithMessage("Nome deve ter entre 2 e 200 caracteres");

            RuleFor(x => x.Coverage)
                .NotEmpty().WithMessage("Cobertura é obrigatória")
                .Length(10, 1000).WithMessage("Cobertura deve ter entre 10 e 1000 caracteres");
        }
    }
}