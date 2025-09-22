namespace RoleManager.Application.Services
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(Guid userId, string permissionKey);
    }
}
