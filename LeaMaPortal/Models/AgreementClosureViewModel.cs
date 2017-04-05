using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class AgreementClosureViewModel
    {
        public int Id { get; set; }
       
        //[DisplayName("Existing Contract Contract Agreement")]
        //public int Agreement_Refno { get; set; }
        [DisplayName("Contract Agreement Number")]
        public int Agreement_No { get; set; }
        [DisplayName("Contract Agreement Date")]
        public DateTime Agreement_Date { get; set; }
        [DisplayName("Contract Tenant Type")]
        public string Tenant_Type { get; set; }
        [DisplayName("Tenant ID")]
        //doubt
        public int Ag_Tenant_id { get; set; }
        [DisplayName("Tenant Name")]
        //doubt
        public string Ag_Tenant_Name { get; set; }
        [DisplayName("Property ID")]
        public int property_id { get; set; }
        [DisplayName("Property Tawtheeq ID")]
        public string Property_ID_Tawtheeq { get; set; }
        [DisplayName("Property Name")]
        public string Properties_Name { get; set; }
        [DisplayName("Unit Tawtheeq ID")]
        public string Unit_ID_Tawtheeq { get; set; }
        [DisplayName("Unit Name")]
        public string Unit_Property_Name { get; set; }
        [DisplayName("Contract Agreement Start Date")]
        public DateTime Agreement_Start_Date { get; set; }
        [DisplayName("Contract Agreement End Date")]
        public DateTime Agreement_End_Date { get; set; }

        [DisplayName("Total Contract Amount")]
        //check
        public decimal Total_Contract_Amount { get; set; }
        [DisplayName("Total Amount Paid")]
        //check
        public decimal Total_Amount_Paid { get; set; }
        [DisplayName("Amount Pending from Tenant")]
        public float Advance_pending { get; set; }
        [DisplayName("Security Deposit Paid")]
        public string Advance_Security_Amount_Paid { get; set; }
        [DisplayName("Less Damages (if any)")]
        public string Less_any_damanges { get; set; }
        [DisplayName("Total Amount to be refunded")]
        public string Amount_to_be_refunded { get; set; }
        [DisplayName("Remarks")]
        public DateTime Remarks { get; set; }

    }
}