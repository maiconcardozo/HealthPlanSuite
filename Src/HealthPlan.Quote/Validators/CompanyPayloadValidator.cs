using FluentValidation;
using HealthPlan.Quote.DTO;

namespace HealthPlan.Quote.Validators
{
    /// <summary>
    /// Validator for CompanyPayLoadDTO.
    /// Defines validation rules for company payload data.
    /// </summary>
    public class CompanyPayloadValidator : AbstractValidator<CompanyPayLoadDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyPayloadValidator"/> class.
        /// </summary>
        public CompanyPayloadValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(200)
                .WithMessage("Name must not exceed 200 characters");

            RuleFor(x => x.CNPJ)
                .NotEmpty()
                .WithMessage("CNPJ is required")
                .MaximumLength(18)
                .WithMessage("CNPJ must not exceed 18 characters");

            RuleFor(x => x.CreatedBy)
                .NotEmpty()
                .WithMessage("CreatedBy is required")
                .MaximumLength(100)
                .WithMessage("CreatedBy must not exceed 100 characters");

            RuleFor(x => x.TradeName)
                .MaximumLength(200)
                .When(x => !string.IsNullOrEmpty(x.TradeName))
                .WithMessage("TradeName must not exceed 200 characters");

            RuleFor(x => x.Email)
                .MaximumLength(100)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Invalid email format");

            RuleFor(x => x.Phone)
                .MaximumLength(20)
                .When(x => !string.IsNullOrEmpty(x.Phone))
                .WithMessage("Phone must not exceed 20 characters");

            RuleFor(x => x.Address)
                .MaximumLength(500)
                .When(x => !string.IsNullOrEmpty(x.Address))
                .WithMessage("Address must not exceed 500 characters");

            RuleFor(x => x.City)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.City))
                .WithMessage("City must not exceed 100 characters");

            RuleFor(x => x.State)
                .MaximumLength(2)
                .When(x => !string.IsNullOrEmpty(x.State))
                .WithMessage("State must not exceed 2 characters");

            RuleFor(x => x.ZipCode)
                .MaximumLength(10)
                .When(x => !string.IsNullOrEmpty(x.ZipCode))
                .WithMessage("ZipCode must not exceed 10 characters");

            RuleFor(x => x.UpdatedBy)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.UpdatedBy))
                .WithMessage("UpdatedBy must not exceed 100 characters");
        }
    }
}
