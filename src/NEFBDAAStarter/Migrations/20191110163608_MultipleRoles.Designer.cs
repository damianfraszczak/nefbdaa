// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NEFBDAAStarter.Database;

namespace NEFBDAAStarter.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20191110163608_MultipleRoles")]
    partial class MultipleRoles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NEFBDAACommons.Database.Models.AppLogEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalData");

                    b.Property<string>("FileName");

                    b.Property<Guid?>("Guid");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Level");

                    b.Property<string>("LineNumber");

                    b.Property<string>("Message");

                    b.Property<string>("Timestamp");

                    b.HasKey("Id");

                    b.ToTable("AppLogs");
                });

            modelBuilder.Entity("NEFBDAACommons.Database.Models.ConfigEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalInfo");

                    b.Property<string>("AppKey");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<long>("CreatorUserId");

                    b.Property<DateTime?>("DeletedUtc");

                    b.Property<long?>("DeleterUserId");

                    b.Property<Guid?>("Guid");

                    b.Property<bool>("HiddenConfig");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<int>("Type");

                    b.Property<DateTime?>("UpdatedUtc");

                    b.Property<long?>("UpdaterUserId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("AppKey")
                        .IsUnique()
                        .HasFilter("[AppKey] IS NOT NULL");

                    b.ToTable("Configs");
                });

            modelBuilder.Entity("NEFBDAACommons.Database.Models.DocumentEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentType");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<long>("CreatorUserId");

                    b.Property<DateTime?>("DeletedUtc");

                    b.Property<long?>("DeleterUserId");

                    b.Property<string>("FilePath");

                    b.Property<string>("FileSize");

                    b.Property<Guid?>("Guid");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<string>("OriginalFilename");

                    b.Property<string>("OwnerDocumentType");

                    b.Property<Guid>("OwnerGuid");

                    b.Property<DateTime?>("UpdatedUtc");

                    b.Property<long?>("UpdaterUserId");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("NEFBDAACommons.Database.Models.EmailEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateSend");

                    b.Property<string>("From");

                    b.Property<Guid?>("Guid");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastException");

                    b.Property<string>("Subject");

                    b.Property<string>("Template");

                    b.Property<string>("TemplateParams");

                    b.Property<string>("To");

                    b.HasKey("Id");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("NEFBDAACommons.Database.Models.NoteEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<long>("CreatorUserId");

                    b.Property<DateTime?>("DeletedUtc");

                    b.Property<long?>("DeleterUserId");

                    b.Property<Guid?>("Guid");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Note");

                    b.Property<Guid>("OwnerGuid");

                    b.Property<DateTime?>("UpdatedUtc");

                    b.Property<long?>("UpdaterUserId");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("NEFBDAACommons.Database.Models.ReportEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<long>("CreatorUserId");

                    b.Property<DateTime?>("DeletedUtc");

                    b.Property<long?>("DeleterUserId");

                    b.Property<bool>("Global");

                    b.Property<Guid?>("Guid");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<Guid>("OwnerGuid");

                    b.Property<string>("Query");

                    b.Property<string>("ServiceType");

                    b.Property<DateTime?>("UpdatedUtc");

                    b.Property<long?>("UpdaterUserId");

                    b.HasKey("Id");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("NEFBDAAStarter.Models.Entities.AppUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid?>("Guid");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Roles");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NEFBDAAStarter.Models.Entities.AppUserCompany", b =>
                {
                    b.Property<long>("CompanyId");

                    b.Property<long>("UserId");

                    b.HasKey("CompanyId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CompanyUser");
                });

            modelBuilder.Entity("NEFBDAAStarter.Models.Entities.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<long>("CreatorUserId");

                    b.Property<DateTime?>("DeletedUtc");

                    b.Property<long?>("DeleterUserId");

                    b.Property<Guid?>("Guid");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("UpdatedUtc");

                    b.Property<long?>("UpdaterUserId");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Z.EntityFramework.Plus.AuditEntry", b =>
                {
                    b.Property<int>("AuditEntryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("EntitySetName")
                        .HasMaxLength(255);

                    b.Property<string>("EntityTypeName")
                        .HasMaxLength(255);

                    b.Property<int>("State");

                    b.Property<string>("StateName")
                        .HasMaxLength(255);

                    b.HasKey("AuditEntryID");

                    b.ToTable("AuditEntries");
                });

            modelBuilder.Entity("Z.EntityFramework.Plus.AuditEntryProperty", b =>
                {
                    b.Property<int>("AuditEntryPropertyID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditEntryID");

                    b.Property<string>("NewValueFormatted")
                        .HasColumnName("NewValue");

                    b.Property<string>("OldValueFormatted")
                        .HasColumnName("OldValue");

                    b.Property<string>("PropertyName")
                        .HasMaxLength(255);

                    b.Property<string>("RelationName")
                        .HasMaxLength(255);

                    b.HasKey("AuditEntryPropertyID");

                    b.HasIndex("AuditEntryID");

                    b.ToTable("AuditEntryProperties");
                });

            modelBuilder.Entity("NEFBDAAStarter.Models.Entities.AppUserCompany", b =>
                {
                    b.HasOne("NEFBDAAStarter.Models.Entities.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NEFBDAAStarter.Models.Entities.AppUser", "User")
                        .WithMany("Companies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Z.EntityFramework.Plus.AuditEntryProperty", b =>
                {
                    b.HasOne("Z.EntityFramework.Plus.AuditEntry", "Parent")
                        .WithMany("Properties")
                        .HasForeignKey("AuditEntryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
