using FikirHavuzu.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FikirHavuzu.Repository.Configurations
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(
                new Permission { Id = 1, Name = "Idea.View", Description = "Fikirleri görüntüleme yetkisi." },
                new Permission { Id = 2, Name = "Idea.Create", Description = "Fikir oluşturma yetkisi." },
                new Permission { Id = 3, Name = "Idea.Evaluate", Description = "Fikirleri karara bağlama, açıklama yazma ve puanlama yetkisi." },
                new Permission { Id = 4, Name = "User.Manage", Description = "Kullanıcı ekleme, güncelleme, pasife alma ve listeleme yetkisi." },
                new Permission { Id = 5, Name = "Permission.Manage", Description = "Kullanıcılara yetki atama ve kaldırma yetkisi." }
            );
        }
    }
}
