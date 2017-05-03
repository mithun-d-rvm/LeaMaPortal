using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    
    public class AgreementCheckListViewModel
    {
        public AgreementCheckListViewModel()
        {
            AgreementCheckList = new List<AgreementCheckListViewModel>();
        }
        public int Id { get; set; }
        [DisplayName("Checklist Id")]
        public string Checklist_id { get; set; }
        [DisplayName("Checklist Name")]
        public string Checklist_Name { get; set; }
        public string Checklist_Type { get; set; }
        [DisplayName("Verified")]
        public int Status { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
        public List<AgreementCheckListViewModel> AgreementCheckList { get; set; }
    }
}