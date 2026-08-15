using FikirHavuzu.Repository.Context;
using FikirHavuzu.Repository.Contracts;
using FikirHavuzu.Repository.Repositories;
using FikirHavuzu.Service.Contracts;
using FikirHavuzu.Service.Services;
using FikirHavuzu.Web.Security.Handlers;
using FikirHavuzu.Web.Security.Requirements;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

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
            services.AddScoped<IPermissionRepository, PermissionRepository>();
        }

        public static void ConfigureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IIdeaService, IdeaService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEvaluationService, EvaluationService>();
        }

        public static void ConfigureCustomAuthentication(this IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "FikirHavuzu.Auth";
                    options.LoginPath = new PathString("/Auth/Login");
                    options.LogoutPath = new PathString("/Auth/Logout");
                    options.AccessDeniedPath = new PathString("/Auth/AccessDenied");
                    options.ExpireTimeSpan = TimeSpan.FromHours(2);
                    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;

                    options.Events = new CookieAuthenticationEvents
                    {
                        OnValidatePrincipal = async context =>
                        {
                            var userIdClaim = context.Principal?.FindFirst(ClaimTypes.NameIdentifier);

                            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                            {
                                var cache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();
                                var cacheKey = $"UserNeedsRefresh_{userId}";

                                if (cache.TryGetValue(cacheKey, out _))
                                {
                                    var serviceManager = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>();

                                    var updatedUser = await serviceManager.UserService.GetOneUserByIdAsync(userId, false);

                                    if (updatedUser == null || !updatedUser.IsActive)
                                    {
                                        context.RejectPrincipal();
                                        await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                                        cache.Remove(cacheKey);
                                        return;
                                    }

                                    var claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.NameIdentifier, updatedUser.Id.ToString()),
                                        new Claim(ClaimTypes.Name, updatedUser.FirstName),
                                        new Claim(ClaimTypes.Surname, updatedUser.LastName),
                                        new Claim(ClaimTypes.Email, updatedUser.Email)
                                    };

                                    var userPermissionIds = await serviceManager.UserService.GetUserPermissionIdsAsync(userId);

                                    var allPermissions = await serviceManager.PermissionService.GetAllPermissionsWithDependenciesAsync(trackChanges: false);

                                    foreach (var permId in userPermissionIds)
                                    {
                                        var permission = allPermissions.FirstOrDefault(p => p.Id == permId);
                                        if (permission != null)
                                        {
                                            claims.Add(new Claim("Permission", permission.Name));
                                        }
                                    }

                                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                                    context.ReplacePrincipal(new ClaimsPrincipal(identity));

                                    context.ShouldRenew = true;

                                    cache.Remove(cacheKey);
                                }
                            }
                        }
                    };
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

                options.AddPolicy("ProfileAccessPolicy", policy =>
                    policy.Requirements.Add(new ProfileAccessRequirement()));
            });

            services.AddScoped<IAuthorizationHandler, ProfileAccessHandler>();
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