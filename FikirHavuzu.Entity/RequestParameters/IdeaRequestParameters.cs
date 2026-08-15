namespace FikirHavuzu.Entity.RequestParameters
{
    public class IdeaRequestParameters : RequestParameters
    {
        public string? SearchQuery { get; set; }

        public string? Email { get; set; }

        public string? FullName { get; set; }

        public int? CategoryId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        private int _pageSize = 9;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
    }
}
