using System;
using System.Collections.Generic;

namespace NEFBDAACommons.Shared.Models
{
    public class EmailViewModel : BaseViewModel<long>
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Template { get; set; }
        public Dictionary<string, string> TemplateParams { get; set; }
        public DateTime? DateSend { get; set; }
        public string LastException { get; set; }
        public EmailViewModel()
        {
        }

        public override string OptionText => $"{To} : {Subject}";
    }
}
