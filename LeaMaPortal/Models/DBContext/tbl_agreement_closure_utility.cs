namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_agreement_closure_utility")]
    public partial class tbl_agreement_closure_utility
    {
        public int id { get; set; }

        public int? Agreement_No { get; set; }

        [StringLength(150)]
        public string Utility_id { get; set; }

        [StringLength(150)]
        public string Utility_Name { get; set; }

        [StringLength(150)]
        public string Payable { get; set; }

        [StringLength(150)]
        public string Amount_Type { get; set; }

        public float? Amount { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        public virtual tbl_agreement_closure tbl_agreement_closure { get; set; }
    }
}
