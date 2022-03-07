using NEFBDAACommons.Database.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEFBDAAStarter.Models.Entities
{
  [Table("Companies")]
  public class Company : BaseAuditableEntity<long>
  {
    public string Name { get; set; }
    public virtual ICollection<AppUserCompany> Users { get; set; }

    public long? LanguageId { get; set; }
    public Language Language { get; set; }
    public Company()
    {
    }
  }
}
