using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class AgreementCheckListViewModel
    {
        public int Id { get; set; }
        [DisplayName("Verified")]
        public int Status { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }

    }
}