using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class ApprovalSettingsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Approval Flag")]
        public string Approval_flag { get; set; }
        [DisplayName("Approval User")]
        public string Userid { get; set; }
    }
}