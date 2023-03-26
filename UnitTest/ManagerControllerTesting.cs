using Football.API;
using Football.API.Controllers;
using DDBB;
using Football.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Xunit;
using Football.API.Models.Others;
using Microsoft.Extensions.Configuration;

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
            _footballContext = new FootballContext(Methods.GetConfigurationRoot().GetConnectionString("DefaultConnection"));
            _managerService = new ManagerService(_logger, _footballContext);
            _managerController = new ManagerController(_managerService);
        }

        [Fact]
        public async void GetAsync_Ok()
        {
            var result = await _managerController.GetAsync();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetAsync_Quantity()
        {
            var result = (OkObjectResult)(await _managerController.GetAsync());
            var managers = Assert.IsType<List<Manager>>(result?.Value);
            Assert.True(managers?.Count > 0);
        }

        [Fact]
        public async void GetByIdAsync_Ok()
        {
            int id = 1;
            var result = await _managerController.GetByIdAsync(id);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetByIdAsync_Exists()
        {
            int id = 1;
            var result = (OkObjectResult)(await _managerController.GetByIdAsync(id));
            var manager = Assert.IsType<Manager>(result?.Value);
            Assert.True(manager != null);
            Assert.Equal(manager?.Id, id);
        }

        [Fact]
        public async void GetByIdAsync_NotFound()
        {
            int id = -1;
            var result = await _managerController.GetByIdAsync(id);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void PostAsync_Ok()
        {
            Manager manager = new Manager()
            {
                Id = 0,
                Name= "test 1",
                RedCard = 1,
                YellowCard = 2
            };
            var result = Assert.IsType<CreatedAtActionResult>(await _managerController.PostAsync(manager));
            Assert.True(result.Value != null);
            Assert.IsType<Manager>(result.Value);
        }

        [Fact]
        public async void UpdateAsync_Ok()
        {
            int id = 1;
            Manager manager = new Manager()
            {
                Id = 0,
                Name = "test 2",
                RedCard = 6,
                YellowCard = 2
            };
            var result = Assert.IsType<CreatedAtActionResult>(await _managerController.UpdateAsync(id, manager));
            Assert.True(result.Value != null);
            Assert.IsType<Manager>(result.Value);
        }

        [Fact]
        public async void UpdateAsync_NotFound()
        {
            int id = -1;
            Manager manager = new Manager()
            {
                Id = 0,
                Name = "test 3",
                RedCard = 4,
                YellowCard = 5
            };
            Assert.IsType<NotFoundResult>(await _managerController.UpdateAsync(id, manager));
        }

        [Fact]
        public async void DeleteAsync_Ok()
        {
            int id = -1;
            var result = Assert.IsType<OkObjectResult>(await _managerController.DeleteAsync(id));
            Assert.True(result.Value != null);
            Assert.IsType<bool>(result.Value);
        }
    }
}
