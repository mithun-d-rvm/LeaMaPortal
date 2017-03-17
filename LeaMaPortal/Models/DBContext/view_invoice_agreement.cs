namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.view_invoice_agreement")]
    public partial class view_invoice_agreement
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Agreement_No { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Ag_Tenant_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string Ag_Tenant_Name { get; set; }

        [StringLength(100)]
        public string Property_ID_Tawtheeq { get; set; }

        [StringLength(100)]
        public string Properties_Name { get; set; }

        [StringLength(100)]
        public string Unit_ID_Tawtheeq { get; set; }

        [StringLength(100)]
        public string Unit_Property_Name { get; set; }

        public float? Perday_Rental { get; set; }

        public DateTime? Agreement_Start_Date { get; set; }

        public DateTime? Agreement_End_Date { get; set; }
    }
}
