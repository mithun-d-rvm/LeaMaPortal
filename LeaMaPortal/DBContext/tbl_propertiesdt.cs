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
    
    public partial class tbl_propertiesdt
    {
        public int id { get; set; }
        public Nullable<int> Property_Id { get; set; }
        public string Property_ID_Tawtheeq { get; set; }
        public string Unit_ID_Tawtheeq { get; set; }
        public string Facility_id { get; set; }
        public string Facility_Name { get; set; }
        public string Numbers_available { get; set; }
        public Nullable<int> Accyear { get; set; }
        public string Delmark { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
    
        public virtual tbl_facilitymaster tbl_facilitymaster { get; set; }
        public virtual tbl_propertiesmaster tbl_propertiesmaster { get; set; }
    }
}
