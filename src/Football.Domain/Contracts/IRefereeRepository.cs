namespace Football.Domain.Contracts
{
    using Football.Domain.Entities.Football;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRefereeRepository
    {
        Task<IEnumerable<RefereeFootball>> GetAsync();
        Task<RefereeFootball> GetByIdAsync(int id);
        Task<RefereeFootball> CreateAsync(RefereeFootball referee);
        Task<RefereeFootball> UpdateAsync(int id, RefereeFootball referee);
        Task<bool> DeleteAsync(int id);
    }
}
