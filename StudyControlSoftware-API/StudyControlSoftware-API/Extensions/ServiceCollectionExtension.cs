using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StudyControlSoftware_API.Database;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Filters;
using StudyControlSoftware_API.Interfaces;
using StudyControlSoftware_API.Services;

namespace StudyControlSoftware_API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "AllowOrigin",
                    builder =>
                    {
                        builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .SetIsOriginAllowed(_ => true);
                    });
            });
        }

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
            string connection = configuration.GetConnectionString("DefaultConnection")!;

            services.AddDbContext<ApplicationContext>(options
                => options.UseMySql(connection, serverVersion));
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        => services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // SignIn settings.
                options.SignIn.RequireConfirmedEmail = true;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@$!%*?&=#";
                options.User.RequireUniqueEmail = true;
            });
        }

        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<SetupResourceFilter>();
            });
        }

        public static void RegisterDependencies(this IServiceCollection services)
        {
            //services.AddScoped<IUserAuthenticationRepository, UserAuthenticationRepository>();
        }
    }
}
