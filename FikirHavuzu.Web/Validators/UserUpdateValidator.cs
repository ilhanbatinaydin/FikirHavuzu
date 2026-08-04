using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Web.Models;
using FluentValidation;

namespace FikirHavuzu.Web.Validators
{
    public class UserUpdateValidator : AbstractValidator<UserUpdateViewModel>
    {
        private readonly IServiceManager _manager;

        public  UserUpdateValidator(IServiceManager manager)
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
                .MustAsync(async (model, tc, cancellationToken) =>
                {
                    return !await _manager.UserService.IsIdentityExistsAsync(tc, model.Id);
                }).WithMessage("Bu T.C. Kimlik numarası sistemde zaten kayıtlı.");

            RuleFor(x => x.RegistrationNumber)
                .NotEmpty().WithMessage("Sicil Numarası alanı zorunludur.")
                .MustAsync(async (model, regNo, cancellationToken) =>
                {
                    return !await _manager.UserService.IsRegistrationNumberExistsAsync(regNo, model.Id);
                }).WithMessage("Bu sicil numarası sistemde zaten kayıtlı.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta alanı zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MustAsync(async (model, email, cancellationToken) =>
                {
                    return !await _manager.UserService.IsEmailExistsAsync(email, model.Id);
                }).WithMessage("Bu e-posta adresi sistemde zaten kullanılıyor.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^(\+90|0)?5\d{9}$").WithMessage("Geçerli bir Türkiye cep telefonu numarası giriniz.")
                .MustAsync(async (model, phone, cancellationToken) =>
                {
                    return !await _manager.UserService.IsPhoneNumberExistsAsync(phone, model.Id);
                }).WithMessage("Bu telefon numarası sistemde zaten kayıtlı.")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

            RuleFor(x => x.Password)
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.")
                .When(x => !string.IsNullOrEmpty(x.Password));

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Şifreler birbiriyle uyuşmuyor.")
                .When(x => !string.IsNullOrEmpty(x.Password) || !string.IsNullOrEmpty(x.ConfirmPassword));
        }
    }
}
