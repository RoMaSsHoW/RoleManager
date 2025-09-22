using RoleManager.Domain.Entities;

namespace RoleManager.Application.Abstractions
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<List<User>> GetAllAsync();
        Task<User?> AddAsync(User user);
        Task<User?> UpdateAsync(User user);
        Task<bool> DeleteAsync(Guid id);
    }
}
