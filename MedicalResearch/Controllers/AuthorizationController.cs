using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserManaging.API.DTOs.AuthModel;
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

                    _logger.LogInformation(HttpContext.Response.StatusCode.ToString());

                    var authModel = await GetAuthModelAsync(user, cancellationToken);

                    return Ok(authModel);
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
                var checkedUser = await _userService.FindByEmailAsync(model.Email, cancellationToken);
                var existedUserName = await _userManager.FindByNameAsync(model.UserName);

                if (checkedUser != null || existedUserName != null)
                {
                    return BadRequest("Sorry, this email or user name is already registered");
                }

                var user = new User
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = Utility.Encrypt(model.Password)
                };

                await _userService.CreateAsync(user, cancellationToken);

                await _userManager.AddToRoleAsync(user, "User");

                await AuthorizeAsync(user);

                var authModel = await GetAuthModelAsync(user, cancellationToken);

                return Ok(authModel);
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
                logout.PostLogoutRedirectUri = @"http://" + HttpContext.Request.Host.Value;

            return Redirect(logout.PostLogoutRedirectUri);
        }

        private async Task<AuthModel> GetAuthModelAsync(User user, CancellationToken cancellationToken)
        {
            var jwtToken = await GetJwtTokenForUserAsync(user);
            var userDto = await _userService.FindByEmailAsync(user.Email, cancellationToken);
            var authModel = new AuthModel(jwtToken, userDto);
            return authModel;
        }

        private async Task<string> GetJwtTokenForUserAsync(User user)
        {
            var token = new JwtSecurityToken(
                claims: new[]
                {
                    new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user)).First().ToLower()),
                    new Claim("sub", user.Email)
                },
                issuer: "API",
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(5.0));

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }

        private Task AuthorizeAsync(User user) => 
            HttpContext.SignInAsync(new IdentityServerUser(user.Email));
    }
}