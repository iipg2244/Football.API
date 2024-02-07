namespace Football.Domain.Interfaces
{
    using Football.Infrastructure;
    using System.Threading.Tasks;

    public interface IRefereeService
    {
        Task<object> GetAsync();
        Task<object> GetByIdAsync(int id);
        Task<object> PostAsync(Referee referee);
        Task<object> UpdateAsync(int id, Referee referee);
        Task<object> DeleteAsync(int id);
    }
}
