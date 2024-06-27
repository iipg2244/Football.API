namespace Football.Application.Services
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Football.Domain.Contracts;
    using Football.Domain.Entities.Football;
    using Football.Infrastructure;
    using Football.Infrastructure.Entities;
    using AutoMapper;

    public class MatchRepository : IMatchRepository
    {
        private readonly ILogger<MatchRepository> _logger;
        private readonly FootballContext _footballContext;
        private readonly IManagerRepository _managerRepository;
        private readonly IRefereeRepository _refereeRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public MatchRepository(
            ILogger<MatchRepository> logger, 
            FootballContext footballContext,
            IManagerRepository managerRepository, 
            IRefereeRepository refereeRepository, 
            IPlayerRepository playerRepository,
            IMapper mapper)
        {
            _logger = logger;
            _footballContext = footballContext;
            _managerRepository = managerRepository;
            _refereeRepository = refereeRepository;
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        //
        // Summary:
        //     Returns the entire list of matches ordered by id.
        //
        // Returns:
        //     A List<Match>.
        public async Task<IEnumerable<MatchFootball>> GetAsync()
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
                    return _mapper.Map<List<MatchFootball>>(matches);
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.ToString());
            }
            return new List<MatchFootball>();
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
        public async Task<MatchFootball> GetByIdAsync(int id)
        {
            try
            {
                var matchTmp = await _footballContext.Matches.Where(x => x.Id == id).Include(x => x.Referee)
                    .Include(x => x.HouseManager).Include(x => x.AwayManager)
                    .Include(x => x.HousePlayers).ThenInclude(y => y.Player)
                    .Include(x => x.AwayPlayers).ThenInclude(y => y.Player)
                    .FirstOrDefaultAsync();
                return _mapper.Map<MatchFootball>(matchTmp);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.ToString());
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
        public async Task<MatchFootball> CreateAsync(MatchFootball match)
        {
            try
            {
                var matchTmp = _mapper.Map<Match>(match);
                if (matchTmp.HouseManagerId != matchTmp.AwayManagerId &&
                    ((await _managerRepository.GetByIdAsync(matchTmp.HouseManagerId)) != null &&
                     (await _managerRepository.GetByIdAsync(matchTmp.AwayManagerId)) != null &&
                     (await _refereeRepository.GetByIdAsync(matchTmp.RefereeId)) != null))
                {
                    //Clear values
                    matchTmp.Id = 0;
                    matchTmp.HouseManager = null;
                    matchTmp.AwayManager = null;
                    matchTmp.Referee = null;
                    foreach (var addHousePlayer in matchTmp.HousePlayers)
                    {
                        addHousePlayer.MatchId = 0;
                        if ((await _playerRepository.GetByIdAsync(addHousePlayer.PlayerId)) == null)
                            return null;
                    }
                    foreach (var addAwayPlayer in matchTmp.AwayPlayers)
                    {
                        addAwayPlayer.MatchId = 0;
                        if ((await _playerRepository.GetByIdAsync(addAwayPlayer.PlayerId)) == null)
                            return null;
                    }
                    await _footballContext.Matches.AddAsync(matchTmp);
                    await _footballContext.SaveChangesAsync();
                    //Delete duplicates that could originate or that existed in the database,
                    //with the home players predominating
                    return await UpdateAsync(matchTmp.Id, _mapper.Map<MatchFootball>(matchTmp));
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
        public async Task<MatchFootball> UpdateAsync(int id, MatchFootball match)
        {
            try
            {
                var matchTmp = await _footballContext.Matches.Where(x => x.Id == id)
                    .Include(x => x.HousePlayers).ThenInclude(y => y.Player)
                    .Include(x => x.AwayPlayers).ThenInclude(y => y.Player)
                    .FirstOrDefaultAsync();
                if (matchTmp != null && ((match.HouseManagerId != match.AwayManagerId) ||
                    (match.HouseManagerId == 0 && match.AwayManagerId == 0)))
                {
                    if ((await _refereeRepository.GetByIdAsync(match.RefereeId)) != null)
                    {
                        matchTmp.RefereeId = match.RefereeId;
                    }
                    if ((await _managerRepository.GetByIdAsync(match.HouseManagerId)) != null)
                    {
                        matchTmp.HouseManagerId = match.HouseManagerId;
                    }
                    if ((await _managerRepository.GetByIdAsync(match.AwayManagerId)) != null)
                    {
                        matchTmp.AwayManagerId = match.AwayManagerId;
                    }

                    var deleteHousePlayers = matchTmp.HousePlayers.Where(x =>
                    !match.HousePlayers.Select(x => x.PlayerId).Contains(x.PlayerId)).ToList();
                    foreach (var deleteHousePlayer in deleteHousePlayers)
                    {
                        deleteHousePlayer.MatchId = matchTmp.Id;
                    }
                    var addHousePlayers = match.HousePlayers.Where(x =>
                    !matchTmp.HousePlayers.Select(x => x.PlayerId).Contains(x.PlayerId)).ToList();
                    foreach (var addHousePlayer in addHousePlayers)
                    {
                        addHousePlayer.MatchId = matchTmp.Id;
                        if ((await _playerRepository.GetByIdAsync(addHousePlayer.PlayerId)) == null)
                            return null;
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
                        if ((await _playerRepository.GetByIdAsync(addAwayPlayer.PlayerId)) == null)
                            return null;
                    }

                    _footballContext.PlayerHouses.RemoveRange(deleteHousePlayers);
                    await _footballContext.PlayerHouses.AddRangeAsync(_mapper.Map<List<PlayerMatchHouse>>(addHousePlayers));

                    _footballContext.PlayerAways.RemoveRange(deleteAwayPlayers);
                    await _footballContext.PlayerAways.AddRangeAsync(_mapper.Map<List<PlayerMatchAway>>(addAwayPlayers));

                    await _footballContext.SaveChangesAsync();
                    return match;
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
        //     Delete the match by id.
        //
        // Parameters:
        //     id:
        //     Match Identifier Code.
        //
        // Returns:
        //     true is all ok or false if not found or there is an error.
        public async Task<bool> DeleteAsync(int id)
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
                _logger.LogInformation(e, e.ToString());
            }
            return false;
        }

        ~MatchRepository()
        {
            _footballContext.Dispose();
        }
    }
}
