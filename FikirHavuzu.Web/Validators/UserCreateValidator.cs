using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Web.Models;
using FluentValidation;

namespace FikirHavuzu.Web.Validators
{
    public class UserCreateValidator : AbstractValidator<UserCreateViewModel>
    {
        private readonly IServiceManager _manager;

        public UserCreateValidator(IServiceManager manager)
        {
            _manager = manager;

            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad alanı zorunludur.")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad alanı zorunludur.")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

            RuleFor(x => x.IdentityNumber)
                .NotEmpty().WithMessage("T.C. Kimlik Numarası zorunludur.")
                .Length(11).WithMessage("T.C. Kimlik Numarası 11 haneli olmalıdır.")
                .MustAsync(async (tc, cancellationToken) =>
                {
                    return !await _manager.UserService.IsIdentityExistsAsync(tc);
                }).WithMessage("Bu T.C. Kimlik numarası sistemde zaten kayıtlı.");

            RuleFor(x => x.RegistrationNumber)
                .NotEmpty().WithMessage("Sicil Numarası alanı zorunludur.")
                .MustAsync(async (regNo, cancellationToken) =>
                {
                    return !await _manager.UserService.IsRegistrationNumberExistsAsync(regNo);
                }).WithMessage("Bu sicil numarası sistemde zaten kayıtlı.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta alanı zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MustAsync(async (email, cancellationToken) =>
                {
                    return !await _manager.UserService.IsEmailExistsAsync(email);
                }).WithMessage("Bu e-posta adresi sistemde zaten kullanılıyor.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^(\+90|0)?5\d{9}$").WithMessage("Geçerli bir Türkiye cep telefonu numarası giriniz.")
                .MustAsync(async (phone, cancellationToken) =>
                {
                    return !await _manager.UserService.IsPhoneNumberExistsAsync(phone);
                }).WithMessage("Bu telefon numarası sistemde zaten kayıtlı.")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre zorunludur.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Şifre tekrarı zorunludur.")
                .Equal(x => x.Password).WithMessage("Şifreler birbiriyle uyuşmuyor.");
        }
    }
}
