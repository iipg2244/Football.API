namespace Football.Application.Services
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Football.Domain.Contracts;
    using Football.Domain.Entities.Football;

    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;

        public MatchService(IMatchRepository matchRepository) => _matchRepository = matchRepository;

        //
        // Summary:
        //     Returns the entire list of matches ordered by id.
        //
        // Returns:
        //     A List<Match>.
        public async Task<IEnumerable<MatchFootball>> GetMatchesAsync() => await _matchRepository.GetAsync();

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
        public async Task<MatchFootball> GetMatchByIdAsync(int id) => await _matchRepository.GetByIdAsync(id);

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
        public async Task<MatchFootball> CreateMatchAsync(MatchFootball match) => await _matchRepository.CreateAsync(match);

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
        public async Task<MatchFootball> UpdateMatchAsync(int id, MatchFootball match) => await _matchRepository.UpdateAsync(id, match);

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
        public async Task<bool> DeleteMatchAsync(int id) => await _matchRepository.DeleteAsync(id);

        ~MatchService()
        {
        }
    }
}
