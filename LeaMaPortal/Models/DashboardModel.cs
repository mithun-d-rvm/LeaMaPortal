using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class DashboardModel
    {
        [DisplayName("Category")]
        public string category { get; set; }
        [DisplayName("Total Value")]
        public string Total_Value { get; set; }
        [DisplayName("Residential")]
        public string Residential { get; set; }
        [DisplayName("Commercial")]
        public string Commercial { get; set; }
        [DisplayName("Water")]
        public string water { get; set; }
        [DisplayName("EB")]
        public string Eb { get; set; }
        [DisplayName("Others")]
        public string Others { get; set; }
        [DisplayName("overdues")]
        public string overdues { get; set; }
        [DisplayName("Pdc")]
        public string Pdc { get; set; }
        [DisplayName("Month")]
        public string Month { get; set; }

        [DisplayName("Year")]
        public string Year { get; set; }

    }

}