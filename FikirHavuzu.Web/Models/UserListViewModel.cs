using FikirHavuzu.Entity.Dtos.User;

namespace FikirHavuzu.Web.Models
{
    public class UserListViewModel
    {
        public IEnumerable<UserDto> Users { get; set; } = new List<UserDto>();

        public Pagination Pagination { get; set; }

    }
}
