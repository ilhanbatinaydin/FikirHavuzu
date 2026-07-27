namespace FikirHavuzu.Entity.Entities
{
    public class IdeaDocument
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public int IdeaId { get; set; }
        public Idea Idea { get; set; } = null!;
    }
}
