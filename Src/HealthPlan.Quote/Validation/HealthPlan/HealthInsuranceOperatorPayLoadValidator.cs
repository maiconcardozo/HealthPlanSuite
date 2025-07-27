using FluentValidation;
using HealthPlan.Quote.DTO.HealthPlan;

namespace HealthPlan.Quote.Validation.HealthPlan
{
    public class HealthInsuranceOperatorPayLoadValidator : AbstractValidator<HealthInsuranceOperatorPayLoadDTO>
    {
        public HealthInsuranceOperatorPayLoadValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .Length(2, 200).WithMessage("Nome deve ter entre 2 e 200 caracteres");

            RuleFor(x => x.CNPJ)
                .NotEmpty().WithMessage("CNPJ é obrigatório")
                .Length(14, 18).WithMessage("CNPJ deve ter entre 14 e 18 caracteres")
                .Matches(@"^\d{2}\.?\d{3}\.?\d{3}\/?\d{4}-?\d{2}$").WithMessage("CNPJ deve ter formato válido");

            RuleFor(x => x.Website)
                .Must(BeValidUrl).WithMessage("Website deve ser uma URL válida")
                .When(x => !string.IsNullOrEmpty(x.Website));

            RuleFor(x => x.Phone)
                .Length(10, 20).WithMessage("Telefone deve ter entre 10 e 20 caracteres")
                .When(x => !string.IsNullOrEmpty(x.Phone));
        }

        private bool BeValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}