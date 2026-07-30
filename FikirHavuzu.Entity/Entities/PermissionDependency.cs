namespace FikirHavuzu.Entity.Entities
{
    public class PermissionDependency
    {
        // Bağımlı (Üst Düzey) Yetki
        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;

        // Gereken (Temel/Alt) Yetki
        public int RequiredPermissionId { get; set; }
        public Permission RequiredPermission { get; set; } = null!;
    }
}
