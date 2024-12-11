using Moq;
using Nezam.Modular.ESS.Identity.Domain.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.Roles;
using Nezam.Modular.ESS.Identity.Domain.Shared.User;
using Nezam.Modular.ESS.Identity.Domain.User;

namespace Nezam.Modular.ESS.Identity.Domain.Tests
{
    public class UserDomainServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IRoleRepository> _mockRoleRepository;
        private readonly UserDomainService _userDomainService;

        public UserDomainServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockRoleRepository = new Mock<IRoleRepository>();
            _userDomainService = new UserDomainService(_mockUserRepository.Object, _mockRoleRepository.Object);
        }

        #region AssignRoleAsync Tests

        [Fact]
        public async Task AssignRoleAsync_ShouldAssignRole_WhenValidUserAndRole()
        {
            // Arrange
            var userId = UserId.NewId();
            var userName = new UserNameValue("TestUser");
            var password = new UserPasswordValue("TestPassword123");
            var profile = new UserProfileValue("asda","asdasd","Test Profile");
            var user = new UserEntity(userId, userName, password, profile);

            var roleId = RoleId.NewId("admin");  // Specify role ID with a string argument
            var role = new RoleEntity(roleId, "Admin");

            var roles = new[] { roleId };

            _mockRoleRepository.Setup(r => r.GetRoleByIdAsync(roleId))
                .ReturnsAsync(role);

            _mockUserRepository.Setup(r => r.UpdateAsync(It.IsAny<UserEntity>(), It.IsAny<bool>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _userDomainService.AssignRoleAsync(user, roles);

            // Assert
            Assert.True(result.IsSuccess);
            _mockUserRepository.Verify(r => r.UpdateAsync(user, true), Times.Once);
        }

        [Fact]
        public async Task AssignRoleAsync_ShouldReturnFailure_WhenUserIsNull()
        {
            // Arrange
            UserEntity user = null;
            var role = RoleId.NewId("admin");

            // Act
            var result = await _userDomainService.AssignRoleAsync(user, role);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task AssignRoleAsync_ShouldReturnFailure_WhenRoleNotFound()
        {
            // Arrange
            var userId = UserId.NewId();
            var userName = new UserNameValue("TestUser");
            var password = new UserPasswordValue("TestPassword123");
            var profile = new UserProfileValue("asda","asdasd","Test Profile");
            var user = new UserEntity(userId, userName, password, profile);

            var roleId = RoleId.NewId("admin");
            var roles = new[] { roleId };

            _mockRoleRepository.Setup(r => r.GetRoleByIdAsync(roleId))
                .ReturnsAsync((RoleEntity)null);  // Role not found

            // Act
            var result = await _userDomainService.AssignRoleAsync(user, roles);

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task AssignRoleAsync_ShouldNotUpdate_WhenRoleAlreadyAssigned()
        {
            // Arrange
            var userId = UserId.NewId();
            var userName = new UserNameValue("TestUser");
            var password = new UserPasswordValue("TestPassword123");
            var profile = new UserProfileValue("asda","asdasd","Test Profile");
            var user = new UserEntity(userId, userName, password, profile);

            var roleId = RoleId.NewId("admin");
            var role = new RoleEntity(roleId, "Admin");
            user.AddRole(role);  // Role already assigned

            var roles = new[] { roleId };

            _mockRoleRepository.Setup(r => r.GetRoleByIdAsync(roleId))
                .ReturnsAsync(role);

            // Act
            var result = await _userDomainService.AssignRoleAsync(user, roles);

            // Assert
            Assert.True(result.IsSuccess);
            _mockUserRepository.Verify(r => r.UpdateAsync(It.IsAny<UserEntity>(), It.IsAny<bool>()), Times.Never);
        }

        #endregion

        #region UnassignRole Tests

        [Fact]
        public async Task UnassignRole_ShouldUnassignRole_WhenRoleExists()
        {
            // Arrange
            var userId = UserId.NewId();
            var userName = new UserNameValue("TestUser");
            var password = new UserPasswordValue("TestPassword123");
            var profile = new UserProfileValue("asda","asdasd","Test Profile");
            var user = new UserEntity(userId, userName, password, profile);

            var roleId = RoleId.NewId("admin");
            var role = new RoleEntity(roleId, "Admin");
            user.AddRole(role);

            _mockRoleRepository.Setup(r => r.GetRoleByIdAsync(roleId))
                .ReturnsAsync(role);

            _mockUserRepository.Setup(r => r.UpdateAsync(It.IsAny<UserEntity>(), It.IsAny<bool>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _userDomainService.UnassignRole(user, roleId);

            // Assert
            Assert.True(result.IsSuccess);
            _mockUserRepository.Verify(r => r.UpdateAsync(user, true), Times.Once);
        }

        [Fact]
        public async Task UnassignRole_ShouldReturnFailure_WhenUserHasNoRole()
        {
            // Arrange
            var userId = UserId.NewId();
            var userName = new UserNameValue("TestUser");
            var password = new UserPasswordValue("TestPassword123");
            var profile = new UserProfileValue("asda","asdasd","Test Profile");
            var user = new UserEntity(userId, userName, password, profile);

            var roleId = RoleId.NewId("admin");  // Role not assigned to user

            // Act
            var result = await _userDomainService.UnassignRole(user, roleId);

            // Assert
            Assert.False(result.IsSuccess);

        }

        [Fact]
        public async Task UnassignRole_ShouldReturnFailure_WhenRoleNotFound()
        {
            // Arrange
            var userId = UserId.NewId();
            var userName = new UserNameValue("TestUser");
            var password = new UserPasswordValue("TestPassword123");
            var profile = new UserProfileValue("asda","asdasd","Test Profile");
            var user = new UserEntity(userId, userName, password, profile);

            var roleId = RoleId.NewId("admin");  // Role not found

            _mockRoleRepository.Setup(r => r.GetRoleByIdAsync(roleId))
                .ReturnsAsync((RoleEntity)null);

            // Act
            var result = await _userDomainService.UnassignRole(user, roleId);

            // Assert
            Assert.False(result.IsSuccess);
        }

        #endregion

        #region UpdateRoles Tests

        [Fact]
        public async Task UpdateRoles_ShouldUpdateRoles_WhenValidRolesProvided()
        {
            // Arrange
            var userId = UserId.NewId();
            var userName = new UserNameValue("TestUser");
            var password = new UserPasswordValue("TestPassword123");
            var profile = new UserProfileValue("asda","asdasd","Test Profile");
            var user = new UserEntity(userId, userName, password, profile);

            var role1Id = RoleId.NewId("admin");
            var role2Id = RoleId.NewId("manager");
            var role1 = new RoleEntity(role1Id, "Admin");
            var role2 = new RoleEntity(role2Id, "Manager");

            var roles = new[] { role1Id, role2Id };

            _mockRoleRepository.Setup(r => r.GetRoleByIdAsync(role1Id))
                .ReturnsAsync(role1);
            _mockRoleRepository.Setup(r => r.GetRoleByIdAsync(role2Id))
                .ReturnsAsync(role2);

            _mockUserRepository.Setup(r => r.UpdateAsync(It.IsAny<UserEntity>(), It.IsAny<bool>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _userDomainService.UpdateRoles(user, roles);

            // Assert
            Assert.True(result.IsSuccess);
            _mockUserRepository.Verify(r => r.UpdateAsync(user, true), Times.Once);
        }

        [Fact]
        public async Task UpdateRoles_ShouldReturnFailure_WhenRolesAreIdentical()
        {
            // Arrange
            var userId = UserId.NewId();
            var userName = new UserNameValue("TestUser");
            var password = new UserPasswordValue("TestPassword123");
            var profile = new UserProfileValue("asda","asdasd","Test Profile");
            var user = new UserEntity(userId, userName, password, profile);

            var roleId = RoleId.NewId("admin");
            var role = new RoleEntity(roleId, "Admin");

            user.AddRole(role);

            var roles = new[] { roleId };

            // Act
            var result = await _userDomainService.UpdateRoles(user, roles);

            // Assert
            Assert.True(result.IsSuccess);
            _mockUserRepository.Verify(r => r.UpdateAsync(It.IsAny<UserEntity>(), It.IsAny<bool>()), Times.Never);
        }

        #endregion
    }
}
