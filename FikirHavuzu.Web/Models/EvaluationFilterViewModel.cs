namespace FikirHavuzu.Web.Models
{
    public class EvaluationFilterViewModel
    {
        public int IdeaId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Comment { get; set; }
        public int? Score { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
