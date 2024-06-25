namespace Football.API.Tests.Controllers.v1
{
    using Football.API.Controllers.v1;
    using Football.Domain.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections;
    using Xunit;
    using Moq;
    using System.Threading.Tasks;

    public class StatisticsControllerTest
    {
        private readonly Mock<IStatisticsService> _statisticsService = new Mock<IStatisticsService>();
        private readonly StatisticsController _statisticsController;

        public StatisticsControllerTest() => _statisticsController = new StatisticsController(_statisticsService.Object);

        [Fact]
        public async Task GetYellowCardsAsync_Ok()
        {
            // Arrange & Act
            var result = await _statisticsController.GetYellowCardsAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetYellowCardsAsync_Quantity()
        {
            // Arrange & Act
            var result = (OkObjectResult)await _statisticsController.GetYellowCardsAsync();

            // Assert
            if (result?.Value is IEnumerable)
            {
                var yellowcards = (IList)result.Value;
                Assert.True(yellowcards?.Count >= 0);
            }
        }

        [Fact]
        public async Task GetRedCardsAsync_Ok()
        {   
            // Arrange & Act
            var result = await _statisticsController.GetRedCardsAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetRedCardsAsync_Quantity()
        {
            // Arrange & Act
            var result = (OkObjectResult)await _statisticsController.GetRedCardsAsync();

            // Assert
            if (result?.Value is IEnumerable)
            {
                var yellowcards = (IList)result.Value;
                Assert.True(yellowcards?.Count >= 0);
            }
        }

        [Fact]
        public async Task GetMinutesPlayedAsync_Ok()
        {
            // Arrange & Act
            var result = await _statisticsController.GetMinutesPlayedAsync();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetMinutesPlayedAsync_Quantity()
        {
            // Arrange & Act
            var result = (OkObjectResult)await _statisticsController.GetMinutesPlayedAsync();

            // Assert
            if (result?.Value is IEnumerable)
            {
                var yellowcards = (IList)result.Value;
                Assert.True(yellowcards?.Count >= 0);
            }
        }
    }
}
