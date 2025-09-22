using Microsoft.EntityFrameworkCore;
using RoleManager.Application.Repositories;
using RoleManager.Domain.Entities;
using RoleManager.Infrastructure.Data;

namespace RoleManager.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly RoleManagerDbContext _context;

        public PermissionRepository(RoleManagerDbContext context)
        {
            _context = context;
        }

        public async Task<Permission?> GetByIdAsync(Guid id)
        {
            return await _context.Permissions
                .Include(p => p.RolePermissions)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Permission>> GetAllAsync()
        {
            return await _context.Permissions
                .Include(p => p.RolePermissions)
                .ToListAsync();
        }

        public async Task<Permission?> AddAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();
            return permission;
        }

        public async Task<Permission?> UpdateAsync(Permission permission)
        {
            var existingPermission = await _context.Permissions.FindAsync(permission.Id);
            if (existingPermission == null)
                return null;

            _context.Entry(existingPermission).CurrentValues.SetValues(permission);
            await _context.SaveChangesAsync();
            return existingPermission;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
                return false;

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
