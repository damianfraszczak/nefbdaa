using NEFBDAACommons.Database.Attributes;
using NEFBDAACommons.Database.Models;
using NEFBDAACommons.Security.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace NEFBDAAStarter.Models.Entities
{
  public class UserTypeEnumeration: Enumeration {

    private UserTypeEnumeration(int value, string displayName) : base(value, displayName) { }

    public static readonly UserTypeEnumeration Manager
        = new UserTypeEnumeration(1, "Manager");
  }
  [Table("Users")]
  public class AppUser : BaseUser
  {
  
  
    [Column("Roles")]
    public string RolesString { get; set; }
    [NotMapped]
    public virtual IList<string> Roles
    {
      get
      {
        return string.IsNullOrEmpty(RolesString) ? new List<string>() : RolesString.Split(',').ToList();
      }
      set
      {
        RolesString = string.Join(",", value);
      }
    }

    public long? UserTypeId { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool AddressVisible { get; set; }
    public string Address { get; set; }
    public int UserDocumentId { get; set; }
    public string UserStaticoptions { get; set; }
    public virtual ICollection<AppUserCompany> Companies { get; set; }

    public long? LanguageId { get; set; }
    public Language Language { get; set; }

    public long? OwnerCompanyId { get; set; }
    public Company OwnerCompany { get; set; }

    public string UserImage { get; set; }
    [NotMapped]
    public ICollection<long> CompaniesCompanyId
    {
      get
      {
        return Companies == null ? new List<long>() : Companies.Select(x => x.CompanyId).ToList();
      }
      set
      {
        Companies = value.Select(x => new AppUserCompany() { CompanyId = x, UserId = Id }).ToList();
      }
    }
  }
}
