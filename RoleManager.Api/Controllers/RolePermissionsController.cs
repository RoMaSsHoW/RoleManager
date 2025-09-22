using Microsoft.AspNetCore.Mvc;
using RoleManager.Application.Abstractions;
using RoleManager.Domain.Entities;

namespace RoleManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolePermissionsController : ControllerBase
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RolePermissionsController(IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rolePermissions = await _rolePermissionRepository.GetAllAsync();
            return Ok(rolePermissions);
        }

        [HttpGet("{roleId:guid}/{permissionId:guid}")]
        public async Task<IActionResult> Get(Guid roleId, Guid permissionId)
        {
            var rolePermission = await _rolePermissionRepository.GetAsync(roleId, permissionId);
            return rolePermission == null ? NotFound() : Ok(rolePermission);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] Guid roleId, [FromQuery] Guid permissionId)
        {
            var newRolePermission = new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId,
            };
            var createdRolePermission = await _rolePermissionRepository.AddAsync(newRolePermission);
            if (createdRolePermission == null)
            {
                return BadRequest("Could not create role permission.");
            }
            return CreatedAtAction(nameof(Get), new { roleId = createdRolePermission.RoleId, permissionId = createdRolePermission.PermissionId }, createdRolePermission);
        }

        [HttpDelete("{roleId:guid}/{permissionId:guid}")]
        public async Task<IActionResult> Delete(Guid roleId, Guid permissionId)
        {
            var deleted = await _rolePermissionRepository.DeleteAsync(roleId, permissionId);
            return deleted ? NoContent() : NotFound();
        }
    }
}
