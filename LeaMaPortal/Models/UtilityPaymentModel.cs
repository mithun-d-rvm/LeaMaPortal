using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class UtilityPaymentModel
    {
        public int id { get; set; }

        [DisplayName("Payment Number")]
        public int PaymentNo { get; set; }

        [DisplayName("Payment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> PaymentDate { get; set; }

        [DisplayName("Utility ID")]
        public string Utility_id { get; set; }

        [DisplayName("Utility Name")]
        public string Utiltiy_name { get; set; }

        [DisplayName("Supplier ID")]
        public Nullable<int> Supplier_id { get; set; }

        [DisplayName("Supplier Name")]
        public string Supplier_name { get; set; }

        [DisplayName("Payment Type")]
        public string PaymentType { get; set; }

        [DisplayName("Payment Mode")]
        public string PaymentMode { get; set; }

        [DisplayName("Total Amount")]
        public Nullable<float> TotalAmount { get; set; }

        [DisplayName("Amount in Words")]
        public string AmtInWords { get; set; }

        [DisplayName("DD/Cheque Number")]
        public string DDChequeNo { get; set; }

        [DisplayName("DD/Cheque Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Cheqdate { get; set; }

        [DisplayName("PDC Status")]
        public string pdcstatus { get; set; }

        [DisplayName("Bank Account Number")]
        public string BankAcCode { get; set; }

        [DisplayName("Bank Account Name")]
        public string BankAcName { get; set; }

        [DisplayName("Advance Payment Number")]
        public string AdvAcCode { get; set; }

        [DisplayName("Narration")]
        public string Narration { get; set; }
        public List<UtilityPaymentDetail> UtilityPaymentDetails { get; set; }
    }
    public class UtilityPaymentDetail
    {
        public int id { get; set; }
        public Nullable<int> PaymentNo { get; set; }
        public Nullable<int> Refno { get; set; }
        public string Meterno { get; set; }
        public string billNo { get; set; }
        public Nullable<System.DateTime> billDate { get; set; }
        public Nullable<float> billAmount { get; set; }
        public Nullable<float> PaidAmount { get; set; }
        public Nullable<float> Debitamt { get; set; }
        public string Remarks { get; set; }
    }
}