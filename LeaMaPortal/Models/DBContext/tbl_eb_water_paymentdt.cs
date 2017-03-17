namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_eb_water_paymentdt")]
    public partial class tbl_eb_water_paymentdt
    {
        public int id { get; set; }

        public int? PaymentNo { get; set; }

        public int? Refno { get; set; }

        [StringLength(100)]
        public string Meterno { get; set; }

        [StringLength(100)]
        public string billNo { get; set; }

        public DateTime? billDate { get; set; }

        public float? billAmount { get; set; }

        public float? PaidAmount { get; set; }

        public float? Debitamt { get; set; }

        [StringLength(1000)]
        public string Remarks { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        public virtual tbl_eb_water_paymenthd tbl_eb_water_paymenthd { get; set; }
    }
}
