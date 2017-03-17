namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_agreement_pdc")]
    public partial class tbl_agreement_pdc
    {
        public int id { get; set; }

        public int? Agreement_No { get; set; }

        [StringLength(50)]
        public string Month { get; set; }

        [StringLength(50)]
        public string Year { get; set; }

        [StringLength(150)]
        public string BankName { get; set; }

        [StringLength(100)]
        public string Cheque_No { get; set; }

        [StringLength(100)]
        public string Cheque_Date { get; set; }

        public float? Cheque_Amount { get; set; }

        [StringLength(20)]
        public string Payment_Mode { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        public virtual tbl_agreement tbl_agreement { get; set; }
    }
}
