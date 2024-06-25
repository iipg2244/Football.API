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

    public class ManagerRepository : IManagerRepository
    {
        private readonly ILogger<ManagerRepository> _logger;
        private readonly FootballContext _footballContext;
        private readonly IMapper _mapper;

        public ManagerRepository(
            ILogger<ManagerRepository> logger, 
            FootballContext footballContext,
            IMapper mapper)
        {
            _logger = logger;
            _footballContext = footballContext;
            _mapper = mapper;
        }

        //
        // Summary:
        //     Returns the entire list of managers ordered by name.
        //
        // Returns:
        //     A List<Manager>.
        public async Task<IEnumerable<ManagerFootball>> GetAsync()
        {
            try
            {
                var managers = await _footballContext.Managers.OrderBy(x => x.Name).ToListAsync();
                if (managers != null)
                {
                    return _mapper.Map<List<ManagerFootball>>(managers);
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.ToString());
            }
            return new List<ManagerFootball>();
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
        public async Task<ManagerFootball> GetByIdAsync(int id)
        {
            try
            {
                var managerTmp = await _footballContext.Managers.FindAsync(id);
                return _mapper.Map<ManagerFootball>(managerTmp);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.ToString());
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
        public async Task<ManagerFootball> CreateAsync(ManagerFootball manager)
        {
            try
            {
                var managerTmp = new Manager() { 
                    Name = manager.Name.Left(100), 
                    YellowCard = manager.YellowCard, 
                    RedCard = manager.RedCard };
                await _footballContext.Managers.AddAsync(managerTmp);
                await _footballContext.SaveChangesAsync();
                return _mapper.Map<ManagerFootball>(managerTmp);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.ToString());
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
        public async Task<ManagerFootball> UpdateAsync(int id, ManagerFootball manager)
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
                    return manager;
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
        //     Delete the manager by id.
        //
        // Parameters:
        //     id:
        //     Manager Identifier Code.
        //
        // Returns:
        //     true is all ok or false if not found or there is an error.
        public async Task<bool> DeleteAsync(int id)
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
                _logger.LogInformation(e, e.ToString());
            }
            return false;
        }

        ~ManagerRepository()
        {
            _footballContext.Dispose();
        }
    }
}
