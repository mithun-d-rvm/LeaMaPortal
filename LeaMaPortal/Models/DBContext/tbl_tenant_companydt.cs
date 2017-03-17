namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_tenant_companydt")]
    public partial class tbl_tenant_companydt
    {
        public int Id { get; set; }

        public int? Tenant_Id { get; set; }

        [StringLength(100)]
        public string Branch { get; set; }

        [StringLength(500)]
        public string BranchAdd { get; set; }

        [StringLength(500)]
        public string Branchadd1 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string Pincode { get; set; }

        [StringLength(50)]
        public string PhoneNo { get; set; }

        [StringLength(50)]
        public string EMailID { get; set; }

        [StringLength(50)]
        public string FaxNo { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }

        public int? Accyear { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }
    }
}
