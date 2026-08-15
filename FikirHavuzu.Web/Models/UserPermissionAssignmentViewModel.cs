namespace FikirHavuzu.Web.Models
{
    public class UserPermissionAssignmentViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IdentityNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public List<int> SelectedPermissionIds { get; set; } = new List<int>();
    }
}
