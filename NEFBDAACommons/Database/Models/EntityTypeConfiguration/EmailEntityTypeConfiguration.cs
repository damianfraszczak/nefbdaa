using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NEFBDAACommons.Database.Models.EntityTypeConfiguration
{
    public class EmailEntityTypeConfiguration : IEntityTypeConfiguration<EmailEntity>
    {
        public EmailEntityTypeConfiguration()
        {
        }

        public void Configure(EntityTypeBuilder<EmailEntity> builder)
        {
            builder
                .Property(b => b.TemplateParams)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<string, string>>(v));
        }
    }
}
