using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class PropertyTypeViewModel
    {
        public int Id { get; set; }
        [DisplayName("Property Category")]
        public string PropertyType { get; set; }
        [DisplayName("Property Type")]

        public string Region_Name { get; set; }
        public string Country { get; set; }
        public string PropertyCategory { get; set; }
        [DisplayName("Usage")]
        public string Usage_name { get; set; }
    }
    public class PropertyTypeModel
    {
        public string PropertyCategory { get; set; }
        public string Usage_name { get; set; }
    }
}