namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_agreement_closure")]
    public partial class tbl_agreement_closure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_agreement_closure()
        {
            tbl_agreement_closure_checklist = new HashSet<tbl_agreement_closure_checklist>();
            tbl_agreement_closure_facility = new HashSet<tbl_agreement_closure_facility>();
            tbl_agreement_closure_pdc = new HashSet<tbl_agreement_closure_pdc>();
            tbl_agreement_closure_utility = new HashSet<tbl_agreement_closure_utility>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Agreement_No { get; set; }

        public float? Advance_pending { get; set; }

        [StringLength(100)]
        public string Advance_Security_Amount_Paid { get; set; }

        [StringLength(100)]
        public string Less_any_damanges { get; set; }

        public float? Amount_to_be_refunded { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        public DateTime? Availabledate { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(150)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        public virtual tbl_agreement_status tbl_agreement_status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_closure_checklist> tbl_agreement_closure_checklist { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_closure_facility> tbl_agreement_closure_facility { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_closure_pdc> tbl_agreement_closure_pdc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_closure_utility> tbl_agreement_closure_utility { get; set; }
    }
}
