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
    
    public partial class dashboard_vacancy
    {
        public int id { get; set; }
        public string Property_Flag { get; set; }
        public string Property_id { get; set; }
        public string Property_Name { get; set; }
        public string Unit_id { get; set; }
        public string Unitname { get; set; }
        public string Property_Usage { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public Nullable<int> Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public Nullable<double> Rental_Rate_Month { get; set; }
        public Nullable<System.DateTime> Vacant_Start_Date { get; set; }
        public Nullable<int> Aging_Days { get; set; }
        public Nullable<double> Loss_Amt { get; set; }
        public string user { get; set; }
    }
}
