namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_metermaster")]
    public partial class tbl_metermaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_metermaster()
        {
            tbl_eb_water_billentrydt = new HashSet<tbl_eb_water_billentrydt>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [StringLength(100)]
        public string Utility_id { get; set; }

        [StringLength(150)]
        public string Utility_Name { get; set; }

        [Key]
        [StringLength(100)]
        public string Meter_no { get; set; }

        [StringLength(100)]
        public string Accno { get; set; }

        [StringLength(100)]
        public string Property_id { get; set; }

        [StringLength(100)]
        public string unit_id { get; set; }

        public int? Dueday { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(150)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_eb_water_billentrydt> tbl_eb_water_billentrydt { get; set; }

        public virtual tbl_utilitiesmaster tbl_utilitiesmaster { get; set; }
    }
}
