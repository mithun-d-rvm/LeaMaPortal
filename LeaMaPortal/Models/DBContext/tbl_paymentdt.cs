namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_paymentdt")]
    public partial class tbl_paymentdt
    {
        public int id { get; set; }

        public int? PaymentNo { get; set; }

        [StringLength(25)]
        public string Invtype { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(100)]
        public string InvoiceNo { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public float? InvoiceAmount { get; set; }

        public float? PaidAmount { get; set; }

        public float? Debitamt { get; set; }

        [StringLength(1000)]
        public string Remarks { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        public virtual tbl_paymenthd tbl_paymenthd { get; set; }
    }
}
