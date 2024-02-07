namespace Football.API.Tests.Controllers.v1
{
    using Football.API.Controllers.v1;
    using Football.Infrastructure;
    using Football.Domain.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Xunit;
    using Moq;
    using System.Threading.Tasks;

    public class ManagerControllerTest
    {
        private readonly Mock<IManagerService> _managerService = new Mock<IManagerService>();
        private ManagerController _managerController;
        private readonly Manager ManagerTmp = new Manager()
        {
            Id = 0,
            Name = "test 1",
            RedCard = 1,
            YellowCard = 2
        };

        public ManagerControllerTest() => _managerController = new ManagerController(_managerService.Object);

        [Fact]
        public async Task GetAsync_Ok()
        {
            // Arrange & Act
            var result = await _managerController.GetAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAsync_Quantity()
        {
            // Arrange
            _managerService.Setup(x => x.GetAsync())
            .Returns(async () =>
            {
                await Task.Delay(500);
                ManagerTmp.Id = 1;
                return new List<Manager>() { ManagerTmp };
            });
            _managerController = new ManagerController(_managerService.Object);

            // Act
            var result = (OkObjectResult)await _managerController.GetAsync();

            // Assert
            var managers = Assert.IsType<List<Manager>>(result?.Value);
            Assert.True(managers?.Count > 0);
        }

        [Fact]
        public async Task GetByIdAsync_Ok()
        {
            // Arrange
            int id = 1;
            _managerService.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                ManagerTmp.Id = id;
                return ManagerTmp;
            });
            _managerController = new ManagerController(_managerService.Object);

            // Act
            var result = await _managerController.GetByIdAsync(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByIdAsync_Exists()
        {
            // Arrange
            int id = 1;
            _managerService.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                ManagerTmp.Id = id;
                return ManagerTmp;
            });
            _managerController = new ManagerController(_managerService.Object);
            var result = (OkObjectResult)await _managerController.GetByIdAsync(id);

            // Act & Assert
            var manager = Assert.IsType<Manager>(result?.Value);
            Assert.NotNull(manager);
            Assert.Equal(manager.Id, id);
        }

        [Fact]
        public async Task GetByIdAsync_NotFound()
        {
            // Arrange
            int id = -1;

            // Act
            var result = await _managerController.GetByIdAsync(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostAsync_Ok()
        {
            // Arrange
            ManagerTmp.Id = 0;
            _managerService.Setup(x => x.PostAsync(It.IsAny<Manager>())).Returns(async () =>
            {
                await Task.Delay(500);
                ManagerTmp.Id = 1;
                return ManagerTmp;
            });
            _managerController = new ManagerController(_managerService.Object);

            // Act & Assert
            var result = Assert.IsType<CreatedAtActionResult>(await _managerController.PostAsync(ManagerTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<Manager>(result.Value);
        }

        [Fact]
        public async Task UpdateAsync_Ok()
        {
            // Arrange
            int id = 1;
            ManagerTmp.Id = id;
            _managerService.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Manager>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                ManagerTmp.Id = id;
                return ManagerTmp;
            });
            _managerController = new ManagerController(_managerService.Object);
            
            // Act & Assert
            var result = Assert.IsType<CreatedAtActionResult>(await _managerController.UpdateAsync(id, ManagerTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<Manager>(result.Value);
        }

        [Fact]
        public async Task UpdateAsync_NotFound()
        {
            // Arrange
            int id = -1;
            ManagerTmp.Id = id;
            _managerService.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Manager>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return null;
            });
            _managerController = new ManagerController(_managerService.Object);

            // Act & Assert
            Assert.IsType<NotFoundResult>(await _managerController.UpdateAsync(id, ManagerTmp));
        }

        [Fact]
        public async Task DeleteAsync_Ok()
        {
            // Arrange
            int id = 1;
            _managerService.Setup(x => x.DeleteAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return true;
            });
            _managerController = new ManagerController(_managerService.Object);
            
            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _managerController.DeleteAsync(id));
            Assert.NotNull(result.Value);
            Assert.IsType<bool>(result.Value);
        }
    }
}
