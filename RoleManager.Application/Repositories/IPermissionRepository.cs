using RoleManager.Domain.Entities;

namespace RoleManager.Application.Repositories
{
    public interface IPermissionRepository
    {
        Task<Permission?> GetByIdAsync(Guid id);
        Task<List<Permission>> GetAllAsync();
        Task<Permission?> AddAsync(Permission permission);
        Task<Permission?> UpdateAsync(Permission permission);
        Task<bool> DeleteAsync(Guid id);
    }
}
