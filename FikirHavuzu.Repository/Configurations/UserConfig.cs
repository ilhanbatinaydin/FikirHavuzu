using FikirHavuzu.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FikirHavuzu.Repository.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var adminUser = new User
            {
                Id = 1,
                FirstName = "Sistem",
                LastName = "Yöneticisi",
                Email = "admin@fikirhavuzu.com",
                PhoneNumber = "05555555555",
                RegistrationNumber = "0001",
                IdentityNumber = "11111111111",
                IsActive = true
            };

            adminUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!");

            builder.HasData(adminUser);
        }
    }
}