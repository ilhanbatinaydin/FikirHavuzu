using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Entity.Dtos.User;

namespace FikirHavuzu.Web.Models
{
    public class UserEvaluationListViewModel
    {
        public UserDto User { get; set; } = null!;
        public IEnumerable<EvaluationDto> Evaluations { get; set; } = new List<EvaluationDto>();
        public Pagination Pagination { get; set; } = new Pagination();
    }
}
