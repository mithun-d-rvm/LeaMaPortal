namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_tenant_company_government_doc")]
    public partial class tbl_tenant_company_government_doc
    {
        public int id { get; set; }

        public int? Tenant_Id { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(150)]
        public string Doc_name { get; set; }

        [StringLength(300)]
        public string Doc_Path { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }
    }
}
