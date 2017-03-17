namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.view_find_pdcstatus")]
    public partial class view_find_pdcstatus
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(50)]
        public string Reccategory { get; set; }

        [StringLength(20)]
        public string RecpType { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReceiptNo { get; set; }

        public DateTime? ReceiptDate { get; set; }

        public int? agreement_no { get; set; }

        [StringLength(100)]
        public string Property_id { get; set; }

        [StringLength(100)]
        public string Property_Name { get; set; }

        [StringLength(100)]
        public string Unit_ID { get; set; }

        [StringLength(100)]
        public string unit_Name { get; set; }

        public int? Tenant_id { get; set; }

        [StringLength(100)]
        public string Tenant_Name { get; set; }

        public float? TotalAmount { get; set; }

        [StringLength(1000)]
        public string AmtInWords { get; set; }

        [StringLength(50)]
        public string DDChequeNo { get; set; }

        [StringLength(20)]
        public string PDCstatus { get; set; }

        [StringLength(50)]
        public string BankAcCode { get; set; }

        [StringLength(100)]
        public string BankAcName { get; set; }

        [StringLength(100)]
        public string AdvAcCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DDChequeDate { get; set; }

        [StringLength(1000)]
        public string Narration { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(150)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }
    }
}
