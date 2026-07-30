
namespace FikirHavuzu.Entity.Dtos.Idea
{
    public class IdeaDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TargetedBenefit { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string AddedByUserFullName { get; set; } = string.Empty;
    }
}
