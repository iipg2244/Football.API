using DDBB;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Football.API.Services
{
    public interface IStatisticsService
    {
        Task<object> GetYellowCardsAsync();
        Task<object> GetRedCardsAsync();
        Task<object> GetMinutesPlayedAsync();
    }
}
