namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_userrights")]
    public partial class tbl_userrights
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string Userid { get; set; }

        [StringLength(100)]
        public string Pwd { get; set; }

        [StringLength(100)]
        public string Cnfpwd { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Phoneno { get; set; }

        public int? AddConfig { get; set; }

        public int? EditConfig { get; set; }

        public int? DeleteConfig { get; set; }

        [StringLength(50)]
        public string MenuConfig { get; set; }

        public int? Active { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(150)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }
    }
}
