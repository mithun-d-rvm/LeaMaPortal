namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_caretaker")]
    public partial class tbl_caretaker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_caretaker()
        {
            tbl_agreement = new HashSet<tbl_agreement>();
            tbl_propertiesmaster = new HashSet<tbl_propertiesmaster>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Caretaker_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string Caretaker_Name { get; set; }

        public DateTime? Dob { get; set; }

        [StringLength(150)]
        public string Address1 { get; set; }

        [StringLength(150)]
        public string Address2 { get; set; }

        [StringLength(150)]
        public string Region_Name { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(20)]
        public string Pincode { get; set; }

        [StringLength(20)]
        public string Phoneno { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        public DateTime? Doj { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(150)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement> tbl_agreement { get; set; }

        public virtual tbl_region tbl_region { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_propertiesmaster> tbl_propertiesmaster { get; set; }
    }
}
