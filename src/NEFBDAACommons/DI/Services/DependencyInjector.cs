using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NEFBDAACommons.Database.Services;
using NEFBDAACommons.Shared.Services;
using System;
using System.Linq;

namespace NEFBDAACommons.DI.Services
{
  public enum DIScope
  {
    Singleton, Transient, Scope
  }
  public static class DependencyInjector
  {
    public static readonly ILoggerFactory DbCommandConsoleLoggerFactory = LoggerFactory.Create(builder =>
    {
      builder.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information)
             .AddConsole();
    });
    private static Type dbContextType;

    private static IServiceProvider ServiceProvider { get; set; }

    public static IServiceCollection Services { get; private set; }

    public static void AddDbContext<T>(string connectionString) where T : DbContext
    {
      Services.AddDbContextPool<T>(options => options
      .UseLoggerFactory(DbCommandConsoleLoggerFactory)
      .UseSqlServer(connectionString)
      .EnableDetailedErrors(true)
      .EnableSensitiveDataLogging(true)
      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
      );
      dbContextType = typeof(T);

      //Services.AddDbContext<T>(options => options
      //  .UseLoggerFactory(ApplicationLogger.GetLoggedFactory())
      //  .UseSqlServer(connectionString)
      //  .EnableDetailedErrors(true)
      //  .EnableSensitiveDataLogging(true)
      //  .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking),
      //ServiceLifetime.Transient);
      //var context = GetService<T>();

      //  context.Database.EnsureCreated();
      // context.Database.Migrate();
    }
    public static AbstractDbContext GetDbContext()
    {
      return GetService<AbstractDbContext>(dbContextType);
    }
    public static void InitServices(IServiceCollection services)
    {
      if (Services == null)
      {
        Services = services;
      }
    }

    public static void AddDbContextInMemoryDatabase<T>() where T : DbContext
    {
      Services.AddDbContextPool<T>(options => options.UseInMemoryDatabase(typeof(T).Name));
      GetService<T>().Database.EnsureCreated();
    }

    // TODO
    public static void RegisterService(Type interfaceType, Type implType, DIScope scope)
    {
      switch (scope)
      {
        case DIScope.Singleton:
          Services.AddSingleton(interfaceType, implType);
          break;
        case DIScope.Transient:
          Services.AddTransient(interfaceType, implType);
          break;
        case DIScope.Scope:
          Services.AddScoped(interfaceType, implType);
          break;
      }
    }

    public static T GetService<T>()
    {
      ServiceProvider = Services.BuildServiceProvider();
      return ServiceProvider.GetService<T>();
    }
    public static T GetService<T>(Type serviceType) where T : class
    {
      ServiceProvider = Services.BuildServiceProvider();
      var service = ServiceProvider.GetService(serviceType);
      return service as T;
    }
    public static Type GetImplementation(Type serviceType)
    {
      return Services
          .Where(x => x.ServiceType == serviceType)
          .Select(x => x.ImplementationType)
          .FirstOrDefault();
    }



  }
}
