//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LeaMaPortal.DBContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class view_receipt_screen_advance_pending
    {
        public int Agreement_No { get; set; }
        public string Prperty_id { get; set; }
        public string Property_name { get; set; }
        public string Unit_id { get; set; }
        public string Unitname { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public int Ag_Tenant_id { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public Nullable<System.DateTime> Agreement_Start_Date { get; set; }
        public Nullable<System.DateTime> Agreement_End_Date { get; set; }
        public string payment_mode { get; set; }
        public string Cheque_No { get; set; }
        public Nullable<System.DateTime> cheque_date { get; set; }
        public string user { get; set; }
        public Nullable<float> Cheque_Amount { get; set; }
        public double totalamount { get; set; }
        public double Balanceamount { get; set; }
        public string Reccategory { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
    }
}
