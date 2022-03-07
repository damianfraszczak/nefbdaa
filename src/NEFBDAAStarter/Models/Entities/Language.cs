using NEFBDAACommons.Database.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEFBDAAStarter.Models.Entities
{
  [Table("Languages")]
  public class Language : BaseAuditableEntity<long>
  {
    public string Name { get; set; }
  }
}
