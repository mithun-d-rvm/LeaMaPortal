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
    
    public partial class tbl_tenant_individual_doc
    {
        public int id { get; set; }
        public Nullable<int> Tenant_Id { get; set; }
        public string Doc_name { get; set; }
        public string Doc_Path { get; set; }
        public string Delmark { get; set; }
    }
}
