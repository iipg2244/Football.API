using Football.API.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Football.API.Services
{
    public class MatchService : IMatchService
    {
        private readonly ILogger<MatchService> _logger;
        private readonly FootballContext _footballContext;

        public MatchService(FootballContext footballContext, ILogger<MatchService> logger)
        {
            _footballContext = footballContext;
            _logger = logger;
        }

        //
        // Summary:
        //     Returns the entire list of matches ordered by id.
        //
        // Returns:
        //     A List<Match>.
        public async Task<object> GetAsync()
        {
            try
            {
                var matches = await _footballContext.Matches.Include(x => x.Referee)
                    .Include(x => x.HouseManager).Include(x => x.AwayManager)
                    .Include(x => x.HousePlayers).ThenInclude(y => y.Player)
                    .Include(x => x.AwayPlayers).ThenInclude(y => y.Player)
                    .OrderBy(x => x.Id).ToListAsync();
                if (matches != null)
                {
                    return matches;
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.ToString());
            }
            return new List<Match>();
        }

        //
        // Summary:
        //     Returns the match by id.
        //
        // Parameters:
        //     id:
        //     Match Identifier Code.
        //
        // Returns:
        //     A Match or null if not found or there is an error.
        public async Task<object> GetByIdAsync(int id)
        {
            try
            {
                return await _footballContext.Matches.Where(x => x.Id == id).Include(x => x.Referee)
                    .Include(x => x.HouseManager).Include(x => x.AwayManager)
                    .Include(x => x.HousePlayers).ThenInclude(y => y.Player)
                    .Include(x => x.AwayPlayers).ThenInclude(y => y.Player)
                    .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.ToString());
            }
            return null;
        }

        //
        // Summary:
        //     Add sent match.
        //
        // Parameters:
        //     match:
        //     Match object.
        //
        // Returns:
        //     The added match or null if there is an error.
        public async Task<object> PostAsync(Match match)
        {
            try
            {
                match.Id = 0;
                match.HouseManager = null;
                match.AwayManager = null;
                match.Referee = null;
                await _footballContext.Matches.AddAsync(match);
                await _footballContext.SaveChangesAsync();
                //Delete duplicates that could originate or that existed in the database,
                //with the home players predominating
                return await UpdateAsync(match.Id, match);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.ToString());
            }
            return null;
        }

        //
        // Summary:
        //     Update the match by id.
        //
        // Parameters:
        //     id:
        //     Match Identifier Code.
        //
        //     match:
        //     Match object.
        //
        // Returns:
        //     The updated match or null if there is an error.
        public async Task<object> UpdateAsync(int id, Match match)
        {
            try
            {
                var matchTmp = await _footballContext.Matches.Where(x => x.Id == id)
                    .Include(x => x.HousePlayers)
                    .Include(x => x.AwayPlayers).FirstOrDefaultAsync();
                if (matchTmp != null)
                {
                    matchTmp.RefereeId = match.RefereeId;
                    matchTmp.HouseManagerId = match.HouseManagerId;
                    matchTmp.AwayManagerId = match.AwayManagerId;

                    var deleteHousePlayers = matchTmp.HousePlayers.Where(x => 
                    !match.HousePlayers.Select(x => x.PlayerId).Contains(x.PlayerId)).ToList();
                    foreach(var deleteHousePlayer in deleteHousePlayers)
                    {
                        deleteHousePlayer.MatchId = matchTmp.Id;
                    }
                    var addHousePlayers = match.HousePlayers.Where(x => 
                    !matchTmp.HousePlayers.Select(x => x.PlayerId).Contains(x.PlayerId)).ToList();
                    foreach (var addHousePlayer in addHousePlayers)
                    {
                        addHousePlayer.MatchId = matchTmp.Id;
                    }

                    //Delete duplicates that could originate or that existed in the database,
                    //with the home players predominating
                    var addHousePlayersId = addHousePlayers.Select(x => x.PlayerId).ToList();
                    var matchTmpHousePlayersId = matchTmp.HousePlayers.Select(x => x.PlayerId).ToList();

                    var deleteAwayPlayers = matchTmp.AwayPlayers.Where(x => 
                    !match.AwayPlayers.Select(x => x.PlayerId).Contains(x.PlayerId) ||
                    addHousePlayersId.Contains(x.PlayerId) ||
                    matchTmpHousePlayersId.Contains(x.PlayerId)).ToList();
                    foreach (var deleteAwayPlayer in deleteAwayPlayers)
                    {
                        deleteAwayPlayer.MatchId = matchTmp.Id;
                    }
                    var addAwayPlayers = match.AwayPlayers.Where(x => 
                    !matchTmp.AwayPlayers.Select(x => x.PlayerId).Contains(x.PlayerId) &&
                    !addHousePlayersId.Contains(x.PlayerId) && 
                    !matchTmpHousePlayersId.Contains(x.PlayerId)).ToList();
                    foreach (var addAwayPlayer in addAwayPlayers)
                    {
                        addAwayPlayer.MatchId = matchTmp.Id;
                    }

                    _footballContext.PlayerHouses.RemoveRange(deleteHousePlayers);
                    await _footballContext.PlayerHouses.AddRangeAsync(addHousePlayers);

                    _footballContext.PlayerAways.RemoveRange(deleteAwayPlayers);
                    await _footballContext.PlayerAways.AddRangeAsync(addAwayPlayers);

                    await _footballContext.SaveChangesAsync();
                    return matchTmp;
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.ToString());
            }
            return null;
        }

        //
        // Summary:
        //     Delete the match by id.
        //
        // Parameters:
        //     id:
        //     Match Identifier Code.
        //
        // Returns:
        //     true is all ok or false if not found or there is an error.
        public async Task<object> DeleteAsync(int id)
        {
            try
            {
                var matchTmp = await _footballContext.Matches.FindAsync(id);
                if (matchTmp != null)
                {
                    _footballContext.Matches.Remove(matchTmp);
                    await _footballContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.ToString());
            }
            return false;
        }
    }
}
