namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_log")]
    public partial class tbl_log
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Table_Name { get; set; }

        [StringLength(50)]
        public string Operations { get; set; }

        public DateTime? Opeartion_Datetime { get; set; }

        [StringLength(50)]
        public string Login_username { get; set; }

        [StringLength(50)]
        public string Keyfield { get; set; }

        [StringLength(50)]
        public string Keyfieldvalue { get; set; }

        [StringLength(75)]
        public string Machine_Name { get; set; }

        [StringLength(100)]
        public string Procedure_Name { get; set; }
    }
}
