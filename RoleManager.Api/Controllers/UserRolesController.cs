using Microsoft.AspNetCore.Mvc;
using RoleManager.Application.Repositories;
using RoleManager.Domain.Entities;

namespace RoleManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRolesController(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userRoles = await _userRoleRepository.GetAllAsync();
            return Ok(userRoles);
        }

        [HttpGet("{userId:guid}/{roleId:guid}")]
        public async Task<IActionResult> Get(Guid userId, Guid roleId)
        {
            var userRole = await _userRoleRepository.GetAsync(userId, roleId);
            return userRole == null ? NotFound() : Ok(userRole);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] Guid userId, [FromQuery] Guid roleId)
        {
            var newUserRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId,
            };
            var createdUserRole = await _userRoleRepository.AddAsync(newUserRole);
            if (createdUserRole == null)
            {
                return BadRequest("Could not create user role.");
            }
            return CreatedAtAction(nameof(Get), new { userId = createdUserRole.UserId, roleId = createdUserRole.RoleId }, createdUserRole);
        }

        [HttpDelete("{userId:guid}/{roleId:guid}")]
        public async Task<IActionResult> Delete(Guid userId, Guid roleId)
        {
            var deleted = await _userRoleRepository.DeleteAsync(userId, roleId);
            return deleted ? NoContent() : NotFound();
        }
    }
}
