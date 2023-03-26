using Football.API;
using Football.API.Controllers;
using Football.API.Models;
using Football.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Xunit;

namespace TestUnit
{
    public class MatchControllerTesting
    {
        private readonly ILogger<MatchService> _logger;
        private readonly FootballContext _footballContext;
        private readonly ILogger<ManagerService> _managerlogger;
        private readonly IManagerService _managerService;
        private readonly ILogger<RefereeService> _refereelogger;
        private readonly IRefereeService _refereeService;
        private readonly ILogger<PlayerService> _playerlogger;
        private readonly IPlayerService _playerService;
        private readonly IMatchService _matchService;
        private readonly MatchController _matchController;

        public MatchControllerTesting()
        {
            _logger = new Logger<MatchService>(MyLogger.LoggerFactory);
            _footballContext = new FootballContext();
            _managerlogger = new Logger<ManagerService>(MyLogger.LoggerFactory);
            _managerService = new ManagerService(_managerlogger, _footballContext);
            _refereelogger = new Logger<RefereeService>(MyLogger.LoggerFactory);
            _refereeService = new RefereeService(_refereelogger, _footballContext);
            _playerlogger = new Logger<PlayerService>(MyLogger.LoggerFactory);
            _playerService = new PlayerService(_playerlogger, _footballContext);
            _matchService = new MatchService(_logger, _footballContext, _managerService, _refereeService, _playerService);
            _matchController = new MatchController(_matchService);
        }

        [Fact]
        public async void GetAsync_Ok()
        {
            var result = await _matchController.GetAsync();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetAsync_Quantity()
        {
            var result = (OkObjectResult)(await _matchController.GetAsync());
            var Matchs = Assert.IsType<List<Match>>(result?.Value);
            Assert.True(Matchs?.Count > 0);
        }

        [Fact]
        public async void GetByIdAsync_Ok()
        {
            int id = 1;
            var result = await _matchController.GetByIdAsync(id);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetByIdAsync_Exists()
        {
            int id = 1;
            var result = (OkObjectResult)(await _matchController.GetByIdAsync(id));
            var Match = Assert.IsType<Match>(result?.Value);
            Assert.True(Match != null);
            Assert.Equal(Match?.Id, id);
        }

        [Fact]
        public async void GetByIdAsync_NotFound()
        {
            int id = -1;
            var result = await _matchController.GetByIdAsync(id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void PostAsync_Ok()
        {
            Match match = new Match()
            {
                Id = 0,
                RefereeId = 1,
                HouseManagerId = 1,
                AwayManagerId = 2,
                HousePlayers = new List<PlayerMatchHouse>() { new PlayerMatchHouse() { MatchId = 0, PlayerId = 1 } },
                AwayPlayers = new List<PlayerMatchAway>() { new PlayerMatchAway() { MatchId = 0, PlayerId = 2 } }
            };
            var result = Assert.IsType<CreatedAtActionResult>(await _matchController.PostAsync(match));
            Assert.True(result.Value != null);
            Assert.IsType<Match>(result.Value);
        }

        [Fact]
        public async void UpdateAsync_Ok()
        {
            int id = 1;
            Match match = new Match()
            {
                Id = 0,
                RefereeId = 1,
                HouseManagerId = 1,
                AwayManagerId = 2,
                HousePlayers = new List<PlayerMatchHouse>() { new PlayerMatchHouse() { MatchId = 0, PlayerId = 1 } },
                AwayPlayers = new List<PlayerMatchAway>() { new PlayerMatchAway() { MatchId = 0, PlayerId = 2 } }
            };
            var result = Assert.IsType<CreatedAtActionResult>(await _matchController.UpdateAsync(id, match));
            Assert.True(result.Value != null);
            Assert.IsType<Match>(result.Value);
        }

        [Fact]
        public async void UpdateAsync_NotFound()
        {
            int id = -1;
            Match match = new Match()
            {
                Id = 0,
                RefereeId = 1,
                HouseManagerId = 1,
                AwayManagerId = 2,
                HousePlayers = new List<PlayerMatchHouse>() { new PlayerMatchHouse() { MatchId = 0, PlayerId = 1 } },
                AwayPlayers = new List<PlayerMatchAway>() { new PlayerMatchAway() { MatchId = 0, PlayerId = 2 } }
            };
            Assert.IsType<NotFoundResult>(await _matchController.UpdateAsync(id, match));
        }

        [Fact]
        public async void DeleteAsync_Ok()
        {
            int id = -1;
            var result = Assert.IsType<OkObjectResult>(await _matchController.DeleteAsync(id));
            Assert.True(result.Value != null);
            Assert.IsType<bool>(result.Value);
        }
    }
}
