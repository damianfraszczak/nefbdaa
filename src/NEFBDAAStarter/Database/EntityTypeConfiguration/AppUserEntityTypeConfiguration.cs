using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NEFBDAAStarter.Models.Entities;

namespace NEFBDAAStarter.Database.EntityTypeConfiguration
{
    public sealed class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public AppUserEntityTypeConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(300);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Password).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
