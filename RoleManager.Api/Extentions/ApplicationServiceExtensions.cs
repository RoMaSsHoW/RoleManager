using Microsoft.EntityFrameworkCore;
using RoleManager.Application.Repositories;
using RoleManager.Infrastructure.Data;
using RoleManager.Infrastructure.Repositories;

namespace RoleManager.Api.Extentions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureRepositories(services);

            ConfigureServices(services);

            ConfigureDatabase(services, configuration);

            return services;
        }

        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RoleManagerDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("PostgresqlDbConnection"));
            });
        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IAuthorService, AuthorService>();
            //services.AddScoped<IFileStorageService, FileStorageService>();
        }
    }
}
