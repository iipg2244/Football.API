namespace Football.Application.Services
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Football.Domain.Contracts;
    using Football.Domain.Entities.Football;
    using Football.Domain.Extensions;

    public class RefereeService : IRefereeService
    {
        private readonly IRefereeRepository _refereeRepository;

        public RefereeService(IRefereeRepository refereeRepository) => _refereeRepository = refereeRepository;

        //
        // Summary:
        //     Returns the entire list of referees ordered by name.
        //
        // Returns:
        //     A List<Referee>.
        public async Task<IEnumerable<RefereeFootball>> GetRefereesAsync() => await _refereeRepository.GetAsync();

        //
        // Summary:
        //     Returns the referee by id.
        //
        // Parameters:
        //     id:
        //     Referee Identifier Code.
        //
        // Returns:
        //     A Referee or null if not found or there is an error.
        public async Task<RefereeFootball> GetRefereeByIdAsync(int id) => await _refereeRepository.GetByIdAsync(id);

        //
        // Summary:
        //     Add sent referee.
        //
        // Parameters:
        //     referee:
        //     Referee object.
        //
        // Returns:
        //     The added referee or null if there is an error.
        public async Task<RefereeFootball> CreateRefereeAsync(RefereeFootball referee) => await _refereeRepository.CreateAsync(referee);

        //
        // Summary:
        //     Update the referee by id.
        //
        // Parameters:
        //     id:
        //     Referee Identifier Code.
        //
        //     referee:
        //     Referee object.
        //
        // Returns:
        //     The updated referee or null if there is an error.
        public async Task<RefereeFootball> UpdateRefereeAsync(int id, RefereeFootball referee) => await _refereeRepository.UpdateAsync(id, referee);

        //
        // Summary:
        //     Delete the referee by id.
        //
        // Parameters:
        //     id:
        //     Referee Identifier Code.
        //
        // Returns:
        //     true is all ok or false if not found or there is an error.
        public async Task<bool> DeleteRefereeAsync(int id) => await _refereeRepository.DeleteAsync(id);

        ~RefereeService()
        {
        }
    }
}
