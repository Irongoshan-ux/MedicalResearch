using Microsoft.AspNetCore.Mvc;
using UserManaging.Domain.Entities.Users;
using UserManaging.Domain.Interfaces;

namespace UserManaging.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUserAsync(User user, CancellationToken cancellationToken)
        {
            if(await _userService.CreateAsync(user, cancellationToken))
                return Ok(user);

            return BadRequest();
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            if (await _userService.DeleteAsync(user, cancellationToken))
                return Ok();

            return BadRequest();
        }

        [HttpGet("FindByEmail")]
        public async Task<IActionResult> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var result = await _userService.FindByEmailAsync(email, cancellationToken);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUserAsync(User user, CancellationToken cancellationToken)
        {
            if (await _userService.UpdateAsync(user, cancellationToken))
                return Ok();

            return BadRequest();
        }

        [HttpPatch("SetUserName")]
        public async Task<IActionResult> SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            await _userService.SetUserNameAsync(user, userName, cancellationToken);

            return Ok();
        }

        [HttpPatch("SetNormalizedUserName")]
        public async Task<IActionResult> SetNormalizedUserNameAsync(User user,
                                                                          string normalizedName,
                                                                          CancellationToken cancellationToken)
        {
            await _userService.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
            return Ok();
        }

        [HttpGet("GetUserName")]
        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return _userService.GetUserNameAsync(user, cancellationToken);
        }

        [HttpGet("GetUserId")]
        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return _userService.GetUserIdAsync(user, cancellationToken);
        }

        [HttpGet("GetNormalizedUserName")]
        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return _userService.GetNormalizedUserNameAsync(user, cancellationToken);
        }

        [HttpGet("FindByName")]
        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _userService.FindByNameAsync(normalizedUserName, cancellationToken);
        }

        [HttpGet("FindById")]
        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _userService.FindByIdAsync(userId, cancellationToken);
        }
    }
}
