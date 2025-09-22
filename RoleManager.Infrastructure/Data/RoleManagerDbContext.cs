using Microsoft.EntityFrameworkCore;
using RoleManager.Domain.Entities;
using RoleManager.Infrastructure.Data.Configurations;

namespace RoleManager.Infrastructure.Data
{
    public class RoleManagerDbContext : DbContext
    {
        public RoleManagerDbContext(DbContextOptions<RoleManagerDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
