using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NEFBDAACommons.Database.Models.EntityTypeConfiguration
{
    public class ConfigEntityTypeConfiguration : IEntityTypeConfiguration<ConfigEntity>
    {
        public ConfigEntityTypeConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<ConfigEntity> builder)
        {
            builder.HasIndex(x => x.AppKey).IsUnique();
        }
    }
}
