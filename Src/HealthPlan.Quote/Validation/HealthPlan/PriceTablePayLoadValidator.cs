using FluentValidation;
using HealthPlan.Quote.DTO.HealthPlan;

namespace HealthPlan.Quote.Validation.HealthPlan
{
    public class PriceTablePayLoadValidator : AbstractValidator<PriceTablePayLoadDTO>
    {
        public PriceTablePayLoadValidator()
        {
            RuleFor(x => x.HealthPlanId)
                .GreaterThan(0).WithMessage("ID do Plano de Saúde é obrigatório");

            RuleFor(x => x.AgeRangeId)
                .GreaterThan(0).WithMessage("ID da Faixa Etária é obrigatório");

            RuleFor(x => x.MonthlyFee)
                .GreaterThan(0).WithMessage("Mensalidade deve ser maior que zero")
                .LessThan(100000).WithMessage("Mensalidade deve ser menor que R$ 100.000,00");

            RuleFor(x => x.CoparticipationValue)
                .GreaterThanOrEqualTo(0).WithMessage("Valor de coparticipação deve ser maior ou igual a zero")
                .When(x => x.CoparticipationValue.HasValue);

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Data de início é obrigatória");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage("Data de fim deve ser posterior à data de início")
                .When(x => x.EndDate.HasValue);
        }
    }
}