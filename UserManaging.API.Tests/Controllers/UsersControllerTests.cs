using System.Threading.Tasks;
using System.Threading;
using UserManaging.Domain.Entities.Users;
using UserManaging.Domain.Interfaces;
using Moq;
using Xunit;
using UserManaging.API.Controllers;
using UserManaging.API.Exceptions.User;

namespace UserManaging.API.Tests.Controllers
{
    public class UsersControllerTests
    {
        private readonly UsersController _controller;
        private readonly Mock<IUserService> _userServiceMock;

        public UsersControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();

            _controller = new UsersController(_userServiceMock.Object);
        }

        private void SetUpAdminRole()
        {
            _userServiceMock.Setup(x => x.IsInRoleAsync(null, "Admin")).Returns(Task.FromResult(true));
        }

        private void SetUpNonAdminRole()
        {
            _userServiceMock.Setup(x => x.IsInRoleAsync(null, "Admin")).Returns(Task.FromResult(false));

            _userServiceMock.Setup(x => x.FindByEmailAsync(string.Empty, CancellationToken.None))
                .Returns(Task.FromResult(new UserDTO { Email = "" }));
        }

        [Fact]
        public async void CreateUserAsync_ShouldAllowToCallFromAdminRole()
        {
            SetUpAdminRole();

            var createdUser = new User
            {
                Id = "123",
                Email = "qwerty@gmail.com",
                PasswordHash = "qwe"
            };

            var result = await _controller.CreateUserAsync(createdUser, CancellationToken.None);

            _userServiceMock.Verify(x => x.CreateAsync(createdUser, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void DeleteAsync_ShouldAllowToCallFromAdminRole()
        {
            var userEmail = string.Empty;

            SetUpAdminRole();

            var result = await _controller.DeleteAsync(userEmail, CancellationToken.None);

            _userServiceMock.Verify(x => x.DeleteAsync(userEmail, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void FindAllAsync_ShouldProvideInfoForAllUsers()
        {
            var result = await _controller.FindAllAsync(CancellationToken.None);

            _userServiceMock.Verify(x => x.FindAllAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void FindByEmailAsync_ShouldProvideInfoForAllUsers()
        {
            var userEmail = "admin@gmail.com";

            var result = await _controller.FindByEmailAsync(userEmail, CancellationToken.None);

            _userServiceMock.Verify(x => x.FindByEmailAsync(userEmail, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void UpdateUserAsync_ShouldAllowToCallFromAdminRole()
        {
            SetUpAdminRole();

            var updatedUser = new UserDTO();

            var result = await _controller.UpdateUserAsync(updatedUser, CancellationToken.None);

            _userServiceMock.Verify(x => x.UpdateAsync(updatedUser, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void UpdateUserAsync_ShouldDeclineCallWithoutAdminRole()
        {
            var updatedUser = new UserDTO();

            await Assert.ThrowsAsync<UserNotFoundException>(async () =>
            {
                await _controller.UpdateUserAsync(updatedUser, CancellationToken.None);
            });

            _userServiceMock.Verify(x => x.UpdateAsync(updatedUser, CancellationToken.None), Times.Never);
        }

        [Fact]
        public async void SetUserNameAsync_ShouldDeclineCallWithoutAdminRole()
        {
            var userName = "qwe";
            var user = new UserDTO();

            await Assert.ThrowsAsync<UserNotFoundException>(async () =>
            {
                await _controller.SetUserNameAsync(user, userName, CancellationToken.None);
            });

            _userServiceMock.Verify(x => x.SetUserNameAsync(user, userName, CancellationToken.None), Times.Never);
        }

        [Fact]
        public async void SetNormalizedUserNameAsync_ShouldDeclineCallWithoutAdminRole()
        {
            var user = new UserDTO();
            var normalizedUserName = string.Empty;

            await Assert.ThrowsAsync<UserNotFoundException>(async () =>
            {
                await _controller.SetNormalizedUserNameAsync(user, normalizedUserName, CancellationToken.None);
            });

            _userServiceMock.Verify(x => x.SetNormalizedUserNameAsync(user, normalizedUserName, CancellationToken.None),
                Times.Never);
        }

        [Fact]
        public async void GetUserNameAsync_ShouldProvideInfoForAllUsers()
        {
            var user = new User();

            var result = await _controller.GetUserNameAsync(user, CancellationToken.None);

            _userServiceMock.Verify(x => x.GetUserNameAsync(user, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void GetUserIdAsync_ShouldProvideInfoForAllUsers()
        {
            var user = new User();

            var result = await _controller.GetUserIdAsync(user, CancellationToken.None);

            _userServiceMock.Verify(x => x.GetUserIdAsync(user, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void GetNormalizedUserNameAsync_ShouldProvideInfoForAllUsers()
        {
            var user = new User();

            var result = await _controller.GetNormalizedUserNameAsync(user, CancellationToken.None);

            _userServiceMock.Verify(x => x.GetNormalizedUserNameAsync(user, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void FindByNameAsync_ShouldProvideInfoForAllUsers()
        {
            var normalizedUserName = string.Empty;

            var result = await _controller.FindByNameAsync(normalizedUserName, CancellationToken.None);

            _userServiceMock.Verify(x => x.FindByNameAsync(normalizedUserName, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void FindByIdAsync_ShouldProvideInfoForAllUsers()
        {
            var userId = "qwe";

            var result = await _controller.FindByIdAsync(userId, CancellationToken.None);

            _userServiceMock.Verify(x => x.FindByIdAsync(userId, CancellationToken.None), Times.Once);
        }
    }
}
