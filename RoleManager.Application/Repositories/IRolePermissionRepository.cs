using RoleManager.Domain.Entities;

namespace RoleManager.Application.Repositories
{
    public interface IRolePermissionRepository
    {
        Task<RolePermission?> GetAsync(Guid roleId, Guid permissionId);
        Task<List<RolePermission>> GetAllAsync();
        Task<RolePermission?> AddAsync(RolePermission rolePermission);
        Task<bool> DeleteAsync(Guid roleId, Guid permissionId);
    }
}
