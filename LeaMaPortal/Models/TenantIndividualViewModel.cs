using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class TenantIndividualViewModel
    {
        public int Tenant_Id { get; set; }
        [StringLength(5)]
        public string Title { get; set; }

        [StringLength(100)]
        [Display(Name = "First Name")]
        public string First_Name { get; set; }

        [StringLength(100)]
        [Display(Name = "Middle Name")]
        public string Middle_Name { get; set; }

        [StringLength(100)]
        public string Last_Name { get; set; }

        [StringLength(100)]
        public string Company_Educational { get; set; }

        [StringLength(100)]
        public string Profession { get; set; }

        [StringLength(100)]
        public string Marital_Status { get; set; }

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

        [StringLength(100)]
        public string Mobile_Countrycode { get; set; }

        [StringLength(100)]
        public string Mobile_Areacode { get; set; }

        [StringLength(100)]
        public string Mobile_No { get; set; }

        [StringLength(100)]
        public string Landline_Countrycode { get; set; }

        [StringLength(100)]
        public string Landline_Areacode { get; set; }

        [StringLength(100)]
        public string Landline_No { get; set; }

        [StringLength(100)]
        public string Fax_Countrycode { get; set; }

        [StringLength(100)]
        public string Fax_Areacode { get; set; }

        [StringLength(100)]
        public string Fax_No { get; set; }

        [StringLength(100)]
        public string Nationality { get; set; }

        [StringLength(100)]
        public string Emiratesid { get; set; }

        public DateTime? Emirate_issuedate { get; set; }

        public DateTime? Emirate_expirydate { get; set; }

        [StringLength(100)]
        public string Passportno { get; set; }

        [StringLength(100)]
        public string Placeofissuance { get; set; }

        public DateTime? Passport_Issuedate { get; set; }

        public DateTime? Passport_Expirydate { get; set; }

        [StringLength(100)]
        public string VisaType { get; set; }

        [StringLength(100)]
        public string Visano { get; set; }

        public DateTime? Visa_IssueDate { get; set; }

        public DateTime? Visa_ExpiryDate { get; set; }

        public DateTime? Dob { get; set; }

        [StringLength(100)]
        public string Familyno { get; set; }

        [StringLength(100)]
        public string Familybookcity { get; set; }

        [StringLength(100)]
        public string ADWEA_Regid { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string Type { get; set; }

        //public int? Accyear { get; set; }

        //public DateTime? Createddatetime { get; set; }

        //[StringLength(150)]
        //public string Createduser { get; set; }

        //[StringLength(1)]
        //public string Delmark { get; set; }
    }
}