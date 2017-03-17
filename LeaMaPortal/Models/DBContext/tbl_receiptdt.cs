namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_receiptdt")]
    public partial class tbl_receiptdt
    {
        public int id { get; set; }

        public int? ReceiptNo { get; set; }

        [StringLength(50)]
        public string Invno { get; set; }

        public DateTime? InvoiceDate { get; set; }

        [StringLength(25)]
        public string Invtype { get; set; }

        public float? InvoiceAmount { get; set; }

        public float? CreditAmt { get; set; }

        public float? ReceivedAmount { get; set; }

        [StringLength(1000)]
        public string Remarks { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        public virtual tbl_receipthd tbl_receipthd { get; set; }
    }
}
