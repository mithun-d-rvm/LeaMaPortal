namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_emailconfigurationmaster")]
    public partial class tbl_emailconfigurationmaster
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string MailServerName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(1073741823)]
        public string PassWord { get; set; }

        public int? Port { get; set; }

        public int? InSSl { get; set; }

        [StringLength(100)]
        public string AttachmentDownloadPath { get; set; }

        [StringLength(100)]
        public string AliasName { get; set; }

        [StringLength(1073741823)]
        public string OutFromId { get; set; }

        [StringLength(1073741823)]
        public string Mode { get; set; }

        [StringLength(100)]
        public string OutMailServerName { get; set; }

        [StringLength(100)]
        public string OutEmailId { get; set; }

        [StringLength(1073741823)]
        public string OutPassword { get; set; }

        public int? OutPortNo { get; set; }

        public int? OutSSl { get; set; }

        [StringLength(100)]
        public string InMailID { get; set; }

        [StringLength(1073741823)]
        public string ServerTimeZone { get; set; }

        [StringLength(45)]
        public string IncNo { get; set; }

        public int? ServerType { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(200)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }
    }
}
