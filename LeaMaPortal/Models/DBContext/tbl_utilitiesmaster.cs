namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_utilitiesmaster")]
    public partial class tbl_utilitiesmaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_utilitiesmaster()
        {
            tbl_metermaster = new HashSet<tbl_metermaster>();
            tbl_propertiesdt1 = new HashSet<tbl_propertiesdt1>();
            tbl_slabmaster = new HashSet<tbl_slabmaster>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string Utility_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(150)]
        public string Utility_Name { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(150)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_metermaster> tbl_metermaster { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_propertiesdt1> tbl_propertiesdt1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_slabmaster> tbl_slabmaster { get; set; }
    }
}
