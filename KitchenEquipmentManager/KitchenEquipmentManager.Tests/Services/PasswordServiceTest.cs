using KitchenEquipmentManager.Infrastructure.Services.Users;

namespace KitchenEquipmentManager.Tests.Services
{
    [TestClass]
    public class PasswordServiceTest
    {
        private readonly PasswordService _passwordService = new PasswordService();

        [TestMethod]
        public void CreateHash_ValidPassword_ReturnsHashedPassword()
        {
            // Arrange
            var password = "testPassword";

            // Act
            var hashedPassword = _passwordService.CreateHash(password);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(hashedPassword));
            Assert.IsTrue(hashedPassword.Contains(":"));
        }

        [TestMethod]
        public void ValidatePassword_ValidPassword_ReturnsTrue()
        {
            // Arrange
            var password = "testPassword";
            var hashedPassword = _passwordService.CreateHash(password);

            // Act
            var isValid = _passwordService.ValidatePassword(password, hashedPassword);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValidatePassword_InvalidPassword_ReturnsFalse()
        {
            // Arrange
            var password = "testPassword";
            var invalidPassword = "invalidPassword";
            var hashedPassword = _passwordService.CreateHash(password);

            // Act
            var isValid = _passwordService.ValidatePassword(invalidPassword, hashedPassword);

            // Assert
            Assert.IsFalse(isValid);
        }
    }
}
