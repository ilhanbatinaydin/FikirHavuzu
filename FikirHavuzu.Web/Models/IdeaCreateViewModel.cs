using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FikirHavuzu.Web.Models
{
    public class IdeaCreateViewModel
    {
        [Display(Name = "Fikir Başlığı")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Hedeflenen Fayda")]
        public string TargetedBenefit { get; set; } = string.Empty;

        [Display(Name = "Fikir Detayları (Açıklama)")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Kategori")]
        public int? CategoryId { get; set; }

        [Display(Name = "Destekleyici Belgeler (İsteğe Bağlı)")]
        public List<IFormFile>? Documents { get; set; }

        public SelectList? CategoryList { get; set; }
    }
}
