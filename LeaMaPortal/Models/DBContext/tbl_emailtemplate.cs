namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_emailtemplate")]
    public partial class tbl_emailtemplate
    {
        public int Id { get; set; }

        public int? TemplateID { get; set; }

        [StringLength(1073741823)]
        public string TemplateName { get; set; }

        [StringLength(1073741823)]
        public string Subject { get; set; }

        [StringLength(1073741823)]
        public string Body { get; set; }

        [StringLength(1073741823)]
        public string Bodytext { get; set; }

        [StringLength(1073741823)]
        public string SubjectParameter { get; set; }

        [StringLength(1073741823)]
        public string BodyParameter { get; set; }

        [Column(TypeName = "bit")]
        public bool? InActive { get; set; }

        [StringLength(1073741823)]
        public string UpdateUser { get; set; }

        public DateTime? UpdateDate { get; set; }

        [StringLength(1073741823)]
        public string SubjectText { get; set; }

        [StringLength(1073741823)]
        public string BodyTextParam { get; set; }

        [StringLength(1073741823)]
        public string SubjectParameterText { get; set; }

        [StringLength(1073741823)]
        public string BodyParameterText { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(100)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }
    }
}
