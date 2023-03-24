using Football.API.Models;
using System.Threading.Tasks;

namespace Football.API.Services
{
    public interface IManagerService
    {
        Task<object> GetAsync();
        Task<object> GetByIdAsync(int id);
        Task<object> PostAsync(Manager manager);
        Task<object> UpdateAsync(int id, Manager manager);
    }
}
