using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NEFBDAACommons.Configuration.Models;
using NEFBDAACommons.DI.Services;

namespace NEFBDAACommons.Configuration
{
    public abstract class BaseStartup
    {
        public BaseStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DependencyInjector.InitServices(services);
            InitDependecyInjection(Configuration, services, GetConfig());
            StartupHelper.ConfigureServicesForApi(Configuration, services, GetConfig());
            StartupHelper.ConfigureServicesForApp(Configuration, services, GetConfig());
            OnConfigureServices(Configuration, services);
        }

        protected abstract void InitDependecyInjection(IConfiguration configuration, IServiceCollection services, AppConfig appConfig);

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder application, IWebHostEnvironment env, ILoggerFactory loggerFactory, IHostApplicationLifetime applicationLifetime)
        {
            StartupHelper.ConfigureForApi(application, env, loggerFactory, GetConfig());
            StartupHelper.ConfigureForApp(application, env, loggerFactory, GetConfig());
            OnConfigure(application, env, loggerFactory);
            applicationLifetime.ApplicationStarted.Register(OnApplicationStarted);
        }

        public virtual void OnApplicationStarted()
        {

        }
        //services.AddDependencyInjectionCustom(services, configuration, appConfig);
        protected abstract AppConfig GetConfig();
        protected virtual void OnConfigure(IApplicationBuilder application, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

        }
        protected virtual void OnConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
        }
    }
}
