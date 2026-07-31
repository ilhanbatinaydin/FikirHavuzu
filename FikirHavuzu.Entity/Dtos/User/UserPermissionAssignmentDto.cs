namespace FikirHavuzu.Entity.Dtos.User
{
    public class UserPermissionAssignmentDto : UserDto
    {
        public List<int> SelectedPermissionIds { get; set; } = new List<int>();
        public string FullName => $"{FirstName} {LastName}";
    }
}
