namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_agreement_checklist")]
    public partial class tbl_agreement_checklist
    {
        public int id { get; set; }

        public int? Agreement_No { get; set; }

        [StringLength(150)]
        public string Checklist_id { get; set; }

        [StringLength(150)]
        public string Checklist_Name { get; set; }

        public int? Status { get; set; }

        [StringLength(1000)]
        public string Remarks { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        public virtual tbl_agreement tbl_agreement { get; set; }

        public virtual tbl_checklistmaster tbl_checklistmaster { get; set; }
    }
}
