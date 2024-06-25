namespace Football.Domain.Contracts
{
    using Football.Domain.Entities.Football;
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
