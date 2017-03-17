namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_propertiesdt")]
    public partial class tbl_propertiesdt
    {
        public int id { get; set; }

        public int? Property_Id { get; set; }

        [StringLength(100)]
        public string Property_ID_Tawtheeq { get; set; }

        [StringLength(100)]
        public string Unit_ID_Tawtheeq { get; set; }

        [StringLength(150)]
        public string Facility_id { get; set; }

        [StringLength(150)]
        public string Facility_Name { get; set; }

        [StringLength(150)]
        public string Numbers_available { get; set; }

        public int? Accyear { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }

        public virtual tbl_facilitymaster tbl_facilitymaster { get; set; }

        public virtual tbl_propertiesmaster tbl_propertiesmaster { get; set; }
    }
}
