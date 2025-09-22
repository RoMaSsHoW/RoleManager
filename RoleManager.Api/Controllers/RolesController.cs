using Microsoft.AspNetCore.Mvc;
using RoleManager.Application.Repositories;
using RoleManager.Domain.Entities;

namespace RoleManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleRepository.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Role role)
        {
            var newRole = new Role
            {
                RoleName = role.RoleName
            };
            var createdRole = await _roleRepository.AddAsync(newRole);
            if (createdRole == null)
            {
                return BadRequest("Could not create role.");
            }
            return CreatedAtAction(nameof(GetById), new { id = createdRole.Id }, createdRole);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Role role)
        {
            var updatedRole = await _roleRepository.UpdateAsync(role);
            if (updatedRole == null)
            {
                return NotFound();
            }
            return Ok(updatedRole);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _roleRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
