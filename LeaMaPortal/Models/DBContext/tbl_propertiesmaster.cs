namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_propertiesmaster")]
    public partial class tbl_propertiesmaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_propertiesmaster()
        {
            tbl_agreement = new HashSet<tbl_agreement>();
            tbl_propertiesdt = new HashSet<tbl_propertiesdt>();
            tbl_propertiesdt1 = new HashSet<tbl_propertiesdt1>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [StringLength(100)]
        public string Property_Flag { get; set; }

        [StringLength(100)]
        public string Property_ID_Tawtheeq { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Property_Id { get; set; }

        [StringLength(100)]
        public string Property_Name { get; set; }

        [StringLength(100)]
        public string Compound { get; set; }

        [StringLength(100)]
        public string Zone { get; set; }

        [StringLength(100)]
        public string sector { get; set; }

        [StringLength(10)]
        public string plotno { get; set; }

        public int? ownedbyregistrant { get; set; }

        [StringLength(100)]
        public string Property_Usage { get; set; }

        [StringLength(100)]
        public string Property_Type { get; set; }

        public int? Commercial_villa { get; set; }

        [StringLength(100)]
        public string Street_Name { get; set; }

        [StringLength(150)]
        public string Address1 { get; set; }

        [StringLength(150)]
        public string Address2 { get; set; }

        [StringLength(150)]
        public string Address3 { get; set; }

        [StringLength(150)]
        public string Region_Name { get; set; }

        [StringLength(150)]
        public string Country { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(100)]
        public string Externalrefno { get; set; }

        public int? Noofoffloors { get; set; }

        public int? Noofunits { get; set; }

        public float? Builtarea { get; set; }

        public float? Plotarea { get; set; }

        public float? Leasablearea { get; set; }

        public float? commonarea { get; set; }

        public DateTime? completion_Date { get; set; }

        public float? AEDvalue { get; set; }

        public DateTime? Purchased_date { get; set; }

        public DateTime? Valued_Date { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        public DateTime? Vacant_Start_Date { get; set; }

        [StringLength(100)]
        public string Caretaker_Name { get; set; }

        public int? Caretaker_ID { get; set; }

        public float? Rental_Rate_Month { get; set; }

        [StringLength(1073741823)]
        public string Comments { get; set; }

        [StringLength(100)]
        public string Ref_unit_Property_ID_Tawtheeq { get; set; }

        public int? Ref_Unit_Property_ID { get; set; }

        [StringLength(100)]
        public string Ref_Unit_Property_Name { get; set; }

        [StringLength(100)]
        public string Unit_ID_Tawtheeq { get; set; }

        [StringLength(100)]
        public string Unit_Property_Name { get; set; }

        [StringLength(100)]
        public string Externalrefno_unit { get; set; }

        public float? AEDvalue_unit { get; set; }

        public DateTime? Purchased_date_unit { get; set; }

        public DateTime? Valued_Date_unit { get; set; }

        [StringLength(100)]
        public string Status_unit { get; set; }

        public DateTime? Vacant_Start_Date_Unit { get; set; }

        public float? Rental_Rate_Month_unit { get; set; }

        [StringLength(100)]
        public string Floorno { get; set; }

        [StringLength(100)]
        public string Floorlevel { get; set; }

        [StringLength(100)]
        public string Property_Usage_unit { get; set; }

        [StringLength(100)]
        public string Property_Type_unit { get; set; }

        public float? Total_Area { get; set; }

        public float? Unit_Common_Area { get; set; }

        public float? Common_Area { get; set; }

        public int? Parkingno { get; set; }

        [StringLength(1073741823)]
        public string Unitcomments { get; set; }

        public int? Company_occupied_Flag { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(50)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement> tbl_agreement { get; set; }

        public virtual tbl_caretaker tbl_caretaker { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_propertiesdt> tbl_propertiesdt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_propertiesdt1> tbl_propertiesdt1 { get; set; }

        public virtual tbl_region tbl_region { get; set; }
    }
}
