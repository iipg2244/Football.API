namespace Football.Application.Services
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Football.Domain.Interfaces;
    using Football.Infrastructure;
    using Football.Domain.Entities.Football;
    using Football.Domain.Extensions;

    public class PlayerService : IPlayerService
    {
        private readonly ILogger<PlayerService> _logger;
        private readonly FootballContext _footballContext;

        public PlayerService(ILogger<PlayerService> logger, FootballContext footballContext)
        {
            _logger = logger;
            _footballContext = footballContext;
        }

        //
        // Summary:
        //     Returns the entire list of players ordered by name.
        //
        // Returns:
        //     A List<Player>.
        public async Task<object> GetAsync()
        {
            try
            {
                var players = await _footballContext.Players.OrderBy(x => x.Name).ToListAsync();
                if (players != null)
                {
                    return players;
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.ToString());
            }
            return new List<Player>();
        }

        //
        // Summary:
        //     Returns the player by id.
        //
        // Parameters:
        //     id:
        //     Player Identifier Code.
        //
        // Returns:
        //     A Player or null if not found or there is an error.
        public async Task<object> GetByIdAsync(int id)
        {
            try
            {
                return await _footballContext.Players.FindAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.ToString());
            }
            return null;
        }

        //
        // Summary:
        //     Add sent player.
        //
        // Parameters:
        //     player:
        //     Player object.
        //
        // Returns:
        //     The added player or null if there is an error.
        public async Task<object> PostAsync(Player player)
        {
            try
            {
                player.Id = 0;
                player.Name = player.Name.Left(100);
                await _footballContext.Players.AddAsync(player);
                await _footballContext.SaveChangesAsync();
                return player;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.ToString());
            }
            return null;
        }

        //
        // Summary:
        //     Update the player by id.
        //
        // Parameters:
        //     id:
        //     Player Identifier Code.
        //
        //     player:
        //     Player object.
        //
        // Returns:
        //     The updated player or null if there is an error.
        public async Task<object> UpdateAsync(int id, Player player)
        {
            try
            {
                var playerTmp = await _footballContext.Players.FindAsync(id);
                if (playerTmp != null)
                {
                    playerTmp.Name = player.Name.Left(100);
                    playerTmp.YellowCard = player.YellowCard;
                    playerTmp.RedCard = player.RedCard;
                    playerTmp.MinutesPlayed = player.MinutesPlayed;
                    await _footballContext.SaveChangesAsync();
                    return playerTmp;
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.ToString());
            }
            return null;
        }

        //
        // Summary:
        //     Delete the player by id.
        //
        // Parameters:
        //     id:
        //     Player Identifier Code.
        //
        // Returns:
        //     true is all ok or false if not found or there is an error.
        public async Task<object> DeleteAsync(int id)
        {
            try
            {
                var playerTmp = await _footballContext.Players.FindAsync(id);
                if (playerTmp != null)
                {
                    _footballContext.Players.Remove(playerTmp);
                    await _footballContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.ToString());
            }
            return false;
        }

        ~PlayerService()
        {
            _footballContext.Dispose();
        }
    }
}
