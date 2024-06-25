namespace Football.Domain.Contracts
{
    using Football.Domain.Entities.Football;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRefereeService
    {
        Task<IEnumerable<RefereeFootball>> GetRefereesAsync();
        Task<RefereeFootball> GetRefereeByIdAsync(int id);
        Task<RefereeFootball> CreateRefereeAsync(RefereeFootball referee);
        Task<RefereeFootball> UpdateRefereeAsync(int id, RefereeFootball referee);
        Task<bool> DeleteRefereeAsync(int id);
    }
}
