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
    using AutoMapper;
    using Football.Domain.Entities.Exemples;

    public class RefereeControllerTest
    {
        private readonly Mock<IRefereeService> _refereeService = new Mock<IRefereeService>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private RefereeController _refereeController;
        private readonly RefereeFootball RefereeTmp = new RefereeFootball()
        {
            Id = 0,
            Name = "test 1",
            MinutesPlayed = 0,
        };
        private readonly RefereeFootballExemple RefereeExempleTmp = new RefereeFootballExemple()
        {
            Name = "test 1",
            MinutesPlayed = 0,
        };

        public RefereeControllerTest() => _refereeController = new RefereeController(_refereeService.Object, _mapper.Object);

        [Fact]
        public async Task GetRefereesAsync_Ok()
        {
            // Arrange & Act
            var result = await _refereeController.GetRefereesAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetRefereesAsync_Quantity()
        {
            // Arrange
            _refereeService.Setup(x => x.GetRefereesAsync())
            .Returns(async () =>
            {
                await Task.Delay(500);
                RefereeTmp.Id = 1;
                return new List<RefereeFootball>() { RefereeTmp };
            });
            _refereeController = new RefereeController(_refereeService.Object, _mapper.Object);

            // Act
            var result = (OkObjectResult)await _refereeController.GetRefereesAsync();

            // Assert
            var Referees = Assert.IsType<List<RefereeFootball>>(result?.Value);
            Assert.True(Referees?.Count > 0);
        }

        [Fact]
        public async Task GetRefereeByIdAsync_Ok()
        {
            // Arrange
            int id = 1;
            _refereeService.Setup(x => x.GetRefereeByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                RefereeTmp.Id = id;
                return RefereeTmp;
            });
            _refereeController = new RefereeController(_refereeService.Object, _mapper.Object);

            // Act
            var result = await _refereeController.GetRefereeByIdAsync(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetRefereeByIdAsync_Exists()
        {
            // Arrange
            int id = 1;
            _refereeService.Setup(x => x.GetRefereeByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                RefereeTmp.Id = id;
                return RefereeTmp;
            });
            _mapper.Setup(x => x.Map<RefereeFootballExemple>(It.IsAny<RefereeFootball>()))
            .Returns(RefereeExempleTmp);
            _refereeController = new RefereeController(_refereeService.Object, _mapper.Object);
            var result = (OkObjectResult)await _refereeController.GetRefereeByIdAsync(id);

            // Act & Assert
            var Referee = Assert.IsType<RefereeFootballExemple>(result?.Value);
            Assert.NotNull(Referee);
        }

        [Fact]
        public async Task GetRefereeByIdAsync_NotFound()
        {
            // Arrange
            int id = -1;

            // Act
            var result = await _refereeController.GetRefereeByIdAsync(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateRefereeAsync_Ok()
        {
            // Arrange
            RefereeTmp.Id = 0;
            _refereeService.Setup(x => x.CreateRefereeAsync(It.IsAny<RefereeFootball>())).Returns(async () =>
            {
                await Task.Delay(500);
                RefereeTmp.Id = 1;
                return RefereeTmp;
            });
            _refereeController = new RefereeController(_refereeService.Object, _mapper.Object);

            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _refereeController.CreateRefereeAsync(RefereeExempleTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<RefereeFootball>(result.Value);
        }

        [Fact]
        public async Task UpdateRefereeAsync_Ok()
        {
            // Arrange
            int id = 1;
            RefereeTmp.Id = id;
            _refereeService.Setup(x => x.UpdateRefereeAsync(It.IsAny<int>(), It.IsAny<RefereeFootball>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                RefereeTmp.Id = id;
                return RefereeTmp;
            });
            _mapper.Setup(x => x.Map<RefereeFootballExemple>(It.IsAny<RefereeFootball>()))
            .Returns(RefereeExempleTmp);
            _refereeController = new RefereeController(_refereeService.Object, _mapper.Object);

            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _refereeController.UpdateRefereeAsync(id, RefereeExempleTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<RefereeFootballExemple>(result.Value);
        }

        [Fact]
        public async Task UpdateRefereeAsync_NotFound()
        {
            // Arrange
            int id = -1;
            RefereeTmp.Id = id;
            _refereeService.Setup(x => x.UpdateRefereeAsync(It.IsAny<int>(), It.IsAny<RefereeFootball>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return null;
            });
            _refereeController = new RefereeController(_refereeService.Object, _mapper.Object);

            // Act & Assert
            Assert.IsType<NotFoundResult>(await _refereeController.UpdateRefereeAsync(id, RefereeExempleTmp));
        }

        [Fact]
        public async Task DeleteRefereeAsync_Ok()
        {
            // Arrange
            int id = 1;
            _refereeService.Setup(x => x.DeleteRefereeAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return true;
            });
            _refereeController = new RefereeController(_refereeService.Object, _mapper.Object);

            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _refereeController.DeleteRefereeAsync(id));
            Assert.NotNull(result.Value);
            Assert.IsType<bool>(result.Value);
        }
    }
}
