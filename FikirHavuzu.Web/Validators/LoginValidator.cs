using FluentValidation;
using FikirHavuzu.Web.Models;

namespace FikirHavuzu.Web.Validators
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Lütfen e-posta adresinizi girin.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Lütfen şifrenizi girin.");
        }
    }
}