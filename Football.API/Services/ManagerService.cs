namespace Football.API.Services
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Football.Domain.Interfaces;
    using Football.Infrastructure;
    using Football.Infrastructure.Extensions;

    public class ManagerService : IManagerService
    {
        private readonly ILogger<ManagerService> _logger;
        private readonly FootballContext _footballContext;

        public ManagerService(ILogger<ManagerService> logger, FootballContext footballContext)
        {
            _logger = logger;
            _footballContext = footballContext;
        }

        //
        // Summary:
        //     Returns the entire list of managers ordered by name.
        //
        // Returns:
        //     A List<Manager>.
        public async Task<object> GetAsync()
        {
            try
            {
                var managers = await _footballContext.Managers.OrderBy(x => x.Name).ToListAsync();
                if (managers != null)
                {
                    return managers;
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.ToString());
            }
            return new List<Manager>();
        }

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
        public async Task<object> GetByIdAsync(int id)
        {
            try
            {
                return await _footballContext.Managers.FindAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.ToString());
            }
            return null;
        }

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
        public async Task<object> PostAsync(Manager manager)
        {
            try
            {
                manager.Id = 0;
                manager.Name = manager.Name.Left(100);
                await _footballContext.Managers.AddAsync(manager);
                await _footballContext.SaveChangesAsync();
                return manager;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.ToString());
            }
            return null;
        }

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
        public async Task<object> UpdateAsync(int id, Manager manager)
        {
            try
            {
                var managerTmp = await _footballContext.Managers.FindAsync(id);
                if (managerTmp != null)
                {
                    managerTmp.Name = manager.Name.Left(100);
                    managerTmp.YellowCard = manager.YellowCard;
                    managerTmp.RedCard = manager.RedCard;
                    await _footballContext.SaveChangesAsync();
                    return managerTmp;
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
        //     Delete the manager by id.
        //
        // Parameters:
        //     id:
        //     Manager Identifier Code.
        //
        // Returns:
        //     true is all ok or false if not found or there is an error.
        public async Task<object> DeleteAsync(int id)
        {
            try
            {
                var managerTmp = await _footballContext.Managers.FindAsync(id);
                if (managerTmp != null)
                {
                    _footballContext.Managers.Remove(managerTmp);
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

        ~ManagerService()
        {
            _footballContext.Dispose();
        }
    }
}
