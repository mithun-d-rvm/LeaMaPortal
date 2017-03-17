namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_invoicedt")]
    public partial class tbl_invoicedt
    {
        public int id { get; set; }

        [StringLength(50)]
        public string invno { get; set; }

        [StringLength(100)]
        public string item { get; set; }

        [StringLength(250)]
        public string description { get; set; }

        public float? qty { get; set; }

        public float? amount { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        public virtual tbl_invoicehd tbl_invoicehd { get; set; }
    }
}
