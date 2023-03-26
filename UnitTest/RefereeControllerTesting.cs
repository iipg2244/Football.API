using Football.API;
using Football.API.Controllers;
using Football.API.Models;
using Football.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestUnit
{
    public class RefereeControllerTesting
    {
        private readonly ILogger<RefereeService> _logger;
        private readonly FootballContext _footballContext;
        private readonly IRefereeService _refereeService;
        private readonly RefereeController _refereeController;

        public RefereeControllerTesting()
        {
            _logger = new Logger<RefereeService>(MyLogger.LoggerFactory);
            _footballContext = new FootballContext();
            _refereeService = new RefereeService(_logger, _footballContext);
            _refereeController = new RefereeController(_refereeService);
        }

        [Fact]
        public async void GetAsync_Ok()
        {
            var result = await _refereeController.GetAsync();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetAsync_Quantity()
        {
            var result = (OkObjectResult)(await _refereeController.GetAsync());
            var Referees = Assert.IsType<List<Referee>>(result?.Value);
            Assert.True(Referees?.Count > 0);
        }

        [Fact]
        public async void GetByIdAsync_Ok()
        {
            int id = 1;
            var result = await _refereeController.GetByIdAsync(id);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetByIdAsync_Exists()
        {
            int id = 1;
            var result = (OkObjectResult)(await _refereeController.GetByIdAsync(id));
            var Referee = Assert.IsType<Referee>(result?.Value);
            Assert.True(Referee != null);
            Assert.Equal(Referee?.Id, id);
        }

        [Fact]
        public async void GetByIdAsync_NotFound()
        {
            int id = -1;
            var result = await _refereeController.GetByIdAsync(id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void PostAsync_Ok()
        {
            Referee referee = new Referee()
            {
                Id = 0,
                Name= "test 1",
                MinutesPlayed = 45
            };
            var result = Assert.IsType<CreatedAtActionResult>(await _refereeController.PostAsync(referee));
            Assert.True(result.Value != null);
            Assert.IsType<Referee>(result.Value);
        }

        [Fact]
        public async void UpdateAsync_Ok()
        {
            int id = 1;
            Referee referee = new Referee()
            {
                Id = 0,
                Name = "test 2",
                MinutesPlayed = 45
            };
            var result = Assert.IsType<CreatedAtActionResult>(await _refereeController.UpdateAsync(id, referee));
            Assert.True(result.Value != null);
            Assert.IsType<Referee>(result.Value);
        }

        [Fact]
        public async void UpdateAsync_NotFound()
        {
            int id = -1;
            Referee referee = new Referee()
            {
                Id = 0,
                Name = "test 3",
                MinutesPlayed = 45
            };
            Assert.IsType<NotFoundResult>(await _refereeController.UpdateAsync(id, referee));
        }

        [Fact]
        public async void DeleteAsync_Ok()
        {
            int id = -1;
            var result = Assert.IsType<OkObjectResult>(await _refereeController.DeleteAsync(id));
            Assert.True(result.Value != null);
            Assert.IsType<bool>(result.Value);
        }
    }
}
