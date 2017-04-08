using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class AgreementPdcViewModel
    {
        public int Id { get; set; }
        [DisplayName("Month")]
        public string Month { get; set; }
        [DisplayName("Year")]
        public string Year { get; set; }
        [DisplayName("Payment Mode")]
        public string Payment_Mode { get; set; }
        [DisplayName("Bank Name")]
        public string BankName { get; set; }
        [DisplayName("Cheque No")]
        public string Cheque_No { get; set; }
        [DisplayName("Cheque Date")]
        public string Cheque_Date { get; set; }
        [DisplayName("Amount")]
        public decimal Cheque_Amount { get; set; }
    }
}