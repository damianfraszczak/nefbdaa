using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NEFBDAACommons.Configuration;
using NEFBDAACommons.Configuration.Models;
using NEFBDAACommons.Database.Services;
using NEFBDAACommons.Security.Helpers;
using NEFBDAACommons.Web.Filters;
using NEFBDAAStarter.Configuration;
using NEFBDAAStarter.Database;

namespace NEFBDAAStarter
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }


        protected override void InitDependecyInjection(IConfiguration configuration, IServiceCollection services,
            AppConfig appConfig)
        {
            services.AddScoped<IServicesProviderLocator, ServicesProviderLocator>();
            // repositories, services injected via annotations
            services.AddDependencyInjectionCustom<DatabaseContext>(configuration, appConfig);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Auth
            services.AddScoped<ICriptography, Criptography>();
            // Filters
            services.AddScoped<ExceptionFilter>();
        }

        public override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            JobsScheduler.OrganizeJobs();
            ModelMappingConfig.ConfigureMappings();
        }

        protected override AppConfig GetConfig()
        {
            return new AppConfig()
            {
                AppName = "Generic app",
                AuthConfig = new AuthConfig() { },
                HangfireConfig = new HangfireConfig(),
                SwaggerConfig = new SwaggerConfig(),
                SpaConfig = new SpaConfig()
                {
                    RootPath = "./angular/dist/front",
                    SpaOptions = new Microsoft.AspNetCore.SpaServices.SpaOptions()
                    {
                        SourcePath = "./angular"
                    }
                },
                LicenceKey = "MkCzLQ5pJr1vLB8="
            };
        }
    }
}