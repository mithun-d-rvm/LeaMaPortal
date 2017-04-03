using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class SlabViewModel
    {
        public int Id { get; set; }
        [DisplayName("Slab Id")]
        public int? SlabId { get; set; }
        [DisplayName("Utility_id ")]
        public string Utility_id { get; set; }

        [DisplayName("Utility Name")]
        public string Utility_Name { get; set; }
        [DisplayName("Unit From")]
        public int? Unit_From { get; set; }
        [DisplayName("Unit To")]
        public int? Unit_to { get; set; }
        [DisplayName("Rate Per Unit")]
        public float? rate { get; set; }
        [DisplayName("Colour Code")]
        public string Colour { get; set; }
        [DisplayName("Residence Type")]
        public string Residence_type { get; set; }
    }
}