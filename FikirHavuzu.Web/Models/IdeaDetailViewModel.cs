using FikirHavuzu.Entity.Dtos.Idea;

namespace FikirHavuzu.Web.Models
{
    public class IdeaDetailViewModel
    {
        public IdeaDetailDto Idea { get; set; } = null!;

        public EvaluationCreateViewModel NewEvaluation { get; set; } = new EvaluationCreateViewModel();
    }
}
