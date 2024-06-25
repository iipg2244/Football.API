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

    public class PlayerControllerTest
    {
        private readonly Mock<IPlayerService> _playerService = new Mock<IPlayerService>();
        private PlayerController _playerController;
        private readonly Player PlayerTmp = new Player()
        {
            Id = 0,
            Name = "test 1",
            RedCard = 1,
            YellowCard = 2,
            MinutesPlayed = 90
        };

        public PlayerControllerTest() => _playerController = new PlayerController(_playerService.Object);

        [Fact]
        public async Task GetAsync_Ok()
        {
            // Arrange & Act
            var result = await _playerController.GetAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAsync_Quantity()
        {
            // Arrange
            _playerService.Setup(x => x.GetAsync())
            .Returns(async () =>
            {
                await Task.Delay(500);
                PlayerTmp.Id = 1;
                return new List<Player>() { PlayerTmp };
            });
            _playerController = new PlayerController(_playerService.Object);
            
            // Act
            var result = (OkObjectResult)await _playerController.GetAsync();

            // Assert
            var Players = Assert.IsType<List<Player>>(result?.Value);
            Assert.True(Players?.Count > 0);
        }

        [Fact]
        public async Task GetByIdAsync_Ok()
        {
            // Arrange
            int id = 1;
            _playerService.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                PlayerTmp.Id = id;
                return PlayerTmp;
            });
            _playerController = new PlayerController(_playerService.Object);

            // Act
            var result = await _playerController.GetByIdAsync(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByIdAsync_Exists()
        {
            // Arrange
            int id = 1;
            _playerService.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                PlayerTmp.Id = id;
                return PlayerTmp;
            });
            _playerController = new PlayerController(_playerService.Object);

            // Act
            var result = (OkObjectResult)await _playerController.GetByIdAsync(id);

            // Assert
            var Player = Assert.IsType<Player>(result?.Value);
            Assert.NotNull(Player);
            Assert.Equal(Player.Id, id);
        }

        [Fact]
        public async Task GetByIdAsync_NotFound()
        {
            // Arrange
            int id = -1;

            // Act
            var result = await _playerController.GetByIdAsync(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostAsync_Ok()
        {
            // Arrange
            PlayerTmp.Id = 0;
            _playerService.Setup(x => x.PostAsync(It.IsAny<Player>())).Returns(async () =>
            {
                await Task.Delay(500);
                PlayerTmp.Id = 1;
                return PlayerTmp;
            });
            _playerController = new PlayerController(_playerService.Object);

            // Act & Assert
            var result = Assert.IsType<CreatedAtActionResult>(await _playerController.PostAsync(PlayerTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<Player>(result.Value);
        }

        [Fact]
        public async Task UpdateAsync_Ok()
        {
            // Arrange
            int id = 1;
            PlayerTmp.Id = id;
            _playerService.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Player>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                PlayerTmp.Id = id;
                return PlayerTmp;
            });
            _playerController = new PlayerController(_playerService.Object);

            // Act & Assert
            var result = Assert.IsType<CreatedAtActionResult>(await _playerController.UpdateAsync(id, PlayerTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<Player>(result.Value);
        }

        [Fact]
        public async Task UpdateAsync_NotFound()
        {
            // Arrange
            int id = -1;
            PlayerTmp.Id = id;
            _playerService.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Player>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return null;
            });
            _playerController = new PlayerController(_playerService.Object);

            // Act & Assert
            Assert.IsType<NotFoundResult>(await _playerController.UpdateAsync(id, PlayerTmp));
        }

        [Fact]
        public async Task DeleteAsync_Ok()
        {
            // Arrange
            int id = 1;
            _playerService.Setup(x => x.DeleteAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return true;
            });
            _playerController = new PlayerController(_playerService.Object);

            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _playerController.DeleteAsync(id));
            Assert.NotNull(result.Value);
            Assert.IsType<bool>(result.Value);
        }
    }
}
