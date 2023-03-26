using Football.API.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Football.API.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ILogger<StatisticsService> _logger;
        private readonly FootballContext _footballContext;

        public StatisticsService(ILogger<StatisticsService> logger, FootballContext footballContext)
        {
            _logger = logger;
            _footballContext = footballContext;
        }

        public async Task<object> GetYellowCardsAsync()
        {
            try
            {
                var playerHouses = await _footballContext.Players.Join(
                      _footballContext.PlayerHouses,
                      player => player.Id,
                      playerHouse => playerHouse.PlayerId,
                      (player, playerHouse) => new
                      {
                          player.Id,
                          player.Name,
                          TeamName = "PlayerHouse",
                          Total = player.YellowCard
                      }).Distinct().ToListAsync();

                var playerAways = await _footballContext.Players.Join(
                      _footballContext.PlayerAways,
                      player => player.Id,
                      playerAway => playerAway.PlayerId,
                      (player, playerAway) => new
                      {
                          player.Id,
                          player.Name,
                          TeamName = "PlayerAway",
                          Total = player.YellowCard
                      }).Distinct().ToListAsync();

                var managerHouses = await _footballContext.Managers.Join(
                      _footballContext.Matches,
                      managerHouse => managerHouse.Id,
                      match => match.HouseManagerId,
                      (managerHouse, match) => new
                      {
                          managerHouse.Id,
                          managerHouse.Name,
                          TeamName = "ManagerHouse",
                          Total = managerHouse.YellowCard
                      }).Distinct().ToListAsync();

                var managerAways = await _footballContext.Managers.Join(
                      _footballContext.Matches,
                      managerAway => managerAway.Id,
                      match => match.AwayManagerId,
                      (managerAway, match) => new
                      {
                          managerAway.Id,
                          managerAway.Name,
                          TeamName = "ManagerAway",
                          Total = managerAway.YellowCard
                      }).Distinct().ToListAsync();

                playerHouses.AddRange(playerAways);
                playerHouses.AddRange(managerHouses);
                playerHouses.AddRange(managerAways);

                return playerHouses;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.ToString());
            }
            return new List<dynamic>();
        }

        public async Task<object> GetRedCardsAsync()
        {
            try
            {
                var playerHouses = await _footballContext.Players.Join(
                      _footballContext.PlayerHouses,
                      player => player.Id,
                      playerHouse => playerHouse.PlayerId,
                      (player, playerHouse) => new
                      {
                          player.Id,
                          player.Name,
                          TeamName = "PlayerHouse",
                          Total = player.RedCard
                      }).Distinct().ToListAsync();

                var playerAways = await _footballContext.Players.Join(
                      _footballContext.PlayerAways,
                      player => player.Id,
                      playerAway => playerAway.PlayerId,
                      (player, playerAway) => new
                      {
                          player.Id,
                          player.Name,
                          TeamName = "PlayerAway",
                          Total = player.YellowCard
                      }).Distinct().ToListAsync();

                var managerHouses = await _footballContext.Managers.Join(
                      _footballContext.Matches,
                      managerHouse => managerHouse.Id,
                      match => match.HouseManagerId,
                      (managerHouse, match) => new
                      {
                          managerHouse.Id,
                          managerHouse.Name,
                          TeamName = "ManagerHouse",
                          Total = managerHouse.RedCard
                      }).Distinct().ToListAsync();

                var managerAways = await _footballContext.Managers.Join(
                      _footballContext.Matches,
                      managerAway => managerAway.Id,
                      match => match.AwayManagerId,
                      (managerAway, match) => new
                      {
                          managerAway.Id,
                          managerAway.Name,
                          TeamName = "ManagerAway",
                          Total = managerAway.RedCard
                      }).Distinct().ToListAsync();

                playerHouses.AddRange(playerAways);
                playerHouses.AddRange(managerHouses);
                playerHouses.AddRange(managerAways);

                return playerHouses;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.ToString());
            }
            return new List<dynamic>();
        }

        public async Task<object> GetMinutesPlayedAsync()
        {
            try
            {
                var playerHouses = await _footballContext.Players.Join(
                      _footballContext.PlayerHouses,
                      player => player.Id,
                      playerHouse => playerHouse.PlayerId,
                      (player, playerHouse) => new
                      {
                          player.Id,
                          player.Name,
                          TeamName = "PlayerHouse",
                          Total = player.MinutesPlayed
                      }).Distinct().ToListAsync();

                var playerAways = await _footballContext.Players.Join(
                      _footballContext.PlayerAways,
                      player => player.Id,
                      playerAway => playerAway.PlayerId,
                      (player, playerAway) => new
                      {
                          player.Id,
                          player.Name,
                          TeamName = "PlayerAway",
                          Total = player.MinutesPlayed
                      }).Distinct().ToListAsync();

                var referees = await _footballContext.Referees.Join(
                      _footballContext.Matches,
                      referee => referee.Id,
                      match => match.RefereeId,
                      (referee, match) => new
                      {
                          referee.Id,
                          referee.Name,
                          TeamName = "Referee",
                          Total = referee.MinutesPlayed
                      }).Distinct().ToListAsync();

                playerHouses.AddRange(playerAways);
                playerHouses.AddRange(referees);

                return playerHouses;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.ToString());
            }
            return new List<dynamic>();
        }

    }
}
