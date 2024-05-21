using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Infrastructure.Services.Sites;
using KitchenEquipmentManager.Repository.Services;
using Moq;

namespace KitchenEquipmentManager.Tests.Services
{
    [TestClass]
    public class SiteServiceTest
    {
        private Mock<IDataRepository<Site>> _mockSiteRepository;
        private SiteService _siteService;

        [TestInitialize]
        public void Setup()
        {
            _mockSiteRepository = new Mock<IDataRepository<Site>>();
            _siteService = new SiteService(_mockSiteRepository.Object);
        }

        [TestMethod]
        public void AddSite_ValidSite_AddsSite()
        {
            // Arrange
            var site = new Site { Id = Guid.NewGuid(), Description = "Test Site", UserId = Guid.NewGuid() };

            // Act
            _siteService.AddSite(site);

            // Assert
            _mockSiteRepository.Verify(repo => repo.Add(It.IsAny<Site>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddSite_RepositoryThrowsException_ThrowsException()
        {
            // Arrange
            var site = new Site { Id = Guid.NewGuid(), Description = "Test Site", UserId = Guid.NewGuid() };
            _mockSiteRepository.Setup(repo => repo.Add(It.IsAny<Site>())).Throws(new InvalidOperationException());

            // Act
            _siteService.AddSite(site);
        }

        [TestMethod]
        public void DeleteSite_ValidSite_DeletesSite()
        {
            // Arrange
            var site = new Site { Id = Guid.NewGuid(), Description = "Test Site", UserId = Guid.NewGuid() };

            // Act
            _siteService.DeleteSite(site);

            // Assert
            _mockSiteRepository.Verify(repo => repo.Remove(site.Id), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteSite_RepositoryThrowsException_ThrowsException()
        {
            // Arrange
            var site = new Site { Id = Guid.NewGuid(), Description = "Test Site", UserId = Guid.NewGuid() };
            _mockSiteRepository.Setup(repo => repo.Remove(It.IsAny<Guid>())).Throws(new InvalidOperationException());

            // Act
            _siteService.DeleteSite(site);
        }

        [TestMethod]
        public void UpdateSite_ValidSite_UpdatesSite()
        {
            // Arrange
            var site = new Site { Id = Guid.NewGuid(), Description = "Updated Site", UserId = Guid.NewGuid() };

            // Act
            _siteService.UpdateSite(site);

            // Assert
            _mockSiteRepository.Verify(repo => repo.Update(site, site.Id), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateSite_RepositoryThrowsException_ThrowsException()
        {
            // Arrange
            var site = new Site { Id = Guid.NewGuid(), Description = "Updated Site", UserId = Guid.NewGuid() };
            _mockSiteRepository.Setup(repo => repo.Update(It.IsAny<Site>(), It.IsAny<Guid>())).Throws(new InvalidOperationException());

            // Act
            _siteService.UpdateSite(site);
        }

        [TestMethod]
        public void RetrieveSitesForUser_ValidUser_ReturnsSites()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), UserName = "testuser", UserType = "Admin" };
            var siteList = new List<Site>
            {
                new Site { Id = Guid.NewGuid(), Description = "Test Site", UserId = user.Id },
                new Site { Id = Guid.NewGuid(), Description = "Another Site", UserId = Guid.NewGuid() }
            };
            _mockSiteRepository.Setup(repo => repo.GetAll()).Returns(siteList.AsQueryable());

            // Act
            var result = _siteService.RetrieveSitesForUser(user);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(siteList[0], result[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RetrieveSitesForUser_RepositoryThrowsException_ThrowsException()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), UserName = "testuser", UserType = "Admin" };
            _mockSiteRepository.Setup(repo => repo.GetAll()).Throws(new InvalidOperationException());

            // Act
            _siteService.RetrieveSitesForUser(user);
        }
    }
}
