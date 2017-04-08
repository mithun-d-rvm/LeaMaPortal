using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class CheckListViewModel
    {
        public int Id { get; set; }
        [DisplayName("Checklist Id")]
        public string Checklist_id { get; set; }
        [DisplayName("Checklist Name")]
        public string Checklist_Name { get; set; }
        [DisplayName("Checklist Type")]
        public string Checklist_Type { get; set; }
    }
}