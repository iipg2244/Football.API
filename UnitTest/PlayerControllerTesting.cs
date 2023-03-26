using Football.API;
using Football.API.Controllers;
using DDBB;
using Football.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Xunit;
using Football.API.Models.Others;
using Microsoft.Extensions.Configuration;

namespace TestUnit
{
    public class PlayerControllerTesting
    {
        private readonly ILogger<PlayerService> _logger;
        private readonly FootballContext _footballContext;
        private readonly IPlayerService _playerService;
        private readonly PlayerController _playerController;

        public PlayerControllerTesting()
        {
            _logger = new Logger<PlayerService>(MyLogger.LoggerFactory);
            _footballContext = new FootballContext(Methods.GetConfigurationRoot().GetConnectionString("DefaultConnection"));
            _playerService = new PlayerService(_logger, _footballContext);
            _playerController = new PlayerController(_playerService);
        }

        [Fact]
        public async void GetAsync_Ok()
        {
            var result = await _playerController.GetAsync();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetAsync_Quantity()
        {
            var result = (OkObjectResult)(await _playerController.GetAsync());
            var Players = Assert.IsType<List<Player>>(result?.Value);
            Assert.True(Players?.Count > 0);
        }

        [Fact]
        public async void GetByIdAsync_Ok()
        {
            int id = 1;
            var result = await _playerController.GetByIdAsync(id);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetByIdAsync_Exists()
        {
            int id = 1;
            var result = (OkObjectResult)(await _playerController.GetByIdAsync(id));
            var Player = Assert.IsType<Player>(result?.Value);
            Assert.True(Player != null);
            Assert.Equal(Player?.Id, id);
        }

        [Fact]
        public async void GetByIdAsync_NotFound()
        {
            int id = -1;
            var result = await _playerController.GetByIdAsync(id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void PostAsync_Ok()
        {
            Player player = new Player()
            {
                Id = 0,
                Name= "test 1",
                RedCard = 1,
                YellowCard = 2,
                MinutesPlayed = 45
            };
            var result = Assert.IsType<CreatedAtActionResult>(await _playerController.PostAsync(player));
            Assert.True(result.Value != null);
            Assert.IsType<Player>(result.Value);
        }

        [Fact]
        public async void UpdateAsync_Ok()
        {
            int id = 1;
            Player player = new Player()
            {
                Id = 0,
                Name = "test 2",
                RedCard = 6,
                YellowCard = 2,
                MinutesPlayed = 45
            };
            var result = Assert.IsType<CreatedAtActionResult>(await _playerController.UpdateAsync(id, player));
            Assert.True(result.Value != null);
            Assert.IsType<Player>(result.Value);
        }

        [Fact]
        public async void UpdateAsync_NotFound()
        {
            int id = -1;
            Player player = new Player()
            {
                Id = 0,
                Name = "test 3",
                RedCard = 4,
                YellowCard = 5,
                MinutesPlayed = 45
            };
            Assert.IsType<NotFoundResult>(await _playerController.UpdateAsync(id, player));
        }

        [Fact]
        public async void DeleteAsync_Ok()
        {
            int id = -1;
            var result = Assert.IsType<OkObjectResult>(await _playerController.DeleteAsync(id));
            Assert.True(result.Value != null);
            Assert.IsType<bool>(result.Value);
        }
    }
}
