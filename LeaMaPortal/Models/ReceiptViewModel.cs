using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Models
{
    public class ReceiptViewModel
    {
        public ReceiptViewModel()
        {
            ReceiptDetailsList = new List<ReceiptDetailsViewModel>();
        }
        public int Id { get; set; }
        [DisplayName("Receipt No")]
        public int ReceiptNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Receipt Date")]
        public DateTime? ReceiptDate { get; set; }

        [DisplayName("Receipt Mode")]
        public string RecpType { get; set; }
        [DisplayName("Receipt Type")]
        public string Reccategory { get; set; }
        [DisplayName("Advance Receipt Number")]
        public string AdvAcCode { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Contract Agreement Date")]
        public DateTime ContracAgreementDate { get; set; }

        [DisplayName("Contract Agreement Number")]
        public int? agreement_no { get; set; }
        [DisplayName("Tenant ID")]
        public int? Tenant_id { get; set; }
        [DisplayName("Tenant Name")]
        public string Tenant_Name { get; set; }
        [DisplayName("Unit ID")]
        public string Unit_ID { get; set; }
        [DisplayName("Unit Name")]
        public string unit_Name { get; set; }
        [DisplayName("Property ID")]
        public string Property_id { get; set; }
        [DisplayName("Property Name")]
        public string Property_Name { get; set; }
        [DisplayName("Total Amount")]
        public float? TotalAmount { get; set; }
        [DisplayName("Amount in words")]
        public string AmtInWords { get; set; }
        [DisplayName("PDC Status")]
        public string PDCstatus { get; set; }
        [DisplayName("DD/Cheque Number")]
        public string DDChequeNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("DD/Cheque Date")]
        public DateTime? DDChequeDate { get; set; }

        [DisplayName("Bank Account Number")]
        public string BankAcCode { get; set; }
        [DisplayName("Bank Account Name")]
        public string BankAcName { get; set; }
        [DisplayName("Narration")]
        public string Narration { get; set; }

        [StringLength(50)]
        public string Createduser { get; set; }
        public string Receiptdt { get; set; }
        
        public List<ReceiptDetailsViewModel> ReceiptDetailsList { get; set; }
    }

    public class ReceiptDetailsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Invoice Type")]
        public string Invtype { get; set; }
        [DisplayName("Invoice Number")]
        public string Invno { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Invoice Date")]
        public DateTime? InvoiceDate { get; set; }

        [DisplayName("Invoice Description")]
        public string Description { get; set; }
        [DisplayName("Invoice Amount")]
        public float? InvoiceAmount { get; set; }
        [DisplayName("Credit Amount")]
        public float? CreditAmt { get; set; }
        [DisplayName("ReceivedAmount")]
        public float? ReceivedAmount { get; set; }
        [DisplayName("Balance Amount")]
        public float? Balance { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }


    }

    public class ReceiptsDDLChangeViewModel
    {
        public string ReceiptNo { get; set; }
        public string agreement_no { get; set; }
        public string Property_id { get; set; }
        public string Property_Name { get; set; }
        public string Unit_ID { get; set; }
        public string unit_Name { get; set; }
        public string Tenant_id { get; set; }
        public string Tenant_Name { get; set; }
        public string TotalAmount { get; set; }
        public string pdc_Amount { get; set; }
        public string DDChequeNo { get; set; }
        public string invno { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string date { get; set; }
        public string incno { get; set; }
        public string invtype { get; set; }
        public string totalamt { get; set; }
        public string InvoiceAmount { get; set; }

    }

    public class AdvancePendingSelectList
    {
        public SelectList ReceiptNo { get; set; }
        public SelectList Agreement_No { get; set; }
        public SelectList Property_Id { get; set; }
        public SelectList Property_Name { get; set; }
        public SelectList Unit_Id { get; set; }
        public SelectList Unit_Name { get; set; }
        public SelectList Tenant_Id { get; set; }
        public SelectList Tenant_Name { get; set; }
        public SelectList TotalAmount { get; set; }
        public SelectList pdc_Amount { get; set; }
        public SelectList DDChequeNo { get; set; }
        public SelectList invno { get; set; }
        public SelectList month { get; set; }
        public SelectList year { get; set; }
        public SelectList date { get; set; }
        public SelectList incno { get; set; }
        public SelectList invtype { get; set; }
        public SelectList totalamt { get; set; }
        public SelectList InvoiceAmount { get; set; }
    }
}