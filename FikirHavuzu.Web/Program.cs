using FikirHavuzu.Repository.Context;
using FikirHavuzu.Web.Infrastructure.Extensions;
using FluentValidation;

namespace FikirHavuzu.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.ConfigureDbContext(builder.Configuration);
            builder.Services.ConfigureRepositoryRegistration();
            builder.Services.ConfigureServiceRegistration();
            builder.Services.ConfigureCustomAuthentication();
            builder.Services.ConfigureCustomAuthorization();
            builder.Services.ConfigureRouting();

            var app = builder.Build();

            app.ConfigureLocalization();

            app.ConfigureAndCheckMigration<AppDbContext>();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}