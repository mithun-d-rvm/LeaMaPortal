using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class NotificationUtilityDuesModel
    {
        public string Region_Name { get; set; }
        public string Property_Name { get; set; }
        public string Unit_Property_Name { get; set; }
        public string Utility_Name { get; set; }
        public string Meterno { get; set; }
        public DateTime billdate { get; set; }
        public int billno { get; set; }
        public float Billamount { get; set; }
        public DateTime duedate { get; set; }
        public string BDate { get; set; }
        public string DDate { get; set; }
    }
}