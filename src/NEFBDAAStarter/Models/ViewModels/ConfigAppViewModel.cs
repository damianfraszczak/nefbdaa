using NEFBDAACommons.DynamicForms.Models;
using NEFBDAAStarter.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEFBDAAStarter.Models.ViewModels
{
    public class ConfigAppViewModel
    {
        public string Version { get; set; }
        public IEnumerable<ModelTypes> AllModelTypes { get; set; }
        public IEnumerable<ModelFieldDefinitionViewModel> Models { get; set; }
    }
}
