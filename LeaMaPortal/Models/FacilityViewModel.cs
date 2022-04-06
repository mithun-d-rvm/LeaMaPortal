using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class FacilityViewModel
    {
        public int Id { get; set; }
        [DisplayName("Facility Id")]
        public string Facility_id { get; set; }
        [DisplayName("Facility Name")]
        public string Facility_Name { get; set; }
        public string Region_Name { get; set; }
        public string Country { get; set; }
    }
}