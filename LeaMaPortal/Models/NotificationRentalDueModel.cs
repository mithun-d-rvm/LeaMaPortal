using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class NotificationRentalDueModel
    {
        public string Region_Name { get; set; }
        public string Properties_Name { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public int Agreement_No { get; set; }
        public string payment_mode { get; set; }
        public string Cheque_No { get; set; }
        public DateTime cheque_date { get; set; }
        public float Cheque_Amount { get; set; }
        public string ChequeDate { get; set; }
    }
}