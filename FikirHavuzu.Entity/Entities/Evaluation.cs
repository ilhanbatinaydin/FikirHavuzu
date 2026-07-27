namespace FikirHavuzu.Entity.Entities
{
    public class Evaluation
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime EvaluationDate { get; set; } = DateTime.Now;
        public int IdeaId { get; set; }
        public Idea Idea { get; set; } = null!;
        public int EvaluatedByUserId { get; set; }
        public User EvaluatedByUser { get; set; } = null!;
    }
}
