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
    
    public partial class test_report1
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
        public Nullable<System.DateTime> Agreement_End_Date { get; set; }
        public Nullable<float> Perday_Rental { get; set; }
        public Nullable<int> Remaining_Days { get; set; }
        public Nullable<double> Contract_Value { get; set; }
    }
}
