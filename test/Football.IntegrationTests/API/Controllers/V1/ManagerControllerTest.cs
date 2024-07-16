namespace Football.IntegrationTests.API.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Football.Domain.Contracts.Refit;
    using Football.Domain.Entities.Exemples;
    using Football.Domain.Entities.Football;
    using Xunit;

    [Collection("Football.IntegrationTests.API.Controllers.V1")]
    public class ManagerControllerTest : ControllerBaseTest<IManagerController>
    {
        private ApiVersion ApiVersion { get; } = ApiVersion.v1;
        
        [Fact]
        public async Task GetManagers_WithNoData_ReturnsEmptyResponse()
        {
            // Arrange
            var manager = new ManagerFootballExemple 
            { 
                Name = "Manager 1",
                YellowCard = 1,
                RedCard = 2
            };

            // Act
            var response = await ApiClient.CreateManagerAsync(manager, ApiVersion);

            // Assert
            Assert.IsType<ManagerFootball>(response);
        }
    }
}
