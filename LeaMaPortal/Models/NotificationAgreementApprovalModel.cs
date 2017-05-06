using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class NotificationAgreementApprovalModel
    {
        public string Region_Name { get; set; }
        public string Properties_Name { get; set; }
        public string Unit_Property_Name { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public int Agreement_No { get; set; }
        public float Total_Rental_amount { get; set; }
    }
}