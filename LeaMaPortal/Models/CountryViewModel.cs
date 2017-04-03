
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class CountryViewModel
    {
        [Required]
        public string Country { get; set; }
        public int Id { get; set; }
        public IList<CountryViewModel> List { get; set; }
    }
}