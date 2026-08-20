using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Entity.RequestParameters;

namespace FikirHavuzu.Web.Models
{
    public class IdeaDetailViewModel
    {
        public IdeaDetailDto Idea { get; set; } = null!;

        public EvaluationCreateViewModel NewEvaluation { get; set; } = new EvaluationCreateViewModel();

        public EvaluationRequestParameters EvaluationParameters { get; set; } = new EvaluationRequestParameters();
    }
}
