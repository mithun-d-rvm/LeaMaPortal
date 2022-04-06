﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class ApprovalSettingsViewModel
    {
        public int Id { get; set; }
        [DisplayName("TCA Approval Required")]

        public string Region_Name { get; set; }
        public string Country { get; set; }
        public string Approval_flag { get; set; }
        [DisplayName("Approval User")]
        public string Userid { get; set; }
    }
}