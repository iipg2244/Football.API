namespace Football.Domain.Interfaces
{
    using Football.Infrastructure;
    using System.Threading.Tasks;

    public interface IMatchService
    {
        Task<object> GetAsync();
        Task<object> GetByIdAsync(int id);
        Task<object> PostAsync(Match match);
        Task<object> UpdateAsync(int id, Match match);
        Task<object> DeleteAsync(int id);
    }
}
