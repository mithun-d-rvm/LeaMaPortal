namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_eb_water_billentryhd")]
    public partial class tbl_eb_water_billentryhd
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_eb_water_billentryhd()
        {
            tbl_eb_water_billentrydt = new HashSet<tbl_eb_water_billentrydt>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Refno { get; set; }

        [Column(TypeName = "date")]
        public DateTime? refdate { get; set; }

        [StringLength(100)]
        public string Utility_id { get; set; }

        [StringLength(100)]
        public string Utiltiy_name { get; set; }

        public int? Supplier_id { get; set; }

        [StringLength(200)]
        public string Supplier_name { get; set; }

        public int? Accyear { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Createddatetime { get; set; }

        [StringLength(100)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_eb_water_billentrydt> tbl_eb_water_billentrydt { get; set; }
    }
}
