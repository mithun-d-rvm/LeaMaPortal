using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class UtilityViewModel
    {
        public int Id { get; set; }
        [DisplayName("Utility Id")]
        public string Utility_id { get; set; }
        [DisplayName("Utility Name")]
        public string Utility_Name { get; set; }

        public string Region_Name { get; set; }
        public string Country { get; set; }
        //public string Utility_Name_Key { get; set; }
        //public string Utility_id_Key { get; set; }
    }
}