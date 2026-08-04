using System.ComponentModel.DataAnnotations;

namespace FikirHavuzu.Entity.Dtos.Idea
{
    public class EvaluationCreateDto
    {
        public int IdeaId { get; set; }

        public int Score { get; set; }

        public bool IsApproved { get; set; }

        public string Comment { get; set; } = string.Empty;
    }
}
