namespace Football.API.Services
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Football.API.DTOs;
    using Football.Domain.Interfaces;
    using Football.Infrastructure;

    public class StatisticsService : IStatisticsService
    {
        private readonly ILogger<StatisticsService> _logger;
        private readonly FootballContext _footballContext;

        public StatisticsService(ILogger<StatisticsService> logger, FootballContext footballContext)
        {
            _logger = logger;
            _footballContext = footballContext;
        }

        //
        // Summary:
        //     Returns the entire list of of people who have participated in matches and have yellow cards.
        //
        // Returns:
        //     A List<dynamic>.
        public async Task<object> GetYellowCardsAsync()
        {
            try
            {
                List<StatisticDto> playerHouses = await _footballContext.Players.Join(
                      _footballContext.PlayerHouses,
                      player => player.Id,
                      playerHouse => playerHouse.PlayerId,
                      (player, playerHouse) => new StatisticDto()
                      {
                          Id = player.Id,
                          Name = player.Name,
                          TeamName = "PlayerHouse",
                          Total = player.YellowCard
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticDto> playerAways = await _footballContext.Players.Join(
                      _footballContext.PlayerAways,
                      player => player.Id,
                      playerAway => playerAway.PlayerId,
                      (player, playerAway) => new StatisticDto()
                      {
                          Id = player.Id,
                          Name = player.Name,
                          TeamName = "PlayerAway",
                          Total = player.YellowCard
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticDto> managerHouses = await _footballContext.Managers.Join(
                      _footballContext.Matches,
                      managerHouse => managerHouse.Id,
                      match => match.HouseManagerId,
                      (managerHouse, match) => new StatisticDto()
                      {
                          Id = managerHouse.Id,
                          Name = managerHouse.Name,
                          TeamName = "ManagerHouse",
                          Total = managerHouse.YellowCard
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticDto> managerAways = await _footballContext.Managers.Join(
                      _footballContext.Matches,
                      managerAway => managerAway.Id,
                      match => match.AwayManagerId,
                      (managerAway, match) => new StatisticDto()
                      {
                          Id = managerAway.Id,
                          Name = managerAway.Name,
                          TeamName = "ManagerAway",
                          Total = managerAway.YellowCard
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

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

        //
        // Summary:
        //     Returns the entire list of of people who have participated in matches and have red cards.
        //
        // Returns:
        //     A List<dynamic>.
        public async Task<object> GetRedCardsAsync()
        {
            try
            {
                List<StatisticDto> playerHouses = await _footballContext.Players.Join(
                      _footballContext.PlayerHouses,
                      player => player.Id,
                      playerHouse => playerHouse.PlayerId,
                      (player, playerHouse) => new StatisticDto()
                      {
                          Id = player.Id,
                          Name = player.Name,
                          TeamName = "PlayerHouse",
                          Total = player.RedCard
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticDto> playerAways = await _footballContext.Players.Join(
                      _footballContext.PlayerAways,
                      player => player.Id,
                      playerAway => playerAway.PlayerId,
                      (player, playerAway) => new StatisticDto()
                      {
                          Id = player.Id,
                          Name = player.Name,
                          TeamName = "PlayerAway",
                          Total = player.YellowCard
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticDto> managerHouses = await _footballContext.Managers.Join(
                      _footballContext.Matches,
                      managerHouse => managerHouse.Id,
                      match => match.HouseManagerId,
                      (managerHouse, match) => new StatisticDto()
                      {
                          Id = managerHouse.Id,
                          Name = managerHouse.Name,
                          TeamName = "ManagerHouse",
                          Total = managerHouse.RedCard
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticDto> managerAways = await _footballContext.Managers.Join(
                      _footballContext.Matches,
                      managerAway => managerAway.Id,
                      match => match.AwayManagerId,
                      (managerAway, match) => new StatisticDto()
                      {
                          Id = managerAway.Id,
                          Name = managerAway.Name,
                          TeamName = "ManagerAway",
                          Total = managerAway.RedCard
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

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

        //
        // Summary:
        //     Returns the entire list of of people who have participated in matches and have minutes played.
        //
        // Returns:
        //     A List<dynamic>.
        public async Task<object> GetMinutesPlayedAsync()
        {
            try
            {
                List<StatisticDto> playerHouses = await _footballContext.Players.Join(
                      _footballContext.PlayerHouses,
                      player => player.Id,
                      playerHouse => playerHouse.PlayerId,
                      (player, playerHouse) => new StatisticDto()
                      {
                          Id = player.Id,
                          Name = player.Name,
                          TeamName = "PlayerHouse",
                          Total = player.MinutesPlayed
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticDto> playerAways = await _footballContext.Players.Join(
                      _footballContext.PlayerAways,
                      player => player.Id,
                      playerAway => playerAway.PlayerId,
                      (player, playerAway) => new StatisticDto()
                      {
                          Id = player.Id,
                          Name = player.Name,
                          TeamName = "PlayerAway",
                          Total = player.MinutesPlayed
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticDto> referees = await _footballContext.Referees.Join(
                      _footballContext.Matches,
                      referee => referee.Id,
                      match => match.RefereeId,
                      (referee, match) => new StatisticDto()
                      {
                          Id = referee.Id,
                          Name = referee.Name,
                          TeamName = "Referee",
                          Total = referee.MinutesPlayed
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

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

        ~StatisticsService()
        {
            _footballContext.Dispose();
        }
    }
}
