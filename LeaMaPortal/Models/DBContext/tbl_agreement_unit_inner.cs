namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_agreement_unit_inner")]
    public partial class tbl_agreement_unit_inner
    {
        public int id { get; set; }

        public int? Agreement_No { get; set; }

        public int? Property_ID { get; set; }

        [StringLength(100)]
        public string Property_ID_Tawtheeq { get; set; }

        [StringLength(100)]
        public string Properties_Name { get; set; }

        [StringLength(100)]
        public string Unit_ID_Tawtheeq { get; set; }

        [StringLength(100)]
        public string Unit_Property_Name { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        public virtual tbl_agreement tbl_agreement { get; set; }
    }
}
