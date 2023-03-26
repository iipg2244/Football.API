using Football.API;
using Football.API.Controllers;
using DDBB;
using Football.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using Football.API.Models.Others;
using Microsoft.Extensions.Configuration;

namespace TestUnit
{
    public class StatisticsControllerTesting
    {
        private readonly ILogger<StatisticsService> _logger;
        private readonly FootballContext _footballContext;
        private readonly IStatisticsService _statisticsService;
        private readonly StatisticsController _statisticsController;

        public StatisticsControllerTesting()
        {
            _logger = new Logger<StatisticsService>(MyLogger.LoggerFactory);
            _footballContext = new FootballContext(Methods.GetConfigurationRoot().GetConnectionString("DefaultConnection"));
            _statisticsService = new StatisticsService(_logger, _footballContext);
            _statisticsController = new StatisticsController(_statisticsService);
        }

        [Fact]
        public async void GetYellowCardsAsync_Ok()
        {
            var result = await _statisticsController.GetYellowCardsAsync();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetYellowCardsAsync_Quantity()
        {
            var result = (OkObjectResult)(await _statisticsController.GetYellowCardsAsync());
            if (result?.Value is IEnumerable) {
                var yellowcards = (IList)result.Value;
                Assert.True(yellowcards?.Count >= 0);
            }           
        }

        [Fact]
        public async void GetRedCardsAsync_Ok()
        {
            var result = await _statisticsController.GetRedCardsAsync();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetRedCardsAsync_Quantity()
        {
            var result = (OkObjectResult)(await _statisticsController.GetRedCardsAsync());
            if (result?.Value is IEnumerable)
            {
                var yellowcards = (IList)result.Value;
                Assert.True(yellowcards?.Count >= 0);
            }
        }

        [Fact]
        public async void GetMinutesPlayedAsync_Ok()
        {
            var result = await _statisticsController.GetMinutesPlayedAsync();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetMinutesPlayedAsync_Quantity()
        {
            var result = (OkObjectResult)(await _statisticsController.GetMinutesPlayedAsync());
            if (result?.Value is IEnumerable)
            {
                var yellowcards = (IList)result.Value;
                Assert.True(yellowcards?.Count >= 0);
            }
        }
    }
}
