using FluentValidation;
using FikirHavuzu.Web.Models;

namespace FikirHavuzu.Web.Validators
{
    public class EvaluationCreateValidator : AbstractValidator<EvaluationCreateViewModel>
    {
        public EvaluationCreateValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.IdeaId)
                .GreaterThan(0).WithMessage("Fikir bilgisi eksik.");

            RuleFor(x => x.Score)
                .NotEmpty().WithMessage("Lütfen bir puan seçiniz.")
                .InclusiveBetween(1, 5).WithMessage("Puan 1 ile 5 arasında olmalıdır.");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Lütfen değerlendirmeniz için bir yorum yazınız.")
                .MaximumLength(500).WithMessage("Yorum en fazla 500 karakter olabilir.");
        }
    }
}