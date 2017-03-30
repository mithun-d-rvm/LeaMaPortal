using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class TenantCompanyViewModel
    {
        public TenantCompanyViewModel()
        {
            CompanyContactDetails = new List<CompanyContactDetail>();
            CompanyDetails = new List<CompanyDetail>();
        }
        public int Id { get; set; }

        [Required]
        public string TenantType { get; set; }

        [Required]
        public int TenantId { get; set; }

        public string CompanyName { get; set; }

        public string MaritalStatus { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Address1 { get; set; }

        public string Emirate { get; set; }

        public string City { get; set; }

        public string PostboxNo { get; set; }

        public string Email { get; set; }

        public string MobileCountryCode { get; set; }
        
        public string MobileAreaCode { get; set; }
        
        public string MobileNo { get; set; }
        
        public string LandlineCountryCode { get; set; }
        
        public string LandlineAreaCode { get; set; }
        
        public string LandlineNo { get; set; }

        public string FaxCountryCode { get; set; }

        public string FaxAreaCode { get; set; }

        public string FaxNo { get; set; }

        public string Nationality { get; set; }

        public string ComapanyActivity { get; set; }

        public string Cocandindustryuid { get; set; }

        public string TradelicenseNo { get; set; }
        [DataType(DataType.Date)]
        public DateTime? LicenseIssueDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? LicenseExpiryDate { get; set; }

        public string Issuance_authority { get; set; }

        public string ADWEARegid { get; set; }
        public List<CompanyDetail> CompanyDetails { get; set; }
       
        public List<CompanyContactDetail> CompanyContactDetails { get; set; }
        
    }
    public class CompanyDetail
    {
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public string Branch { get; set; }

        public string BranchAddress { get; set; }

        public string BranchAddress1 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Pincode { get; set; }

        public string Phoneno { get; set; }

        public string EmailId { get; set; }

        public string FaxNo { get; set; }

        public string Remarks { get; set; }
    }
    public class CompanyContactDetail
    {
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public string ContactPersonName { get; set; }

        public string Designation { get; set; }

        public string Department { get; set; }

        public string Phone { get; set; }

        public string Extension { get; set; }

        public string MobileNo { get; set; }

        public string Salutations { get; set; }

    }
}