using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class ContractApprovalSettingViewModel
    {
        public int Id { get; set; }
        [DisplayName("Approval")]
        public string Approval_flag { get; set; }
        //  [DisplayName("User")]
        //public List<User> CountryModel { get; set; }
    }
}