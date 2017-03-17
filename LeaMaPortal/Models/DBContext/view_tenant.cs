namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.view_tenant")]
    public partial class view_tenant
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Tenant_id { get; set; }

        [StringLength(100)]
        public string First_Name { get; set; }

        [StringLength(100)]
        public string Middle_Name { get; set; }

        [StringLength(100)]
        public string Last_Name { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string type { get; set; }
    }
}
