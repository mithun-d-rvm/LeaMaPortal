namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_region")]
    public partial class tbl_region
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_region()
        {
            tbl_caretaker = new HashSet<tbl_caretaker>();
            tbl_propertiesmaster = new HashSet<tbl_propertiesmaster>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(75)]
        public string Region_Name { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(75)]
        public string Country { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(75)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_caretaker> tbl_caretaker { get; set; }

        public virtual tbl_country tbl_country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_propertiesmaster> tbl_propertiesmaster { get; set; }
    }
}
