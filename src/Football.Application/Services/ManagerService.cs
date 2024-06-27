namespace Football.Application.Services
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Football.Domain.Contracts;
    using Football.Domain.Entities.Football;
    using Football.Domain.Extensions;

    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;

        public ManagerService(IManagerRepository managerRepository) => _managerRepository = managerRepository;

        //
        // Summary:
        //     Returns the entire list of managers ordered by name.
        //
        // Returns:
        //     A List<Manager>.
        public async Task<IEnumerable<ManagerFootball>> GetManagersAsync() => await _managerRepository.GetAsync();

        //
        // Summary:
        //     Returns the manager by id.
        //
        // Parameters:
        //     id:
        //     Manager Identifier Code.
        //
        // Returns:
        //     A Manager or null if not found or there is an error.
        public async Task<ManagerFootball> GetManagerByIdAsync(int id) => await _managerRepository.GetByIdAsync(id);

        //
        // Summary:
        //     Add sent manager.
        //
        // Parameters:
        //     manager:
        //     Manager object.
        //
        // Returns:
        //     The added manager or null if there is an error.
        public async Task<ManagerFootball> CreateManagerAsync(ManagerFootball manager) => await _managerRepository.CreateAsync(manager);

        //
        // Summary:
        //     Update the manager by id.
        //
        // Parameters:
        //     id:
        //     Manager Identifier Code.
        //
        //     manager:
        //     Manager object.
        //
        // Returns:
        //     The updated manager or null if there is an error.
        public async Task<ManagerFootball> UpdateManagerAsync(int id, ManagerFootball manager) => await _managerRepository.UpdateAsync(id, manager);

        //
        // Summary:
        //     Delete the manager by id.
        //
        // Parameters:
        //     id:
        //     Manager Identifier Code.
        //
        // Returns:
        //     true is all ok or false if not found or there is an error.
        public async Task<bool> DeleteManagerAsync(int id) => await _managerRepository.DeleteAsync(id);

        ~ManagerService()
        {
        }
    }
}
