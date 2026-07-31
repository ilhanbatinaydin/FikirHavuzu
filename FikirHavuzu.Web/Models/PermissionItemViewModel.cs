namespace FikirHavuzu.Web.Models
{
    public class PermissionItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsAssigned { get; set; }

        public string Description { get; set; }

        public string DependencyIdsAsJson { get; set; }
    }
}
