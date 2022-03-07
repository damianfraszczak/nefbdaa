using Microsoft.Extensions.DependencyInjection;
using NEFBDAACommons.DI.Attributes;
using NEFBDAACommons.DI.Services;
using Scrutor;
using System;
using System.Collections.Generic;
using System.Linq;
namespace NEFBDAACommons.Configuration
{
  public static class DependencyInjectorConfigurator
  {
    private static Dictionary<Type, Type> _servicesDict = new Dictionary<Type, Type>();

    public static void RegisterServices()
    {
      RegisterServices(new ServiceCollection());
    }
    public static void RegisterServices(IServiceCollection services)
    {
      DependencyInjector.InitServices(services);
      // Register all repositories via reflection
      // RegisterAllRepositories(services);
      // RegisterAllServices(services);

      services.Scan(scan => scan.FromApplicationDependencies()
          .AddClasses(x => x.WithAttribute(typeof(SingletonServiceAttribute)))
          .UsingRegistrationStrategy(RegistrationStrategy.Skip)
          .AsSelfWithInterfaces()
          .WithSingletonLifetime());

      services.Scan(scan => scan.FromApplicationDependencies()
          .AddClasses(x => x.WithAttribute(typeof(ScopeServiceAttribute)))
          .UsingRegistrationStrategy(RegistrationStrategy.Skip)
          .AsSelfWithInterfaces()
          .WithScopedLifetime());

      services.Scan(scan => scan.FromApplicationDependencies()
          .AddClasses(x => x.WithAttribute(typeof(TransientServiceAttribute)))
          .UsingRegistrationStrategy(RegistrationStrategy.Skip)
          .AsSelfWithInterfaces()
          .WithTransientLifetime());

      services.Scan(scan => scan.FromApplicationDependencies()
          .AddClasses(x => x.WithAttribute(typeof(RepositoryAttribute)))
          .UsingRegistrationStrategy(RegistrationStrategy.Skip)
          .AsSelfWithInterfaces()
          .WithScopedLifetime());

    }



  }
}
