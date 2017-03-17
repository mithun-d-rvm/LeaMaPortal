namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_agreement_status")]
    public partial class tbl_agreement_status
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Agreement_No { get; set; }

        public int? Ag_Tenant_id { get; set; }

        [StringLength(100)]
        public string Ag_Tenant_Name { get; set; }

        public int? Properties_ID { get; set; }

        [StringLength(100)]
        public string Properties_Name { get; set; }

        public int? Caretaker_id { get; set; }

        [StringLength(100)]
        public string Caretaker_Name { get; set; }

        [StringLength(100)]
        public string Unit_id { get; set; }

        [StringLength(100)]
        public string Unit_name { get; set; }

        [StringLength(100)]
        public string Renewal_Close_Flag { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(150)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        public virtual tbl_agreement tbl_agreement { get; set; }

        public virtual tbl_agreement_closure tbl_agreement_closure { get; set; }
    }
}
