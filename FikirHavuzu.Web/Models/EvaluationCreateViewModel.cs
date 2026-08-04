using System.ComponentModel.DataAnnotations;

namespace FikirHavuzu.Web.Models
{
    public class EvaluationCreateViewModel
    {
        public int IdeaId { get; set; }
        public int Score { get; set; }
        public bool IsApproved { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
