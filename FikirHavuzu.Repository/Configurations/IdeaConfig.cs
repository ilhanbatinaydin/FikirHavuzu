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
                new Idea() { Id = 1, Title = "Fikir 1", TargetedBenefit = "Hedef 1", Description = "Açıklama 1", UserId = 1, CategoryId = 1 },
                new Idea() { Id = 2, Title = "Fikir 2", TargetedBenefit = "Hedef 2", Description = "Açıklama 2", UserId = 1, CategoryId = 1 },
                new Idea() { Id = 3, Title = "Fikir 3", TargetedBenefit = "Hedef 3", Description = "Açıklama 3", UserId = 1, CategoryId = 1 },
                new Idea() { Id = 4, Title = "Fikir 4", TargetedBenefit = "Hedef 4", Description = "Açıklama 4", UserId = 1, CategoryId = 2 },
                new Idea() { Id = 5, Title = "Fikir 5", TargetedBenefit = "Hedef 5", Description = "Açıklama 5", UserId = 1, CategoryId = 2 },
                new Idea() { Id = 6, Title = "Fikir 6", TargetedBenefit = "Hedef 6", Description = "Açıklama 6", UserId = 1, CategoryId = 2 },
                new Idea() { Id = 7, Title = "Fikir 7", TargetedBenefit = "Hedef 7", Description = "Açıklama 7", UserId = 1, CategoryId = 2 },
                new Idea() { Id = 8, Title = "Fikir 8", TargetedBenefit = "Hedef 8", Description = "Açıklama 8", UserId = 1, CategoryId = 3 },
                new Idea() { Id = 9, Title = "Fikir 9", TargetedBenefit = "Hedef 9", Description = "Açıklama 9", UserId = 1, CategoryId = 3 },
                new Idea() { Id = 10, Title = "Fikir 10", TargetedBenefit = "Hedef 10", Description = "Açıklama 10", UserId = 1, CategoryId = 3 }
            );
        }
    }
}
