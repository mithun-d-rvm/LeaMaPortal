namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_eb_water_billentrydt")]
    public partial class tbl_eb_water_billentrydt
    {
        public int id { get; set; }

        public int? Refno { get; set; }

        [StringLength(100)]
        public string Meterno { get; set; }

        [StringLength(100)]
        public string property_id { get; set; }

        [StringLength(100)]
        public string Unit_id { get; set; }

        public float? Total_units { get; set; }

        [StringLength(100)]
        public string Meterreadingno { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Reading_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? billdate { get; set; }

        public int? billno { get; set; }

        [Column(TypeName = "date")]
        public DateTime? duedate { get; set; }

        public int? daysofuse { get; set; }

        public float? rate { get; set; }

        public float? amount { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        public virtual tbl_eb_water_billentryhd tbl_eb_water_billentryhd { get; set; }

        public virtual tbl_metermaster tbl_metermaster { get; set; }
    }
}
