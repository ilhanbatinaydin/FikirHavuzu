namespace FikirHavuzu.Entity.RequestParameters
{
    public class UserRequestParameters : RequestParameters
    {
        public string? FullName { get; set; }

        public string? IdentityNumber { get; set; }

        public int? PermissionId { get; set; }

        public bool? IsActive { get; set; }

        private int _pageSize = 10;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
    }
}