namespace FikirHavuzu.Entity.Dtos.Idea
{
    public class IdeaDetailDto : IdeaDto
    {
        public string AddedByUserEmail { get; set; } = string.Empty;

        public IEnumerable<IdeaDocumentDto> Documents { get; set; } = new List<IdeaDocumentDto>();
    }
}
