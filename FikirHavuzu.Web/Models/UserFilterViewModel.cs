using Microsoft.AspNetCore.Mvc.Rendering;

namespace FikirHavuzu.Web.Models
{
    public class UserFilterViewModel
    {
        public string? FullName { get; set; }
        public string? IdentityNumber { get; set; }
        public int? PermissionId { get; set; }
        public bool? IsActive { get; set; }
        public SelectList? PermissionList { get; set; }
    }
}
