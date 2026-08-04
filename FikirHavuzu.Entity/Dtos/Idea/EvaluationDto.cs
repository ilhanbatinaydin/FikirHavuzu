namespace FikirHavuzu.Entity.Dtos.Idea
{
    public class EvaluationDto
    {
        public int Id { get; set; }
        public bool IsApproved { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime EvaluationDate { get; set; }
        public int IdeaId { get; set; }
        public int EvaluatedByUserId { get; set; }
        public string EvaluatorFullName { get; set; } = string.Empty;
        public string EvaluatorEmail {  get; set; } = string.Empty;
    }
}
