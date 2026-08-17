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

            RuleFor(x => x.Documents)
                .Custom((documents, context) =>
                {
                    if (documents == null || !documents.Any())
                        return;

                    var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".png", ".jpg", ".jpeg" };

                    foreach (var file in documents)
                    {
                        if (file.Length > 5 * 1024 * 1024)
                        {
                            context.AddFailure(nameof(IdeaCreateViewModel.Documents), $"'{file.FileName}' adlı dosya 5MB boyutunu aşıyor.");
                        }

                        var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
                        if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
                        {
                            context.AddFailure(nameof(IdeaCreateViewModel.Documents), $"'{file.FileName}' desteklenmeyen bir dosya formatı. (Sadece PDF, Word ve Görseller)");
                        }
                    }
                });
        }
    }
}