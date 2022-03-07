using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NEFBDAAStarter.Models.Entities;

namespace NEFBDAAStarter.Database.EntityTypeConfiguration
{
    public class AppUserCompanyEntityTypeConfiguration : IEntityTypeConfiguration<AppUserCompany>
    {
        public AppUserCompanyEntityTypeConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<AppUserCompany> modelBuilder)
        {
            modelBuilder.HasKey(bc => new { bc.CompanyId, bc.UserId });
        }
    }
}
