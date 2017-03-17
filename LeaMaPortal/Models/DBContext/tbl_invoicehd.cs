namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_invoicehd")]
    public partial class tbl_invoicehd
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_invoicehd()
        {
            tbl_invoicedt = new HashSet<tbl_invoicedt>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Key]
        [StringLength(50)]
        public string invno { get; set; }

        public DateTime? date { get; set; }

        public int? Tenant_id { get; set; }

        [StringLength(100)]
        public string Tenant_Name { get; set; }

        [StringLength(50)]
        public string invtype { get; set; }

        public int? Agreement_No { get; set; }

        [StringLength(100)]
        public string Property_ID { get; set; }

        [StringLength(100)]
        public string Property_Name { get; set; }

        [StringLength(100)]
        public string Unit_ID { get; set; }

        [StringLength(100)]
        public string unit_Name { get; set; }

        public int? month { get; set; }

        public int? year { get; set; }

        public float? totalamt { get; set; }

        public DateTime? duedate { get; set; }

        [StringLength(500)]
        public string bank_details { get; set; }

        [StringLength(500)]
        public string remarks { get; set; }

        public int? incno { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(150)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_invoicedt> tbl_invoicedt { get; set; }
    }
}
