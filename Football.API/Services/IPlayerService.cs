using Football.API.Models;
using System.Threading.Tasks;

namespace Football.API.Services
{
    public interface IPlayerService
    {
        Task<object> GetAsync();
        Task<object> GetByIdAsync(int id);
        Task<object> PostAsync(Player player);
        Task<object> UpdateAsync(int id, Player player);
        Task<object> DeleteAsync(int id);
    }
}
