namespace Football.Domain.Contracts
{
    using Football.Domain.Entities.Football;
    using System.Threading.Tasks;

    public interface IManagerService
    {
        Task<object> GetAsync();
        Task<object> GetByIdAsync(int id);
        Task<object> PostAsync(Manager manager);
        Task<object> UpdateAsync(int id, Manager manager);
        Task<object> DeleteAsync(int id);
    }
}
