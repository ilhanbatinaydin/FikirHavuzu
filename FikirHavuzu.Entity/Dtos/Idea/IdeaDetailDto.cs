namespace FikirHavuzu.Entity.Dtos.Idea
{
    public class IdeaDetailDto : IdeaDto
    {
        public IEnumerable<IdeaDocumentDto> Documents { get; set; } = new List<IdeaDocumentDto>();
    }
}
