using RoleManager.Domain.Entities;

namespace RoleManager.Application.Abstractions
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(Guid id);
        Task<List<Role>> GetAllAsync();
        Task<Role?> AddAsync(Role role);
        Task<Role?> UpdateAsync(Role role);
        Task<bool> DeleteAsync(Guid id);
    }
}
