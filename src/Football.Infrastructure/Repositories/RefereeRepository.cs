namespace Football.Application.Services
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

    public class RefereeRepository : IRefereeRepository
    {
        private readonly ILogger<RefereeRepository> _logger;
        private readonly FootballContext _footballContext;
        private readonly IMapper _mapper;

        public RefereeRepository(
            ILogger<RefereeRepository> logger,
            FootballContext footballContext,
            IMapper mapper)
        {
            _logger = logger;
            _footballContext = footballContext;
            _mapper = mapper;
        }

        //
        // Summary:
        //     Returns the entire list of referees ordered by name.
        //
        // Returns:
        //     A List<Referee>.
        public async Task<IEnumerable<RefereeFootball>> GetAsync()
        {
            try
            {
                var referees = await _footballContext.Referees.OrderBy(x => x.Name).ToListAsync();
                if (referees != null)
                {
                    return _mapper.Map<List<RefereeFootball>>(referees);
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.ToString());
            }
            return new List<RefereeFootball>();
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
        public async Task<RefereeFootball> GetByIdAsync(int id)
        {
            try
            {
                var refereeTmp = await _footballContext.Referees.FindAsync(id);
                return _mapper.Map<RefereeFootball>(refereeTmp);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.ToString());
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
        public async Task<RefereeFootball> CreateAsync(RefereeFootball referee)
        {
            try
            {
                var refereeTmp = new Referee
                {
                    Name = referee.Name.Left(100),
                    MinutesPlayed = referee.MinutesPlayed
                };
                await _footballContext.Referees.AddAsync(refereeTmp);
                await _footballContext.SaveChangesAsync();
                return _mapper.Map<RefereeFootball>(refereeTmp);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.ToString());
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
        public async Task<RefereeFootball> UpdateAsync(int id, RefereeFootball referee)
        {
            try
            {
                var refereeTmp = await _footballContext.Referees.FindAsync(id);
                if (refereeTmp != null)
                {
                    refereeTmp.Name = referee.Name.Left(100);
                    refereeTmp.MinutesPlayed = referee.MinutesPlayed;
                    await _footballContext.SaveChangesAsync();
                    return referee;
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
        //     Delete the referee by id.
        //
        // Parameters:
        //     id:
        //     Referee Identifier Code.
        //
        // Returns:
        //     true is all ok or false if not found or there is an error.
        public async Task<bool> DeleteAsync(int id)
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
                _logger.LogInformation(e, e.ToString());
            }
            return false;
        }

        ~RefereeRepository()
        {
            _footballContext.Dispose();
        }
    }
}
