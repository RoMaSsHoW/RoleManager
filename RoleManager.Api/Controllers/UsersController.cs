using Microsoft.AspNetCore.Mvc;
using RoleManager.Application.Abstractions;
using RoleManager.Domain.Entities;

namespace RoleManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            var newUser = new User
            {
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
            };
            var createdUser = await _userRepository.AddAsync(newUser);
            if (createdUser == null)
            {
                return BadRequest("Could not create user.");
            }
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            var updatedUser = await _userRepository.UpdateAsync(user);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userRepository.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
