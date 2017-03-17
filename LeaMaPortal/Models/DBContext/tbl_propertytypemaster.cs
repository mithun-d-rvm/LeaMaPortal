namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_propertytypemaster")]
    public partial class tbl_propertytypemaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Type_Flag { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(75)]
        public string Type_name { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Usage_name { get; set; }

        [StringLength(10)]
        public string Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(50)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }
    }
}
