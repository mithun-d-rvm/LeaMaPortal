namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_combo_master")]
    public partial class tbl_combo_master
    {
        public int id { get; set; }

        [StringLength(200)]
        public string screen_name { get; set; }

        [StringLength(200)]
        public string comboname { get; set; }

        [StringLength(1073741823)]
        public string combovalue { get; set; }
    }
}
