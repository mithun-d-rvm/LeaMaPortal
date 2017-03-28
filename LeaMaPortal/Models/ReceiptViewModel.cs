using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }
        [DisplayName("Receipt No")]
        public int ReceiptNo { get; set; }
        [DisplayName("Receipt Date")]
        public DateTime ReceiptDate { get; set; }
        [DisplayName("Receipt Mode")]
        public string RecpType { get; set; }
        [DisplayName("Receipt Type")]
        public string Reccategory { get; set; }
        [DisplayName("Advance Receipt Number")]
        public string AdvAcCode { get; set; }
        [DisplayName("Contract Agreement Date")]
        public DateTime ContracAgreementDate { get; set; }
        [DisplayName("Contract Agreement Number")]
        public int agreement_no { get; set; }
        [DisplayName("Tenant ID")]
        public int Tenant_id { get; set; }
        [DisplayName("Tenant Name")]
        public string Tenant_Name { get; set; }
        [DisplayName("Unit ID")]
        public int Unit_ID { get; set; }
        [DisplayName("Unit Name")]
        public string unit_Name { get; set; }
        [DisplayName("Property ID")]
        public string Property_id { get; set; }
        [DisplayName("Property Name")]
        public string Property_Name { get; set; }
        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }
        [DisplayName("Amount in words")]
        public string AmtInWords { get; set; }
        [DisplayName("PDC Status")]
        public string PDCstatus { get; set; }
        [DisplayName("DD/Cheque Number")]
        public string DDChequeNo { get; set; }
        [DisplayName("DD/Cheque Date")]
        public DateTime DDChequeDate { get; set; }
        [DisplayName("Bank Account Number")]
        public string BankAcCode { get; set; }
        [DisplayName("Bank Account Name")]
        public string BankAcName { get; set; }
        [DisplayName("Narration")]
        public string Narration { get; set; }

    }

    public class ReceiptDetailsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Invoice Type")]
        public string Invtype { get; set; }
        [DisplayName("Invoice Number")]
        public string Invno { get; set; }
        [DisplayName("Invoice Date")]
        public DateTime InvoiceDate { get; set; }
        [DisplayName("Invoice Description")]
        public string Description { get; set; }
        [DisplayName("Invoice Amount")]
        public decimal InvoiceAmount { get; set; }
        [DisplayName("Credit Amount")]
        public decimal CreditAmt { get; set; }
        [DisplayName("ReceivedAmount")]
        public decimal ReceivedAmount { get; set; }
        [DisplayName("Balance Amount")]
        public decimal Balance { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
   

    }
}