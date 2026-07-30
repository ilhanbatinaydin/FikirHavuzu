using System.ComponentModel.DataAnnotations;

namespace FikirHavuzu.Entity.Dtos.Idea
{
    public class IdeaCreateDto
    {
        [Display(Name = "Fikir Başlığı")]
        [Required(ErrorMessage = "Lütfen fikriniz için anlaşılır bir başlık giriniz.")]
        [StringLength(255, ErrorMessage = "Başlık en fazla 255 karakter, en az 5 karakter olmalıdır.", MinimumLength = 5)]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Hedeflenen Fayda")]
        [Required(ErrorMessage = "Bu fikrin şirkete veya sürece sağlayacağı faydayı belirtmelisiniz.")]
        [StringLength(127, ErrorMessage = "Hedeflenen fayda alanı en fazla 127 karakter olabilir.")]
        public string TargetedBenefit { get; set; } = string.Empty;

        [Display(Name = "Fikir Detayları (Açıklama)")]
        [Required(ErrorMessage = "Lütfen fikrinizin detaylarını boş bırakmayınız.")]
        [MinLength(20, ErrorMessage = "Açıklama alanı çok kısa, lütfen fikrinizi biraz daha detaylandırınız.")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Lütfen fikrinize en uygun kategoriyi seçiniz.")]
        public int? CategoryId { get; set; }

        [Display(Name = "Destekleyici Belgeler (İsteğe Bağlı)")]
        public List<UploadedDocumentDto> UploadedDocuments { get; set; } = new List<UploadedDocumentDto>();
    }
}
