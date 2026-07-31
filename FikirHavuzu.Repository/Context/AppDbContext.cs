using FikirHavuzu.Entity.Entities;
using FikirHavuzu.Repository.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FikirHavuzu.Repository.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<IdeaDocument> IdeaDocuments { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<PermissionDependency> PermissionDependencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cascade path hatasını engellemek için gerekli kısıtlamalar
            modelBuilder.Entity<Evaluation>()
                .HasOne(e => e.EvaluatedByUser)
                .WithMany(u => u.Evaluations)
                .HasForeignKey(e => e.EvaluatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Evaluation>()
                .HasOne(e => e.Idea)
                .WithMany(i => i.Evaluations)
                .HasForeignKey(e => e.IdeaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
