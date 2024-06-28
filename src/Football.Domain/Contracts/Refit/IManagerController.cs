namespace Football.Domain.Contracts.Refit
{
    using Football.Domain.Entities.Football;
    using System.Threading.Tasks;
    using global::Refit;
    using System.Collections.Generic;
    using Football.Domain.Entities.Exemples;

    public interface IManagerController
    {
        [Get("/api/v{version}/manager")]
        Task<IEnumerable<ManagerFootball>> GetManagersAsync([Query] int version = (int)Version.v1);

        [Get("/api/v{version}/manager/options")]
        Task<ManagerFootballExemple> GetManagerByIdAsync([Header("id")] int id, [Query] int version = (int)Version.v1);

        [Post("/api/v{version}/manager/options")]
        Task<ManagerFootball> CreateManagerAsync([Body] ManagerFootballExemple manager, [Query] int version = (int)Version.v1);

        [Put("/api/v{version}/manager/options")]
        Task<ManagerFootballExemple> UpdateManagerAsync([Header("id")] int id, [Body] ManagerFootballExemple manager, [Query] int version = (int)Version.v1);

        [Delete("/api/v{version}/manager/options")]
        Task<bool> DeleteManagerAsync([Header("id")] int id, [Query] int version = (int)Version.v1);
    }
}
