using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleManager.Application.Services;

namespace RoleManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public TestsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts(Guid userId)
        {
            if (!await _permissionService.HasPermissionAsync(userId, "Bla Bla"))
                return BadRequest();

            return Ok(new { message = "Вот список постов" });
        }
    }
}
