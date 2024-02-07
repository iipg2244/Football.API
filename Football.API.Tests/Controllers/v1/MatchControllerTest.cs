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

    public class MatchControllerTest
    {
        private readonly Mock<IMatchService> _matchService = new Mock<IMatchService>();
        private MatchController _matchController;
        private readonly Infrastructure.Match MatchTmp = new Infrastructure.Match()
        {
            Id = 0,
            HouseManagerId = 1,
            HouseManager = new Manager()
            {
                Id = 1,
                Name = "test 1",
                RedCard = 1,
                YellowCard = 2
            },
            HousePlayers = new List<PlayerMatchHouse>(),
            AwayManagerId = 2,
            AwayManager = new Manager()
            {
                Id = 2,
                Name = "test 2",
                RedCard = 0,
                YellowCard = 1
            },
            AwayPlayers = new List<PlayerMatchAway>(),
            RefereeId = 1,
            Referee = new Referee()
            {
                Id = 3,
                Name = "test 3",
                Matches = new List<Infrastructure.Match>(),
                MinutesPlayed = 90
            }
        };

        public MatchControllerTest() => _matchController = new MatchController(_matchService.Object);

        [Fact]
        public async Task GetAsync_Ok()
        {
            // Arrange & Act
            var result = await _matchController.GetAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAsync_Quantity()
        {
            // Arrange
            _matchService.Setup(x => x.GetAsync())
            .Returns(async () =>
            {
                await Task.Delay(500);
                MatchTmp.Id = 1;
                return new List<Infrastructure.Match>() { MatchTmp };
            });
            _matchController = new MatchController(_matchService.Object);
            
            // Act
            var result = (OkObjectResult)await _matchController.GetAsync();

            // Assert
            var Matchs = Assert.IsType<List<Infrastructure.Match>>(result?.Value);
            Assert.True(Matchs?.Count > 0);
        }

        [Fact]
        public async Task GetByIdAsync_Ok()
        {
            // Arrange
            int id = 1;
            _matchService.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                MatchTmp.Id = id;
                return MatchTmp;
            });
            _matchController = new MatchController(_matchService.Object);

            // Act
            var result = await _matchController.GetByIdAsync(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByIdAsync_Exists()
        {
            // Arrange
            int id = 1;
            _matchService.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                MatchTmp.Id = id;
                return MatchTmp;
            });
            _matchController = new MatchController(_matchService.Object);

            // Act
            var result = (OkObjectResult)await _matchController.GetByIdAsync(id);

            // Assert
            var Match = Assert.IsType<Infrastructure.Match>(result?.Value);
            Assert.NotNull(Match);
            Assert.Equal(Match.Id, id);
        }

        [Fact]
        public async Task GetByIdAsync_NotFound()
        {
            // Arrange
            int id = -1;

            // Act
            var result = await _matchController.GetByIdAsync(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostAsync_Ok()
        {
            // Arrange
            MatchTmp.Id = 0;
            _matchService.Setup(x => x.PostAsync(It.IsAny<Infrastructure.Match>())).Returns(async () =>
            {
                await Task.Delay(500);
                MatchTmp.Id = 1;
                return MatchTmp;
            });
            _matchController = new MatchController(_matchService.Object);

            // Act & Assert
            var result = Assert.IsType<CreatedAtActionResult>(await _matchController.PostAsync(MatchTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<Infrastructure.Match>(result.Value);
        }

        [Fact]
        public async Task UpdateAsync_Ok()
        {
            // Arrange
            int id = 1;
            MatchTmp.Id = id;
            _matchService.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Infrastructure.Match>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                MatchTmp.Id = id;
                return MatchTmp;
            });
            _matchController = new MatchController(_matchService.Object);

            // Act & Assert
            var result = Assert.IsType<CreatedAtActionResult>(await _matchController.UpdateAsync(id, MatchTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<Infrastructure.Match>(result.Value);
        }

        [Fact]
        public async Task UpdateAsync_NotFound()
        {
            // Arrange
            int id = -1;
            MatchTmp.Id = id;
            _matchService.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Infrastructure.Match>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return null;
            });
            _matchController = new MatchController(_matchService.Object);

            // Act & Assert
            Assert.IsType<NotFoundResult>(await _matchController.UpdateAsync(id, MatchTmp));
        }

        [Fact]
        public async Task DeleteAsync_Ok()
        {
            // Arrange
            int id = 1;
            _matchService.Setup(x => x.DeleteAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return true;
            });
            _matchController = new MatchController(_matchService.Object);

            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _matchController.DeleteAsync(id));
            Assert.NotNull(result.Value);
            Assert.IsType<bool>(result.Value);
        }
    }
}
