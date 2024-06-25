namespace Football.Domain.Contracts
{
    using Football.Domain.Entities.Football;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IManagerService
    {
        Task<IEnumerable<ManagerFootball>> GetManagersAsync();
        Task<ManagerFootball> GetManagerByIdAsync(int id);
        Task<ManagerFootball> CreateManagerAsync(ManagerFootball manager);
        Task<ManagerFootball> UpdateManagerAsync(int id, ManagerFootball manager);
        Task<bool> DeleteManagerAsync(int id);
    }
}
