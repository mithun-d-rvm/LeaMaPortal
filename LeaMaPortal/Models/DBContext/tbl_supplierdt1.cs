namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_supplierdt1")]
    public partial class tbl_supplierdt1
    {
        public int id { get; set; }

        public int? Supplier_Id { get; set; }

        [StringLength(175)]
        public string Cper { get; set; }

        [StringLength(50)]
        public string Cpercode { get; set; }

        [StringLength(150)]
        public string Desig { get; set; }

        [StringLength(150)]
        public string Dept { get; set; }

        [StringLength(150)]
        public string Phone { get; set; }

        [StringLength(150)]
        public string Ext { get; set; }

        [StringLength(150)]
        public string MobNo { get; set; }

        [StringLength(50)]
        public string Salutations { get; set; }

        public int? Accyear { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        public virtual tbl_suppliermaster tbl_suppliermaster { get; set; }
    }
}
