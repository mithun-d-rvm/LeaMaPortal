namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.view_invoice_receipt_pending")]
    public partial class view_invoice_receipt_pending
    {
        [Key]
        [StringLength(50)]
        public string invno { get; set; }

        public int? Agreement_No { get; set; }

        [StringLength(100)]
        public string Property_ID { get; set; }

        [StringLength(100)]
        public string Property_Name { get; set; }

        [StringLength(100)]
        public string Unit_ID { get; set; }

        [StringLength(100)]
        public string unit_Name { get; set; }

        public int? Tenant_id { get; set; }

        [StringLength(100)]
        public string Tenant_Name { get; set; }

        public int? month { get; set; }

        public int? year { get; set; }

        public DateTime? date { get; set; }

        public int? incno { get; set; }

        [StringLength(50)]
        public string invtype { get; set; }

        public float? totalamt { get; set; }

        public double? InvoiceAmount { get; set; }
    }
}
