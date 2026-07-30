namespace FikirHavuzu.Entity.RequestParameters
{
    public class IdeaRequestParameters : RequestParameters
    {
        // Fikir başlığı veya açıklamasında arama
        public string? SearchQuery { get; set; }

        // Ekleyen kişinin Adı-Soyadı içinde arama
        public string? FullName { get; set; }

        // Dropdown'dan seçilen spesifik Kategori ID'si
        public int? CategoryId { get; set; }

        // Belirli bir tarihte oluşturulan fikirler
        public DateTime? FilterDate { get; set; }

        // Varsayılan olarak ekranda 6 kart gösterilecek
        private int _pageSize = 9;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
    }
}
