using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoleManager.Domain.Entities;

namespace RoleManager.Infrastructure.Data.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("permissions");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();
            builder.Property(p => p.PermissionName)
                .IsRequired()
                .HasColumnName("permission_name")
                .HasMaxLength(100);
            builder.Property(p => p.Description)
                .HasColumnName("description")
                .HasMaxLength(500);

            builder.HasMany(p => p.RolePermissions)
                .WithOne(rp => rp.Permission)
                .HasForeignKey(rp => rp.PermissionId);
        }
    }
}
