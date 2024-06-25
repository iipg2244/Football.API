namespace Football.API.Tests.Controllers.v1
{
    using Football.API.Controllers.v1;
    using Football.Domain.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Xunit;
    using Moq;
    using System.Threading.Tasks;
    using Football.Domain.Entities.Football;
    using AutoMapper;
    using Football.Domain.Entities.Exemples;

    public class ManagerControllerTest
    {
        private readonly Mock<IManagerService> _managerService = new Mock<IManagerService>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private ManagerController _managerController;
        private readonly ManagerFootball ManagerTmp = new ManagerFootball()
        {
            Name = "test 1",
            RedCard = 1,
            YellowCard = 2
        };
        private readonly ManagerFootballExemple ManagerExempleTmp = new ManagerFootballExemple()
        {
            Name = "test 1",
            RedCard = 1,
            YellowCard = 2
        };

        public ManagerControllerTest() => _managerController = new ManagerController(_managerService.Object, _mapper.Object);

        [Fact]
        public async Task GetManagersAsync_Ok()
        {
            // Arrange & Act
            var result = await _managerController.GetManagersAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetManagersAsync_Quantity()
        {
            // Arrange
            _managerService.Setup(x => x.GetManagersAsync())
            .Returns(async () =>
            {
                await Task.Delay(500);
                ManagerTmp.Id = 1;
                return new List<ManagerFootball>() { ManagerTmp };
            });
            _managerController = new ManagerController(_managerService.Object, _mapper.Object);

            // Act
            var result = (OkObjectResult)await _managerController.GetManagersAsync();

            // Assert
            var managers = Assert.IsType<List<ManagerFootball>>(result?.Value);
            Assert.True(managers?.Count > 0);
        }

        [Fact]
        public async Task GetManagerByIdAsync_Ok()
        {
            // Arrange
            int id = 1;
            _managerService.Setup(x => x.GetManagerByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                ManagerTmp.Id = id;
                return ManagerTmp;
            });
            _managerController = new ManagerController(_managerService.Object, _mapper.Object);

            // Act
            var result = await _managerController.GetManagerByIdAsync(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetManagerByIdAsync_Exists()
        {
            // Arrange
            int id = 1;
            _managerService.Setup(x => x.GetManagerByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                ManagerTmp.Id = id;
                return ManagerTmp;
            });
            _mapper.Setup(x => x.Map<ManagerFootballExemple>(It.IsAny<ManagerFootball>()))
            .Returns(ManagerExempleTmp);
            _managerController = new ManagerController(_managerService.Object, _mapper.Object);
            var result = (OkObjectResult)await _managerController.GetManagerByIdAsync(id);

            // Act & Assert
            var manager = Assert.IsType<ManagerFootballExemple>(result?.Value);
            Assert.NotNull(manager);
        }

        [Fact]
        public async Task GetManagerByIdAsync_NotFound()
        {
            // Arrange
            int id = -1;

            // Act
            var result = await _managerController.GetManagerByIdAsync(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateManagerAsync_Ok()
        {
            // Arrange
            ManagerTmp.Id = 0;
            _managerService.Setup(x => x.CreateManagerAsync(It.IsAny<ManagerFootball>())).Returns(async () =>
            {
                await Task.Delay(500);
                ManagerTmp.Id = 1;
                return ManagerTmp;
            });
            _managerController = new ManagerController(_managerService.Object, _mapper.Object);

            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _managerController.CreateManagerAsync(ManagerExempleTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<ManagerFootball>(result.Value);
        }

        [Fact]
        public async Task UpdateManagerAsync_Ok()
        {
            // Arrange
            int id = 1;
            ManagerTmp.Id = id;
            _managerService.Setup(x => x.UpdateManagerAsync(It.IsAny<int>(), It.IsAny<ManagerFootball>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                ManagerTmp.Id = id;
                return ManagerTmp;
            });
            _mapper.Setup(x => x.Map<ManagerFootballExemple>(It.IsAny<ManagerFootball>()))
            .Returns(ManagerExempleTmp);
            _managerController = new ManagerController(_managerService.Object, _mapper.Object);

            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _managerController.UpdateManagerAsync(id, ManagerExempleTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<ManagerFootballExemple>(result.Value);
        }

        [Fact]
        public async Task UpdateManagerAsync_NotFound()
        {
            // Arrange
            int id = -1;
            ManagerTmp.Id = id;
            _managerService.Setup(x => x.UpdateManagerAsync(It.IsAny<int>(), It.IsAny<ManagerFootball>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return null;
            });
            _managerController = new ManagerController(_managerService.Object, _mapper.Object);

            // Act & Assert
            Assert.IsType<NotFoundResult>(await _managerController.UpdateManagerAsync(id, ManagerExempleTmp));
        }

        [Fact]
        public async Task DeleteManagerAsync_Ok()
        {
            // Arrange
            int id = 1;
            _managerService.Setup(x => x.DeleteManagerAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return true;
            });
            _managerController = new ManagerController(_managerService.Object, _mapper.Object);

            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _managerController.DeleteManagerAsync(id));
            Assert.NotNull(result.Value);
            Assert.IsType<bool>(result.Value);
        }
    }
}
