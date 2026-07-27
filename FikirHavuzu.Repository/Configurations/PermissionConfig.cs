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
                // 1. Kullanıcı Yönetimi Yetkisi
                new Permission { Id = 1, Name = "User.Manage", Description = "Kullanıcı ekleme, güncelleme, pasife alma ve listeleme yetkisi." },

                // 2. Fikir/Öneri Yönetimi Yetkileri
                new Permission { Id = 2, Name = "Idea.Create", Description = "Yeni fikir/öneri kaydı oluşturma yetkisi." },
                new Permission { Id = 3, Name = "Idea.View", Description = "Fikirleri listeleme ve filtreleme yetkisi." },
                new Permission { Id = 4, Name = "Idea.Evaluate", Description = "Fikirleri karara bağlama, açıklama yazma ve puanlama yetkisi." },

                // 3. Yetki Yönetimi Yetkileri
                new Permission { Id = 5, Name = "Permission.Grant", Description = "Diğer kullanıcılara yetki atama yetkisi." },
                new Permission { Id = 6, Name = "Permission.Revoke", Description = "Diğer kullanıcılardan yetki kaldırma yetkisi." }
            );
        }
    }
}
