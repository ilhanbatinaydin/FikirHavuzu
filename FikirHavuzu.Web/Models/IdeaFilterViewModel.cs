using Microsoft.AspNetCore.Mvc.Rendering;

namespace FikirHavuzu.Web.Models
{
    public class IdeaFilterViewModel
    {
        public string? SearchQuery { get; set; }
        public string? Email { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? FullName { get; set; }
        public SelectList? CategoryList { get; set; }
    }
}
