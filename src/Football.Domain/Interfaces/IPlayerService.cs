namespace Football.Domain.Interfaces
{
    using Football.Domain.Entities.Football;
    using System.Threading.Tasks;

    public interface IPlayerService
    {
        Task<object> GetAsync();
        Task<object> GetByIdAsync(int id);
        Task<object> PostAsync(Player player);
        Task<object> UpdateAsync(int id, Player player);
        Task<object> DeleteAsync(int id);
    }
}
