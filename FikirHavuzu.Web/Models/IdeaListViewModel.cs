using FikirHavuzu.Entity.Dtos.Idea;
using FikirHavuzu.Entity.RequestParameters;

namespace FikirHavuzu.Web.Models
{
    public class IdeaListViewModel
    {
        public IEnumerable<IdeaDto> Ideas { get; set; } = new List<IdeaDto>();

        public Pagination Pagination { get; set; }
    }
}
