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
    
    public partial class tbl_invoicedt
    {
        public int id { get; set; }
        public string invno { get; set; }
        public string item { get; set; }
        public string description { get; set; }
        public Nullable<float> qty { get; set; }
        public Nullable<float> amount { get; set; }
        public string Delmark { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
    }
}
