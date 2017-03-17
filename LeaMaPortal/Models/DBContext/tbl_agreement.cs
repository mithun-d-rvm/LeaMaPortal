namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_agreement")]
    public partial class tbl_agreement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_agreement()
        {
            tbl_agreement_facility = new HashSet<tbl_agreement_facility>();
            tbl_agreement_unit_inner = new HashSet<tbl_agreement_unit_inner>();
            tbl_agreement_pdc = new HashSet<tbl_agreement_pdc>();
            tbl_agreement_checklist = new HashSet<tbl_agreement_checklist>();
            tbl_agreement_doc = new HashSet<tbl_agreement_doc>();
            tbl_agreement_utility = new HashSet<tbl_agreement_utility>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [StringLength(20)]
        public string Single_Multiple_Flag { get; set; }

        public int? Agreement_Refno { get; set; }

        [StringLength(20)]
        public string New_Renewal_flag { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Agreement_No { get; set; }

        public DateTime? Agreement_Date { get; set; }

        public int Ag_Tenant_id { get; set; }

        [Required]
        [StringLength(100)]
        public string Ag_Tenant_Name { get; set; }

        public int property_id { get; set; }

        [StringLength(100)]
        public string Property_ID_Tawtheeq { get; set; }

        [StringLength(100)]
        public string Properties_Name { get; set; }

        [StringLength(100)]
        public string Unit_ID_Tawtheeq { get; set; }

        [StringLength(100)]
        public string Unit_Property_Name { get; set; }

        public int Caretaker_id { get; set; }

        [Required]
        [StringLength(100)]
        public string Caretaker_Name { get; set; }

        public DateTime? Vacantstartdate { get; set; }

        public DateTime? Agreement_Start_Date { get; set; }

        public DateTime? Agreement_End_Date { get; set; }

        public float? Total_Rental_amount { get; set; }

        public float? Perday_Rental { get; set; }

        public float? Advance_Security_Amount { get; set; }

        [StringLength(20)]
        public string Security_Flag { get; set; }

        [StringLength(50)]
        public string Security_chequeno { get; set; }

        public DateTime? Security_chequedate { get; set; }

        public int? Notice_Period { get; set; }

        public int? nofopayments { get; set; }

        public int? Approval_Flag { get; set; }

        [StringLength(75)]
        public string Approved_By { get; set; }

        public DateTime? Approved_Date { get; set; }

        [StringLength(25)]
        public string Tenant_Type { get; set; }

        [StringLength(10)]
        public string Status { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(150)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_facility> tbl_agreement_facility { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_unit_inner> tbl_agreement_unit_inner { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_pdc> tbl_agreement_pdc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_checklist> tbl_agreement_checklist { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_doc> tbl_agreement_doc { get; set; }

        public virtual tbl_propertiesmaster tbl_propertiesmaster { get; set; }

        public virtual tbl_caretaker tbl_caretaker { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_utility> tbl_agreement_utility { get; set; }

        public virtual tbl_agreement_status tbl_agreement_status { get; set; }
    }
}
