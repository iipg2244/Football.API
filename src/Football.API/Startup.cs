namespace Football.API
{
    using System;
    using System.Linq;
    using Football.Infrastructure;
    using Football.Domain.Contracts;
    using Football.Application.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json.Serialization;
    using Football.Infrastructure.Repositories;
    using Football.Infrastructure.Mappings;

    public class Startup
    {
        readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(x =>
             {
                 x.AddPolicy("MyPolicy", y =>
                 {
                     y.AllowAnyOrigin()
                     .WithMethods("GET", "POST", "PUT", "DELETE")
                     .AllowAnyHeader()
                     .Build();
                 });
             });

            services.AddDbContext<FootballContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(MappingInfrastructureProfile));
            services.AddAutoMapper(typeof(MappingDomainProfile));
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IRefereeRepository, RefereeRepository>();
            services.AddScoped<IRefereeService, RefereeService>();
            //services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddHostedService<JobScheduledService>();

            services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            });

            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Implement Swagger UI v1",
                    Description = "A simple example to Implement Swagger UI v1",
                });
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "Implement Swagger UI v2",
                    Description = "A simple example to Implement Swagger UI v2",
                });
                options.IncludeXmlComments($"{AppContext.BaseDirectory}/SwaggerDocs/football-api.xml");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            ////app.UseEndpoints(endpoints =>
            ////{
            ////    endpoints.MapControllerRoute(
            ////        name: "default",
            ////        pattern: "{controller=Home}/{action=Index}/{id?}");
            ////});

            app.UseSwagger();
            app.UseSwaggerUI(endpoints =>
            {
                foreach (var description in provider.ApiVersionDescriptions.Select(x => x.GroupName))
                {
                    endpoints.SwaggerEndpoint($"/swagger/{description}/swagger.json", description);
                }
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
