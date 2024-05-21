using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Infrastructure.Exceptions;
using KitchenEquipmentManager.Infrastructure.Services.Users;
using KitchenEquipmentManager.Repository.Services;
using Moq;

namespace KitchenEquipmentManager.Tests.Services
{
    [TestClass]
    public class AuthenticationServiceTest
    {
        private Mock<IPasswordService> _mockPasswordService;
        private Mock<IDataRepository<User>> _mockUserRepository;
        private AuthenticationService _authenticationService;

        [TestInitialize]
        public void Setup()
        {
            _mockPasswordService = new Mock<IPasswordService>();
            _mockUserRepository = new Mock<IDataRepository<User>>();
            _authenticationService = new AuthenticationService(_mockPasswordService.Object, _mockUserRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserAndPasswordException))]
        public void Login_InvalidUsername_ThrowsInvalidUserAndPasswordException()
        {
            // Arrange
            var users = new List<User>();
            _mockUserRepository.Setup(repo => repo.GetAll()).Returns(users.AsQueryable());

            // Act
            _authenticationService.Login("invalidUser", "password");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserAndPasswordException))]
        public void Login_InvalidPassword_ThrowsInvalidUserAndPasswordException()
        {
            // Arrange
            var user = new User { UserName = "validUser", Password = "hashedPassword" };
            var users = new List<User> { user };
            _mockUserRepository.Setup(repo => repo.GetAll()).Returns(users.AsQueryable());
            _mockPasswordService.Setup(service => service.ValidatePassword(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            // Act
            _authenticationService.Login("validUser", "invalidPassword");
        }

        [TestMethod]
        public void Login_ValidUser_ReturnsUser()
        {
            // Arrange
            var validUser = new User { UserName = "validUser", Password = "hashedPassword" };
            var users = new List<User> { validUser };
            _mockUserRepository.Setup(repo => repo.GetAll()).Returns(users.AsQueryable());
            _mockPasswordService.Setup(service => service.ValidatePassword("password", "hashedPassword")).Returns(true);

            // Act
            var result = _authenticationService.Login("validUser", "password");

            // Assert
            Assert.AreEqual(validUser, result);
        }

        [TestMethod]
        [ExpectedException(typeof(UserAlreadyExistsException))]
        public void Register_ExistingUser_ThrowsUserAlreadyExistsException()
        {
            // Arrange
            var existingUser = new User { UserName = "existingUser" };
            var users = new List<User> { existingUser };
            _mockUserRepository.Setup(repo => repo.GetAll()).Returns(users.AsQueryable());

            // Act
            _authenticationService.Register(existingUser);
        }

        [TestMethod]
        public void Register_NewUser_AddsUser()
        {
            // Arrange
            var newUser = new User { UserName = "newUser", Password = "password" };
            var users = new List<User>();
            _mockUserRepository.Setup(repo => repo.GetAll()).Returns(users.AsQueryable());

            // Act
            _authenticationService.Register(newUser);

            // Assert
            _mockUserRepository.Verify(repo => repo.Add(newUser), Times.Once);
        }
    }
}
