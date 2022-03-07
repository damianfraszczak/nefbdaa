using Microsoft.EntityFrameworkCore;
using NEFBDAACommons.Database.Services;
using NEFBDAACommons.Helpers.Security;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAAStarter.Database.EntityTypeConfiguration;
using NEFBDAAStarter.Models.Entities;
using System.Linq;
using AgileObjects.AgileMapper.Extensions;

namespace NEFBDAAStarter.Database
{
    public class DatabaseContext : AbstractDbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<AppUserCompany> CompanyUsers { get; set; }

        protected override void InitModelMappings(ModelBuilder modelBuilder)
        {
            base.InitModelMappings(modelBuilder);
            modelBuilder
                .ApplyConfiguration(new AppUserEntityTypeConfiguration());
            modelBuilder
                .ApplyConfiguration(new AppUserCompanyEntityTypeConfiguration());
        }


        public override void Seed()
        {
            base.Seed();
            SeedCompanies();
            SeedUsers();
        }

        private void SeedCompanies()
        {
            var dtCompany = Companies.Find(1L);
            if (dtCompany == null)
            {
                Companies.Add(new Company()
                {
                    Name = "Digital trading"
                });

                Companies.Add(new Company()
                {
                    Name = "Test company"
                });
            }

            SaveChanges();
        }

        private void SeedUsers()
        {
            var adminUser = Users.Find(1L);
            var serviceUser = Users.Find(2L);
            Users.Where(x => string.IsNullOrEmpty(x.FullName))
                .ForEach(user => { user.FullName = $"{user.FirstName ?? ""} {user.LastName ?? ""}"; });

            if (adminUser != null && serviceUser != null)
            {
                return;
            }

            if (adminUser == null)
            {
                var userModel = new AppUser
                {
                    FirstName = "Administrator",
                    LastName = "Administrator",
                    Email = "admin@dt.co.uk",
                    Password = CryptoHelper.CreateHash("admin"),
                    Roles = new string[] {AppUserRoleEnum.Admin.ToString()},
                    Status = UserStatusEnum.Active
                };
                Users.Add(userModel);
            }

            if (serviceUser == null)
            {
                var userModel = new AppUser
                {
                    FirstName = "Service",
                    LastName = "AdminHangfireistrator",
                    Email = AppConstants.HANGFIRE_USER_LOGIN,
                    Password = CryptoHelper.CreateHash(AppConstants.HANGFIRE_USER_PASSWORD),
                    Roles = new string[] {AppUserRoleEnum.Admin.ToString()},
                    Status = UserStatusEnum.Active
                };
                Users.Add(userModel);
            }

            SaveChanges();
            var localUsers = Users.ToList();
        }
    }
}