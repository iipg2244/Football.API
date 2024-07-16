namespace Football.IntegrationTests.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Football.IntegrationTests.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class ControllerBaseTest<T>
    {
        protected ControllerBaseTest()
        {
            webApplicationFactory = new CustomWebApplicationFactory();
            var scopeProvider = webApplicationFactory.Services.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider;
            if (scopeProvider != null)
            {
                ApiClient = scopeProvider.GetService<T>();
            }
        }

        public CustomWebApplicationFactory webApplicationFactory { get; set; }

        public T ApiClient { get; set; }
    }
}
