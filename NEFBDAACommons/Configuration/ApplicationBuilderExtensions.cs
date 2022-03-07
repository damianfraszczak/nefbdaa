using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Hosting;
using NEFBDAACommons.Configuration.Models;
using NEFBDAACommons.DI.Services;
using NEFBDAACommons.Shared.Services;
using NEFBDAACommons.Web.Filters;
using System.Collections.Generic;

namespace NEFBDAACommons.Configuration
{
  public static class ApplicationBuilderExtensions
  {

    private static IWebHostEnvironment Environment { get; } = DependencyInjector.GetService<IWebHostEnvironment>();

    public static void UseCorsCustom(this IApplicationBuilder application, AppConfig config)
    {
      application.UseCors(CommonsConstants.CORS_ALLOW_POLICY_NAME);
    }
    public static void UseExceptionCustom(this IApplicationBuilder application)
    {
      
      if (Environment.IsDevelopment())
      {
        application.UseDatabaseErrorPage();
        application.UseDeveloperExceptionPage();
      }
    }
    public static void UseHangfire(this IApplicationBuilder application, AppConfig config)
    {
      if (config.HangfireConfig != null)
      {
        var serverOptions = new BackgroundJobServerOptions
        {
          WorkerCount = config.HangfireConfig.WorkerCount,
          ServerName = config.HangfireConfig.ServerName

        };
        //one thread to avoid invoking 2 jobs synchronously
        application.UseHangfireServer(serverOptions);
        var dashboardOptions = new DashboardOptions
        {
          Authorization = new List<IDashboardAuthorizationFilter>() {
                    new HangfireAuthorizationFilter(config.HangfireConfig.AuthProvider)
                },
        };
        application.UseHangfireDashboard(config.HangfireConfig.DashboardUrl, dashboardOptions);
      }


    }

    public static void UseMiddlewareCustom(this IApplicationBuilder application)
    {
      //application.UseMiddleware<RequestResponseLoggingMiddleware>();
    }
    public static void UseHstsCustom(this IApplicationBuilder application, AppConfig config)
    {
      if (Environment.IsDevelopment())
      {
        return;
      }
      if (config.UseHsts)
        application.UseHsts();
    }
    public static void UseSpaCustom(this IApplicationBuilder application, AppConfig config)
    {
      if (config.SpaConfig != null)
      {
        application.UseSpa(spa =>
        {
          spa.Options.SourcePath = config.SpaConfig.SourcePath;

          if (!Environment.IsDevelopment())
          {
            return;
          }
          spa.UseAngularCliServer("start");
        });
      }


    }

    public static void UseCustomSwagger(this IApplicationBuilder application, AppConfig config)
    {
      if (config.SwaggerConfig != null)
      {
        application.UseSwagger(c =>
        {
          c.SerializeAsV2 = true;
        });
        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
        // specifying the Swagger JSON endpoint.
        application.UseSwaggerUI(c =>
        {

          c.SwaggerEndpoint(config.SwaggerConfig.Url, config.SwaggerConfig.Name);
        });
      }


    }
  }
}