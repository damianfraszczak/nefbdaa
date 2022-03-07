using Microsoft.EntityFrameworkCore;
using NEFBDAACommons.Database.Extensions;
using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Database.Models.EntityTypeConfiguration;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Shared.Helpers;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Plus;

namespace NEFBDAACommons.Database.Services
{
  public interface IDbContext
  {

  }
  public abstract class AbstractDbContext : DbContext, IDbContext
  {
    static AbstractDbContext()
    {
      // TODO add autiting in future currenlty it takes to much time and memory
      // AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
      //    // ADD "Where(x => x.AuditEntryID == 0)" to allow multiple SaveChanges with same Audit
      //    (context as AbstractDbContext).AuditEntries.AddRange(audit.Entries);
    }
    public AuthenticatedUserViewModel AuthenticatedUser { get; set; }

    // public DbSet<AuditEntry> AuditEntries { get; set; }
    // public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }
    public DbSet<DocumentEntity> Documents { get; set; }
    public DbSet<ConfigEntity> Configs { get; set; }
    public DbSet<EmailEntity> Emails { get; set; }
    public DbSet<ReportEntity> Reports { get; set; }
    public DbSet<NoteEntity> Notes { get; set; }
    public DbSet<AppLogEntity> AppLogs { get; set; }

    public AbstractDbContext(DbContextOptions options) : base(options)
    {

    }
    public override int SaveChanges()
    {
      return SaveChanges(AuthenticatedUser);
    }
    public int SaveChanges(AuthenticatedUserViewModel authenticatedUser)
    {
      if (!ChangeTracker.LazyLoadingEnabled)
      {
        this.RemoveAddedEntitiesByMapper();
      }
      
      this.UpdateEntitiesBeforeSaveChanges(authenticatedUser);
      var changedRows = SaveChangesWithAudit(authenticatedUser);
      return changedRows;
    }

    private void RemoveAddedEntitiesByMapper()
    {
      var entries = this.ChangeTracker.Entries();
      entries
                .Where(e => e.State == EntityState.Added)
                .ToList()
                .ForEach(e =>
                {
                  var withId = e.Entity as IEntityWithIdAsStr;
                  if (withId != null && ParsingHelper.GetInt(withId.IdStr()) > 0)
                  {
                    e.State = EntityState.Detached;
                  }
                });
    }

    protected int SaveChangesWithAudit(AuthenticatedUserViewModel authenticatedUser)
    {
      var audit = new Audit
      {
        CreatedBy = authenticatedUser?.Id + ""
      };
      audit.PreSaveChanges(this);
      var rowAffecteds = base.SaveChanges();
      audit.PostSaveChanges();
      if (audit.Configuration.AutoSavePreAction != null)
      {
        audit.Configuration.AutoSavePreAction(this, audit);
        base.SaveChanges();
      }
      return rowAffecteds;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      this.InitModelMappings(modelBuilder);
      this.InitIsDeletedGlobalFilter(modelBuilder);
      modelBuilder.AddStoreStringListAsStringFields();
      modelBuilder.AddStoreEnumAsString();
    }

    protected virtual void InitModelMappings(ModelBuilder modelBuilder)
    {
      modelBuilder
          .ApplyConfiguration(new ConfigEntityTypeConfiguration());
      modelBuilder
          .ApplyConfiguration(new EmailEntityTypeConfiguration());
    }

    protected virtual void UpdateEntitiesBeforeSaveChanges(AuthenticatedUserViewModel authenticatedUser)
    {
      this.UpdateAuditableEntities(authenticatedUser?.Id ?? 0);
    }



    private void InitIsDeletedGlobalFilter(ModelBuilder builder)
    {
      foreach (var entityType in builder.Model.GetEntityTypes())
      {
        //need to set only in base not abstract classes
        //ignore twittermention becasue it inherits from TwitterTweet
        if ((typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType)) && entityType.ClrType.IsAbstract)
        {
          var parameter = Expression.Parameter(entityType.ClrType);
          var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
          var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));
          var compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));
          var lambda = Expression.Lambda(compareExpression, parameter);
          builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
        }

      }
    }

    public virtual void Seed()
    {
      AddEmptyConfigIfNotExists(CommonsConstants.CONFIG_EMAIL_HOST);
      AddEmptyConfigIfNotExists(CommonsConstants.CONFIG_EMAIL_PASSWORD);
      AddEmptyConfigIfNotExists(CommonsConstants.CONFIG_EMAIL_PORT);
      AddEmptyConfigIfNotExists(CommonsConstants.CONFIG_EMAIL_USER);
      AddEmptyConfigIfNotExists(CommonsConstants.APP_NAME, ConfigEntityType.STRING, "App");
      AddEmptyConfigIfNotExists(CommonsConstants.USER_PASSWORD_RESET_TEMPLATE,
          ConfigEntityType.HTML,
          "Hi @Model.FullName<br>" +
          "You have reseted password for you account, new password is <b>@Model.Password</b><br/>" +
          "Please change it after login <br/>" +
          "Best regards");
      AddEmptyConfigIfNotExists(CommonsConstants.USER_REGISTRATION_TEMPLATE,
          ConfigEntityType.HTML,
          "Hi @Model.FullName<br>" +
          "You have sucessfully registered to our system <br/>" +
          "Thanks<br/>" +
          "Best regards");
    }

    protected void AddEmptyConfigIfNotExists(
        string appKey,
        ConfigEntityType confitType = ConfigEntityType.STRING,
        string value = "")
    {

      var currentConfig = Configs.FirstOrDefault(x => x.AppKey == appKey);
      if (currentConfig == null)
      {
        currentConfig = new ConfigEntity()
        {
          AppKey = appKey,
          Type = confitType,
          Value = value
        };
        Configs.Add(currentConfig);
      }
      if (string.IsNullOrEmpty(currentConfig.Value))
      {
        currentConfig.Value = value;
      }
      SaveChanges();
    }
  }
}
