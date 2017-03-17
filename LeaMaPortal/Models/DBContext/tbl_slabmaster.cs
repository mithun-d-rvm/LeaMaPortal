namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_slabmaster")]
    public partial class tbl_slabmaster
    {
        public int id { get; set; }

        public int? slabid { get; set; }

        [StringLength(100)]
        public string Utility_id { get; set; }

        [StringLength(150)]
        public string Utility_Name { get; set; }

        public int? Unit_From { get; set; }

        public int? Unitto { get; set; }

        public float? rate { get; set; }

        [StringLength(20)]
        public string Colour { get; set; }

        [StringLength(50)]
        public string Residence_type { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(150)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        public virtual tbl_utilitiesmaster tbl_utilitiesmaster { get; set; }
    }
}
