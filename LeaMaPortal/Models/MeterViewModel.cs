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
    }
}