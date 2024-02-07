namespace Football.API.Services
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Football.Domain.Interfaces;
    using Football.API.Extensions;
    using Football.Infrastructure;

    public class RefereeService : IRefereeService
    {
        private readonly ILogger<RefereeService> _logger;
        private readonly FootballContext _footballContext;

        public RefereeService(ILogger<RefereeService> logger, FootballContext footballContext)
        {
            _logger = logger;
            _footballContext = footballContext;
        }

        //
        // Summary:
        //     Returns the entire list of referees ordered by name.
        //
        // Returns:
        //     A List<Referee>.
        public async Task<object> GetAsync()
        {
            try
            {
                var referees = await _footballContext.Referees.OrderBy(x => x.Name).ToListAsync();
                if (referees != null)
                {
                    return referees;
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.ToString());
            }
            return new List<Referee>();
        }

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
        public async Task<object> GetByIdAsync(int id)
        {
            try
            {
                return await _footballContext.Referees.FindAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.ToString());
            }
            return null;
        }

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
        public async Task<object> PostAsync(Referee referee)
        {
            try
            {
                referee.Id = 0;
                referee.Name = referee.Name.Left(100);
                await _footballContext.Referees.AddAsync(referee);
                await _footballContext.SaveChangesAsync();
                return referee;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.ToString());
            }
            return null;
        }

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
        public async Task<object> UpdateAsync(int id, Referee referee)
        {
            try
            {
                var refereeTmp = await _footballContext.Referees.FindAsync(id);
                if (refereeTmp != null)
                {
                    refereeTmp.Name = referee.Name.Left(100);
                    refereeTmp.MinutesPlayed = referee.MinutesPlayed;
                    await _footballContext.SaveChangesAsync();
                    return refereeTmp;
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
        //     Delete the referee by id.
        //
        // Parameters:
        //     id:
        //     Referee Identifier Code.
        //
        // Returns:
        //     true is all ok or false if not found or there is an error.
        public async Task<object> DeleteAsync(int id)
        {
            try
            {
                var refereeTmp = await _footballContext.Referees.FindAsync(id);
                if (refereeTmp != null)
                {
                    _footballContext.Referees.Remove(refereeTmp);
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

        ~RefereeService()
        {
            _footballContext.Dispose();
        }
    }
}
