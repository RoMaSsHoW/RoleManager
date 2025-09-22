using Microsoft.EntityFrameworkCore;
using RoleManager.Application.Repositories;
using RoleManager.Domain.Entities;
using RoleManager.Infrastructure.Data;

namespace RoleManager.Infrastructure.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly RoleManagerDbContext _context;

        public RolePermissionRepository(RoleManagerDbContext context)
        {
            _context = context;
        }

        public async Task<RolePermission?> GetAsync(Guid roleId, Guid permissionId)
        {
            return await _context.RolePermissions
                .Include(rp => rp.Role)
                .Include(rp => rp.Permission)
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
        }

        public async Task<List<RolePermission>> GetAllAsync()
        {
            return await _context.RolePermissions
                .Include(rp => rp.Role)
                .Include(rp => rp.Permission)
                .ToListAsync();
        }

        public async Task<RolePermission?> AddAsync(RolePermission rolePermission)
        {
            await _context.RolePermissions.AddAsync(rolePermission);
            await _context.SaveChangesAsync();
            return rolePermission;
        }

        public async Task<bool> DeleteAsync(Guid roleId, Guid permissionId)
        {
            var rolePermission = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
            if (rolePermission == null)
                return false;

            _context.RolePermissions.Remove(rolePermission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
