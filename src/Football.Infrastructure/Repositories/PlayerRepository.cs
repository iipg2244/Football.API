namespace Football.Infrastructure.Repositories
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
    using Football.Domain.Extensions;
    using Football.Infrastructure.Entities;
    using AutoMapper;

    public class PlayerRepository : IPlayerRepository
    {
        private readonly ILogger<PlayerRepository> _logger;
        private readonly FootballContext _footballContext;
        private readonly IMapper _mapper;

        public PlayerRepository(
            ILogger<PlayerRepository> logger,
            FootballContext footballContext,
            IMapper mapper)
        {
            _logger = logger;
            _footballContext = footballContext;
            _mapper = mapper;
        }

        //
        // Summary:
        //     Returns the entire list of players ordered by name.
        //
        // Returns:
        //     A List<Player>.
        public async Task<IEnumerable<PlayerFootball>> GetAsync()
        {
            try
            {
                var players = await _footballContext.Players.OrderBy(x => x.Name).ToListAsync();
                if (players != null)
                {
                    return _mapper.Map<List<PlayerFootball>>(players);
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.ToString());
            }
            return new List<PlayerFootball>();
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
        public async Task<PlayerFootball> GetByIdAsync(int id)
        {
            try
            {
                var playerTmp = await _footballContext.Players.FindAsync(id);
                return _mapper.Map<PlayerFootball>(playerTmp);
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
        public async Task<PlayerFootball> CreateAsync(PlayerFootball player)
        {
            try
            {
                var playerTmp = new Player()
                {
                    Name = player.Name.Left(100),
                    YellowCard = player.YellowCard,
                    RedCard = player.RedCard,
                    MinutesPlayed = player.MinutesPlayed
                };               
                await _footballContext.Players.AddAsync(playerTmp);
                await _footballContext.SaveChangesAsync();
                return _mapper.Map<PlayerFootball>(playerTmp);
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
        public async Task<PlayerFootball> UpdateAsync(int id, PlayerFootball player)
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
                    return player;
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
        public async Task<bool> DeleteAsync(int id)
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

        ~PlayerRepository()
        {
            _footballContext.Dispose();
        }
    }
}
