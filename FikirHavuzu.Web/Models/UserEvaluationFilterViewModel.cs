using Microsoft.AspNetCore.Mvc.Rendering;

namespace FikirHavuzu.Web.Models
{
    public class UserEvaluationFilterViewModel
    {
        public int UserId { get; set; }
        public string? SearchQuery { get; set; }
        public int? CategoryId { get; set; }
        public SelectList? CategoryList { get; set; }
        public string? Comment { get; set; }
        public int? Score { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
