using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class MeterViewModel
    {
        public int Id { get; set; }

        public List<UtilityViewModel> Utility { get; set; }
        [DisplayName("Meter No")]
        public string Meter_no { get; set; }
        [DisplayName("Utility Id ")]
        public string Utility_id { get; set; }
        [DisplayName("Utility Name ")]
        public string Utility_Name { get; set; }
        [DisplayName("Account Number")]
        public string Accno { get; set; }
        [DisplayName("Unit Id")]
        public string unit_id { get; set; }
        [DisplayName("Due Day")]
        public string Dueday { get; set; }
        [DisplayName("Property Id")]
        public string Property_id { get; set; }

    }
}