using Microsoft.EntityFrameworkCore;

namespace NEFBDAACommons.Database.Models
{
    [Owned]
    public class Geolocation
    {
        public double? Lat { get; set; }
        public double? Lng { get; set; }
    }
}
