namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_eb_water_paymenthd")]
    public partial class tbl_eb_water_paymenthd
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_eb_water_paymenthd()
        {
            tbl_eb_water_paymentdt = new HashSet<tbl_eb_water_paymentdt>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PaymentNo { get; set; }

        public DateTime? PaymentDate { get; set; }

        [StringLength(100)]
        public string Utility_id { get; set; }

        [StringLength(100)]
        public string Utiltiy_name { get; set; }

        public int? Supplier_id { get; set; }

        [StringLength(200)]
        public string Supplier_name { get; set; }

        [StringLength(25)]
        public string PaymentType { get; set; }

        [StringLength(25)]
        public string PaymentMode { get; set; }

        public float? TotalAmount { get; set; }

        [StringLength(1000)]
        public string AmtInWords { get; set; }

        [StringLength(100)]
        public string DDChequeNo { get; set; }

        public DateTime? Cheqdate { get; set; }

        [StringLength(20)]
        public string pdcstatus { get; set; }

        [StringLength(50)]
        public string BankAcCode { get; set; }

        [StringLength(100)]
        public string BankAcName { get; set; }

        [StringLength(100)]
        public string AdvAcCode { get; set; }

        [StringLength(1000)]
        public string Narration { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(150)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_eb_water_paymentdt> tbl_eb_water_paymentdt { get; set; }
    }
}
