using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class SupplierViewModel
    {
        public int id { get; set; }
        
        public int Supplier_Id { get; set; }

        [StringLength(100)]
        public string Supplier_Name { get; set; }

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

        [StringLength(200)]
        public string Locationlink { get; set; }

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

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? License_issueDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? License_ExpiryDate { get; set; }

        [StringLength(50)]
        public string Issuance_authority { get; set; }

        [StringLength(50)]
        public string ADWEA_Regid { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public string EmiratesId { get; set; }
        public int? Accyear { get; set; }

        public DateTime? Createddatetime { get; set; }

        [StringLength(50)]
        public string Createduser { get; set; }

        [StringLength(1)]
        public string Region_Name { get; set; }
        public string Country { get; set; }
        public string Delmark { get; set; }
        public string supplierdt { get; set; }
        public string supplierdt1 { get; set; }
        public List<Supplierdt> SupplierdtList { get; set; }
        public List<Supplierdt1> Supplierdt1List { get; set; }
    }
    public class Supplierdt
    {
        public int id { get; set; }

        public int? Supplier_Id { get; set; }

        [StringLength(100)]
        public string Branch { get; set; }

        [StringLength(500)]
        public string BranchAdd { get; set; }

        [StringLength(500)]
        public string Branchadd1 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string Pincode { get; set; }

        [StringLength(50)]
        public string PhoneNo { get; set; }

        [StringLength(50)]
        public string EMailID { get; set; }

        [StringLength(50)]
        public string FaxNo { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }

        public int? Accyear { get; set; }

        [StringLength(1)]
        public string Delmark { get; set; }
    }
    public class Supplierdt1
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
    }
}