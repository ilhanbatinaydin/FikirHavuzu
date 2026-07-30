namespace FikirHavuzu.Entity.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();

        // Bu yetkinin İHTİYAÇ DUYDUĞU temel yetkiler (Prerequisites)
        public ICollection<PermissionDependency> RequiredPermissions { get; set; } = new List<PermissionDependency>();

        // Bu yetkiye BAĞIMLI OLAN üst düzey yetkiler (Dependents)
        public ICollection<PermissionDependency> DependentPermissions { get; set; } = new List<PermissionDependency>();
    }
}
