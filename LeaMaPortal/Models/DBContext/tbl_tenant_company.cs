namespace LeaMaPortal.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("leama.tbl_tenant_company")]
    public partial class tbl_tenant_company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Tenant_Id { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        [StringLength(10)]
        public string Marital_Status { get; set; }

        [StringLength(5)]
        public string Title { get; set; }

        [StringLength(100)]
        public string First_Name { get; set; }

        [StringLength(100)]
        public string Middle_Name { get; set; }

        [StringLength(100)]
        public string Last_Name { get; set; }

        [StringLength(500)]
        public string address { get; set; }

        [StringLength(500)]
        public string address1 { get; set; }

        [StringLength(100)]
        public string Emirate { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string PostboxNo { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(10)]
        public string Mobile_Countrycode { get; set; }

        [StringLength(10)]
        public string Mobile_Areacode { get; set; }

        [StringLength(20)]
        public string Mobile_No { get; set; }

        [StringLength(10)]
        public string Landline_Countrycode { get; set; }

        [StringLength(10)]
        public string Landline_Areacode { get; set; }

        [StringLength(20)]
        public string Landline_No { get; set; }

        [StringLength(10)]
        public string Fax_Countrycode { get; set; }

        [StringLength(10)]
        public string Fax_Areacode { get; set; }

        [StringLength(20)]
        public string Fax_No { get; set; }

        [StringLength(20)]
        public string Nationality { get; set; }

        [StringLength(50)]
        public string Actitvity { get; set; }

        [StringLength(50)]
        public string Cocandindustryuid { get; set; }

        [StringLength(100)]
        public string TradelicenseNo { get; set; }

        public DateTime? License_issueDate { get; set; }

        public DateTime? License_ExpiryDate { get; set; }

        [StringLength(50)]
        public string Issuance_authority { get; set; }

        [StringLength(50)]
        public string ADWEA_Regid { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Type { get; set; }

        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(50)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }
    }
}
