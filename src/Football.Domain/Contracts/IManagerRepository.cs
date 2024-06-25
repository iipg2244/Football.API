namespace Football.Domain.Contracts
{
    using Football.Domain.Entities.Football;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IManagerRepository
    {
        Task<List<ManagerFootball>> GetAsync();
        Task<ManagerFootball> GetByIdAsync(int id);
        Task<ManagerFootball> PostAsync(ManagerFootball manager);
        Task<ManagerFootball> UpdateAsync(int id, ManagerFootball manager);
        Task<bool> DeleteAsync(int id);
    }
}
