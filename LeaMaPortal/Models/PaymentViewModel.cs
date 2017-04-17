using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        [DisplayName("Payment Number")]
        public int PaymentNo { get; set; }
        [DisplayName("Payment Date")]
        public DateTime PaymentDate { get; set; }
        [DisplayName("PaymentType")]
        public string PaymentType { get; set; }
        [DisplayName("Payment Mode")]
        public string PaymentMode { get; set; }
        [DisplayName("Supplier ID")]
        public DateTime Supplier_id { get; set; }
        [DisplayName("Supplier Name")]
        public int Supplier_Name { get; set; }
        [DisplayName("Contract Agreeement Number")]
        public int agreement_no { get; set; }
        [DisplayName("Property ID")]
        public string Property_id { get; set; }
        [DisplayName("Property Name")]
        public string Property_Name { get; set; }
        [DisplayName("Unit ID")]
        public decimal Unit_ID { get; set; }
        [DisplayName("Unit Name")]
        public string unit_Name { get; set; }
        [DisplayName("Total Amount")]
        public string TotalAmount { get; set; }
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
        [DisplayName("Advance Payment Number")]
        public string AdvAcCode { get; set; }
        public List<PaymentDetailsViewModel> PaymentDetailsViewModel { get; set; }

    }
    public class PaymentDetailsViewModel
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
        [DisplayName("Debit Amount")]
        public decimal DebitAmount { get; set; }
        [DisplayName("Paid Amount")]
        public decimal PaidAmount { get; set; }
        [DisplayName("Balance Amount")]
        public decimal Balance { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }


    }

}