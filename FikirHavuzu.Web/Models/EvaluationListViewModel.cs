using FikirHavuzu.Entity.Dtos.Idea;

namespace FikirHavuzu.Web.Models
{
    public class EvaluationListViewModel
    {
        public int IdeaId { get; set; }
        public IEnumerable<EvaluationDto> Evaluations { get; set; } = new List<EvaluationDto>();
        public Pagination Pagination { get; set; } = new Pagination();
    }
}
