using Football.API;
using Football.API.Controllers;
using Football.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace TestUnit
{
    public class ManagerControllerTesting
    {
        private readonly ILogger<ManagerService> _logger;
        private readonly FootballContext _footballContext;
        private readonly IManagerService _managerService;
        private readonly ManagerController _managerController;

        public ManagerControllerTesting()
        {
            _logger = new Logger<ManagerService>(MyLogger.LoggerFactory);
            _footballContext = new FootballContext();
            _managerService = new ManagerService(_logger, _footballContext);
            _managerController = new ManagerController(_managerService);
        }

        [Fact]
        public async void GetAsync_Ok()
        {
            var result = await _managerController.GetAsync();
            Assert.IsType<OkObjectResult>(result);
        }


    }
}
