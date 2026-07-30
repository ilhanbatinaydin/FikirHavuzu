using FikirHavuzu.Repository.Context;
using FikirHavuzu.Repository.Contracts;
using FikirHavuzu.Repository.Repositories;
using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Service.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace FikirHavuzu.Web.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("FikirHavuzu.Repository"));

                options.EnableSensitiveDataLogging(true);
            });
        }

        public static void ConfigureRepositoryRegistration(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IIdeaRepository, IdeaRepository>();
            services.AddScoped<IEvaluationRepository, EvaluationRepository>();
        }

        public static void ConfigureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IIdeaService, IdeaService>();
        }

        public static void ConfigureCustomAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "FikirHavuzu.Auth";
                    options.LoginPath = new PathString("/Auth/Login");
                    options.LogoutPath = new PathString("/Auth/Logout");
                    options.AccessDeniedPath = new PathString("/Auth/AccessDenied");
                    options.ExpireTimeSpan = TimeSpan.FromHours(2);
                    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                });
        }

        public static void ConfigureCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserManagePolicy", policy =>
                    policy.RequireClaim("Permission", "User.Manage"));

                options.AddPolicy("PermissionManagePolicy", policy =>
                    policy.RequireClaim("Permission", "Permission.Manage"));

                options.AddPolicy("IdeaViewPolicy", policy =>
                    policy.RequireClaim("Permission", "Idea.View"));

                options.AddPolicy("IdeaCreatePolicy", policy =>
                    policy.RequireClaim("Permission", "Idea.Create"));

                options.AddPolicy("IdeaEvaluatePolicy", policy =>
                    policy.RequireClaim("Permission", "Idea.Evaluate"));
            });
        }

        public static void ConfigureRouting(this IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = false;
            });
        }



    }
}