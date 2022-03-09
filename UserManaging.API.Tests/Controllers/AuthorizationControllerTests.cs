using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using UserManaging.API.Controllers;
using UserManaging.API.Tests.Helpers;
using UserManaging.Domain.Entities.Users;
using UserManaging.Domain.Interfaces;
using Xunit;

namespace UserManaging.API.Tests.Controllers
{
    public class AuthorizationControllerTests
    {
        private readonly Mock<ILogger<AuthorizationController>> _mockLogger;
        private readonly Mock<IIdentityServerInteractionService> _mockInteractionService;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IUserAuthenticaionService> _mockUserAuthService;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly AuthorizationController _controller;

        public AuthorizationControllerTests()
        {
            _mockLogger = new Mock<ILogger<AuthorizationController>>();
            _mockInteractionService = new Mock<IIdentityServerInteractionService>();
            _mockUserService = new Mock<IUserService>();
            _mockUserAuthService = new Mock<IUserAuthenticaionService>();
            _mockUserManager = MockHelpers.MockUserManager<User>();

            _controller = new AuthorizationController(_mockLogger.Object, _mockInteractionService.Object, _mockUserService.Object,
                _mockUserAuthService.Object, _mockUserManager.Object);
        }

        //[Fact]
        public async void LoginAsync_ShouldReturnAuthModel()
        {
            var userInDb = new User
            {
                Email = "admin@gmail.com",
                PasswordHash = Utilities.Utility.Encrypt("qwe"),
            };

            _mockUserService.Setup(x => x.CreateAsync(userInDb, CancellationToken.None));

            var loginCredentials = new UserLoginCredentials("admin@gmail.com", "qwe");

            var userFromDb = await _mockUserService.Object.FindByEmailAsync(loginCredentials.Email, CancellationToken.None);

            var result = await _controller.LoginAsync(loginCredentials, CancellationToken.None);

            _mockUserAuthService.Verify(x => x.AuthenticateAsync(userInDb.Email, userInDb.PasswordHash, CancellationToken.None), Times.Once);

            Assert.Equal(loginCredentials.Email, userFromDb.Email);
        }

        //[HttpPost("Register")]
        //public async Task<IActionResult> RegisterAsync(RegisterModel model, CancellationToken cancellationToken)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var checkedUser = await _userService.FindByEmailAsync(model.Email, cancellationToken);
        //        var existedUserName = await _userManager.FindByNameAsync(model.UserName);

        //        if (checkedUser != null || existedUserName != null)
        //        {
        //            return BadRequest("Sorry, this email or user name is already registered");
        //        }

        //        var user = new User
        //        {
        //            Email = model.Email,
        //            UserName = model.UserName,
        //            PhoneNumber = model.PhoneNumber,
        //            PasswordHash = Utility.Encrypt(model.Password)
        //        };

        //        await _userService.CreateAsync(user, cancellationToken);

        //        await _userManager.AddToRoleAsync(user, "User");

        //        await AuthorizeAsync(user);

        //        var authModel = await GetAuthModelAsync(user, cancellationToken);

        //        return Ok(authModel);
        //    }

        //    return BadRequest();
        //}

        //[HttpGet("Logout")]
        //public async Task<IActionResult> Logout(string? logoutId)
        //{
        //    if (logoutId is null)
        //        logoutId = await _interaction.CreateLogoutContextAsync();

        //    var logout = await _interaction.GetLogoutContextAsync(logoutId);

        //    await HttpContext.SignOutAsync();

        //    Response.Cookies.Delete(".AspNetCore.Identity.Application");

        //    if (logout.PostLogoutRedirectUri is null)
        //        logout.PostLogoutRedirectUri = @"https://" + HttpContext.Request.Host.Value;

        //    return Redirect(logout.PostLogoutRedirectUri);
        //}

        //private async Task<AuthModel> GetAuthModelAsync(User user, CancellationToken cancellationToken)
        //{
        //    var jwtToken = await GetJwtTokenForUserAsync(user);
        //    var userDto = await _userService.FindByEmailAsync(user.Email, cancellationToken);
        //    var authModel = new AuthModel(jwtToken, userDto);
        //    return authModel;
        //}

        //private async Task<string> GetJwtTokenForUserAsync(User user)
        //{
        //    var token = new JwtSecurityToken(
        //        claims: new[]
        //        {
        //            new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user)).First().ToLower()),
        //            new Claim("sub", user.Email)
        //        },
        //        issuer: "API",
        //        notBefore: DateTime.Now,
        //        expires: DateTime.Now.AddMinutes(5.0));

        //    var handler = new JwtSecurityTokenHandler();

        //    return handler.WriteToken(token);
        //}

        //private Task AuthorizeAsync(User user) =>
        //    HttpContext.SignInAsync(new IdentityServerUser(user.Email));
    }
}