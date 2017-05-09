using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class AgreementUtilityViewModel
    {
        public AgreementUtilityViewModel()
        {
            AgreementUtilityList = new List<AgreementUtilityViewModel>();
        }
        public int Id { get; set; }
        [DisplayName("Utility ID")]
        public string Utility_id { get; set; }
        [DisplayName("Utility Name")]
        public string Utility_Name { get; set; }
        [DisplayName("Payable by")]
        public string Payable { get; set; }
        [DisplayName("Amount Type")]
        public string Amount_Type { get; set; }
        [DisplayName("Amount")]
        public float Amount { get; set; } 
        public List<AgreementUtilityViewModel> AgreementUtilityList { get; set; }

    }
}