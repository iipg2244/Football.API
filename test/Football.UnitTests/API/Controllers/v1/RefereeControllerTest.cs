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

    public class RefereeControllerTest
    {
        private readonly Mock<IRefereeService> _refereeService = new Mock<IRefereeService>();
        private RefereeController _refereeController;
        private readonly Referee RefereeTmp = new Referee()
        {
            Id = 0,
            Name = "test 1",
            MinutesPlayed = 0,
            Matches = new List<Domain.Entities.Football.Match>()
        };

        public RefereeControllerTest() => _refereeController = new RefereeController(_refereeService.Object);

        [Fact]
        public async Task GetAsync_Ok()
        {
            // Arrange & Act
            var result = await _refereeController.GetAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAsync_Quantity()
        {
            // Arrange
            _refereeService.Setup(x => x.GetAsync())
            .Returns(async () =>
            {
                await Task.Delay(500);
                RefereeTmp.Id = 1;
                return new List<Referee>() { RefereeTmp };
            });
            _refereeController = new RefereeController(_refereeService.Object);

            // Act
            var result = (OkObjectResult)await _refereeController.GetAsync();

            // Assert
            var Referees = Assert.IsType<List<Referee>>(result?.Value);
            Assert.True(Referees?.Count > 0);
        }

        [Fact]
        public async Task GetByIdAsync_Ok()
        {
            // Arrange
            int id = 1;
            _refereeService.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                RefereeTmp.Id = id;
                return RefereeTmp;
            });
            _refereeController = new RefereeController(_refereeService.Object);

            // Act
            var result = await _refereeController.GetByIdAsync(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByIdAsync_Exists()
        {
            // Arrange
            int id = 1;
            _refereeService.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                RefereeTmp.Id = id;
                return RefereeTmp;
            });
            _refereeController = new RefereeController(_refereeService.Object);

            // Act
            var result = (OkObjectResult)await _refereeController.GetByIdAsync(id);

            // Assert
            var Referee = Assert.IsType<Referee>(result?.Value);
            Assert.NotNull(Referee);
            Assert.Equal(Referee.Id, id);
        }

        [Fact]
        public async Task GetByIdAsync_NotFound()
        {
            // Arrange
            int id = -1;
            
            // Act
            var result = await _refereeController.GetByIdAsync(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostAsync_Ok()
        {
            // Arrange
            RefereeTmp.Id = 0;
            _refereeService.Setup(x => x.PostAsync(It.IsAny<Referee>())).Returns(async () =>
            {
                await Task.Delay(500);
                RefereeTmp.Id = 1;
                return RefereeTmp;
            });
            _refereeController = new RefereeController(_refereeService.Object);

            // Act & Assert
            var result = Assert.IsType<CreatedAtActionResult>(await _refereeController.PostAsync(RefereeTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<Referee>(result.Value);
        }

        [Fact]
        public async Task UpdateAsync_Ok()
        {
            // Arrange
            int id = 1;
            RefereeTmp.Id = id;
            _refereeService.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Referee>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                RefereeTmp.Id = id;
                return RefereeTmp;
            });
            _refereeController = new RefereeController(_refereeService.Object);

            // Act & Assert
            var result = Assert.IsType<CreatedAtActionResult>(await _refereeController.UpdateAsync(id, RefereeTmp));
            Assert.NotNull(result.Value);
            Assert.IsType<Referee>(result.Value);
        }

        [Fact]
        public async Task UpdateAsync_NotFound()
        {
            // Arrange
            int id = -1;
            RefereeTmp.Id = id;
            _refereeService.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Referee>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return null;
            });
            _refereeController = new RefereeController(_refereeService.Object);

            // Act & Assert
            Assert.IsType<NotFoundResult>(await _refereeController.UpdateAsync(id, RefereeTmp));
        }

        [Fact]
        public async Task DeleteAsync_Ok()
        {
            // Arrange
            int id = -1;
            _refereeService.Setup(x => x.DeleteAsync(It.IsAny<int>()))
            .Returns(async () =>
            {
                await Task.Delay(500);
                return true;
            });
            _refereeController = new RefereeController(_refereeService.Object);

            // Act & Assert
            var result = Assert.IsType<OkObjectResult>(await _refereeController.DeleteAsync(id));
            Assert.NotNull(result.Value);
            Assert.IsType<bool>(result.Value);
        }
    }
}
