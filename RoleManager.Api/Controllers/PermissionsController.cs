using Microsoft.AspNetCore.Mvc;
using RoleManager.Application.Abstractions;
using RoleManager.Domain.Entities;

namespace RoleManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionsController(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var permissions = await _permissionRepository.GetAllAsync();
            return Ok(permissions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var permission = await _permissionRepository.GetByIdAsync(id);
            if (permission == null)
            {
                return NotFound();
            }
            return Ok(permission);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Permission permission)
        {
            var newPermission = new Permission
            {
                PermissionName = permission.PermissionName,
                Description = permission.Description
            };

            var createdPermission = await _permissionRepository.AddAsync(newPermission);
            if (createdPermission == null)
            {
                return BadRequest("Could not create permission.");
            }
            return CreatedAtAction(nameof(GetById), new { id = createdPermission.Id }, createdPermission);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Permission permission)
        {
            var updatedPermission = await _permissionRepository.UpdateAsync(permission);
            if (updatedPermission == null)
            {
                return NotFound();
            }
            return Ok(updatedPermission);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _permissionRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
