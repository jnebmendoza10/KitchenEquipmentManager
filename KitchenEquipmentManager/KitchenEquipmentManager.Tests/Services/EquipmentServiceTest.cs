using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Infrastructure.Services.Equipments;
using KitchenEquipmentManager.Repository.Services;
using Moq;

namespace KitchenEquipmentManager.Tests.Services
{
    [TestClass]
    public class EquipmentServiceTest
    {
        private Mock<IDataRepository<Equipment>> _mockEquipmentRepository;
        private Mock<IDataRepository<RegisteredEquipment>> _mockRegisteredEquipmentRepository;
        private EquipmentService _equipmentService;

        [TestInitialize]
        public void Setup()
        {
            _mockEquipmentRepository = new Mock<IDataRepository<Equipment>>();
            _mockRegisteredEquipmentRepository = new Mock<IDataRepository<RegisteredEquipment>>();
            _equipmentService = new EquipmentService(_mockEquipmentRepository.Object, _mockRegisteredEquipmentRepository.Object);
        }

        [TestMethod]
        public void AddEquipment_ValidEquipment_AddsEquipment()
        {
            // Arrange
            var equipment = new Equipment { Id = Guid.NewGuid(), SerialNumber = "123", Description = "Test Equipment", Condition = "Working" };

            // Act
            _equipmentService.AddEquipment(equipment);

            // Assert
            _mockEquipmentRepository.Verify(repo => repo.Add(It.IsAny<Equipment>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddEquipment_RepositoryThrowsException_ThrowsException()
        {
            // Arrange
            var equipment = new Equipment { Id = Guid.NewGuid(), SerialNumber = "123", Description = "Test Equipment", Condition = "Working" };
            _mockEquipmentRepository.Setup(repo => repo.Add(It.IsAny<Equipment>())).Throws(new InvalidOperationException());

            // Act
            _equipmentService.AddEquipment(equipment);
        }

        [TestMethod]
        public void RegisterEquipmentToSite_ValidEquipment_RegistersEquipment()
        {
            // Arrange
            var registeredEquipment = new RegisteredEquipment { Id = Guid.NewGuid(), EquipmentId = Guid.NewGuid(), SiteId = Guid.NewGuid() };

            // Act
            _equipmentService.RegisterEquipmentToSite(registeredEquipment);

            // Assert
            _mockRegisteredEquipmentRepository.Verify(repo => repo.Add(It.IsAny<RegisteredEquipment>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RegisterEquipmentToSite_RepositoryThrowsException_ThrowsException()
        {
            // Arrange
            var registeredEquipment = new RegisteredEquipment { Id = Guid.NewGuid(), EquipmentId = Guid.NewGuid(), SiteId = Guid.NewGuid() };
            _mockRegisteredEquipmentRepository.Setup(repo => repo.Add(It.IsAny<RegisteredEquipment>())).Throws(new InvalidOperationException());

            // Act
            _equipmentService.RegisterEquipmentToSite(registeredEquipment);
        }

        [TestMethod]
        public void DeleteEquipment_ValidEquipment_DeletesEquipment()
        {
            // Arrange
            var equipment = new Equipment { Id = Guid.NewGuid(), SerialNumber = "123", Description = "Test Equipment", Condition = "Working" };

            // Act
            _equipmentService.DeleteEquipment(equipment);

            // Assert
            _mockEquipmentRepository.Verify(repo => repo.Remove(equipment.Id), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteEquipment_RepositoryThrowsException_ThrowsException()
        {
            // Arrange
            var equipment = new Equipment { Id = Guid.NewGuid(), SerialNumber = "123", Description = "Test Equipment", Condition = "Working" };
            _mockEquipmentRepository.Setup(repo => repo.Remove(It.IsAny<Guid>())).Throws(new InvalidOperationException());

            // Act
            _equipmentService.DeleteEquipment(equipment);
        }

        [TestMethod]
        public void RetrieveEquipmentsForUser_ValidUser_ReturnsEquipments()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), UserName = "testuser", UserType = "Admin" };
            var equipmentList = new List<Equipment>
            {
                new Equipment { Id = Guid.NewGuid(), SerialNumber = "123", Description = "Test Equipment", Condition = "Working", UserId = user.Id }
            };
            _mockEquipmentRepository.Setup(repo => repo.GetAll()).Returns(equipmentList.AsQueryable());

            // Act
            var result = _equipmentService.RetrieveEquipmentsForUser(user);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(equipmentList[0], result[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RetrieveEquipmentsForUser_RepositoryThrowsException_ThrowsException()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), UserName = "testuser", UserType = "Admin" };
            _mockEquipmentRepository.Setup(repo => repo.GetAll()).Throws(new InvalidOperationException());

            // Act
            _equipmentService.RetrieveEquipmentsForUser(user);
        }

        [TestMethod]
        public void UpdateEquipment_ValidEquipment_UpdatesEquipment()
        {
            // Arrange
            var equipment = new Equipment { Id = Guid.NewGuid(), SerialNumber = "123", Description = "Updated Equipment", Condition = "Working" };

            // Act
            _equipmentService.UpdateEquipment(equipment);

            // Assert
            _mockEquipmentRepository.Verify(repo => repo.Update(equipment, equipment.Id), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateEquipment_RepositoryThrowsException_ThrowsException()
        {
            // Arrange
            var equipment = new Equipment { Id = Guid.NewGuid(), SerialNumber = "123", Description = "Updated Equipment", Condition = "Working" };
            _mockEquipmentRepository.Setup(repo => repo.Update(It.IsAny<Equipment>(), It.IsAny<Guid>())).Throws(new InvalidOperationException());

            // Act
            _equipmentService.UpdateEquipment(equipment);
        }
    }
}
