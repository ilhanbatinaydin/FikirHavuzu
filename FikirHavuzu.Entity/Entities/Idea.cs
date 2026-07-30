using System.ComponentModel.DataAnnotations;

namespace FikirHavuzu.Entity.Entities
{
    public class Idea
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string TargetedBenefit { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
        public ICollection<IdeaDocument> Documents { get; set; } = new List<IdeaDocument>();
    }
}
