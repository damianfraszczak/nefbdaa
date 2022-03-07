using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace NEFBDAAStarter.Database
{

    public class DesignTimeDatabaseContextContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json")
                      .AddJsonFile("appsettings.Development.json", optional: true)
                      .Build();

            var builder = new DbContextOptionsBuilder<DatabaseContext>();

            builder.UseSqlServer(configuration["ConnectionStrings:DatabaseContext"], b => b.MigrationsAssembly("NEFBDAAStarter"));

            return new DatabaseContext(builder.Options);
        }
    }
}
