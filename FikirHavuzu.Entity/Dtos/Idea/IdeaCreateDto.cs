using System.ComponentModel.DataAnnotations;

namespace FikirHavuzu.Entity.Dtos.Idea
{
    public class IdeaCreateDto
    {
        public string Title { get; set; } = string.Empty;

        public string TargetedBenefit { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public List<UploadedDocumentDto> UploadedDocuments { get; set; } = new List<UploadedDocumentDto>();
    }
}
