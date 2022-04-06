using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class AgreementClosureViewModel
    {
       public AgreementClosureViewModel()
        {

            AgreementPdcList = new List<AgreementPdcViewModel>();
            agreementDocumentExistList = new List<AgreementDocumentExist>();
            agreementFacilityList = new List<AgreementFacilityViewModel>();
            AgreementUtilityList = new List<AgreementUtilityViewModel>();
            AgreementCheckList = new List<AgreementCheckListViewModel>();
        }
        public int Id { get; set; }
       
        //[DisplayName("Existing Contract Contract Agreement")]
        //public int Agreement_Refno { get; set; }
        [DisplayName("Contract Agreement Number")]
        public int Agreement_No { get; set; }
        [DisplayName("Contract Agreement Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Agreement_Date { get; set; }
        [DisplayName("Contract Tenant Type")]
        public string Tenant_Type { get; set; }
        [DisplayName("Tenant ID")]
        //doubt
        public int Ag_Tenant_id { get; set; }
        [DisplayName("Tenant Name")]
        //doubt
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
        [DisplayName("Contract Agreement Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Agreement_Start_Date { get; set; }
        [DisplayName("Contract Agreement End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Agreement_End_Date { get; set; }

        [DisplayName("Total Contract Amount")]
        //check
        public float? Total_Contract_Amount { get; set; }
        [DisplayName("Total Amount Paid")]
        //check
        public float? Total_Amount_Paid { get; set; }
        [DisplayName("Amount Pending from Tenant")]
        public float Advance_pending { get; set; }
        [DisplayName("Security Deposit Paid")]
        public float? Advance_Security_Amount_Paid { get; set; }
        [DisplayName("Less Damages (if any)")]
        public string Less_any_damanges { get; set; }
        [DisplayName("Total Amount to be refunded")]
        public float Amount_to_be_refunded { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        public List<AgreementPdcViewModel> AgreementPdcList { get; set; }
        public List<AgreementDocumentExist> agreementDocumentExistList { get; set; }
        public string pAgclpdc { get; set; }
        public List<AgreementFacilityViewModel> agreementFacilityList { get; set; }
        public string pAgclfac { get; set; }
        public List<AgreementUtilityViewModel> AgreementUtilityList { get; set; }
        public string pAgcluti { get; set; }
        public List<AgreementCheckListViewModel> AgreementCheckList { get; set; }
        public string pAgclchk { get; set; }

    }

    public class ClosureAmountViewModel
    {
        //agreement_no,totalamount,paidamount,advancepending
        public int agreement_no { get; set; }
        public float totalamount { get; set; }
        public float paidamount { get; set; }
        public float advancepending { get; set; }
        public float total_rental_amount { get; set; }
        public float Securityamount { get; set; }

    }
}