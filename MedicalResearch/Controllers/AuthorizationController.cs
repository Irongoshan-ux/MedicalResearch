using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManaging.API.DTOs.Users;
using UserManaging.API.Utilities;
using UserManaging.Domain.Entities.Users;
using UserManaging.Domain.Interfaces;

namespace UserManaging.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IUserService _userService;
        private readonly IUserAuthenticaionService _userAuthService;
        private readonly UserManager<User> _userManager;

        public AuthorizationController(ILogger<AuthorizationController> logger,
                                       IIdentityServerInteractionService interaction,
                                       IUserService userService,
                                       IUserAuthenticaionService userAuthService,
                                       UserManager<User> userManager)
        {
            _logger = logger;
            _interaction = interaction;
            _userService = userService;
            _userAuthService = userAuthService;
            _userManager = userManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(UserLoginCredentials credentials,
                                                    CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var user = await _userAuthService.AuthenticateAsync(credentials.Email,
                    Utility.Encrypt(credentials.Password), cancellationToken);

                if (user != null)
                {
                    await AuthorizeAsync(user);

                    _logger.LogInformation($"User has succesfully logged in with email: {user.Email}");

                    return Ok();
                }
            }

            _logger.LogInformation($"User {credentials.Email} failed to log in.");

            return Unauthorized();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var checkedUser = await _userService.FindByNameAsync(model.UserName, cancellationToken);

                if (checkedUser != null)
                {
                    return BadRequest("Sorry, this email is already registered");
                }

                var user = new User
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    PhoneNumber = model.PhoneNumber
                };

                await _userService.CreateAsync(user, cancellationToken);

                await _userManager.AddToRoleAsync(user, "User");

                await AuthorizeAsync(user);

                return Ok("Successfully authorized");
            }

            return BadRequest();
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout(string? logoutId)
         {
            if (logoutId is null)
                logoutId = await _interaction.CreateLogoutContextAsync();

            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            await HttpContext.SignOutAsync();

            Response.Cookies.Delete(".AspNetCore.Identity.Application");

            if (logout.PostLogoutRedirectUri is null)
                logout.PostLogoutRedirectUri = @"https://" + HttpContext.Request.Host.Value;

            return Redirect(logout.PostLogoutRedirectUri);
        }

        private Task AuthorizeAsync(User user) => 
            HttpContext.SignInAsync(new IdentityServerUser(user.Email));
    }
}