namespace FikirHavuzu.Web.Infrastructure.Extensions
{
    public static class UIExtensions
    {
        public static string GetCategoryBadgeClass(this string categoryName)
        {
            return categoryName switch
            {
                "Ürün" => "bg-success text-white",
                "Hizmet" => "bg-warning text-dark",
                "Süreç" => "bg-danger text-white",
                _ => "bg-secondary text-white"
            };
        }

        public static string GetFileIconClass(this string extension)
        {
            return extension switch
            {
                ".pdf" => "fa-file-pdf text-danger",
                ".doc" or ".docx" => "fa-file-word text-primary",
                ".xls" or ".xlsx" => "fa-file-excel text-success",
                ".jpg" or ".jpeg" or ".png" => "fa-file-image text-info",
                ".zip" or ".rar" => "fa-file-zipper text-warning",
                ".ppt" or ".pptx" => "fa-file-powerpoint text-warning",
                _ => "fa-file text-secondary"
            };
        }
    }
}
