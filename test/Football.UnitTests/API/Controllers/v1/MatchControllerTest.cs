namespace Football.UnitTests.API.Controllers.v1
{
    using Football.API.Controllers.v1;
    using Football.Domain.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Xunit;
    using Moq;
    using System.Threading.Tasks;
    using Football.Domain.Entities.Football;
    using Football.Infrastructure.Entities;
    using Football.Domain.Entities.Exemples;
    using AutoMapper;

    public class MatchControllerTest
    {
        private readonly Mock<IMatchService> _matchService = new Mock<IMatchService>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private MatchController _matchController;
        private readonly MatchFootball MatchTmp = new MatchFootball()
        {
            Id = 0,
            HouseManagerId = 1,
            HouseManager = new ManagerFootball()
            {
                Id = 1,
                Name = "test 1",
                RedCard = 1,
                YellowCard = 2
            },
            HousePlayers = new List<PlayerMatchHouseFootball>(),
            AwayManagerId = 2,
            AwayManager = new ManagerFootball()
            {
                Id = 2,
                Name = "test 2",
                RedCard = 0,
                YellowCard = 1
            },
            AwayPlayers = new List<PlayerMatchAwayFootball>(),
            RefereeId = 1,
            Referee = new RefereeFootball()
            {
                Id = 3,
                Name = "test 3",
                MinutesPlayed = 90
            }
        };
        private readonly MatchFootballExemple MatchExempleTmp = new MatchFootballExemple()
        {
            HouseManagerId = 1,
            HouseManager = new ManagerFootballExemple()
            {
                Name = "test 1",
                RedCard = 1,
                YellowCard = 2
            },
            HousePlayers = new List<PlayerMatchHouseFootballExemple>(),
            AwayManagerId = 2,
            AwayManager = new ManagerFootballExemple()
            {
                Name = "test 2",
                RedCard = 0,
                YellowCard = 1
            },
            AwayPlayers = new List<PlayerMatchAwayFootballExemple>(),
            RefereeId = 1,
            Referee = new RefereeFootballExemple()
            {
                Name = "test 3",
                MinutesPlayed = 90
            }
        };

        public MatchControllerTest() => _matchController = new MatchController(_matchService.Object, _mapper.Object);

        [Fact]
        public async Task GetMatchesAsync_Ok()
        {
            // Arrange & Act
            var result = await _matchController.GetMatchesAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetMatchesAsync_Quantity()
        {
            // Arrange
            _matchService.Setup(x => x.GetMatchesAsync())
            .Returns(async () =>
            {
                await Task.Delay(500);
                MatchTmp.Id = 1;
                return new List<MatchFootball>() { MatchTmp };
            });
            _matchController = new MatchController(_matchService.Object, _mapper.Object);

            // Act
            var result = (OkObjectResult)await _matchController.GetMatchesAsync();

            // Assert
            var Matchs = Assert.IsType<List<MatchFootball>>(result?.Value);
            Assert.True(Matchs?.Count > 0);
        }

        [Fact]
        public async Task GetMatchByIdAsync_Ok()
        {
            // Arrange
            int id = 1;
            _matchService.Setup(x => x.GetMatchByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                MatchTmp.Id = id;
                return MatchTmp;
            });
            _matchController = new MatchController(_matchService.Object, _mapper.Object);

            // Act
            var result = await _matchController.GetMatchByIdAsync(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetMatchByIdAsync_Exists()
        {
            // Arrange
            int id = 1;
            _matchService.Setup(x => x.GetMatchByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                MatchTmp.Id = id;
                return MatchTmp;
            });
            _mapper.Setup(x => x.Map<MatchFootballExemple>(It.IsAny<MatchFootball>()))
            .Returns(MatchExempleTmp);
            _matchController = new MatchController(_matchService.Object, _mapper.Object);

            // Act
            var result = (OkObjectResult)await _matchController.GetMatchByIdAsync(id);

            // Assert
            var Match = Assert.IsType<MatchFootballExemple>(result?.Value);
            Assert.NotNull(Match);
        }

        [Fact]
        public async Task GetMatchByIdAsync_NotFound()
        {
            // Arrange
            int id = -1;

            // Act
            var result = await _matchController.GetMatchByIdAsync(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateMatchAsync_Ok()
        {
            // Arrange
            MatchTmp.Id = 0;
            _matchService.Setup(x => x.CreateMatchAsync(It.IsAny<MatchFootball>())).Returns(async () =>
            {
                await Task.Delay(500);
                MatchTmp.Id = 1;
                return MatchTmp;
            });
            _matchController = new MatchController(_matchService.Object, _mapper.Object);

            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _matchController.CreateMatchAsync(MatchExempleTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<MatchFootball>(result.Value);
        }

        [Fact]
        public async Task UpdateMatchAsync_Ok()
        {
            // Arrange
            int id = 1;
            MatchTmp.Id = id;
            _matchService.Setup(x => x.UpdateMatchAsync(It.IsAny<int>(), It.IsAny<MatchFootball>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                MatchTmp.Id = id;
                return MatchTmp;
            });
            _mapper.Setup(x => x.Map<MatchFootballExemple>(It.IsAny<MatchFootball>()))
            .Returns(MatchExempleTmp);
            _matchController = new MatchController(_matchService.Object, _mapper.Object);

            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _matchController.UpdateMatchAsync(id, MatchExempleTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<MatchFootballExemple>(result.Value);
        }

        [Fact]
        public async Task UpdateMatchAsync_NotFound()
        {
            // Arrange
            int id = -1;
            MatchTmp.Id = id;
            _matchService.Setup(x => x.UpdateMatchAsync(It.IsAny<int>(), It.IsAny<MatchFootball>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return null;
            });
            _matchController = new MatchController(_matchService.Object, _mapper.Object);

            // Act & Assert
            Assert.IsType<NotFoundResult>(await _matchController.UpdateMatchAsync(id, MatchExempleTmp));
        }

        [Fact]
        public async Task DeleteMatchAsync_Ok()
        {
            // Arrange
            int id = 1;
            _matchService.Setup(x => x.DeleteMatchAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return true;
            });
            _matchController = new MatchController(_matchService.Object, _mapper.Object);

            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _matchController.DeleteMatchAsync(id));
            Assert.NotNull(result.Value);
            Assert.IsType<bool>(result.Value);
        }
    }
}
