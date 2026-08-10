namespace FikirHavuzu.Entity.RequestParameters
{
    public class EvaluationRequestParameters : RequestParameters
    {
        public int IdeaId { get; set; }
        public int? Score { get; set; }
        public bool? IsApproved { get; set; }
        public string? Comment { get; set; }
        public string? FullName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        private int _pageSize = 1;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
    }
}
