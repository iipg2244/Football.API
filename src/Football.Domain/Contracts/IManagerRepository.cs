namespace Football.Domain.Contracts
{
    using Football.Domain.Entities.Football;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IManagerRepository
    {
        Task<IEnumerable<ManagerFootball>> GetAsync();
        Task<ManagerFootball> GetByIdAsync(int id);
        Task<ManagerFootball> CreateAsync(ManagerFootball manager);
        Task<ManagerFootball> UpdateAsync(int id, ManagerFootball manager);
        Task<bool> DeleteAsync(int id);
    }
}
