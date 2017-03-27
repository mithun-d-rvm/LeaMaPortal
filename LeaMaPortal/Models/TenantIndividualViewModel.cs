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
       
        [Display(Name = "Tenant ID:")]
        public int Tenant_Id { get; set; }
        [StringLength(5)]

        [Display(Name = "Title:")]
        public string Title { get; set; }

        [StringLength(100)]
        [Display(Name = "First Name:")]
        public string First_Name { get; set; }

        [StringLength(100)]
        [Display(Name = "Middle Name:")]
        public string Middle_Name { get; set; }

        [StringLength(100)]
        [Display(Name = "Last Name:")]
        public string Last_Name { get; set; }

        [StringLength(100)]
        [Display(Name = "Company Name/ Educational Institute:")]
        public string Company_Educational { get; set; }

        [StringLength(100)]
        [Display(Name = "Profession:")]
        public string Profession { get; set; }

        [StringLength(100)]
        [Display(Name = "Marital Status:")]
        public string Marital_Status { get; set; }

        [StringLength(500)]
        [Display(Name = "Address:")]
        public string address { get; set; }

        [StringLength(500)]
        [Display(Name = "Address 01:")]
        public string address1 { get; set; }

        [StringLength(100)]
        [Display(Name = "Emirate:")]
        public string Emirate { get; set; }

        [StringLength(100)]
        [Display(Name = "City:")]
        public string City { get; set; }

        [StringLength(100)]
        [Display(Name = "Post Box:")]
        public string PostboxNo { get; set; }

        [StringLength(100)]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [StringLength(100)]

        public string Mobile_Countrycode { get; set; }

        [StringLength(100)]
        public string Mobile_Areacode { get; set; }

        [StringLength(100)]
        [Display(Name = "Mobile:")]
        public string Mobile_No { get; set; }

        [StringLength(100)]
        public string Landline_Countrycode { get; set; }

        [StringLength(100)]
        public string Landline_Areacode { get; set; }

        [StringLength(100)]
        [Display(Name = "Landline:")]
        public string Landline_No { get; set; }

        [StringLength(100)]
        public string Fax_Countrycode { get; set; }

        [StringLength(100)]
        public string Fax_Areacode { get; set; }

        [StringLength(100)]
        [Display(Name = "Fax:")]
        public string Fax_No { get; set; }

        [StringLength(100)]
        [Display(Name = "Nationality:")]
        public string Nationality { get; set; }

        [StringLength(100)]
        [Display(Name = "Emirates ID:")]
        public string Emiratesid { get; set; }
        [Display(Name = "Emirate Issue Date:")]
        [DataType(DataType.Date)]
        public DateTime? Emirate_issuedate { get; set; }
        [Display(Name = "Emirates Expiry Date:")]
        [DataType(DataType.Date)]
        public DateTime? Emirate_expirydate { get; set; }

        [StringLength(100)]
        [Display(Name = "Passport Number:")]
        public string Passportno { get; set; }

        [StringLength(100)]
        [Display(Name = "Place of Insurance:")]
        public string Placeofissuance { get; set; }

        [Display(Name = "Passport Issue Date:")]
        [DataType(DataType.Date)]
        public DateTime? Passport_Issuedate { get; set; }

        [Display(Name = "Passport Expiry Date:")]
        [DataType(DataType.Date)]
        public DateTime? Passport_Expirydate { get; set; }

        [StringLength(100)]
        [Display(Name = "Visa Type:")]
        public string VisaType { get; set; }

        [StringLength(100)]
        public string Visano { get; set; }
        [Display(Name = "Visa Issue Date:")]
        [DataType(DataType.Date)]
        public DateTime? Visa_IssueDate { get; set; }
        [Display(Name = "Visa Expiry Date:")]
        [DataType(DataType.Date)]
        public DateTime? Visa_ExpiryDate { get; set; }
        [Display(Name = "Date of Birth:")]
        [DataType(DataType.Date)]
        public DateTime? Dob { get; set; }

        [StringLength(100)]
        [Display(Name = "Family Number:")]
        public string Familyno { get; set; }

        [StringLength(100)]
        [Display(Name = "Family Book City:")]
        public string Familybookcity { get; set; }

        [StringLength(100)]
        [Display(Name = "ADWEA Resigtration Id:")]
        public string ADWEA_Regid { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string Type { get; set; }

        [StringLength(150)]
        public string Createduser { get; set; }
        
        public string tenantdocdetails { get; set; }
        public List<TenantDocumentVM> TenantDocument { get; set; }
        public List<tbl_tenant_individual_docVM> TenantDocumentList { get; set; }
    }

    public class TenantDocumentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HttpPostedFileBase File { get; set; }
    }

    public partial class tbl_tenant_individual_doc
    {
        public int id { get; set; }

        public int? Tenant_Id { get; set; }

        [StringLength(150)]
        public string Doc_name { get; set; }

        [StringLength(300)]
        public string Doc_Path { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }
    }
    public partial class tbl_tenant_individual_docVM
    {
        public int id { get; set; }

        public int? Tenant_Id { get; set; }

        [StringLength(150)]
        public string Doc_name { get; set; }

        [StringLength(300)]
        public string Doc_Path { get; set; }

     
    }
}