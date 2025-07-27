using Authentication.Login.DTO;
using Authentication.Login.Resource;
using FluentValidation;

namespace Authentication.Login.Util
{
    public class AccountPayloadValidator : AbstractValidator<AccountPayLoadDTO>
    {
        public AccountPayloadValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(ResourceLogin.UserNameRequired)
                .Must(u => !string.IsNullOrWhiteSpace(u) && !u.Contains(" ")).WithMessage(ResourceLogin.UserNameCannotContainSpacesNullEmpty)
                .MinimumLength(6).WithMessage(ResourceLogin.UserNameMustLeast6Characters)
                .MaximumLength(50).WithMessage(ResourceLogin.UserNameMustMost50Characters);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ResourceLogin.PasswordRequired)
                .Must(p => !string.IsNullOrWhiteSpace(p) && !p.Contains(" ")).WithMessage(ResourceLogin.PasswordCannotContainSpacesNullEmpty)
                .MinimumLength(6).WithMessage(ResourceLogin.PasswordMustLeast6Characters)
                .MaximumLength(50).WithMessage(ResourceLogin.PasswordMustMost50Characters);
        }
    }
}