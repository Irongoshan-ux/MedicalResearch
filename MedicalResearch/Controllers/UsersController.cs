using Microsoft.AspNetCore.Mvc;
using UserManaging.API.Utilities;
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
            if (await IsCurrentUserAdminAsync())
            {
                if (await _userService.CreateAsync(user, cancellationToken))
                    return Ok(user);

                else return StatusCode(StatusCodes.Status500InternalServerError, "User has been already registered.");
            }
            
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteAsync(string userEmail, CancellationToken cancellationToken)
        {
            if (await IsCurrentUserAdminAsync())
            {
                if (await _userService.DeleteAsync(userEmail, cancellationToken))
                    return Ok();
            }

            return StatusCode(StatusCodes.Status403Forbidden);
        }

        [HttpGet("FindAll")]
        public async Task<IActionResult> FindAllAsync(CancellationToken cancellationToken)
        {
            var result = await _userService.FindAllAsync(cancellationToken);

            if (result is null)
                return NotFound();

            return Ok(result);
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
        public async Task<IActionResult> UpdateUserAsync(UserDTO user, CancellationToken cancellationToken)
        {
            if (await IsCurrentUserAdminAsync() || (await GetCurrentUserAsync()).Email.Equals(user.Email))
            {
                if (await _userService.UpdateAsync(user, cancellationToken))
                    return Ok();
            }
            
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        [HttpPatch("SetUserName")]
        public async Task<IActionResult> SetUserNameAsync(UserDTO user, string userName, CancellationToken cancellationToken)
        {
            var currentUser = await GetCurrentUserAsync();

            if (currentUser.Email.Equals(user.Email) || await IsCurrentUserAdminAsync())
            {
                await _userService.SetUserNameAsync(user, userName, cancellationToken);
                return Ok();
            }

            return Forbid();
        }

        [HttpPatch("SetNormalizedUserName")]
        public async Task<IActionResult> SetNormalizedUserNameAsync(UserDTO user,
                                                                          string normalizedName,
                                                                          CancellationToken cancellationToken)
        {
            var currentUser = await GetCurrentUserAsync();

            if (currentUser.Email.Equals(user.Email) || await IsCurrentUserAdminAsync())
            {
                await _userService.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
                return Ok();
            }

            return Forbid();
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
        public Task<UserDTO> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _userService.FindByNameAsync(normalizedUserName, cancellationToken);
        }

        [HttpGet("FindById")]
        public Task<UserDTO> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _userService.FindByIdAsync(userId, cancellationToken);
        }

        private async Task<bool> IsCurrentUserAdminAsync()
        {
            var currentUser = await GetCurrentUserAsync();

            return await IsUserAdminAsync(currentUser);
        }

        private async Task<bool> IsUserAdminAsync(UserDTO user) => await _userService.IsInRoleAsync(user, "Admin");

        private async Task<UserDTO> GetCurrentUserAsync() => await HttpUserHelper.GetCurrentUserAsync(_userService, HttpContext);
    }
}
