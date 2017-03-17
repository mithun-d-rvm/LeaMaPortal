namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.view_auto_receipt")]
    public partial class view_auto_receipt
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

        public double? InvoiceAmount { get; set; }

        public int? incno { get; set; }

        public float? invamt { get; set; }
    }
}
