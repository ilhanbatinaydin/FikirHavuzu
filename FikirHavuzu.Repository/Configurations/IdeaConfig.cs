using FikirHavuzu.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FikirHavuzu.Repository.Configurations
{
    public class IdeaConfig : IEntityTypeConfiguration<Idea>
    {
        public void Configure(EntityTypeBuilder<Idea> builder)
        {
            builder.HasData(
                new Idea() { Id = 1, Title = "ASP.NET CORE MİMARİSİ 1", TargetedBenefit = "Mimari iyileştirmesi", Description = "Bu mimariye geçiş yapmak daha iyi olur", UserId = 1, CategoryId = 1 },
                new Idea() { Id = 2, Title = "ASP.NET CORE MİMARİSİ 2", TargetedBenefit = "Mimari iyileştirmesi", Description = "Bu mimariye geçiş yapmak daha iyi olur", UserId = 1, CategoryId = 1 },
                new Idea() { Id = 3, Title = "ASP.NET CORE MİMARİSİ 3", TargetedBenefit = "Mimari iyileştirmesi", Description = "Bu mimariye geçiş yapmak daha iyi olur", UserId = 1, CategoryId = 1 },
                new Idea() { Id = 4, Title = "ASP.NET CORE MİMARİSİ 4", TargetedBenefit = "Mimari iyileştirmesi", Description = "Bu mimariye geçiş yapmak daha iyi olur", UserId = 1, CategoryId = 2 },
                new Idea() { Id = 5, Title = "ASP.NET CORE MİMARİSİ 5", TargetedBenefit = "Mimari iyileştirmesi", Description = "Bu mimariye geçiş yapmak daha iyi olur", UserId = 1, CategoryId = 2 },
                new Idea() { Id = 6, Title = "ASP.NET CORE MİMARİSİ 6", TargetedBenefit = "Mimari iyileştirmesi", Description = "Bu mimariye geçiş yapmak daha iyi olur", UserId = 1, CategoryId = 2 },
                new Idea() { Id = 7, Title = "ASP.NET CORE MİMARİSİ 7", TargetedBenefit = "Mimari iyileştirmesi", Description = "Bu mimariye geçiş yapmak daha iyi olur", UserId = 1, CategoryId = 2 },
                new Idea() { Id = 8, Title = "ASP.NET CORE MİMARİSİ 8", TargetedBenefit = "Mimari iyileştirmesi", Description = "Bu mimariye geçiş yapmak daha iyi olur", UserId = 1, CategoryId = 3 },
                new Idea() { Id = 9, Title = "ASP.NET CORE MİMARİSİ 9", TargetedBenefit = "Mimari iyileştirmesi", Description = "Bu mimariye geçiş yapmak daha iyi olur", UserId = 1, CategoryId = 3 },
                new Idea() { Id = 10, Title = "ASP.NET CORE MİMARİSİ 10", TargetedBenefit = "Mimari iyileştirmesi", Description = "Bu mimariye geçiş yapmak daha iyi olur", UserId = 1, CategoryId = 3 }
            );
        }
    }
}
