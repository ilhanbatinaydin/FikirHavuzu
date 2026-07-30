using FikirHavuzu.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FikirHavuzu.Repository.Configurations
{
    public class UserPermissionConfig : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasKey(up => new { up.UserId, up.PermissionId });

            builder.HasOne(up => up.User)
                   .WithMany(u => u.UserPermissions)
                   .HasForeignKey(up => up.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(up => up.Permission)
                   .WithMany(p => p.UserPermissions)
                   .HasForeignKey(up => up.PermissionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new UserPermission { UserId = 1, PermissionId = 1 },
                new UserPermission { UserId = 1, PermissionId = 2 },
                new UserPermission { UserId = 1, PermissionId = 3 },
                new UserPermission { UserId = 1, PermissionId = 4 },
                new UserPermission { UserId = 1, PermissionId = 5 }
            );
        }
    }
}