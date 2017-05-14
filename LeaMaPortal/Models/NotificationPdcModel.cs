using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class NotificationPdcModel
    {
        public string Region_Name { get; set; }
        public string Property_Name { get; set; }
        public string unit_Name { get; set; }
        public string Tenant_Name { get; set; }
        public int Agreement_No { get; set; }
        public string DDChequeNo { get; set; }
        public DateTime DDChequedate { get; set; }
        public float pdc_Amount { get; set; }
        public string DDDate { get; set; }
    }
}