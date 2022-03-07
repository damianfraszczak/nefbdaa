using Microsoft.AspNetCore.SpaServices;

namespace NEFBDAACommons.Configuration.Models
{
    public class SpaConfig
    {
        public SpaOptions SpaOptions { get; set; }
        public string RootPath { get; set; }
        public string SourcePath { get; set; }
    }
}
