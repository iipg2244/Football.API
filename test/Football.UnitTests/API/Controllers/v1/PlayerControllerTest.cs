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

    public class PlayerControllerTest
    {
        private readonly Mock<IPlayerService> _playerService = new Mock<IPlayerService>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private PlayerController _playerController;
        private readonly PlayerFootball PlayerTmp = new PlayerFootball()
        {
            Id = 0,
            Name = "test 1",
            RedCard = 1,
            YellowCard = 2,
            MinutesPlayed = 90
        };
        private readonly PlayerFootballExemple PlayerExempleTmp = new PlayerFootballExemple()
        {
            Name = "test 1",
            RedCard = 1,
            YellowCard = 2,
            MinutesPlayed = 90
        };

        public PlayerControllerTest() => _playerController = new PlayerController(_playerService.Object, _mapper.Object);

        [Fact]
        public async Task GetPlayersAsync_Ok()
        {
            // Arrange & Act
            var result = await _playerController.GetPlayersAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetPlayersAsync_Quantity()
        {
            // Arrange
            _playerService.Setup(x => x.GetPlayersAsync())
            .Returns(async () =>
            {
                await Task.Delay(500);
                PlayerTmp.Id = 1;
                return new List<PlayerFootball>() { PlayerTmp };
            });
            _playerController = new PlayerController(_playerService.Object, _mapper.Object);

            // Act
            var result = (OkObjectResult)await _playerController.GetPlayersAsync();

            // Assert
            var Players = Assert.IsType<List<PlayerFootball>>(result?.Value);
            Assert.True(Players?.Count > 0);
        }

        [Fact]
        public async Task GetPlayerByIdAsync_Ok()
        {
            // Arrange
            int id = 1;
            _playerService.Setup(x => x.GetPlayerByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                PlayerTmp.Id = id;
                return PlayerTmp;
            });
            _playerController = new PlayerController(_playerService.Object, _mapper.Object);

            // Act
            var result = await _playerController.GetPlayerByIdAsync(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetPlayerByIdAsync_Exists()
        {
            // Arrange
            int id = 1;
            _playerService.Setup(x => x.GetPlayerByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                PlayerTmp.Id = id;
                return PlayerTmp;
            });
            _mapper.Setup(x => x.Map<PlayerFootballExemple>(It.IsAny<PlayerFootball>()))
            .Returns(PlayerExempleTmp);
            _playerController = new PlayerController(_playerService.Object, _mapper.Object);
            var result = (OkObjectResult)await _playerController.GetPlayerByIdAsync(id);

            // Act & Assert
            var Player = Assert.IsType<PlayerFootballExemple>(result?.Value);
            Assert.NotNull(Player);
        }

        [Fact]
        public async Task GetPlayerByIdAsync_NotFound()
        {
            // Arrange
            int id = -1;

            // Act
            var result = await _playerController.GetPlayerByIdAsync(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreatePlayerAsync_Ok()
        {
            // Arrange
            PlayerTmp.Id = 0;
            _playerService.Setup(x => x.CreatePlayerAsync(It.IsAny<PlayerFootball>())).Returns(async () =>
            {
                await Task.Delay(500);
                PlayerTmp.Id = 1;
                return PlayerTmp;
            });
            _playerController = new PlayerController(_playerService.Object, _mapper.Object);

            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _playerController.CreatePlayerAsync(PlayerExempleTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<PlayerFootball>(result.Value);
        }

        [Fact]
        public async Task UpdatePlayerAsync_Ok()
        {
            // Arrange
            int id = 1;
            PlayerTmp.Id = id;
            _playerService.Setup(x => x.UpdatePlayerAsync(It.IsAny<int>(), It.IsAny<PlayerFootball>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                PlayerTmp.Id = id;
                return PlayerTmp;
            });
            _mapper.Setup(x => x.Map<PlayerFootballExemple>(It.IsAny<PlayerFootball>()))
            .Returns(PlayerExempleTmp);
            _playerController = new PlayerController(_playerService.Object, _mapper.Object);

            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _playerController.UpdatePlayerAsync(id, PlayerExempleTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<PlayerFootballExemple>(result.Value);
        }

        [Fact]
        public async Task UpdatePlayerAsync_NotFound()
        {
            // Arrange
            int id = -1;
            PlayerTmp.Id = id;
            _playerService.Setup(x => x.UpdatePlayerAsync(It.IsAny<int>(), It.IsAny<PlayerFootball>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return null;
            });
            _playerController = new PlayerController(_playerService.Object, _mapper.Object);

            // Act & Assert
            Assert.IsType<NotFoundResult>(await _playerController.UpdatePlayerAsync(id, PlayerExempleTmp));
        }

        [Fact]
        public async Task DeletePlayerAsync_Ok()
        {
            // Arrange
            int id = 1;
            _playerService.Setup(x => x.DeletePlayerAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return true;
            });
            _playerController = new PlayerController(_playerService.Object, _mapper.Object);

            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _playerController.DeletePlayerAsync(id));
            Assert.NotNull(result.Value);
            Assert.IsType<bool>(result.Value);
        }
    }
}
