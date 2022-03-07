using System.ComponentModel.DataAnnotations.Schema;

namespace NEFBDAAStarter.Models.Entities
{
    [Table("CompanyUser")]
    public class AppUserCompany

    {
       
        public long CompanyId { get; set; }
        public Company Company { get; set; }
 
        public long UserId { get; set; }
        public AppUser User { get; set; }
    }
}
