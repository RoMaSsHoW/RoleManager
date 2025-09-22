using RoleManager.Domain.Entities;

namespace RoleManager.Application.Abstractions
{
    public interface IUserRoleRepository
    {
        Task<UserRole?> GetAsync(Guid userId, Guid roleId);
        Task<List<UserRole>> GetAllAsync();
        Task<UserRole?> AddAsync(UserRole userRole);
        Task<bool> DeleteAsync(Guid userId, Guid roleId);
    }
}
