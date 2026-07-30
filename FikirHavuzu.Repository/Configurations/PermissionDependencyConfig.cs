using FikirHavuzu.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FikirHavuzu.Repository.Configurations
{
    public class PermissionDependencyConfig : IEntityTypeConfiguration<PermissionDependency>
    {
        public void Configure(EntityTypeBuilder<PermissionDependency> builder)
        {
            builder.HasKey(pd => new { pd.PermissionId, pd.RequiredPermissionId });

            builder.HasOne(pd => pd.Permission)
                   .WithMany(p => p.RequiredPermissions)
                   .HasForeignKey(pd => pd.PermissionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pd => pd.RequiredPermission)
                   .WithMany(p => p.DependentPermissions)
                   .HasForeignKey(pd => pd.RequiredPermissionId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasData(
                // 1. Idea.Create (2), Idea.View (1) gerektirir
                new PermissionDependency { PermissionId = 2, RequiredPermissionId = 1 },

                // 2. Idea.Evaluate (3), Idea.Create (2) ve Idea.View (1) gerektirir
                new PermissionDependency { PermissionId = 3, RequiredPermissionId = 2 },
                new PermissionDependency { PermissionId = 3, RequiredPermissionId = 1 },

                // 3. Permission.Manage (5), User.Manage (4) gerektirir
                new PermissionDependency { PermissionId = 5, RequiredPermissionId = 4 }
            );
        }
    }
}
