using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Models
{
    public class RegionViewModel
    {       
        public int Id { get; set; }
        [DisplayName("Region Name")]
        public string Region_Name { get; set; }
        [DisplayName("Country")]
        public string Country { get; set; }
    }
}