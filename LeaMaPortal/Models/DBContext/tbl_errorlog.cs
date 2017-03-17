namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_errorlog")]
    public partial class tbl_errorlog
    {
        public int Id { get; set; }

        [StringLength(75)]
        public string Procedure_Name { get; set; }

        [StringLength(1073741823)]
        public string Error_Text { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(50)]
        public string User { get; set; }

        [StringLength(75)]
        public string Machine_Name { get; set; }
    }
}
