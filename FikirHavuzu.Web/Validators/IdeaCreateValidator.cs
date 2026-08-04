using FluentValidation;
using FikirHavuzu.Web.Models;
using System.IO;
using System.Linq;

namespace FikirHavuzu.Web.Validators
{
    public class IdeaCreateValidator : AbstractValidator<IdeaCreateViewModel>
    {
        public IdeaCreateValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Lütfen fikriniz için anlaşılır bir başlık giriniz.")
                .Length(5, 255).WithMessage("Başlık en fazla 255 karakter, en az 5 karakter olmalıdır.");

            RuleFor(x => x.TargetedBenefit)
                .NotEmpty().WithMessage("Bu fikrin şirkete veya sürece sağlayacağı faydayı belirtmelisiniz.")
                .MaximumLength(127).WithMessage("Hedeflenen fayda alanı en fazla 127 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Lütfen fikrinizin detaylarını boş bırakmayınız.")
                .MinimumLength(20).WithMessage("Açıklama alanı çok kısa, lütfen fikrinizi biraz daha detaylandırınız.");

            RuleFor(x => x.CategoryId)
                .NotNull().WithMessage("Lütfen fikrinize en uygun kategoriyi seçiniz.");

            When(x => x.Documents != null && x.Documents.Any(), () =>
            {
                RuleForEach(x => x.Documents)
                    .Must(file => file.Length <= 5 * 1024 * 1024)
                    .WithMessage((model, file) => $"'{file.FileName}' adlı dosya 5MB boyutunu aşıyor.")

                    .Must(file =>
                    {
                        var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".png", ".jpg", ".jpeg" };
                        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                        return allowedExtensions.Contains(extension);
                    })
                    .WithMessage((model, file) => $"'{file.FileName}' desteklenmeyen bir dosya formatı. (Sadece PDF, Word ve Görseller)");
            });
        }
    }
}