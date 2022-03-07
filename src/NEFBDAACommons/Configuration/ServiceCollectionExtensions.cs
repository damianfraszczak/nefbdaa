using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NEFBDAACommons.Configuration.Models;
using NEFBDAACommons.Database.Services;
using NEFBDAACommons.DI.Services;
using NEFBDAACommons.Security.Models.JwtToken;
using NEFBDAACommons.Shared.Services;
using NEFBDAACommons.Web.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;

namespace NEFBDAACommons.Configuration
{
  public static class ServiceCollectionExtensions
  {

    public static void AddSwagger(this IServiceCollection services, AppConfig config)
    {
      if (config.SwaggerConfig != null)
      {
        services.AddSwaggerGen(c =>
        {
          c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
          c.DescribeAllParametersInCamelCase();

          c.SwaggerDoc(config.SwaggerConfig.Info.Version, config.SwaggerConfig.Info);

          c.CustomOperationIds(apiDesc =>
          {
            return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
          });
          // Swagger 2.+ support
          //  var security = new Dictionary<string, IEnumerable<string>>
          //{
          //            {config.AuthConfig?.AuthorizationHeaderName, new string[] { }},
          //};
          // TODO

          c.AddSecurityDefinition(config.SwaggerConfig.SecurityName, config.SwaggerConfig.ApiKeyScheme);


          c.AddSecurityRequirement(config.SwaggerConfig.ApiRequirement);
          //// TODO
          //  c.DescribeAllEnumsAsStrings();
          //c.DescribeStringEnumsInCamelCase();
          c.MapType(typeof(DateTime), () => new OpenApiSchema { Type = "string", Pattern = CommonsConstants.API_DATE_TIME_FORMAT });
          c.OperationFilter<FormFileSwaggerFilter>();
        });
      }


    }
    public static void AddAuthenticationCustom(this IServiceCollection services, AppConfig config)
    {
      if (config.AuthConfig.UseDefaultTokenConfig)
      {
        void JwtBearer(JwtBearerOptions jwtBearer)
        {
          jwtBearer.TokenValidationParameters = JwtTokenSettings.TokenValidationParameters;
        }
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearer);
      }

    }

    public static void AddMvcCustom(this IServiceCollection services)
    {
      void Mvc(MvcOptions mvc)
      {
        mvc.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
        mvc.Filters.Add(typeof(ModelValidationFilter));
        mvc.Filters.Add(typeof(ExceptionFilter));
      }


      services.AddControllers().AddMvcOptions(Mvc).AddNewtonsoftJson(json =>
           {
             json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
             json.SerializerSettings.Error = (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args) =>
             {
               ApplicationLogger.CreateLogger(json.GetType()).LogError("Serialization error", args);
             };
             json.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified;
             json.SerializerSettings.DateFormatString = CommonsConstants.API_DATE_TIME_FORMAT;
             json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
             json.SerializerSettings.SerializationBinder = new DefaultSerializationBinder();
             json.SerializerSettings.Formatting = Formatting.None;
             json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
             json.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter() {  });
           }

      );

    }

    public static void AddSpaStaticFilesCustom(this IServiceCollection services, AppConfig config)
    {
      if (config.SpaConfig != null)
      {
        services.AddSpaStaticFiles(spa => spa.RootPath = config.SpaConfig.RootPath);
      }

    }
    public static void AddDependencyInjectionCustom<T>(this IServiceCollection services, IConfiguration configuration, AppConfig config) where T : AbstractDbContext
    {
      DependencyInjectorConfigurator.RegisterServices(services);
      DependencyInjector.AddDbContext<T>(configuration.GetConnectionString(config.DbConnectionName));
      DependencyInjector.GetService<T>().Seed();
    }
  }
}
