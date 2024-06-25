namespace Football.Application.Services
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Football.Domain.Contracts;
    using Football.Infrastructure;
    using Football.Domain.Entities.Football;

    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly ILogger<StatisticsRepository> _logger;
        private readonly FootballContext _footballContext;

        public StatisticsRepository(
            ILogger<StatisticsRepository> logger, 
            FootballContext footballContext)
        {
            _logger = logger;
            _footballContext = footballContext;
        }

        //
        // Summary:
        //     Returns the entire list of of people who have participated in matches and have yellow cards.
        //
        // Returns:
        //     A List<StatisticFootball>.
        public async Task<IEnumerable<StatisticFootball>> GetYellowCardsAsync()
        {
            try
            {
                List<StatisticFootball> playerHouses = await _footballContext.Players.Join(
                      _footballContext.PlayerHouses,
                      player => player.Id,
                      playerHouse => playerHouse.PlayerId,
                      (player, playerHouse) => new StatisticFootball()
                      {
                          Id = player.Id,
                          Name = player.Name,
                          TeamName = "PlayerHouse",
                          Total = player.YellowCard
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticFootball> playerAways = await _footballContext.Players.Join(
                      _footballContext.PlayerAways,
                      player => player.Id,
                      playerAway => playerAway.PlayerId,
                      (player, playerAway) => new StatisticFootball()
                      {
                          Id = player.Id,
                          Name = player.Name,
                          TeamName = "PlayerAway",
                          Total = player.YellowCard
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticFootball> managerHouses = await _footballContext.Managers.Join(
                      _footballContext.Matches,
                      managerHouse => managerHouse.Id,
                      match => match.HouseManagerId,
                      (managerHouse, match) => new StatisticFootball()
                      {
                          Id = managerHouse.Id,
                          Name = managerHouse.Name,
                          TeamName = "ManagerHouse",
                          Total = managerHouse.YellowCard
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticFootball> managerAways = await _footballContext.Managers.Join(
                      _footballContext.Matches,
                      managerAway => managerAway.Id,
                      match => match.AwayManagerId,
                      (managerAway, match) => new StatisticFootball()
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
                _logger.LogInformation(e, e.ToString());
            }
            return new List<StatisticFootball>();
        }

        //
        // Summary:
        //     Returns the entire list of of people who have participated in matches and have red cards.
        //
        // Returns:
        //     A List<StatisticFootball>.
        public async Task<IEnumerable<StatisticFootball>> GetRedCardsAsync()
        {
            try
            {
                List<StatisticFootball> playerHouses = await _footballContext.Players.Join(
                      _footballContext.PlayerHouses,
                      player => player.Id,
                      playerHouse => playerHouse.PlayerId,
                      (player, playerHouse) => new StatisticFootball()
                      {
                          Id = player.Id,
                          Name = player.Name,
                          TeamName = "PlayerHouse",
                          Total = player.RedCard
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticFootball> playerAways = await _footballContext.Players.Join(
                      _footballContext.PlayerAways,
                      player => player.Id,
                      playerAway => playerAway.PlayerId,
                      (player, playerAway) => new StatisticFootball()
                      {
                          Id = player.Id,
                          Name = player.Name,
                          TeamName = "PlayerAway",
                          Total = player.YellowCard
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticFootball> managerHouses = await _footballContext.Managers.Join(
                      _footballContext.Matches,
                      managerHouse => managerHouse.Id,
                      match => match.HouseManagerId,
                      (managerHouse, match) => new StatisticFootball()
                      {
                          Id = managerHouse.Id,
                          Name = managerHouse.Name,
                          TeamName = "ManagerHouse",
                          Total = managerHouse.RedCard
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticFootball> managerAways = await _footballContext.Managers.Join(
                      _footballContext.Matches,
                      managerAway => managerAway.Id,
                      match => match.AwayManagerId,
                      (managerAway, match) => new StatisticFootball()
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
                _logger.LogInformation(e, e.ToString());
            }
            return new List<StatisticFootball>();
        }

        //
        // Summary:
        //     Returns the entire list of of people who have participated in matches and have minutes played.
        //
        // Returns:
        //     A List<StatisticFootball>.
        public async Task<IEnumerable<StatisticFootball>> GetMinutesPlayedAsync()
        {
            try
            {
                List<StatisticFootball> playerHouses = await _footballContext.Players.Join(
                      _footballContext.PlayerHouses,
                      player => player.Id,
                      playerHouse => playerHouse.PlayerId,
                      (player, playerHouse) => new StatisticFootball()
                      {
                          Id = player.Id,
                          Name = player.Name,
                          TeamName = "PlayerHouse",
                          Total = player.MinutesPlayed
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticFootball> playerAways = await _footballContext.Players.Join(
                      _footballContext.PlayerAways,
                      player => player.Id,
                      playerAway => playerAway.PlayerId,
                      (player, playerAway) => new StatisticFootball()
                      {
                          Id = player.Id,
                          Name = player.Name,
                          TeamName = "PlayerAway",
                          Total = player.MinutesPlayed
                      }).Distinct().Where(x => x.Total > 0).ToListAsync();

                List<StatisticFootball> referees = await _footballContext.Referees.Join(
                      _footballContext.Matches,
                      referee => referee.Id,
                      match => match.RefereeId,
                      (referee, match) => new StatisticFootball()
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
                _logger.LogInformation(e, e.ToString());
            }
            return new List<StatisticFootball>();
        }

        ~StatisticsRepository()
        {
            _footballContext.Dispose();
        }
    }
}
