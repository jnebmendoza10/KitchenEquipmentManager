using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Infrastructure.Services.Users;
using KitchenEquipmentManager.Repository.Services;
using Moq;

namespace KitchenEquipmentManager.Tests.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly Mock<IDataRepository<User>> _mockUserRepository = new Mock<IDataRepository<User>>();
        private readonly UserService _userService;

        public UserServiceTest()
        {
            _userService = new UserService(_mockUserRepository.Object);
        }

        [TestMethod]
        public void DeleteUser_ValidUser_DeletesUser()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid() };

            // Act
            _userService.DeleteUser(user);

            // Assert
            _mockUserRepository.Verify(repo => repo.Remove(user.Id), Times.Once);
        }

        [TestMethod]
        public void GetAllUsers_ReturnsListOfUsers()
        {
            // Arrange
            var expectedUsers = new List<User>
            {
                new User { Id = Guid.NewGuid(), UserName = "User1" },
                new User { Id = Guid.NewGuid(), UserName = "User2" },
                new User { Id = Guid.NewGuid(), UserName = "User3" }
            };
            _mockUserRepository.Setup(repo => repo.GetAll()).Returns(expectedUsers);

            // Act
            var actualUsers = _userService.GetAllUsers();

            // Assert
            CollectionAssert.AreEqual(expectedUsers, actualUsers);
        }

        [TestMethod]
        public void UpdateUser_ValidUser_UpdatesUser()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), UserName = "NewUserName" };

            // Act
            _userService.UpdateUser(user);

            // Assert
            _mockUserRepository.Verify(repo => repo.Update(user, user.Id), Times.Once);
        }
    }
}
