using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Models
{
    public class AgreementFormViewModel
    {
        public AgreementFormViewModel()
        {
            //AgreementUtility = new AgreementUtilityViewModel();
            AgreementUtilityList = new List<AgreementUtilityViewModel>();
            //AgreementPd = new AgreementPdcViewModel();
            AgreementPdcList = new List<AgreementPdcViewModel>();
            agreementDocumentList = new List<AgreementDocumentViewModel>();
        }
        public int Id { get; set; }
        [DisplayName("Single Unit or Multiple Unit")]
        public string Single_Multiple_Flag { get; set; }
        [DisplayName("Existing Contract Contract Agreement")]
        public int Agreement_Refno { get; set; }
        [DisplayName("Contract Agreement Number")]
        public int Agreement_No { get; set; }
        [DisplayName("Contract Agreement Date")]

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Agreement_Date { get; set; }
        [DisplayName("Contract Tenant Type")]
        public string Tenant_Type { get; set; }
        [DisplayName("Tenant ID")]
        public int Ag_Tenant_id { get; set; }
        [DisplayName("Tenant Name")]
        public string Ag_Tenant_Name { get; set; }
        [DisplayName("Property ID")]
        public int property_id { get; set; }
        [DisplayName("Property Tawtheeq ID")]
        public string Property_ID_Tawtheeq { get; set; }
        [DisplayName("Property Name")]
        public string Properties_Name { get; set; }
        [DisplayName("Unit Tawtheeq ID")]
        public string Unit_ID_Tawtheeq { get; set; }
        [DisplayName("Unit Name")]
        public string Unit_Property_Name { get; set; }
        [DisplayName("Property Available From")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Vacantstartdate { get; set; }
        [DisplayName("Contract Agreement Start Date")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Agreement_Start_Date { get; set; }
        [DisplayName("Contract Agreement End Date")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Agreement_End_Date { get; set; }
        [DisplayName("Contract Total Rental amount")]
        public decimal Total_Rental_amount { get; set; }
        [DisplayName("Contract Perday Rental")]
        public decimal Perday_Rental { get; set; }
        [DisplayName("Total Number of Payments")]
        public int nofopayments { get; set; }
        [DisplayName("Contract Security Deposit Amount")]
        public decimal Advance_Security_Amount { get; set; }
        [DisplayName("Security Deposit Payment Mode")]
        public string Security_Flag { get; set; }
        [DisplayName("Security Deposit cheque/Ref no")]
        public string Security_chequeno { get; set; }
        [DisplayName("Security Cheque date")]
       // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Security_chequedate { get; set; }
        [DisplayName("Contract Notice Period Days")]
        public int Notice_Period { get; set; }
      //  public AgreementPdcViewModel AgreementPd { get; set; }
       // public AgreementUtilityViewModel AgreementUtility { get; set; }
        public List<AgreementUtilityViewModel> AgreementUtilityList { get; set; }
        public List<AgreementPdcViewModel> AgreementPdcList { get; set; }
        public List<AgreementDocumentViewModel> agreementDocumentList { get; set; }
        
    }

    public class DdlTenentDetailsViewModel
    {
        public SelectList TenantId { get; set; }
        public SelectList TenantName { get; set; }
    }
}