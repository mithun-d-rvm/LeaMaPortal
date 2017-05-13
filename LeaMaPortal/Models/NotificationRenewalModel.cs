using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class NotificationRenewalModel
    {
        public string region_name { get; set; }
        public string Properties_Name { get; set; }
        public string Unit_Property_Name { get; set; }
        public int Agreement_No { get; set; }
        public DateTime Agreement_Start_Date { get; set; }
        public DateTime Agreement_End_Date { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}