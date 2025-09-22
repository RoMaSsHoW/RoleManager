using RoleManager.Application.Abstractions;

namespace RoleManager.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUserRepository _userRepository;

        public PermissionService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> HasPermissionAsync(Guid userId, string permissionKey)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return false;

            var roles = user.UserRoles.Select(ur => ur.Role).ToList();
            if (!roles.Any())
                return false;

            foreach (var role in roles)
            {
                var permissions = role.RolePermissions.Select(rp => rp.Permission).ToList();
                if (permissions.Any(p => p.PermissionName == permissionKey))
                    return true;
            }
            return false;
        }
    }
}
