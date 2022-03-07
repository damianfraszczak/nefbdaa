using AgileObjects.AgileMapper;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using NEFBDAACommons.Configuration.Models;
using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAACommons.Shared.Services;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NEFBDAACommons.Configuration
{
  public static class StartupHelper
  {

    public static void ConfigureServicesForApi(IConfiguration configuration, IServiceCollection services, AppConfig appConfig)
    {
      VerifyLicence(appConfig);
      services.AddAuthenticationCustom(appConfig);
      services.AddResponseCompression();
      services.AddResponseCaching();
      if (appConfig.UseAllowAllCors)
      {
        services.AddCors(
            options => options.AddPolicy(CommonsConstants.CORS_ALLOW_POLICY_NAME,
            builder =>
            {
              builder
                          .AllowAnyOrigin()
                          .WithMethods(CommonsConstants.CORS_ALLOW_METHODS)
                          .AllowAnyHeader();
            })
        );
      }
      services.AddMvcCustom();
      services.AddMemoryCache();
      // TODO config it
      //services.AddMemoryCache((config) =>
      //{
      //    config.ExpirationScanFrequency = TimeSpan.FromMinutes(15);
      //    config.SizeLimit = 1024;
      //});
      services.AddSwagger(appConfig);

      services.Configure<ApiBehaviorOptions>(options =>
      {
        options.SuppressConsumesConstraintForFormFileParameters = true;
        options.SuppressInferBindingSourcesForParameters = true;
        options.SuppressModelStateInvalidFilter = true;
      });
      if (appConfig.HangfireConfig != null)
      {
        services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString(appConfig.DbConnectionName)));
        // services.AddHangfireServer();
        // GlobalConfiguration.Configuration.UseSqlServerStorage(configuration.GetConnectionString(appConfig.DbConnectionName));
      }
      // ignroe referenced entities
      Mapper.WhenMapping
        .IgnoreTargetMembersWhere(m => m.IsPropertyMatching(x => typeof(IEntity).IsAssignableFrom(x.PropertyType)));
    }

    private static void VerifyLicence(AppConfig appConfig)
    {
      if (!string.IsNullOrEmpty(appConfig.LicenceKey))
      {
        var rawLicenceData = encryptDecrypt(appConfig.LicenceKey);
        var licenceDate = ParsingHelper.GetDateTimeOrDefault(System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(rawLicenceData)));
        if (licenceDate == null)
        {
          throw new Exception("Invalid licence key has been provided");
        }
        else if (licenceDate < DateTime.Now)
        {
          throw new Exception("Provided licence is expired");
        }
      }
      else
      {
        throw new Exception("Licence key has not been provided");
      }

    }

    public static void ConfigureServicesForApp(IConfiguration configuration, IServiceCollection services, AppConfig appConfig)
    {


      services.Configure<IISOptions>(options =>
      {

      });
      services.AddSpaStaticFilesCustom(appConfig);
      services.AddLogging(loggingBuilder =>
      {
        loggingBuilder.AddConfiguration(configuration.GetSection(CommonsConstants.LOGGING_CONFIG_SECTION_NAME));
        loggingBuilder.AddConsole();
        loggingBuilder.AddDebug();
      });
    }
    public static void ConfigureForApi(IApplicationBuilder application, IWebHostEnvironment env, ILoggerFactory loggerFactory, AppConfig appConfig)
    {
      ApplicationLogger.ConfigureLogger(loggerFactory);
      //if (env.IsDevelopment())
      //{
      //    app.UseDeveloperExceptionPage();
      //}
      //app.UseMvc();
      application.UseExceptionCustom();
      application.UseMiddlewareCustom();
      application.UseAuthentication();
      application.UseHstsCustom(appConfig);
      // TODO
      //application.UseHttpsRedirection();
      if (appConfig.UseAllowAllCors)
      {
        application.UseCorsCustom(appConfig);
      }
      if (appConfig.UseResponseCaching)
      {
        application.UseResponseCaching();
      }



      //hangfire - dashboard for task management
      application.UseHangfire(appConfig);
      application.UseCustomSwagger(appConfig);
    }
    public static void ConfigureForApp(IApplicationBuilder application, IWebHostEnvironment env, ILoggerFactory loggerFactory, AppConfig appConfig)
    {


      //// SPA DISABLED
      // application.UseSpaStaticFiles();
      //application.UseSpaCustom();
      application.Use(async (context, next) =>
      {
        await next();
        if (context.Response.StatusCode == 404 &&
                 !Path.HasExtension(context.Request.Path.Value) &&
                 !(appConfig.IgnoreNotAngularPaths.Any(x => context.Request.Path.Value.Contains(x))))
        {
          context.Request.Path = "/index.html";
          await next();
        }
      });
      application.UseRouting();
      application.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      application.UseDefaultFiles();
      application.UseStaticFiles();
      application.UseStaticFiles(new StaticFileOptions()
      {
        FileProvider = new PhysicalFileProvider(Path.Combine(FileHelper.AppDataFolder(), CommonsConstants.UploadsFolder)),
        RequestPath = new PathString($"/{CommonsConstants.UploadsFolder}")
      });
    }

    private static string encryptDecrypt(string input, string keyString = "defdev796133457")
    {
      char[] key = keyString.ToCharArray(); //Any chars will work, in an array of any size
      char[] output = new char[input.Length];

      for (int i = 0; i < input.Length; i++)
      {
        output[i] = (char)(input[i] ^ key[i % key.Length] % 50);
      }

      return new string(output);
    }
  }
}
