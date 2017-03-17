namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_formmaster")]
    public partial class tbl_formmaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(2)]
        public string Menutype { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string MenuName { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(100)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }
    }
}
