namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.test_report1")]
    public partial class test_report1
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Agreement_No { get; set; }

        [StringLength(100)]
        public string Prperty_id { get; set; }

        [StringLength(100)]
        public string Property_name { get; set; }

        [StringLength(100)]
        public string Unit_id { get; set; }

        [StringLength(100)]
        public string Unitname { get; set; }

        [StringLength(150)]
        public string Region_Name { get; set; }

        [StringLength(150)]
        public string Country { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Caretaker_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string Caretaker_Name { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Ag_Tenant_id { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string Ag_Tenant_Name { get; set; }

        public DateTime? Agreement_End_Date { get; set; }

        public float? Perday_Rental { get; set; }

        public int? Remaining_Days { get; set; }

        public double? Contract_Value { get; set; }
    }
}
